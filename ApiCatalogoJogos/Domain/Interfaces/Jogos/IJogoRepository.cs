using ApiCatalogoJogos.Domain.Dtos.Jogos;
using ApiCatalogoJogos.Domain.Entities.Jogos;

namespace ApiCatalogoJogos.Domain.Interfaces.Jogos
{
    public interface IJogoRepository : IDisposable
    {
        Task<IEnumerable<Jogo>?> Obter(int pagina, int quantidade);
        Task<Jogo?> Obter(Guid id);
        Task<IEnumerable<Jogo>?> Obter(string nome, string produtora);
        Task<Jogo?> Inserir(Jogo dto);
        Task Atualizar(Guid id, Jogo dto);
        Task Excluir(Guid id);
    }
}
