namespace ApiCatalogoJogos.Domain.Exceptions
{
    public class InvalidRequestException : Exception
    {
        public static string DefaultMessage = "Parâmetros inválidos para esta operação.";
        
        public InvalidRequestException() : base(DefaultMessage) { }

        public InvalidRequestException(string message) : base(message) { }
    }
}
