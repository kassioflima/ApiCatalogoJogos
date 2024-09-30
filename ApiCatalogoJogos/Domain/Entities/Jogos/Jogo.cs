namespace ApiCatalogoJogos.Domain.Entities.Jogos
{
    public class Jogo
    {
        public Guid Id { get; private set; }
        public string? Nome { get; private set; }
        public string? Produtora { get; private set; }
        public decimal Preco { get; private set; }

        public Jogo(string? nome, string? produtora, decimal preco)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Produtora = produtora;
            Preco = preco;
        }

        public Jogo(Guid id, string? nome, string? produtora, decimal preco)
        {
            Id = id;
            Nome = nome;
            Produtora = produtora;
            Preco = preco;
        }

        internal void SetNome(string? nome)
            => Nome = nome;

        internal void SetProdutora(string? produtora)
            => Produtora = produtora;

        internal void SetPreco(decimal preco)
            => Preco = preco;
    }
}
