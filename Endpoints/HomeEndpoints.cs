using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace MinimalApi.Endpoints
{
    public static class HomeEndpoints
    {
        public static void MapHome(this WebApplication app)
        {
            app.MapGet("/", async context =>
            {
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync(@$"
                    <!DOCTYPE html>
                    <html lang='pt-BR'>
                    <head>
                        <meta charset='UTF-8'>
                        <title>Home</title>
                        <style>
                            html, body {{ height: 100%; margin: 0; font-family: Arial, sans-serif; }}
                            .background-container {{
                                position: relative;
                                width: 100%;
                                height: 100%;
                                display: flex;
                                flex-direction: column;
                                justify-content: center;
                                align-items: center;
                                overflow: hidden;
                            }}
                            
                            }}
                            .overlay {{
                                position: absolute;
                                top: 0; left: 0;
                                width: 100%; height: 100%;
                                background: linear-gradient(to bottom, rgba(0,0,0,0.3), rgba(0,0,0,0.7));
                                z-index: -1;
                            }}
                            .message {{
                                color: #ffffff;
                                font-size: 2rem;
                                text-align: center;
                                padding: 30px 50px;
                                border-radius: 12px;
                                background-color: rgba(0, 0, 0, 0.4);
                                box-shadow: 0 0 20px rgba(0,0,0,0.5);
                                margin-bottom: 40px;
                            }}
                            footer {{
                                color: #400c0cff;
                                font-size: 1rem;
                                text-align: center;
                                margin-top: auto;
                                padding: 10px 0;
                                background-color: rgba(91, 41, 41, 0.7);
                                width: 100%;
                                position: relative;
                                z-index: 1;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='background-container'>
                            <div class='overlay'></div>
                            <div class='message'>Bem-vindo à Minimal API- Projeto 2025!</div>
                            <div class='message'>Bootcamp-DIO-Avanade</div>
                            <footer>Vando Ramos - {DateTime.Now:dd/MM/yyyy}</footer>
                        </div>
                    </body>
                    </html>
                ");
            });
        }
    }
}
