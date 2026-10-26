using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformadeJogos.Data;
using PlataformadeJogos.DTOs;
using PlataformadeJogos.Models;

namespace PlataformadeJogos.Controllers;

[ApiController]
[Route("api/estudios")]
public class EstudiosController : ControllerBase
{
    private readonly AppDbContext _db;
    public EstudiosController(AppDbContext db) => _db = db;

    private static EstudioResponse Map(Estudio s) => new(s.Id, s.Nome, s.Pais, s.Jogos.Count);

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EstudioResponse>>> Listar([FromQuery] string? search, [FromQuery] string? pais)
    {
        var q = _db.Estudios.AsNoTracking().Include(s => s.Jogos).AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
            q = q.Where(s => EF.Functions.Like(s.Nome, $"%{search}%"));
        if (!string.IsNullOrWhiteSpace(pais))
            q = q.Where(s => s.Pais == pais);
        var lista = await q.OrderBy(s => s.Nome).ToListAsync();
        return Ok(lista.Select(Map));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EstudioResponse>> ObterPorId(int id)
    {
        var s = await _db.Estudios.AsNoTracking().Include(x => x.Jogos).FirstOrDefaultAsync(x => x.Id == id);
        return s is null ? NotFound(new { message = "Estúdio não encontrado." }) : Ok(Map(s));
    }

    [HttpPost]
    public async Task<ActionResult<EstudioResponse>> Criar(EstudioRequest req)
    {
        var nome = req.Nome.Trim();
        if (await _db.Estudios.AnyAsync(s => s.Nome == nome))
            return Conflict(new { message = "Já existe um estúdio com esse nome." });

        var s = new Estudio { Nome = nome, Pais = req.Pais };
        _db.Estudios.Add(s);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(ObterPorId), new { id = s.Id }, Map(s));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<EstudioResponse>> Atualizar(int id, EstudioRequest req)
    {
        var s = await _db.Estudios.Include(x => x.Jogos).FirstOrDefaultAsync(x => x.Id == id);
        if (s is null) return NotFound(new { message = "Estúdio não encontrado." });
        var nome = req.Nome.Trim();
        if (await _db.Estudios.AnyAsync(x => x.Nome == nome && x.Id != id))
            return Conflict(new { message = "Já existe um estúdio com esse nome." });

        s.Nome = nome;
        s.Pais = req.Pais;
        await _db.SaveChangesAsync();
        return Ok(Map(s));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var s = await _db.Estudios.FindAsync(id);
        if (s is null) return NotFound(new { message = "Estúdio não encontrado." });
        if (await _db.Jogos.AnyAsync(j => j.EstudioId == id))
            return Conflict(new { message = "Não é possível excluir: o estúdio possui jogos cadastrados." });

        _db.Estudios.Remove(s);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
