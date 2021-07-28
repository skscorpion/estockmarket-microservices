namespace EventBus.Messages.Events
{
    public class CompanyDeleteEvent : IntegrationBaseEvent
    {
        public int UserId { get; set; }
        public string Code { get; set; }
    }
}
