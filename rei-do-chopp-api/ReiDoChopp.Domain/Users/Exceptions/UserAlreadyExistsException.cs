namespace ReiDoChopp.Domain.Users.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public string Email { get; set; }
        public UserAlreadyExistsException(string email) : base($"There's already a user with this this e-mail adress: {email}")
        {
            Email = email;
        }

        public UserAlreadyExistsException(Exception innerException, string email) : base($"There's already a user with this this e-mail adress: {email}", innerException)
        {
            Email = email;
        }
    }
}
