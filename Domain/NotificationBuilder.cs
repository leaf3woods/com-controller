
namespace Domain
{
    public class NotificationBuilder
    {
        private string? _header;
        private string? _time;
        private string? _content;
        private string? _type;

        public NotificationBuilder WithHeader(string header)
        {
            _header = header;
            return this;
        }
        public NotificationBuilder WithTime(DateTime time)
        {
            _time = time.ToShortTimeString();
            return this;
        }
        public NotificationBuilder WithType(NotifyType type)
        {
            _type = type switch
            {
                NotifyType.Exception => "[异常]",
                NotifyType.State => "[状态]",
                NotifyType.Instant => "[消息]",
                _ => throw new ArgumentException("unexcepted notify type")
            };
            return this;
        }

        public NotificationBuilder WithMsg(string msg)
        {
            _content = msg;
            return this;
        }

        public string Build()
        {
            if (_content is null) throw new ArgumentNullException("a notification must content message");
            return $"{_header}@{_time}>{_type}: {_content}";
        }
    }

    public enum NotifyType
    {
        Exception,
        Instant,
        State,
    }
}
