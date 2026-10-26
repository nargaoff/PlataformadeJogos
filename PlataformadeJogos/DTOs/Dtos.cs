using System.ComponentModel.DataAnnotations;

namespace PlataformadeJogos.DTOs;

// Limites de tamanho alinhados com as validações do Form1 (front-end).

// ---------- Categorias ----------
public class CategoriaRequest
{
    [Required, StringLength(80, MinimumLength = 2)] public string Nome { get; set; } = "";
    [StringLength(300)] public string? Descricao { get; set; }
}
public record CategoriaResponse(int Id, string Nome, string? Descricao, int TotalJogos);

// ---------- Estúdios ----------
public class EstudioRequest
{
    [Required, StringLength(120, MinimumLength = 2)] public string Nome { get; set; } = "";
    [StringLength(80)] public string? Pais { get; set; }
}
public record EstudioResponse(int Id, string Nome, string? Pais, int TotalJogos);

// ---------- Jogos ----------
public class JogoRequest
{
    [Required, StringLength(100, MinimumLength = 1)] public string Nome { get; set; } = "";
    [StringLength(500)] public string? Descricao { get; set; }
    [Range(0, 10000000)] public decimal Preco { get; set; }
    [Range(1, int.MaxValue)] public int CategoriaId { get; set; }
    [Range(1, int.MaxValue)] public int EstudioId { get; set; }
}
public record JogoResponse(
    int Id, string Nome, string? Descricao, decimal Preco,
    int CategoriaId, string? CategoriaNome, int EstudioId, string? EstudioNome);

// ---------- Usuários ----------
public class UsuarioCreateRequest
{
    [Required, StringLength(120, MinimumLength = 2)] public string Nome { get; set; } = "";
    [Required, EmailAddress, StringLength(254)] public string Email { get; set; } = "";
    [Required, StringLength(100, MinimumLength = 6)] public string Senha { get; set; } = "";
}
// No Form1 a senha é "CreateOnly": a edição envia apenas nome e e-mail.
public class UsuarioUpdateRequest
{
    [Required, StringLength(120, MinimumLength = 2)] public string Nome { get; set; } = "";
    [Required, EmailAddress, StringLength(254)] public string Email { get; set; } = "";
}
public record UsuarioResponse(int Id, string Nome, string Email, DateTime CriadoEm);
