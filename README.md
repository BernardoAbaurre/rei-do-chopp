# Rei do Chopp

Sistema de gestão para distribuidora de bebidas, incluindo API backend, frontend web e helper desktop para impressão.

## Tecnologias

- **Backend**: .NET 8 (ASP.NET Core) com Entity Framework Core
- **Frontend**: Angular 19 com Angular Material
- **Banco de Dados**: SQL Server (Docker)
- **Helper**: Python (Flask, Tkinter, PyInstaller)
- **Infraestrutura**: Docker, Docker Compose, GitHub Actions, GHCR

## Estrutura do Projeto

```
rei-do-chopp/
├── rei-do-chopp-api/          # API .NET
│   ├── ReiDoChopp.Api/        # Projeto principal da API
│   ├── ReiDoChopp.Application/# Lógica de aplicação
│   ├── ReiDoChopp.Domain/     # Domínio e entidades
│   ├── ReiDoChopp.Infra/      # Infraestrutura (EF Core, etc.)
│   └── ReiDoChopp.Ioc/        # Injeção de dependência
├── rei-do-chopp-site/         # Frontend Angular
├── rei-do-chopp-helper/       # Helper Python para impressão
├── rei-do-chopp-scripts/      # Scripts SQL
├── docker-compose.yml         # Orquestração Docker
├── Dockerfile.api             # Dockerfile para API
├── Dockerfile.angular         # Dockerfile para frontend
└── .env.example               # Exemplo de variáveis de ambiente
```

## Pré-requisitos

- Docker e Docker Compose
- .NET 8 SDK
- Node.js 20+
- Python 3.8+ (para helper)
- Git

## Configuração Local

1. Clone o repositório:

   ```bash
   git clone https://github.com/bernardoabaurre/rei-do-chopp.git
   cd rei-do-chopp
   ```

2. Configure variáveis de ambiente:

   ```bash
   cp .env.example .env
   # Edite .env com suas chaves
   ```

3. Execute os serviços:

   ```bash
   docker-compose up -d
   ```

4. Para desenvolvimento local:
   - API: `cd rei-do-chopp-api && dotnet run`
   - Frontend: `cd rei-do-chopp-site && npm install && ng serve`
   - Helper: `cd rei-do-chopp-helper && python app.py`

## Deploy

### VM (Digital Ocean)

1. A VM contém apenas Docker e as imagens.
2. Volumes persistentes para dados do banco.
3. Acesso via SSH: `ssh root@64.23.161.175`

### CI/CD

- Commits na branch `master` acionam build automático no GitHub Actions.
- Imagens são pushadas para GHCR.
- Deploy automático na VM via SSH.

## API

### Endpoints Principais

- `GET /api/orders` - Listar pedidos
- `POST /api/orders` - Criar pedido
- `GET /api/products` - Listar produtos
- `POST /api/users/login` - Login

Documentação completa via Swagger: `http://localhost:5000/swagger`

## Banco de Dados

- SQL Server em container Docker.
- Migrations EF Core aplicadas automaticamente.
- Porta 1433 exposta para conexões externas (DBeaver).

## Helper Python

Aplicação desktop para gerenciar impressão de pedidos.

### Build

```bash
cd rei-do-chopp-helper
pyinstaller --onefile --noconsole --icon=icon-rei-do-chopp.ico --add-data "icon-rei-do-chopp.ico;." --name="Rei do Chopp" app.py
```

## Scripts

- `limpar-base.sql` - Limpar base de dados
- `vendas-por-produto.sql` - Relatório de vendas

## Segurança

- Secrets armazenados em `.env` na VM (não commitado).
- Repositório público, mas dados sensíveis isolados.
- JWT para autenticação.

## Contribuição

1. Fork o projeto
2. Crie uma branch: `git checkout -b feature/nova-feature`
3. Commit suas mudanças: `git commit -am 'Adiciona nova feature'`
4. Push: `git push origin feature/nova-feature`
5. Abra um Pull Request

## Licença

Este projeto é privado e propriedade de Rei do Chopp Distribuidora.
