# ATS API

> Protótipo de API para sistema ATS.

---

## 📖 Visão geral

Atualmente a API suporta:
- CRUD de candidatos

A ser adicionado:
- CRUD de vagas
- Candidato se candidatar a uma vaga
- Listagem de candidatos candidatados a uma vaga
- Cadastro de currículo para um candidato

> Deixei os CRUDs "não obrigatórios" por último porque seria um tanto repetitivo. Portanto, tendo um CRUD pronto, preferi focar em adicionar funcionalidades que dão robustez a uma API ter desde seus primeiros passos.

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

### Como desenvolvedor (com IDE)

Faça o clone do repositório.

A fim de ter um banco de dados MongoDB local para desenvolver, rode o comando Docker Compose:
```bash
docker compose up -d
```

Este comando lerá o arquivo `docker-compose.yml` e subirá os containers:
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

### Com Docker

*Em breve.*

### Como rodar testes unitários

Usando a CLI: rode o comando `dotnet test` na pasta da solução.

Com uma IDE como o Visual Studio, clique com botão direito no projeto de Testes e clique em `Run`.

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


## 🧪 Testes

Utilizam o padrão Builder para gerar recursos comuns, principalmente entidades e requests, com fake data (Bogus) e mocks (Moq).

Testei principalmente:
- Casos de uso: pois contêm regras de negócio;
- Validators: importante garantir o funcionamento correto.

Não adicionei testes unitários para os Controllers, porque testes de integração já ajudam a testá-los, quando existentes. Mas seria possível adicionar testes para eles também.

Da mesma forma, como a API até aqui é bastante simples, não houve necessidade de adicionar testes a nível de domínio (entidades, value-objects ou eventos de domínio).


## 🔬 Health Check

Endpoint para consultar saúde da base de dados disponível em `/_health`.

A resposta vai além da padrão (que só traz Healthy ou Unhealthy), e inclui:
- Status: Healthy ou Unhealthy.
- Duração total da requisição.
- Detalhes de cada entrada que compõem o teste de saúde.


## ⚠️ Filtro de Exceptions

Usei filtro de exceptions que traz várias vantagens:

- evitar try/catch nos controllers.
- tratamento de erros global com status codes apropriados.
- não expõe stack trace completo de erros.


## 📊 Logs e Monitoramento

Adicionei logs estruturados com Serilog, algo que no longo prazo, com estratégias de monitoramento apropriadas, é muito útil.

No momento, apenas loga no Console, mas outras estratégias podem ser adicionadas facilmente.


## 🗺️ Mapping

Mapping manual usando Extension methods, tanto de Request --> Entidade quanto de Entidade --> Response.

Em um cenário mais robusto, convém adicionar um Mapper.


## 🔗 Endpoints

Nomeados com letras minúsculas e com prefixo `api/`.

Para ter os endpoints centralizados em um só lugar, criei a classe estática `ApiEndpoints` no projeto API com constantes que facilitam manutenção e oferecem maior legibilidade. Exemplo:

❌ `[HttpGet("{id:guid}")]`

✅ `[HttpGet(ApiEndpoints.Candidate.GetById)]`


## 🗯️ Mensagens de Erro

Usando a mesma estratégia da classe estática com valores constantes.

**Alternativa:** Poderia criar um Resource com arquivos de tradução e adicionar Middleware de tradução no futuro.


## Mensagens de commit

Buscam seguir o Conventional Commits, dividindo commits em categorias com seus respectivos prefixos: feat, fix, refactor, chore, test (etc.).

Da mesma forma, os commits buscam se concentrar em uma única funcionalidade ou mudança.
