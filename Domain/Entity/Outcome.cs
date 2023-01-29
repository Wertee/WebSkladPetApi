namespace Domain.Entity
{
    public class Outcome
    {
        public Guid Id { get; set; }
        public DateTime OutcomeDate { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public string Recipient { get; set; }

    }
}
