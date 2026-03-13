# Rei do Chopp

Sistema de gestão para distribuidora de bebidas (administrativo + vendas) com **API REST**, **frontend web** e **helper desktop para impressão**.

> ✅ Projeto com arquitetura em camadas (Clean Architecture), autenticação via JWT + roles, testes básicos, envio de e-mail com Mailjet, e pipeline CI/CD (GitHub Actions) para build, publish de imagens Docker e deploy automático.

---

## 🧱 Visão Geral da Arquitetura

O projeto segue uma arquitetura em camadas com separação clara entre domínios e infraestrutura:

- **API (`rei-do-chopp-api`)**
  - `ReiDoChopp.Api`: ponto de entrada (controllers, middlewares, Swagger)
  - `ReiDoChopp.Application`: casos de uso, validações e DTOs
  - `ReiDoChopp.Domain`: entidades, regras de negócio e serviços de domínio
  - `ReiDoChopp.Infra`: implementação de persistência (EF Core/Postgres), envio de e-mail (Mailjet), configurações e infraestrutura
  - `ReiDoChopp.Ioc`: configuração de DI (injeção de dependência) e setups (auth, EF, identity)

- **Frontend (`rei-do-chopp-site`)**
  - Angular 19 + Angular Material, consumindo API via JWT

- **Helper desktop (`rei-do-chopp-helper`)**
  - Aplicativo Python (Flask + Tkinter) para impressão de pedidos em impressora local

- **Docker + CI/CD**
  - Docker Compose para desenvolvimento/produção local
  - GitHub Actions para build/push de imagens e deploy automático na VM (SSH)

---

## 🚀 Tecnologias

- **Backend**: .NET 8 (ASP.NET Core), Entity Framework Core, Clean Architecture
- **Banco de Dados**: PostgreSQL (via Docker)
- **Autenticação**: JWT (Bearer Token) + roles (Administrador / Desenvolvedor)
- **E-mail**: Mailjet (envio de reset de senha)
- **Frontend**: Angular 19, Angular Material, RxJS
- **Helper desktop**: Python 3 (Flask + Tkinter), empacotável com PyInstaller
- **Infraestrutura**: Docker, Docker Compose, GitHub Actions, GHCR (GitHub Container Registry)

---

## 🗂 Estrutura do Repositório

```
rei-do-chopp/
├── rei-do-chopp-api/          # API backend (.NET)
├── rei-do-chopp-site/         # Frontend Angular
├── rei-do-chopp-helper/       # Helper desktop (Python)
├── rei-do-chopp-scripts/      # SQL scripts (relatórios / limpeza)
├── docker-compose.yml         # Orquestração Docker para todos os serviços
├── Dockerfile.api             # Build da API (.NET)
├── Dockerfile.angular         # Build do frontend (Angular)
└── .env.example               # Exemplo de variáveis de ambiente
```

---

## ▶️ Como rodar localmente (Docker)

### 1) Pré-requisitos

- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/)
- (Opcional) .NET 8 SDK, Node.js 20+, Python 3.8+

### 2) Configurar variáveis de ambiente

```bash
cp .env.example .env
# Edite .env com valores reais (senha do banco, JWT, chaves Mailjet, etc.)
```

### 3) Subir containers

```bash
docker compose up -d
```

### 4) Acessos

- Frontend (UI): http://localhost
- API Swagger: http://localhost:5000/swagger
- Banco de dados (Postgres): localhost:5432 (usuário: `postgres`)

---

## 🛠️ Rodar em modo de desenvolvimento (sem Docker)

### API (.NET)

```bash
cd rei-do-chopp-api
dotnet run
```

### Frontend (Angular)

```bash
cd rei-do-chopp-site
npm install
ng serve
```

### Helper (Python)

```bash
cd rei-do-chopp-helper
python app.py
```

---

## 📦 CI / CD (GitHub Actions)

O pipeline está configurado para:

1. Buildar e empurrar imagens Docker para o **GHCR** (`ghcr.io/bernardoabaurre/*`).
2. Realizar **deploy automático** em uma VM (via SSH), fazendo pull das imagens e subindo containers usando `docker-compose`.
3. Limpeza automática de images/containers antigos.

---

## 🧩 Principais funcionalidades implementadas

- Gestão de pedidos (criar, editar, listar, status)
- Gestão de produtos com controle de estoque
- Gestão de fornecedores / reposições
- Permissões baseadas em roles (JWT + políticas de autorização)
- Reset de senha via e-mail (Mailjet)
- Relatórios SQL (ex.: vendas por produto)
- Arquitetura modular (separação de domínios + testes unitários)

---

## 🔐 Variáveis de ambiente importantes

| Variável            | Descrição                                         |
| ------------------- | ------------------------------------------------- |
| `DB_PASSWORD`       | Senha do Postgres (usada no `docker-compose.yml`) |
| `JWT_SECRET`        | Chave secreta para assinatura dos tokens JWT      |
| `EMAIL_PUBLIC_KEY`  | Chave pública Mailjet                             |
| `EMAIL_PRIVATE_KEY` | Chave privada Mailjet                             |
| `API_URL`           | URL base do backend (usado pelo frontend)         |

---

## 🔍 Dicas para explorar o projeto

- Identifique o uso de **Clean Architecture** (`Domain`, `Application`, `Infra`, `IoC`).
- Compare o fluxo de login + JWT (controllers → serviços → Identity) com o consumo no Angular (interceptadores e guardas).
- Observe o helper Python como exemplo de solução multiplataforma (desktop) para impressão local.

---

## ✅ Como contribuir

1. Fork o repositório
2. Crie uma branch: `git checkout -b feature/<nome>`
3. Faça suas alterações
4. Crie um pull request explicando as mudanças

---

## 📄 Licença

Este projeto é propriedade de Rei do Chopp Distribuidora.
