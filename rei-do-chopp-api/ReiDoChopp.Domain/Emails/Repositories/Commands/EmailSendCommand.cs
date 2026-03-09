namespace ReiDoChopp.Domain.Emails.Repositories.Models
{
    public class EmailSendCommand
    {
        public required string Subject { get; set; }
        public required string Text { get; set; }
        public required string RecipientName { get; set; }
        public required string RecipientAdress { get; set; }
        public bool IsHtml { get; set; }
    }
}
