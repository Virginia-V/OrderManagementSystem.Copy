namespace OMS.Common.Dtos.Invoices
{
    public class OpenInvoiceDto
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public string Customer { get; set; }
        public DateTime DueDate { get; set; }
        public int Aging => (DateTime.Now - DueDate).Days;
        public decimal Balance { get; set; }
        public decimal CurrentBalance => (Aging <= 0) ? Balance : 0.00m;
        public decimal BalanceDueUpTo30Days => (Aging >= 1 && Aging <= 30) ? Balance : 0.00m;
        public decimal BalanceDueBetween31And60Days => (Aging > 30 && Aging <= 60) ? Balance : 0.00m;
        public decimal BalanceDueBetween61And90Days => (Aging > 60 && Aging <= 90) ? Balance : 0.00m;
        public decimal BalanceDueOver91Days => (Aging > 90) ? Balance : 0.00m;
    }
}

