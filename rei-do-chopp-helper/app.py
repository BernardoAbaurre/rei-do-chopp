import threading
import tkinter as tk
from tkinter import messagebox
import win32print
from unidecode import unidecode
import requests
import re
import time
import os
import webbrowser
import sys
from collections import deque

PRINTER_NAME = "POS-58"

COMMANDS = {
    "ALIGN_CENTER": b"\x1B\x61\x01",
    "BIG_FONT": b"\x1D\x21\x11",
    "TEXT_NORMAL": b"\x1D\x21\x00" + b"\x1B\x45\x00" + b"\x1B\x61\x00",
    "BIG_LINE": ("=" * 32 + "\n").encode("ascii", errors="ignore"),
    "LINE": ("-" * 32 + "\n").encode("ascii", errors="ignore"),
    "BOLD": b"\x1B\x45\x01",
}

panel_root = tk.Tk()
last_print_id = 0

# ====== Buffer de logs (mantém só os últimos 100) ======
_log_buffer = deque(maxlen=100)
_log_lock = threading.Lock()
_log_window = None
_log_text = None

def add_log(*args, sep=" ", end="\n"):
    """
    Adiciona mensagens ao painel de logs (mantém últimas 100 linhas)
    e também escreve no console padrão.
    Use add_log(...) no lugar de print(...).
    """
    msg = sep.join(str(a) for a in args) + end
    lines = msg.splitlines()
    with _log_lock:
        for line in lines:
            _log_buffer.append(line)
    # opcional: refletir no console
    # (não intercepta print global; apenas usamos aqui localmente)
    print(*args, sep=sep, end=end)


def absolute_path(path: str) -> str:
    if hasattr(sys, "_MEIPASS"):
        return os.path.join(sys._MEIPASS, path)
    return os.path.join(os.path.abspath("."), path)

def project_path() -> str:
    """Retorna a raiz do projeto, inclusive quando rodando via PyInstaller."""
    if hasattr(sys, "_MEIPASS"):
        base = os.path.dirname(sys.executable)
    else:
        base = os.path.dirname(os.path.abspath(__file__))
    return os.path.abspath(os.path.join(base, ".."))


# ====== Painel de Logs ======
def open_log_panel():
    global _log_window, _log_text

    if _log_window is not None and _log_window.winfo_exists():
        _log_window.deiconify()
        _log_window.lift()
        _log_window.focus_force()
        return

    _log_window = tk.Toplevel(panel_root)
    _log_window.title("Logs (últimas 100 linhas)")
    _log_window.geometry("700x400")

    frame = tk.Frame(_log_window)
    frame.pack(fill="both", expand=True)

    scrollbar = tk.Scrollbar(frame)
    scrollbar.pack(side="right", fill="y")

    _log_text = tk.Text(frame, wrap="none", state="disabled")
    _log_text.pack(side="left", fill="both", expand=True)
    _log_text.configure(font=("Consolas", 10))

    _log_text.config(yscrollcommand=scrollbar.set)
    scrollbar.config(command=_log_text.yview)

    def clear_logs():
        with _log_lock:
            _log_buffer.clear()
        _refresh_logs(force=True)

    btns = tk.Frame(_log_window)
    btns.pack(fill="x")
    tk.Button(btns, text="Limpar", command=clear_logs).pack(side="left", padx=6, pady=6)
    tk.Button(btns, text="Fechar", command=_log_window.destroy).pack(side="right", padx=6, pady=6)

    _refresh_logs()

def _refresh_logs(force: bool = False):
    if _log_window is None or not _log_window.winfo_exists():
        return

    with _log_lock:
        content = "\n".join(_log_buffer)

    _log_text.config(state="normal")
    _log_text.delete("1.0", tk.END)
    _log_text.insert(tk.END, content)
    _log_text.config(state="disabled")
    _log_text.see(tk.END)

    _log_window.after(300, _refresh_logs)


# ====== Printer Methods ======
def check_print_requests():
    global last_print_id
    API_URL = "http://64.23.161.175:5000/api/print-controls/last"

    while True:
        try:
            response = requests.get(API_URL)
            add_log("requisição feita:", response.status_code)
            if response.status_code == 200:
                data = response.json()
                if data:
                    if data["Id"] == last_print_id:
                        add_log("loop de impressão interrompido")
                    else:
                        add_log(f'{data["Id"]} requisição retornou')
                        print_receipt(data["Content"], data["Id"])
                        last_print_id = data["Id"]
                else:
                    add_log("Error getting")
        except Exception as e:
            add_log(f"Erro ao fazer requisição: {e}")
        time.sleep(1)

def respond_print_control(id: int, status: int):
    API_URL = "http://64.23.161.175:5000/api/print-controls"

    try:
        response = requests.put(f"{API_URL}/{id}", json={"Status": status})
        if response.status_code != 200:
            add_log(f"Erro ao atualizar controle: {response.status_code}")
    except Exception as e:
        add_log(f"Erro ao atualizar controle: {e}")

def print_receipt(text: str, id: int):
    try:
        if not text:
            add_log("Campo 'text' não encontrado")
            return

        raw_data = b"\x1B\x40" + b"\x1B\x52\x00"

        final_text = b""
        for sub in re.split(r"(\[.*?\])", unidecode(text)):
            if sub.startswith("[") and sub.endswith("]") and sub[1:-1] in COMMANDS:
                final_text += COMMANDS[sub[1:-1]]
            else:
                final_text += sub.encode("ascii", errors="ignore")

        raw_data += final_text + b"\n\n\x1D\x56\x00"

        hPrinter = win32print.OpenPrinter(PRINTER_NAME)
        try:
            win32print.StartDocPrinter(hPrinter, 1, ("Cupom", None, "RAW"))
            win32print.StartPagePrinter(hPrinter)
            win32print.WritePrinter(hPrinter, raw_data)
            win32print.EndPagePrinter(hPrinter)
            win32print.EndDocPrinter(hPrinter)
            respond_print_control(id, 1)
            add_log(f"Impressão concluída para Id={id}")
        except Exception as e:
            respond_print_control(id, 2)
            add_log(f"Falha ao imprimir Id={id}: {e}")
        finally:
            win32print.ClosePrinter(hPrinter)

    except Exception as e:
        add_log(f"Erro: {e}")


def open_system_window():
    webbrowser.open_new("http://rei-do-chopp.duckdns.org/login")


# ====== Panel Methods ======
def start_panel():
    panel_root.title("Rei do Chopp - Painel da impressora")
    panel_root.iconbitmap(absolute_path("icon-rei-do-chopp.ico"))
    panel_root.geometry("380x200")

    tk.Button(panel_root, text="Abrir janela do sistema", command=open_system_window)\
        .pack(pady=6, padx=6)
    tk.Button(panel_root, text="Abrir painel de logs", command=open_log_panel)\
        .pack(pady=6, padx=6)

    panel_root.protocol("WM_DELETE_WINDOW", on_exit)
    panel_root.mainloop()

def on_exit():
    if messagebox.askokcancel("Sair", "Deseja fechar o painel da impressora ?"):
        panel_root.destroy()


if __name__ == "__main__":
    add_log("iniciar")
    thread = threading.Thread(target=check_print_requests, daemon=True)
    thread.start()
    open_system_window()
    start_panel()
