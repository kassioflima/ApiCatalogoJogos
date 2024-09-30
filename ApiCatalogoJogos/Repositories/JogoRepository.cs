using ApiCatalogoJogos.Domain.Entities.Jogos;
using ApiCatalogoJogos.Domain.Interfaces.Jogos;
using System.Data.SqlClient;

namespace ApiCatalogoJogos.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private readonly SqlConnection _sqlConnection;
        private readonly ILogger<JogoRepository> _logger;

        public JogoRepository(IConfiguration configuration, ILogger<JogoRepository> logger)
        {
            _sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
            _logger = logger;
        }

        public async Task Atualizar(Guid id, Jogo dto)
        {
            var comando = $"update jogos set nome = '{dto.Nome}', produtora = '{dto.Produtora}', preco = {dto.Preco.ToString().Replace(",", ".")} where id = '{dto.Id}'";
            await _sqlConnection.OpenAsync();
            SqlCommand command = new(comando, _sqlConnection);
            await command.ExecuteNonQueryAsync();
            await _sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            _logger.LogInformation("Chamando dispose...");
            _sqlConnection?.Close();
            _sqlConnection?.Dispose();
        }

        public async Task Excluir(Guid id)
        {
            var comando = $"delete from jogos where id = {id}";
            await _sqlConnection.OpenAsync();
            SqlCommand command = new(comando, _sqlConnection);
            await command.ExecuteNonQueryAsync();
            await _sqlConnection.CloseAsync();
        }

        public async Task<Jogo?> Inserir(Jogo dto)
        {
            var comando = $"insert jogos (id, nome, produtora, preco) values ('{dto.Id}', '{dto.Nome}', '{dto.Produtora}', {dto.Preco.ToString().Replace(",",".")})";
            await _sqlConnection.OpenAsync();
            SqlCommand command = new(comando, _sqlConnection);
            await command.ExecuteNonQueryAsync();
            await _sqlConnection.CloseAsync();
            return dto;
        }

        public async Task<IEnumerable<Jogo>?> Obter(int pagina, int quantidade)
        {
            var jogos = new List<Jogo>();
            var comando = $"select * from jogos order by id offset {(pagina - 1) * quantidade} rows fetch next {quantidade} rows only";
            await _sqlConnection.OpenAsync();
            SqlCommand command = new(comando, _sqlConnection);
            SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                jogos.Add(new Jogo(Guid.Parse(reader["Id"].ToString()!),
                                    reader["Nome"]?.ToString(),
                                    reader["Produtora"]?.ToString(),
                                    decimal.Parse(reader["Preco"].ToString()!)));
            }

            await _sqlConnection.CloseAsync();
            return jogos;
        }

        public async Task<Jogo?> Obter(Guid id)
        {
            Jogo? jogo = null;
            var comando = $"select * from jogos where id = '{id}'";
            await _sqlConnection.OpenAsync();
            SqlCommand command = new(comando, _sqlConnection);
            SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                jogo = new Jogo(Guid.Parse(reader["Id"].ToString()!),
                    reader["Nome"]?.ToString(),
                    reader["Produtora"]?.ToString(),
                    decimal.Parse(reader["Preco"].ToString()!));
            }

            await _sqlConnection.CloseAsync();
            return jogo;
        }

        public async Task<IEnumerable<Jogo>?> Obter(string nome, string produtora)
        {
            var jogos = new List<Jogo>();
            var comando = $"select * from jogos where nome like '{nome}%' and produtora like '{produtora}%' ";
            await _sqlConnection.OpenAsync();
            SqlCommand command = new(comando, _sqlConnection);
            SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                jogos.Add(new Jogo(Guid.Parse(reader["Id"].ToString()!),
                    reader["Nome"]?.ToString(),
                    reader["Produtora"]?.ToString(),
                    decimal.Parse(reader["Preco"].ToString()!)));
            }

            await _sqlConnection.CloseAsync();
            return jogos;
        }
    }
}
