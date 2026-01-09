# ATS API

> Protótipo de API para sistema ATS.

---

## 📖 Visão geral

Atualmente a API suporta:

- CRUD de candidatos
- CRUD de vagas
- Candidato se candidatar a uma vaga
- Listagem de candidatos candidatados a uma vaga

---

## 🏗️ Arquitetura

- API REST
- Arquitetura limpa e separação de camadas e responsabilidades
- Domain-Driven Design

---

## 🛠️ Stack utilizada

- .NET Core 10;
- MongoDB;
- MongoExpress para visualizar dados;
- Entity Framework Core;
- xUnit para testes de unidade, com Moq para mockar interfaces e Bogus para gerar dados falsos;
- Docker para banco de dados local;
- FluentValidation para facilitar validação;
- Logging: structured logs com Serilog.

---

## ⚙️ Pré-requisitos

Para rodar o ambiente de desenvolvimento, instale:

- SDK do .NET Core 10
- Docker

---

## ▶️ Como rodar o projeto

### 📦 Quick Start com Docker

1. Certifique-se de ter o Docker instalado.
2. Na raiz do projeto, execute: `docker-compose --profile full up -d --build`
3. A interface para testes com Scalar (similar ao Swagger) estará disponível em `http://localhost:8080/scalar/v1`
4. O Health Check pode ser verificado em `http://localhost:8080/_health`

> ⚠️ **ATENÇÃO:** para rodar o projeto completo é importante passar a flag `--profile full`.

> O arquivo compatível com OpenAPI será encontrado em `http://localhost:8080/openapi/v1.json`

### 👨‍💻 Como desenvolvedor (com IDE)

Faça o clone do repositório.

A fim de ter um banco de dados MongoDB local para desenvolver, rode o comando Docker Compose:

```bash
docker compose up -d
```

Este comando lerá o arquivo `docker-compose.yml` e subirá apenas os containers:

- MongoDB: ficará disponível na porta 27017.
- MongoExpress: disponível na porta 8081.

> **Atenção:** ao acessar o MongoExpress, será pedido um login. Preencha user com usuário "user" e senha "pass". Como é apenas um container local, não há problemas de segurança.

Abra a solução na IDE de sua preferência e rode o projeto de API. Exemplo de como rodar no Visual Studio:

![como rodar o projeto vs2026](https://github.com/user-attachments/assets/7d586220-395b-4067-8ab5-8e5b92db1aaa)

Ou é possível rodar direto com a CLI do .NET. Abra um terminal na pasta do projeto e rode o comando:

```bash
dotnet run
```

A versão HTTPS ficará disponível na porta 7071, e a versão HTTP, na porta 5158, conforme especificado no arquivo `Properties/launchSettings.json` do projeto API.

### Como rodar testes unitários

Usando a CLI: rode o comando `dotnet test` na pasta da solução.

Com uma IDE como o Visual Studio, clique com botão direito no projeto de Testes e clique em `Run`.

---

# Endpoints

## CRUD de candidatos

| Método | Endpoint | Descrição | Entrada (Request) | Retorno (Success) |
|---|---|---|---|---|
| POST | `/api/candidate` | Cadastra um novo candidato | JSON Body | 201 Created |
| GET | `/api/candidate/{id}` | Busca um candidato por ID | Route ID | 200 OK |
| GET | `/api/candidate` | Lista candidatos (Paginado) | Query Params | 200 OK |
| GET | `/api/candidate/{id}/applications` | Lista aplicações de um candidato. | Route ID | 200 OK |
| PUT | `/api/candidate/{id}` | Atualiza dados do candidato | Body + ID | 200 OK |
| DELETE | `/api/candidate/{id}` | Remove um candidato | Route ID | 204 No Content |

- Cache Strategy: Os endpoints de GET utilizam a política CandidatesCache.
- Cache Invalidation: Operações de escrita (POST, PUT, DELETE) executam automaticamente o `EvictByTagAsync` para a tag "candidates", garantindo que a listagem esteja sempre atualizada após uma mudança.
- Resiliência: Todos os endpoints suportam `CancellationToken` para interromper o processamento caso o cliente cancele a requisição.

> 💡 **Dica:** Para testar os payloads e visualizar os modelos de dados completos, acesse a documentação interativa em `{host}/scalar/v1` (apenas ambiente de desenvolvimento).

## CRUD de vagas

| Método | Endpoint | Descrição | Entrada (Request) | Retorno (Success) |
|---|---|---|---|---|
| POST | `/api/job` | Cadastra uma nova vaga | JSON Body | 201 Created |
| GET | `/api/job/{id}` | Busca uma vaga por ID | Route ID | 200 OK |
| GET | `/api/job` | Lista vagas (Paginado) | Query Params | 200 OK |
| GET | `/api/job/{id}/applications` | Lista aplicações de uma vaga. | Route ID | 200 OK |
| PUT | `/api/job/{id}` | Atualiza dados da vaga | Body + ID | 200 OK |
| DELETE | `/api/job/{id}` | Remove uma vaga | Route ID | 204 No Content |

Os endpoints de GET utilizam a política JobsCache, criada propositalmente apenas para demonstrar que é possível separar políticas de cache.

## Aplicação para vaga (JobApplication)

| Método | Endpoint | Descrição | Entrada (Request) | Retorno (Success) |
|---|---|---|---|---|
| POST | `/api/application` | Registra aplicação de um candidato a uma vaga. | JSON Body | 201 Created |

---

# Decisões técnicas e recursos

## 🗂️ Estrutura

O projeto busca seguir a Arquitetura Limpa, que permite melhor manutenção e potencial de crescimento no longo prazo. É composta pelos seguintes projetos:

- src/Domain: contém entidades, enums, interfaces de repositórios, exceções customizadas e mensagens de erro.
- src/Contracts: define requisições e respostas.
- src/Application: lógica e regras do negócio.
- src/Infrastructure: comunicação com banco de dados e gateways.
- src/Api: projeto ASP.NET Web API que contém endpoints.
- tests/UnitTests: projeto xUnit para testes de unidade.

## ⚠️ Filtro de Exceptions

Usei filtro de exceptions que traz várias vantagens:

- evitar try/catch nos controllers.
- tratamento de erros global com status codes apropriados.
- não expõe stack trace completo de erros.

## 🔬 Health Check

Endpoint para consultar saúde da base de dados disponível em `/_health`.

É uma prática essencial para garantir confiabilidade e observabilidade do sistema. A resposta vai além da padrão (que só traz Healthy ou Unhealthy), e inclui:

- Status: Healthy ou Unhealthy.
- Duração total da requisição.
- Detalhes de cada entrada que compõem o teste de saúde.

## 📊 Logs e Monitoramento

Adicionei logs estruturados com Serilog, algo que no longo prazo, com estratégias de monitoramento apropriadas, é muito útil.

No momento, apenas loga no Console, mas outras estratégias podem ser adicionadas facilmente.

## 🚅 Cache

Implementei um cache bem simples, de 1 minuto, nativo do .NET, para endpoints de leitura.

Demonstração nos logs:

<img width="1158" height="128" alt="cache" src="https://github.com/user-attachments/assets/10fb6bb0-eaab-4b2f-9de8-a60b63cca6e3" />

Um olhar atento verá que defini políticas customizadas para consultas de aplicações, seja para vagas ou candidatos.

Em vez de usar um cache genérico que precisaria ser limpo por completo a cada mudança, as políticas permitem criar Tags Dinâmicas baseadas no ID da rota (ex: `job-apps-{id}`).

O benefício prático é que quando um candidato se inscreve em uma vaga, o sistema identifica e apaga apenas o cache específico daquela vaga e daquele candidato.

Isso garante que os dados estejam sempre atualizados sem sacrificar a velocidade do sistema ou sobrecarregar o banco de dados desnecessariamente.

## 🗺️ Mapping

Fiz um mapping usando Extension methods, tanto de Request --> Entidade quanto de Entidade --> Response.

Em um cenário mais robusto, convém adicionar um Mapper.

## 🔗 Endpoints

Nomeados com letras minúsculas e com prefixo `api/`.

Para ter os endpoints centralizados em um só lugar, criei a classe estática `ApiEndpoints` no projeto API com constantes que facilitam manutenção e oferecem maior legibilidade. Exemplo:

❌ `[HttpGet("{id:guid}")]`

✅ `[HttpGet(ApiEndpoints.Candidate.GetById)]`

## 🗯️ Mensagens de Erro

Usando a mesma estratégia da classe estática com valores constantes.

**Alternativa:** Poderia criar um Resource com arquivos de tradução e adicionar Middleware de tradução no futuro.

## 🛂 Mensagens de commit

Buscam seguir o Conventional Commits, dividindo commits em categorias com seus respectivos prefixos: feat, fix, refactor, chore, test (etc.).

Da mesma forma, os commits buscam se concentrar em uma única funcionalidade ou mudança.

## 🧪 Testes

Utilizam o padrão Builder para gerar recursos comuns, principalmente entidades e requests, com fake data (Bogus) e mocks (Moq).

Testei principalmente:

- Casos de uso: pois contêm regras de negócio;
- Validators: importante garantir o funcionamento correto.

---

## 🧐 Considerações

Principalmente sobre recursos não implementados, porém possíveis.

### Soft delete

Em vez de apagar registros do banco de dados, é possível implementar um mecanismo de soft delete para apenas inativar registros.

O EF Core possui métodos que filtram registros com base em regras definidas, evitando que se faça a filtragem manual com `.Where()`, que pode ser esquecida pelo desenvolvedor.

### Testes unitários para controllers

Não adicionei testes unitários para os Controllers, porque testes de integração já ajudam a testá-los, quando existentes. Mas seria possível adicionar testes para eles também.

Da mesma forma, como a API até aqui é bastante simples, não houve necessidade de adicionar testes a nível de domínio (entidades, value-objects ou eventos de domínio).

### Rate Limiting

Fundamental para proteger a aplicação, e poderia ser implementada nativamente com o .NET, mas não adicionei por dois motivos: primeiro, para priorizar outros pontos; segundo, porque essa camada pode ficar fora da aplicação, em um API gateway.

### Autenticação

A princípio, autenticação via chave API key estava nos planos, mas decidi priorizar a fluidez nos testes do avaliador. A ausência de autenticação simplifica a exploração imediata dos endpoints via Swagger/Scalar sem a necessidade de configurações adicionais de Headers.

Mas poderia ser adicionada no futuro uma chave fixa no appsettings.json, ou algo mais robusto como salvar chaves criptografadas no banco de dados e só permitir requisições a partir delas.
