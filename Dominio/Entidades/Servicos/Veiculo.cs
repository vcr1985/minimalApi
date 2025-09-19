using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalApi.Dominio.Entidades.Servicos
{
    public class Veiculo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; } = default!;

        [Required]
        [MaxLength(100)]
        public string Marca { get; set; } = default!;

        [Required]
        [MaxLength(100)]
        public string Modelo { get; set; } = default!;

        [Required]
        [Range(1886, 2100)]
        public int Ano { get; set; }

        [MaxLength(50)]
        public required string Cor { get; set; }   // Adicionado

        [Column(TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; } // Adicionado
    }
}
