namespace MinimalApi.Dominio.DTO
{
    public class TokenDTO
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ValidoAte { get; set; }

        public TokenDTO() { }

        public TokenDTO(string token, DateTime validoAte)
        {
            Token = token;
            ValidoAte = validoAte;
        }
    }
}
