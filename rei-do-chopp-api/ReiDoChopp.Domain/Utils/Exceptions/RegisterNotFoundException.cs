namespace ReiDoChopp.Domain.Utils.Exceptions
{
    public class RegisterNotFound : Exception
    {
        public int Id { get; set; }
        public RegisterNotFound(int id) : base($"No register found with id {id}")
        {
            Id = id;
        }
    }
}
