using ApiCatalogoJogos.Configurations;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace ApiCatalogoJogos.Domain.Dtos.Jogos
{
    public class JogoInputDto
    {
        /// <summary>
        /// Nome
        /// </summary>
        /// <example>Sonic</example>
        [SwaggerSchema(Description = "Nome"), SwaggerSchemaExample("Sonic")]
        [Required, StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do jogo deve conter entre 3 e 100 caracteres.")]
        public string? Nome { get; set; }

        /// <summary>
        /// Produtora
        /// </summary>
        /// <example>Sony</example>
        [SwaggerSchema(Description = "Produtora"), SwaggerSchemaExample("Sony")]
        [Required, StringLength(100, MinimumLength = 1, ErrorMessage = "O nome da produtora deve conter entre 3 e 100 caracteres.")]
        public string? Produtora { get; set; }

        /// <summary>
        /// Preco
        /// </summary>
        /// <example>10.50</example>
        [SwaggerSchema(Description = "Preco"), SwaggerSchemaExample("10.50")]
        [Required, Range(1, 99999, ErrorMessage = "O preço deve ser no minimo 1 real e no máximo 99999 reais.")]
        public decimal Preco { get; set; }
    }
}
