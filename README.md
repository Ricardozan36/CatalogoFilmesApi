# Catálogo de Filmes API

## Descrição
API Web RESTful desenvolvida para gerenciamento de um catálogo de filmes, diretores e avaliações. O projeto permite operações de CRUD completas, com controle de acesso para proteção de recursos sensíveis.

## Tecnologias Utilizadas
* **Linguagem:** C#
* **Framework:** .NET 8 (ASP.NET Core Web API)
* **Banco de Dados:** SQLite
* **ORM:** Entity Framework Core
* **Autenticação:** JWT (JSON Web Token)
* **Documentação:** Swagger / OpenAPI

## Pré-requisitos
* SDK do [.NET 8](https://dotnet.microsoft.com/download) instalado.
* Ferramenta EF Core Tools global (`dotnet tool install --global dotnet-ef`).

## Instalação e Execução

1. Clone o repositório:
   `git clone https://github.com/SEU_USUARIO/CatalogoFilmesApi.git`
2. Acesse a pasta do projeto:
   `cd CatalogoFilmesApi`
3. Restaure as dependências:
   `dotnet restore`
4. Crie o banco de dados e aplique as migrations (isso também irá inserir os dados iniciais de teste):
   `dotnet ef database update`
5. Inicie a aplicação:
   `dotnet run`

## Documentação e Testes (Swagger)
A API utiliza o Swagger para documentação interativa. 
Com a aplicação rodando, acesse no navegador: `http://localhost:<porta>/swagger`

## Autenticação
A rota de exclusão de filmes é protegida. Para testá-la:
1. Acesse o endpoint `POST /api/Auth/login`.
2. Envie o payload de teste: `{"username": "admin", "password": "admin123"}`.
3. Copie o token gerado.
4. No topo do Swagger, clique em **Authorize** e digite `Bearer SEU_TOKEN`.

## Resumo dos Principais Endpoints
* `GET /api/Filmes` - Lista os filmes.
* `GET /api/Filmes/{id}` - Consulta um filme específico.
* `POST /api/Filmes` - Cria um novo filme.
* `PUT /api/Filmes/{id}` - Atualiza um filme.
* `DELETE /api/Filmes/{id}` - Remove um filme (Requer Autenticação JWT).