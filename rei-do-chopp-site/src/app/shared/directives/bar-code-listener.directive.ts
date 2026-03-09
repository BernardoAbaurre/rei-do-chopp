// barcode-listener.directive.ts
import { Directive, EventEmitter, HostListener, Input, Output } from '@angular/core';

@Directive({
  selector: '[barcodeListener]',
  standalone: true,
})
export class BarcodeListenerDirective {
  @Input() minLength = 6;                      // mínimo de chars pra considerar “leitura”
  @Input() endKey: 'Enter' | 'Tab' | '' = 'Enter'; // tecla de término; '' para usar timeout
  @Input() maxGapMs = 35;                      // tempo máx entre teclas (scanner << digitação humana)
  @Input() idleEmitMs = 120;                   // se não houver endKey, emite após ocioso

  @Output() scanned = new EventEmitter<string>();

  private buffer = '';
  private lastTs = 0;
  private idleTimer: any;

  @HostListener('document:keydown', ['$event'])
  onKeydown(e: KeyboardEvent) {
    const now = performance.now();

    // Reinicia buffer se demorou demais entre teclas
    if (this.lastTs && now - this.lastTs > this.maxGapMs) {
      this.flushIfValid(); // opcional: tenta emitir antes de limpar
      this.reset();
    }
    this.lastTs = now;

    // Se tecla finalizadora chegou, emite o que já tem
    if (this.endKey && e.key === this.endKey) {
      if (this.buffer.length >= this.minLength) {
        this.scanned.emit(this.buffer);
        e.preventDefault(); // evita submissões/efeitos do Enter
      }
      this.reset();
      return;
    }

    // Captura apenas caracteres “imprimíveis”
    if (e.key.length === 1) {
      this.buffer += e.key;
      // Reagenda emissão por ociosidade (quando não há endKey)
      if (!this.endKey) {
        clearTimeout(this.idleTimer);
        this.idleTimer = setTimeout(() => {
          this.flushIfValid();
          this.reset();
        }, this.idleEmitMs);
      }
    } else {
      // Ignora modificadores, shift, etc.
      // (se seu leitor envia F-keys como prefixo/sufixo, trate aqui)
    }
  }

  private flushIfValid() {
    if (this.buffer.length >= this.minLength) {
      this.scanned.emit(this.buffer);
    }
  }
  private reset() {
    this.buffer = '';
    this.lastTs = 0;
    clearTimeout(this.idleTimer);
  }
}
