using ReiDoChopp.Domain.PrintControls.Enums;
using ReiDoChopp.Domain.Users.Entities;

namespace ReiDoChopp.Domain.PrintControls.Entities
{
    public class PrintControl
    {
        public int Id { get; protected set; }
        public PrintControlStatusEnum Status { get; protected set; }
        public DateTime RequestDate { get; protected set; }
        public virtual User User { get; protected set; }
        public int UserId { get; protected set; }
        public string Content { get; protected set; }

        protected PrintControl() { }

        public PrintControl(User user, string content)
        {
            SetStatus(PrintControlStatusEnum.Requested);
            SetRequestDate(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc));
            SetUser(user);
            SetContent(content);
        }

        public virtual void SetStatus(PrintControlStatusEnum status)
        {
            Status = status;
        }

        public virtual void SetRequestDate(DateTime requestDate)
        {
            if (requestDate == DateTime.MinValue)
                throw new ArgumentException("Required field: RequestDate");
            RequestDate = DateTime.SpecifyKind(requestDate, DateTimeKind.Utc);
        }
        public virtual void SetUser(User user)
        {
            User = user;
            UserId = user.Id;
        }
        public virtual void SetContent(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Required field: Content");

            Content = content;
        }

    }
}
