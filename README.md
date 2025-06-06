# GeoGuardian API

## ✨ Visão Geral

A **GeoGuardian API** é uma aplicação ASP.NET Core projetada para monitoramento e gestão de **áreas com o risco de deslizamentos, enchentes e rompimento de barragens**. O foco principal é oferecer uma base robusta e sólida para a aplicação da GeoGuardian, para o monitoramento de desastres e para o cuidado dos cidadãos.

- Integração com banco **Oracle** via Entity Framework Core

- Uso de boas práticas como DTOs, serviços, controllers e middleware de exceção

- Documentação interativa com **Swagger/OpenAPI** para testes e validações

- Sistema de autenticação com **JWT**


---

## 🛠️ Tecnologias Utilizadas

- ASP.NET Core

- C# 13

- Oracle DB (com Oracle Entity Framework Core)

- Swagger (via Swashbuckle)

- JWT (JSON Web Token)


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
  },
  "JwtSettings": {
    "SecretKey": "SUA_CHAVE_SECRETA",
    "Issuer": "GeoGuardianAPI",
    "Audience": "GeoGuardianMobile",
    "ExpireMinutes": 60
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

## 🔐 Autenticação com JWT

A maioria das rotas requer autenticação JWT. Para acessar rotas protegidas no **Swagger**:

1. Se você **ainda não tem um usuário**, vá até a rota `POST /api/users` e crie uma nova conta.

2. Em seguida, faça login pela rota `POST /api/auth/login`, informando seu email e senha. O retorno será um token JWT.

3. Copie o valor do token **que está entre aspas.**

4. No Swagger, clique em **Authorize** no topo direito.

5. Cole o token no campo.

6. Clique em **Authorize** e feche.

7. Agora você pode testar rotas que exigem autenticação!


> ⚠️ Caso tente acessar uma rota protegida sem estar autenticado, receberá um `401 Unauthorized`. Se estiver autenticado mas sem permissão (ex: usuário comum tentando rota de admin), receberá `403 Forbidden`.

---

## 🔄 Endpoints

### 📍 Endereços

`/api/users/{userId}/addresses`

- `GET` → Lista todos os endereços do usuário

- `POST` → Cria um novo endereço

- `PUT /{addressId}` → Atualiza um endereço do usuário

- `DELETE /{addressId}` → Remove um endereço

- `POST /api/users/me/addresses` → Cria um endereço novo ligado diretamente ao usuário

- `PUT /api/users/me/addresses/{id}` → edita um endereço ligado diretamente ao usuário

- `DELETE /api/users/me/addresses/{id}` → Deleta um endereço ligado diretamente ao usuário



> ⚠️ Campos obrigatórios: `StreetName`, `Neighborhood`, `Number`, além dos `CountryId`, `StateId` e `CityId` válidos no banco.

### 🌍 Location

- `GET /api/location/countries`

- `GET /api/location/states`

- `GET /api/location/cities`




> 🔄 Esses dados são úteis no cadastro de endereço. Eles mostram os países, estados e cidades cadastrados no banco.

### 🧭 Tipos de Área de Risco

- `1 = FLOOD` → Enchente

- `2 = LANDSLIDE` → Deslizamento de terra

- `3 = DAM_BREAK` → Rompimento de barragem


### ⚠️ Áreas de Risco — `/api/riskarea`

- `GET` → Lista todas as áreas de risco.

- `GET /{id}` → Retorna dados de uma área específica.

- `POST /admin` → Cria nova área de risco (somente admins).

- `PUT /admin/{id}` → Atualiza área existente (admin).

- `DELETE /admin/{id}` → Exclui uma área (admin).


> 🔐 Apenas usuários administradores (`UserTypeId = 1`) podem criar, editar e excluir áreas de risco.

### 📡 Sensores — `/api/sensor`

- `GET` → Lista sensores cadastrados.

- `GET /{id}` → Detalha sensor por ID.

- `POST /admin` → Cria sensor (admin).

- `PUT /admin/{id}` → Atualiza sensor (admin).

- `DELETE /admin/{id}` → Exclui sensor (admin).


> 🔐 Apenas usuários administradores (`UserTypeId = 1`) podem criar, editar e excluir sensores.

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

- `GET` → Lista usuários cadastrados (admin).

- `GET /{id}` → Detalha usuário por ID (admin).

- `POST` → Cria novo usuário (tipo 1 = admin, tipo 2 = comum).

- `PUT /{id}` → Atualiza informações do usuário (admin).

- `DELETE /{id}` → Exclui um usuário (admin).


### 👤 Ações do usuário logado — `/api/users/me`

- `GET /me` → Retorna seus próprios dados (com base no JWT).

- `PUT /me` → Atualiza seu próprio perfil.

- `DELETE /me` → Remove sua própria conta.


> 🔐 Todas essas rotas requerem autenticação. Se o token estiver ausente ou inválido, a API retorna `401 Unauthorized`.

---

## 🧪 Exemplos de Testes

### 🔑 Autenticar e copiar o token
> POST /api/auth/login 
```json
{
  "userMail": "leo@fiap.com",
  "password": "Leo123"
}
```

### ➕👤 Criar usuário
> POST /api/user
```json
{
  "userName": "Leonardo",
  "userMail": "leo@fiap.com",
  "password": "Leo123",
  "userTypeId": 1
}
```

### ➕🏠 Criar endereço
> POST /api/users/me/addresses
```json
{
  "countryId": 1,
  "stateId": 1,
  "cityId": 3,
  "neighborhood": "Centro",
  "streetName": "Rua das Águas",
  "number": "123",
  "complement": "Próximo ao rio"
}
```

### ➕⚠️ Criar área de risco
> POST /api/riskarea/admin
```json
{
  "name": "Enchente Zona Norte",
  "riskAreaTypeId": 1,
  "cityId": 3
}
```

###  ➕📡️ Criar sensor
> POST /api/sensor/admin
```json
{
  "name": "Sensor Rio Tietê",
  "riskAreaId": 5
}
```

### ➕🔔 Criar alerta
> POST /api/alert
```json
{
  "riskLevel": 3,
  "alertTypeId": 2,
  "addressId": 10
}
```
### ➡️ 'addressId' é o id do endereço criado, que você deseja colocar o alerta:


---

## ⚠️ Observações Importantes

- **NUNCA envie o campo** `**Id**` **nos métodos POST**, o sistema os gera automaticamente.

- O campo `Number` no endereço é obrigatório e deve ser preenchido sempre.

- Os relacionamentos com `UserId`, `CityId`, `RiskAreaId`, etc. **precisam existir no banco**.

- O Swagger ajuda a verificar se os campos obrigatórios estão sendo preenchidos corretamente e ainda retorna as mensagens de erro detalhadas para facilitar o debug.


---

## 🌟 Autor

Desenvolvido por Gabriel Lima Silva para o projeto GeoGuardian.