namespace ReiDoChopp.Domain.Users.Exceptions
{
    public class InvalidLoginException : ArgumentException
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public InvalidLoginException(string email, string password) : base("e-mail ou senha inválido")
        {
            Email = email;
            Password = password;
        }

        public InvalidLoginException(Exception innerException, string email, string password) : base("e-mail ou senha inválido", innerException)
        {
            Email = email;
            Password = password;
        }
    }
}
