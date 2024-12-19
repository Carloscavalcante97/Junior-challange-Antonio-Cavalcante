using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Junior_Challenge.Communication.Request;

public class Anel
{

    [Key]
    [JsonIgnore]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(50)]
    public string? Nome { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string? Poder { get; set; } = string.Empty;

    [Required]
    public string? Portador { get; set; } = string.Empty;

    [Required]
    public string? Forjador { get; set; } = string.Empty;

    [Url]
    [DefaultValue("https://quintacapa.com.br/wp-content/uploads/2021/11/O-Senhor-dos-Aneis-O-Um-Anel-destaque.jpg")]
    public string Imagem { get; set; } = "https://quintacapa.com.br/wp-content/uploads/2021/11/O-Senhor-dos-Aneis-O-Um-Anel-destaque.jpg";
}
