namespace MinimalApi.Dominio.DTOs
{
    public class HomeDTO
    {
        public string Mensagem { get; set; } = "Bem-vindo à Home!";
        public DateTime HoraServidor { get; set; } = DateTime.Now;
    }
}
