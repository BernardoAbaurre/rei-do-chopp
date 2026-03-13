# Rei do Chopp

Management system for a beverage distributor (administration + sales) with **REST API**, **web frontend**, and **desktop helper for printing**.

> ✅ Project with layered architecture (Clean Architecture), JWT + role-based authentication, basic tests, email sending via Mailjet, and a CI/CD pipeline (GitHub Actions) for building, publishing Docker images, and automated deployment.

---

## 🧱 Architecture Overview

The project follows a layered architecture with a clear separation between domains and infrastructure:

- **API (`rei-do-chopp-api`)**
  - `ReiDoChopp.Api`: entry point (controllers, middleware, Swagger)
  - `ReiDoChopp.Application`: use cases, validation, and DTOs
  - `ReiDoChopp.Domain`: entities, business rules, and domain services
  - `ReiDoChopp.Infra`: persistence implementation (EF Core/Postgres), email sending (Mailjet), configuration and infrastructure
  - `ReiDoChopp.Ioc`: DI configuration and setup (auth, EF, identity)

- **Frontend (`rei-do-chopp-site`)**
  - Angular 19 + Angular Material, consuming API via JWT

- **Desktop helper (`rei-do-chopp-helper`)**
  - Python app (Flask + Tkinter) to print orders on a local printer

- **Docker + CI/CD**
  - Docker Compose for local and production orchestration
  - GitHub Actions for build/push of images and automated deployment via SSH

---

## 🚀 Technologies

- **Backend**: .NET 8 (ASP.NET Core), Entity Framework Core, Clean Architecture
- **Database**: PostgreSQL (via Docker)
- **Authentication**: JWT (Bearer Token) + roles (Administrator / Developer)
- **Email**: Mailjet (password reset)
- **Frontend**: Angular 19, Angular Material, RxJS
- **Desktop helper**: Python 3 (Flask + Tkinter), packable with PyInstaller
- **Infrastructure**: Docker, Docker Compose, GitHub Actions, GHCR (GitHub Container Registry)

---

## 🗂 Repository Structure

```
rei-do-chopp/
├── rei-do-chopp-api/          # API backend (.NET)
├── rei-do-chopp-site/         # Frontend Angular
├── rei-do-chopp-helper/       # Desktop helper (Python)
├── rei-do-chopp-scripts/      # SQL scripts (reports / cleanup)
├── docker-compose.yml         # Docker orchestration for all services
├── Dockerfile.api             # API build (Docker)
├── Dockerfile.angular         # Frontend build (Docker)
└── .env.example               # Sample environment variables
```

---

## ▶️ Running Locally (Docker)

### 1) Prerequisites

- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/)
- (Optional) .NET 8 SDK, Node.js 20+, Python 3.8+

### 2) Configure environment variables

```bash
cp .env.example .env
# Edit .env with real values (DB password, JWT secret, Mailjet keys, etc.)
```

### 3) Start containers

```bash
docker compose up -d
```

### 4) Access

- Frontend (UI): http://localhost
- API Swagger: http://localhost:5000/swagger
- Database (Postgres): localhost:5432 (user: `postgres`)

---

## 🛠️ Run in Development Mode (without Docker)

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

The pipeline is configured to:

1. Build and push Docker images to **GHCR** (`ghcr.io/bernardoabaurre/*`).
2. Deploy automatically to a VM (via SSH), pulling images and starting containers with `docker-compose`.
3. Automatically clean up old images/containers.

---

## 🧩 Key Features Implemented

- Order management (create, edit, list, status)
- Product management with stock control
- Restocking management
- Role-based access control (JWT + policies)
- Password reset by email (Mailjet)
- SQL reports (e.g., sales by product)
- Modular architecture (domain separation + unit tests)

---

## 🔐 Important Environment Variables

| Variable            | Description                                      |
| ------------------- | ------------------------------------------------ |
| `DB_PASSWORD`       | Postgres password (used in `docker-compose.yml`) |
| `JWT_SECRET`        | Secret key for signing JWT tokens                |
| `EMAIL_PUBLIC_KEY`  | Mailjet public API key                           |
| `EMAIL_PRIVATE_KEY` | Mailjet private API key                          |
| `API_URL`           | Backend base URL (used by frontend)              |

---

## 🔍 Tips for Exploring the Project

- Identify the use of **Clean Architecture** (`Domain`, `Application`, `Infra`, `IoC`).
- Compare the login + JWT flow (controllers → services → Identity) with the Angular consumption (interceptors and guards).
- Observe the Python helper as an example of a cross-platform desktop solution for local printing.

---

## ✅ How to Contribute

1. Fork the repository
2. Create a branch: `git checkout -b feature/<name>`
3. Make your changes
4. Open a pull request with a clear description of the changes

---

## 📄 License

This project is owned by Rei do Chopp Distribuidora.
