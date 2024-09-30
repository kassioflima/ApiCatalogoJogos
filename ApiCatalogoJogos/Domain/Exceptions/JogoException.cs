namespace ApiCatalogoJogos.Domain.Exceptions
{
    public class JogoException : Exception
    {
        public static string DefaultMessage = "Erro ao realizar uma operação com um jogo.";
        
        public JogoException() : base(DefaultMessage) { }

        public JogoException(string message) : base(message) { }
    }
}
