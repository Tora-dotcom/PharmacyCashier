namespace PharmacyCashier.Models
{
          public class Transaction
        {
            public int TransactionId { get; set; }
            public DateTime Date { get; set; }
            public decimal TotalAmount { get; set; }
            public int EmployeeId { get; set; }
            public Employee Employee { get; set; }
            public int CustomerId { get; set; }
            public Customer Customer { get; set; }
        }
    }


