using ApiCatalogoJogos.Domain.Dtos.Jogos;
using ApiCatalogoJogos.Domain.Entities.Jogos;

namespace ApiCatalogoJogos.Domain.Interfaces.Jogos
{
    public interface IJogoService : IDisposable
    {
        Task<IEnumerable<JogoDto>?> Obter(int pagina, int quantidade);
        Task<JogoDto?> Obter(Guid id);
        Task<JogoDto?> Inserir(JogoInputDto dto);
        Task Atualizar(Guid id, JogoInputDto dto);
        Task Atualizar(Guid id, decimal preco);
        Task Excluir(Guid id);

    }
}
