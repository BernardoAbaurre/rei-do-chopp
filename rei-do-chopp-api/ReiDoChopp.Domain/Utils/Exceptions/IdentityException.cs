using Microsoft.AspNetCore.Identity;

namespace ReiDoChopp.Domain.Utils.Exceptions
{
    public class IdentityException : Exception
    {
        public IEnumerable<IdentityError> Errors { get; set; }

        public IdentityException(IEnumerable<IdentityError> errors) : base(string.Join("; ", errors.Select(e => e.Description)))
        {
            Errors = errors;
        }
        public IdentityException(string message, IEnumerable<IdentityError> errors) : base($"{message}: {string.Join(" ", errors.Select(e => e.Description))}")
        {
            Errors = errors;
        }

    }
}
