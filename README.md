# Catálogo de Filmes - API & Cliente Web

## Descrição
Solução *Full-Stack* desenvolvida para a gestão de um catálogo de filmes e realizadores. O projeto é composto por uma API Web RESTful robusta e uma interface de utilizador (Front-end) que consome os dados em tempo real, permitindo operações CRUD completas com controlo de acesso para rotas destrutivas.

## Tecnologias Utilizadas
**Back-end:**
* **Linguagem:** C#
* **Framework:** .NET 8 (ASP.NET Core Web API)
* **Base de Dados:** SQLite
* **ORM:** Entity Framework Core
* **Autenticação:** JWT (JSON Web Token)
* **Documentação:** Swagger / OpenAPI

**Front-end:**
* **Estrutura:** HTML5
* **Estilização:** CSS3 (Design responsivo e tema escuro)
* **Lógica e Integração:** JavaScript Vanilla (Fetch API, LocalStorage)

## Pré-requisitos
* SDK do [.NET 8](https://dotnet.microsoft.com/download) instalado.
* Ferramenta EF Core Tools global (`dotnet tool install --global dotnet-ef`).
* Um navegador web atualizado.

## Instalação e Execução

### 1. Iniciar a API (Back-end)
1. Clone o repositório:
   `git clone https://github.com/SEU_USUARIO/CatalogoFilmesApi.git`
2. Aceda à pasta do projeto:
   `cd CatalogoFilmesApi`
3. Restaure as dependências:
   `dotnet restore`
4. Crie a base de dados e aplique as *migrations* (isto irá inserir os dados iniciais de teste):
   `dotnet ef database update`
5. Inicie a aplicação:
   `dotnet run`

### 2. Iniciar a Interface Visual (Front-end)
1. Com a API a correr no terminal, navegue até à pasta `FrontEnd-Catalogo` através do explorador de ficheiros do seu sistema.
2. Dê um duplo clique no ficheiro `index.html`.
3. A página será aberta no seu navegador, comunicando automaticamente com a API local.

## Documentação e Testes (Swagger)
A API utiliza o Swagger para documentação interativa dos *endpoints*. 
Com a aplicação a correr, aceda no navegador: `http://localhost:<porta>/swagger`

## Autenticação e Exclusão de Filmes
A rota de exclusão de filmes (`DELETE`) é protegida. O projeto demonstra esta segurança de duas formas:

**Pela Interface Web:**
1. Clique no botão **Login Diretor**.
2. Utilize as credenciais `admin` / `admin123`.
3. O token será gerido automaticamente e os botões de exclusão ficarão visíveis nos cartões dos filmes.

**Pelo Swagger:**
1. Aceda ao endpoint `POST /api/Auth/login`.
2. Envie o *payload*: `{"username": "admin", "password": "admin123"}`.
3. Copie o token gerado.
4. No topo do Swagger, clique em **Authorize** e digite `Bearer SEU_TOKEN`.

## Resumo dos Principais Endpoints
* `GET /api/Filmes` - Lista o catálogo completo.
* `GET /api/Filmes/{id}` - Consulta um filme específico.
* `POST /api/Filmes` - Cria um novo registo.
* `PUT /api/Filmes/{id}` - Atualiza dados de um filme.
* `DELETE /api/Filmes/{id}` - Remove um filme (Requer Autenticação JWT).