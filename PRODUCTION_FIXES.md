# Correções para Produção - Rei do Chopp API

## Problemas Identificados e Soluços

### ❌ Problema 1: String de Conexão Incorreta (CRÍTICO)

**Erro**: `Couldn't set trusted_connection - The given key was not present in the dictionary`

**Causa**: O `docker-compose.yml` estava usando uma string de conexão do **SQL Server** (parameters: `Server`, `TrustServerCertificate`, `Encrypt`) para conectar ao **PostgreSQL**.

**Solução**:

```yaml
# ❌ ANTES (SQL Server)
ConnectionStrings__DefaultConnection=Server=db,1433;Database=ReiDoChopp;User Id=sa;Password=${SA_PASSWORD};TrustServerCertificate=True;Encrypt=False

# ✅ DEPOIS (PostgreSQL)
ConnectionStrings__DefaultConnection=Host=db;Port=5432;Database=reidochopp;Username=postgres;Password=${DB_PASSWORD};
```

**Arquivos modificados**: `docker-compose.yml`

---

### ❌ Problema 2: Variáveis de Ambiente Faltando (CRÍTICO)

**Erro**: `The ConnectionString property has not been initialized`

**Causa**: As variáveis `JWT_SECRET`, `EMAIL_PUBLIC_KEY` e `EMAIL_PRIVATE_KEY` não estavam sendo passadas ao container da API.

**Solução**: Adicionadas ao docker-compose.yml:

```yaml
environment:
  - ASPNETCORE_ENVIRONMENT=Production
  - ConnectionStrings__DefaultConnection=Host=db;Port=5432;Database=reidochopp;Username=postgres;Password=${DB_PASSWORD};
  - Jwt__SecretKey=${JWT_SECRET}
  - Email__ApiKeys__Public=${EMAIL_PUBLIC_KEY}
  - Email__ApiKeys__Private=${EMAIL_PRIVATE_KEY}
```

**Arquivos modificados**: `docker-compose.yml`

---

### ❌ Problema 3: Logging Sensível Habilitado em Produção

**Aviso**: `Sensitive data logging is enabled. Log entries and exception messages may include sensitive application data`

**Causa**: `EnableSensitiveDataLogging()` estava sempre ativado no EntityFrameworkConfiguration.

**Solução**: Condicionalizar baseado no ambiente:

```csharp
// ✅ DEPOIS
var isDevelopment = configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT") == "Development";

if (isDevelopment)
{
    options.EnableSensitiveDataLogging();
}
```

**Arquivos modificados**: `ReiDoChopp.Ioc/Configurations/EntityFrameworkConfiguration.cs`

---

### ❌ Problema 4: Chave de ConnectionString Incorreta

**Causa**: O código estava procurando por `ReiDoChoppConnectionString` mas a chave configurada era `DefaultConnection`.

**Solução**:

```csharp
// ❌ ANTES
configuration["ConnectionStrings:ReiDoChoppConnectionString"]!

// ✅ DEPOIS
configuration.GetConnectionString("DefaultConnection")
```

**Arquivos modificados**: `ReiDoChopp.Ioc/Configurations/EntityFrameworkConfiguration.cs`

---

### ⚠️ Problema 5: Logging Verboso em Produção

**Aviso**: `No XML encryptor configured`, `Failed to determine the https port`

**Solução**: Ajustados níveis de log em appsettings.Production.json:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Warning"
    }
  }
}
```

**Arquivos modificados**: `appsettings.Production.json`

---

## Variáveis de Ambiente Necessárias

Quando fazer o deploy em produção via docker-compose, você PRECISA definir:

```bash
export DB_PASSWORD="sua_senha_postgresql"
export JWT_SECRET="sua_chave_jwt_segura"
export EMAIL_PUBLIC_KEY="sua_chave_publica_email"
export EMAIL_PRIVATE_KEY="sua_chave_privada_email"

docker-compose up -d
```

Ou criar um arquivo `.env` na raiz do projeto:

```env
DB_PASSWORD=sua_senha_postgresql
JWT_SECRET=sua_chave_jwt_segura
EMAIL_PUBLIC_KEY=sua_chave_publica_email
EMAIL_PRIVATE_KEY=sua_chave_privada_email
```

---

## Arquivos Modificados

1. ✅ `docker-compose.yml` - Corrigida string de conexão e variáveis de ambiente
2. ✅ `ReiDoChopp.Api/appsettings.Production.json` - Ajustados logs e retirado logging sensível
3. ✅ `ReiDoChopp.Ioc/Configurations/EntityFrameworkConfiguration.cs` - Corrigida chave de conexão e condicionalizado logging sensível

---

## Como Deploy com as Correções

```bash
# 1. Navegar até a pasta do projeto
cd ~/rei-do-chopp

# 2. Definir variáveis de ambiente
export DB_PASSWORD="sua_senha_segura"
export JWT_SECRET="sua_jwt_key_segura"
export EMAIL_PUBLIC_KEY="sua_chave_email"
export EMAIL_PRIVATE_KEY="sua_chave_email_privada"

# 3. Fazer o rebuild da imagem da API (se necessário)
docker-compose build

# 4. Subir os containers
docker-compose up -d

# 5. Verificar os logs
docker logs -f rei-do-chopp-api
```

---

## Conclusão

Todos os problemas foram corrigidos! A API agora:

- ✅ Conecta corretamente ao PostgreSQL
- ✅ Recebe variáveis de ambiente corretas
- ✅ Não expõe dados sensíveis em logs (produção)
- ✅ Segue as melhores práticas de configuração
