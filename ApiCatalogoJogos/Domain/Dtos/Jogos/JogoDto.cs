using ApiCatalogoJogos.Configurations;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiCatalogoJogos.Domain.Dtos.Jogos
{
    public class JogoDto
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <example>5b20351c-3f86-495f-a741-0438fc02af08</example>
        [SwaggerSchema(Description = "Id"), SwaggerSchemaExample("5b20351c-3f86-495f-a741-0438fc02af08")]
        public Guid Id { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        /// <example>Sonic</example>
        [SwaggerSchema(Description = "Nome"), SwaggerSchemaExample("Sonic")]
        public string? Nome { get; set; }

        /// <summary>
        /// Produtora
        /// </summary>
        /// <example>Sony</example>
        [SwaggerSchema(Description = "Produtora"), SwaggerSchemaExample("Sony")]
        public string? Produtora { get; set; }

        /// <summary>
        /// Preco
        /// </summary>
        /// <example>10.50</example>
        [SwaggerSchema(Description = "Preco"), SwaggerSchemaExample("10.50")]
        public decimal Preco { get; set; }
    }
}
