using ApiCatalogoJogos.Domain.Dtos.Jogos;
using ApiCatalogoJogos.Domain.Entities.Jogos;
using ApiCatalogoJogos.Domain.Exceptions;
using ApiCatalogoJogos.Domain.Interfaces.Jogos;

namespace ApiCatalogoJogos.Services.Jogos
{
    public class JogoService(ILogger<JogoService> _logger, IJogoRepository _jogoRepository) : IJogoService
    {
        public async Task Atualizar(Guid id, JogoInputDto dto)
        {
            if (dto is null || id.Equals(Guid.Empty)) 
                throw new InvalidRequestException("Parâmetros inválidos para cadastrar jogo.");

            var jogo = await _jogoRepository.Obter(id);
            if (jogo is null)
                throw new JogoNaoEncontradoException("Jogo não encontrado.");

            jogo.SetPreco(dto.Preco);
            jogo.SetProdutora(dto.Produtora);
            jogo.SetNome(dto.Nome);
            await _jogoRepository.Atualizar(id, jogo);
        }

        public async Task Atualizar(Guid id, decimal preco)
        {
            if (id.Equals(Guid.Empty)) 
                throw new InvalidRequestException("Parâmetros inválidos para cadastrar jogo.");

            var jogo = await _jogoRepository.Obter(id);
            if (jogo is null)
                throw new JogoNaoEncontradoException("Jogo não encontrado.");

            jogo.SetPreco(preco);
            await _jogoRepository.Atualizar(id, jogo);
        }

        public void Dispose()
        {
            _logger.LogInformation($"Jogo service fazendo Dispose...");
            _jogoRepository.Dispose();
        }

        public async Task Excluir(Guid id)
        {
            if (id.Equals(Guid.Empty))
                throw new InvalidRequestException("Parâmetros inválidos para cadastrar jogo.");

            _logger.LogInformation($"Excluindo jogo {id}");
            var jogo = await _jogoRepository.Obter(id);
            if (jogo is null)
                throw new JogoNaoEncontradoException("Jogo não encontrado.");

            await _jogoRepository.Excluir(id);
        }

        public async Task<JogoDto?> Inserir(JogoInputDto dto)
        {
            if(dto is null) throw new InvalidRequestException("Parâmetros inválidos para cadastrar jogo.");

            var jogos = await _jogoRepository.Obter(dto.Nome!, dto.Produtora!);
            if (jogos?.Count() > 0)
                throw new JogoException("Jogo já cadastrado.");

            var jogoInsert = new Jogo(dto.Nome, dto.Produtora, dto.Preco);
            await _jogoRepository.Inserir(jogoInsert);
            return new JogoDto
            {
                Id = jogoInsert.Id,
                Nome = jogoInsert.Nome,
                Preco = jogoInsert.Preco,
                Produtora = jogoInsert.Produtora
            };
        }

        public async Task<IEnumerable<JogoDto>?> Obter(int pagina, int quantidade)
        {
            var jogos = await _jogoRepository.Obter(pagina, quantidade);
            return jogos?.Select(x => new JogoDto
            {
                Id = x.Id,
                Nome = x.Nome,
                Preco = x.Preco,
                Produtora = x.Produtora
            });
        }

        public async Task<JogoDto?> Obter(Guid id)
        {
            var jogo = await _jogoRepository.Obter(id);
            if (jogo == null) return default;
            return new JogoDto
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Preco = jogo.Preco,
                Produtora = jogo.Produtora
            };
        }
    }
}
