# GeoGuardian API

## ✨ Visão Geral

A **GeoGuardian API** é uma aplicação ASP.NET Core  projetada para monitoramento e gestão de **áreas com o risco de deslizamentos, enchentes e rompimento de barragens**. O foco principal é oferecer uma base robusta e sólida para a aplicação da GeoGuardian, para o monitoramento de desastres e para o cuidado dos cidadãos.

* Integração com banco **Oracle** via Entity Framework Core
* Uso de boas práticas como DTOs, serviços, controllers e middleware de exceção
* Documentação interativa com **Swagger/OpenAPI** para testes e validações

---

## 🛠️ Tecnologias Utilizadas

* ASP.NET Core
* C# 13
* Oracle DB (com Oracle Entity Framework Core)
* Swagger (via Swashbuckle)

---

## 🔧 Instalação

1. **Clone o repositório**:

```bash
git clone https://github.com/Byells/GeoGuardianAPI.git
```

2. **Configure a connection string Oracle**:
   Edite o arquivo `appsettings.json` com:

```json
{
  "ConnectionStrings": {
    "Oracle": "User Id=SEU_USUARIO;Password=SUA_SENHA;Data Source=HOST:1521/SERVICE_NAME"
  }
}
```

3. **Execute a API**:

```bash
dotnet run
```

4. **Acesse a documentação Swagger**:
   [GeoGuardian](http://localhost:5033/swagger/index.html)

---

## 🔍 Swagger/OpenAPI

### 🔧 Configuração feita no `Program.cs`:

```csharp
builder.Services.AddSwaggerGen(c => {
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.SwaggerDoc("v1", new OpenApiInfo {
        Title = "GeoGuardian API",
        Version = "v1",
        Description = "API REST do GeoGuardian para prevenção de desastres."
    });
});
```

### 📌 Como usar o Swagger:

1. Acesse o endpoint `/swagger` após iniciar a API.
2. Navegue pelas rotas organizadas por controller.
3. Clique sobre um método (GET/POST/etc) para expandi-lo.
4. Preencha os campos de entrada (se houver).
5. Clique em "Try it out" > "Execute" para testar a chamada.

---

## 🔄 Endpoints

### 📍 Endereços

`/api/users/{userId}/addresses`

- `GET` → Lista todos os endereços do usuário

- `POST` → Cria um novo endereço

- `PUT /{addressId}` → Atualiza um endereço do usuário

- `DELETE /{addressId}` → Remove um endereço


> ⚠️ Campos obrigatórios: `StreetName`, `Neighborhood`, `Number`, `Latitude`, `Longitude`, além dos `CountryId`, `StateId` e `CityId` válidos no banco.

### 📌 Países, Estados e Cidades

- `/api/countries` → `GET` para listar todos os países

- `/api/states?countryId=X` → `GET` para listar os estados de um país

- `/api/cities?stateId=X` → `GET` para listar as cidades de um estado


> 🔄 Esses dados são úteis no cadastro de endereço. Os filtros por `countryId` e `stateId` são obrigatórios para garantir a consistência dos dados.

### 🧭 Tipos de Área de Risco

- `1 = FLOOD` → Enchente

- `2 = LANDSLIDE` → Deslizamento de terra

- `3 = DAM_BREAK` → Rompimento de barragem


### ⚠️ Áreas de Risco — `/api/riskarea`

- `GET` → Lista todas as áreas de risco.

- `GET /{id}` → Retorna dados de uma área específica.

- `POST /admin/{userId}` → Cria nova área de risco (somente admins).

- `PUT /admin/{userId}/{id}` → Atualiza área existente (admin).

- `DELETE /admin/{userId}/{id}` → Exclui uma área (admin).


> 🔐 Apenas usuários administradores (`UserTypeId = 1`) podem criar, editar e excluir áreas de risco

### 📡 Sensores — `/api/sensor`

- `GET` → Lista sensores cadastrados.

- `GET /{id}` → Detalha sensor por ID.

- `POST /admin/{userId}` → Cria sensor (admin).

- `PUT /admin/{userId}/{id}` → Atualiza sensor (admin).

- `DELETE /admin/{userId}/{id}` → Exclui sensor (admin).


> 🔐 Apenas usuários administradores (`UserTypeId = 1`) podem criar, editar e excluir sensores

### 📦 Modelos de Sensor

- `1 = ULTRASONIC_WL-X100 (Acme)` → Sensor de nível de água por ultrassom

- `2 = SOILMOIST-S200 (Acme)` → Sensor de umidade do solo


### 🔔 Alertas — `/api/alerts`

- `GET` → Lista todos os alertas emitidos.

- `GET /{id}` → Detalha alerta por ID.

- `POST` → Cria novo alerta com:

   - `AlertTypeId` (tipo de alerta)

   - `RiskAreaId` (área de risco vinculada)

   - `RiskLevel` de 1 a 3:

      - `1 = leve`

      - `2 = sério`

      - `3 = crítico`


### 📢 Tipos de Alerta (AlertTypeId)

- `1 = INFO` → Informativo

- `2 = WARNING` → Aviso

- `3 = CRITICAL` → Crítico

### 👤 Usuários — `/api/users`

- `GET` → Lista usuários cadastrados.

- `GET /{id}` → Detalha usuário por ID.

- `POST` → Cria novo usuário (tipo 1 = admin, tipo 2 = comum).

- `PUT /{id}` → Atualiza informações do usuário.

> 🧑‍⚖️ **Permissões**: Usuários com `UserTypeId = 1` são administradores e têm acesso total. Usuários com `UserTypeId = 2` podem visualizar tudo, mas **não podem criar, editar ou excluir** sensores e áreas de risco.

---

## ⚠️ Observações Importantes

- **NUNCA envie o campo** `**Id**` **nos métodos POST**, o sistema os gera automaticamente.

- O campo `Number` no endereço é obrigatório e deve ser preenchido sempre.

- Os relacionamentos com `UserId`, `CityId`, `RiskAreaId`, etc. **precisam existir no banco**.

- O Swagger ajuda a verificar se os campos obrigatórios estão sendo preenchidos corretamente e ainda retorna as mensagens de erro detalhadas para facilitar o debug.
---

## 🌟 Autor

Desenvolvido por Gabriel Lima Silva para o projeto GeoGuardian.