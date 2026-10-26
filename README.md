# Plataforma de Jogos (SenaiJogos)

Solução com **dois projetos**:

| Projeto | Tecnologia | Função |
|---|---|---|
| `PlataformadeJogos` | ASP.NET Core Web API (.NET 10) + Entity Framework Core + SQLite | Back-End (API RESTful em JSON) |
| `PlataformadeJogos.Front` | Windows Forms (.NET 10) | Front-End (`Form1`), consome a API via `HttpClient` |

## Pré-requisitos
- [.NET SDK 10](https://dotnet.microsoft.com/download)
- Windows (o Front-End é Windows Forms). A API roda em qualquer sistema.

## Como executar
1. **Suba a API primeiro** (perfil `http`, porta **5041**):
   ```bash
   dotnet run --project PlataformadeJogos --launch-profile http
   ```
   O banco `plataforma.db` é criado automaticamente.
2. **Abra o Front-End** (outro terminal ou pelo Visual Studio, com vários projetos de inicialização):
   ```bash
   dotnet run --project PlataformadeJogos.Front
   ```
3. Cadastre **categoria** e **estúdio** antes de cadastrar **jogos** (o jogo exige `categoriaId` e `estudioId`; os IDs aparecem em *Explorar*).

> O endereço da API usado pelo Front está em `PlataformadeJogos.Front/Form1.cs` (`BaseAddress`). Se mudar a porta, altere lá.

## Modelo de dados
```
Categoria 1 ───< N Jogo N >─── 1 Estudio        Usuario (independente)
```
- Categoria/estúdio com jogos **não podem ser excluídos** (409).
- `Categoria.Nome`, `Estudio.Nome` e `Usuario.Email` são únicos.
- A senha do usuário é salva como hash PBKDF2 e **nunca** é retornada pela API.

## Endpoints (contrato definido pelo Form1)

| Recurso | Rota base | Campos (JSON) |
|---|---|---|
| Jogos | `/api/jogos` | `nome`, `descricao`, `preco`, `categoriaId`, `estudioId` |
| Categorias | `/api/categorias` | `nome`, `descricao` |
| Estúdios | `/api/estudios` | `nome`, `pais` |
| Usuários | `/api/usuarios` | `nome`, `email`, `senha` (somente no cadastro) |

Para todos: `GET /` (lista), `GET /{id}`, `POST /` (201), `PUT /{id}` (200), `DELETE /{id}` (204).

Filtros: `GET /api/jogos?search=&categoriaId=&estudioId=&precoMin=&precoMax=&ordenar=nome|preco&pagina=&tamanhoPagina=` (sem `pagina`, retorna tudo; total no header `X-Total-Count`) · `GET /api/categorias?search=` · `GET /api/estudios?search=&pais=` · `GET /api/usuarios?search=`.

Exemplos prontos em `PlataformadeJogos/PlataformadeJogos.http`.

## Status HTTP
| Código | Quando |
|---|---|
| 200 / 201 / 204 | Sucesso (consulta/edição, criação, exclusão) |
| 400 | Validação falhou, categoria/estúdio inexistente, paginação inválida |
| 404 | Registro não encontrado |
| 409 | Nome/e-mail duplicado ou exclusão bloqueada por dependências |
| 500 | Erro inesperado (middleware global, resposta em JSON) |

## Estrutura
```
PlataformadeJogos.slnx
├── PlataformadeJogos/        (API: Controllers, Models, DTOs, Data, Services, Middleware)
└── PlataformadeJogos.Front/  (Form1.cs, Form1.Designer.cs, Form1.resx, Program.cs)
```
