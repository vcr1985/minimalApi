namespace MinimalApi.Dominio.ModelViews
{
    public static class Home
    {
        public static string GetHtml()
        {
            return @"
                <html>
                <body>
                    <h1>Bem-vindo à Minimal API</h1>
                </body>
                </html>";
        }
    }
}
