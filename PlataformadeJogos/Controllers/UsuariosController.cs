using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformadeJogos.Data;
using PlataformadeJogos.DTOs;
using PlataformadeJogos.Models;
using PlataformadeJogos.Services;

namespace PlataformadeJogos.Controllers;

[ApiController]
[Route("api/usuarios")]
public class UsuariosController : ControllerBase
{
    private readonly AppDbContext _db;
    public UsuariosController(AppDbContext db) => _db = db;

    // A senha (nem o hash) nunca é retornada.
    private static UsuarioResponse Map(Usuario u) => new(u.Id, u.Nome, u.Email, u.CriadoEm);

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioResponse>>> Listar([FromQuery] string? search)
    {
        var q = _db.Usuarios.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
            q = q.Where(u => EF.Functions.Like(u.Nome, $"%{search}%") || EF.Functions.Like(u.Email, $"%{search}%"));
        var lista = await q.OrderBy(u => u.Nome).ToListAsync();
        return Ok(lista.Select(Map));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UsuarioResponse>> ObterPorId(int id)
    {
        var u = await _db.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return u is null ? NotFound(new { message = "Usuário não encontrado." }) : Ok(Map(u));
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioResponse>> Criar(UsuarioCreateRequest req)
    {
        var email = req.Email.Trim().ToLower();
        if (await _db.Usuarios.AnyAsync(u => u.Email == email))
            return Conflict(new { message = "E-mail já cadastrado." });

        var u = new Usuario { Nome = req.Nome.Trim(), Email = email, SenhaHash = SenhaHasher.Gerar(req.Senha) };
        _db.Usuarios.Add(u);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(ObterPorId), new { id = u.Id }, Map(u));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<UsuarioResponse>> Atualizar(int id, UsuarioUpdateRequest req)
    {
        var u = await _db.Usuarios.FindAsync(id);
        if (u is null) return NotFound(new { message = "Usuário não encontrado." });
        var email = req.Email.Trim().ToLower();
        if (await _db.Usuarios.AnyAsync(x => x.Email == email && x.Id != id))
            return Conflict(new { message = "E-mail já cadastrado." });

        u.Nome = req.Nome.Trim();
        u.Email = email;
        await _db.SaveChangesAsync();
        return Ok(Map(u));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var u = await _db.Usuarios.FindAsync(id);
        if (u is null) return NotFound(new { message = "Usuário não encontrado." });
        _db.Usuarios.Remove(u);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
