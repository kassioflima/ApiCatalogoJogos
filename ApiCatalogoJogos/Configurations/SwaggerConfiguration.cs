using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiCatalogoJogos.Configurations
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            return services.AddSwaggerGen(options =>
            {
                ConfigureSwaggerInfo(options);
                AddXmlDocumentation(options);
                options.SchemaFilter<EnumSchemaFilter>();
                options.SchemaFilter<SwaggerSchemaExampleFilter>();
                options.EnableAnnotations();
            });
        }

        private static void ConfigureSwaggerInfo(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1",
                new OpenApiInfo()
                {
                    Title = "ApiCatalogoJogos",
                    Version = "v1",
                    Description = "## Objetivo\nAPI para manipulação dos dados cadastrais de jogos.\n" +
                          "## Público-Alvo\nClientes internos.\n" +
                          "## Provedores\nEquipe de Desenvolvimento.\n" +
                          "## Contexto em que a API se Insere\nA API faz parte do sistema de gestão de cadastros.\n" +
                          "## \n A Consulta Contrato de Planos e Benefícios, tem por objetivo disponibilizar dados sobre contratos de planos e benefícios de participantes para o sistema CRM",
                    Contact = new OpenApiContact { Name = "API Jogos", Email = "seuemail@exemplo.com", Url = new Uri("https://seusite.com") },
                    License = new OpenApiLicense { Name = "Licença API Jogos", Url = new Uri("https://seusite.com/licenca") }
                });
            options.EnableAnnotations();
        }

        private static void AddXmlDocumentation(SwaggerGenOptions options)
        {
            AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Select(x => x.GetName().Name)
                .Where(x => x!.StartsWith("API Jogos"))
                .ToList()
                .ForEach(assembly =>
                {
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{assembly}.xml");
                    if (File.Exists(xmlPath))
                        options.IncludeXmlComments(xmlPath);
                });
        }

    }
}
