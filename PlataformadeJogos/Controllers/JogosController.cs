using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformadeJogos.Data;
using PlataformadeJogos.DTOs;
using PlataformadeJogos.Models;

namespace PlataformadeJogos.Controllers;

[ApiController]
[Route("api/jogos")]
public class JogosController : ControllerBase
{
    private readonly AppDbContext _db;
    public JogosController(AppDbContext db) => _db = db;

    private static JogoResponse Map(Jogo j) => new(
        j.Id, j.Nome, j.Descricao, j.Preco,
        j.CategoriaId, j.Categoria?.Nome, j.EstudioId, j.Estudio?.Nome);

    /// <summary>
    /// Sem parâmetros retorna TODOS os jogos (é o que a tela "Explorar" do Form1 espera).
    /// Filtros: search, categoriaId, estudioId, precoMin, precoMax, ordenar (nome|preco), pagina, tamanhoPagina.
    /// A paginação só é aplicada se "pagina" for informada.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<JogoResponse>>> Listar(
        [FromQuery] string? search, [FromQuery] int? categoriaId, [FromQuery] int? estudioId,
        [FromQuery] decimal? precoMin, [FromQuery] decimal? precoMax, [FromQuery] string? ordenar,
        [FromQuery] int? pagina, [FromQuery] int tamanhoPagina = 20)
    {
        if (pagina is < 1 || tamanhoPagina < 1 || tamanhoPagina > 100)
            return BadRequest(new { message = "Paginação inválida (pagina >= 1 e 1 <= tamanhoPagina <= 100)." });

        var q = _db.Jogos.AsNoTracking().Include(j => j.Categoria).Include(j => j.Estudio).AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
            q = q.Where(j => EF.Functions.Like(j.Nome, $"%{search}%") ||
                             (j.Descricao != null && EF.Functions.Like(j.Descricao, $"%{search}%")));
        if (categoriaId.HasValue) q = q.Where(j => j.CategoriaId == categoriaId);
        if (estudioId.HasValue) q = q.Where(j => j.EstudioId == estudioId);

        // SQLite não filtra/ordena decimal no servidor; preço e ordenação são aplicados em memória
        var itens = await q.ToListAsync();
        if (precoMin.HasValue) itens = itens.Where(j => j.Preco >= precoMin).ToList();
        if (precoMax.HasValue) itens = itens.Where(j => j.Preco <= precoMax).ToList();

        itens = ordenar?.ToLower() == "preco"
            ? itens.OrderBy(j => j.Preco).ToList()
            : itens.OrderBy(j => j.Nome).ToList();

        Response.Headers["X-Total-Count"] = itens.Count.ToString();
        if (pagina.HasValue)
            itens = itens.Skip((pagina.Value - 1) * tamanhoPagina).Take(tamanhoPagina).ToList();

        return Ok(itens.Select(Map));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<JogoResponse>> ObterPorId(int id)
    {
        var j = await _db.Jogos.AsNoTracking().Include(x => x.Categoria).Include(x => x.Estudio)
            .FirstOrDefaultAsync(x => x.Id == id);
        return j is null ? NotFound(new { message = "Jogo não encontrado." }) : Ok(Map(j));
    }

    [HttpPost]
    public async Task<ActionResult<JogoResponse>> Criar(JogoRequest req)
    {
        var erro = await ValidarRelacoes(req);
        if (erro is not null) return BadRequest(new { message = erro });

        var j = new Jogo
        {
            Nome = req.Nome.Trim(), Descricao = req.Descricao, Preco = req.Preco,
            CategoriaId = req.CategoriaId, EstudioId = req.EstudioId
        };
        _db.Jogos.Add(j);
        await _db.SaveChangesAsync();
        await _db.Entry(j).Reference(x => x.Categoria).LoadAsync();
        await _db.Entry(j).Reference(x => x.Estudio).LoadAsync();
        return CreatedAtAction(nameof(ObterPorId), new { id = j.Id }, Map(j));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<JogoResponse>> Atualizar(int id, JogoRequest req)
    {
        var j = await _db.Jogos.FindAsync(id);
        if (j is null) return NotFound(new { message = "Jogo não encontrado." });

        var erro = await ValidarRelacoes(req);
        if (erro is not null) return BadRequest(new { message = erro });

        j.Nome = req.Nome.Trim();
        j.Descricao = req.Descricao;
        j.Preco = req.Preco;
        j.CategoriaId = req.CategoriaId;
        j.EstudioId = req.EstudioId;
        await _db.SaveChangesAsync();
        await _db.Entry(j).Reference(x => x.Categoria).LoadAsync();
        await _db.Entry(j).Reference(x => x.Estudio).LoadAsync();
        return Ok(Map(j));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var j = await _db.Jogos.FindAsync(id);
        if (j is null) return NotFound(new { message = "Jogo não encontrado." });
        _db.Jogos.Remove(j);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    private async Task<string?> ValidarRelacoes(JogoRequest req)
    {
        if (!await _db.Categorias.AnyAsync(c => c.Id == req.CategoriaId)) return "A categoria informada não existe.";
        if (!await _db.Estudios.AnyAsync(s => s.Id == req.EstudioId)) return "O estúdio informado não existe.";
        return null;
    }
}
