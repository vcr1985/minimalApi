namespace MinimalApi.Dominio.DTO
{
    public class VeiculoDTO
    {
        public int Id { get; set; }   // 🔹 agora o DTO tem Id também
        public string Nome { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Ano { get; set; }
        public string Cor { get; set; } = string.Empty;
        public decimal Preco { get; set; }
    }
}
