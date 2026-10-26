using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformadeJogos.Data;
using PlataformadeJogos.DTOs;
using PlataformadeJogos.Models;

namespace PlataformadeJogos.Controllers;

[ApiController]
[Route("api/categorias")]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _db;
    public CategoriasController(AppDbContext db) => _db = db;

    private static CategoriaResponse Map(Categoria c) => new(c.Id, c.Nome, c.Descricao, c.Jogos.Count);

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaResponse>>> Listar([FromQuery] string? search)
    {
        var q = _db.Categorias.AsNoTracking().Include(c => c.Jogos).AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
            q = q.Where(c => EF.Functions.Like(c.Nome, $"%{search}%"));
        var lista = await q.OrderBy(c => c.Nome).ToListAsync();
        return Ok(lista.Select(Map));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoriaResponse>> ObterPorId(int id)
    {
        var c = await _db.Categorias.AsNoTracking().Include(x => x.Jogos).FirstOrDefaultAsync(x => x.Id == id);
        return c is null ? NotFound(new { message = "Categoria não encontrada." }) : Ok(Map(c));
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaResponse>> Criar(CategoriaRequest req)
    {
        var nome = req.Nome.Trim();
        if (await _db.Categorias.AnyAsync(c => c.Nome == nome))
            return Conflict(new { message = "Já existe uma categoria com esse nome." });

        var c = new Categoria { Nome = nome, Descricao = req.Descricao };
        _db.Categorias.Add(c);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(ObterPorId), new { id = c.Id }, Map(c));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CategoriaResponse>> Atualizar(int id, CategoriaRequest req)
    {
        var c = await _db.Categorias.Include(x => x.Jogos).FirstOrDefaultAsync(x => x.Id == id);
        if (c is null) return NotFound(new { message = "Categoria não encontrada." });
        var nome = req.Nome.Trim();
        if (await _db.Categorias.AnyAsync(x => x.Nome == nome && x.Id != id))
            return Conflict(new { message = "Já existe uma categoria com esse nome." });

        c.Nome = nome;
        c.Descricao = req.Descricao;
        await _db.SaveChangesAsync();
        return Ok(Map(c));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var c = await _db.Categorias.FindAsync(id);
        if (c is null) return NotFound(new { message = "Categoria não encontrada." });
        if (await _db.Jogos.AnyAsync(j => j.CategoriaId == id))
            return Conflict(new { message = "Não é possível excluir: existem jogos nessa categoria." });

        _db.Categorias.Remove(c);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
