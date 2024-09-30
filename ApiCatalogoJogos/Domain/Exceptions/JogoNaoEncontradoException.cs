namespace ApiCatalogoJogos.Domain.Exceptions
{
    public class JogoNaoEncontradoException : Exception
    {
        public static string DefaultMessage = "Jogo não encontrado.";

        public JogoNaoEncontradoException() : base(DefaultMessage) { }

        public JogoNaoEncontradoException(string message) : base(message) { }
    }
}
