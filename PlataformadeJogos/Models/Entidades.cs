namespace PlataformadeJogos.Models;

public class Categoria
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public string? Descricao { get; set; }
    public List<Jogo> Jogos { get; set; } = new();
}

public class Estudio
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public string? Pais { get; set; }
    public List<Jogo> Jogos { get; set; } = new();
}

public class Jogo
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
    public int EstudioId { get; set; }
    public Estudio? Estudio { get; set; }
}

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public string Email { get; set; } = "";
    public string SenhaHash { get; set; } = "";
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
}
