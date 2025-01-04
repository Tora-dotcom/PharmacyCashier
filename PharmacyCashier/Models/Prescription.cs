namespace PharmacyCashier.Models
{
          public class Prescription
        {
            public int PrescriptionId { get; set; }
            public string ProductName { get; set; }
            public DateTime Date { get; set; }

            // Relationships
            public int CustomerId { get; set; }
            public Customer Customer { get; set; }

            public int EmployeeId { get; set; }
            public Employee Employee { get; set; }
        }
    }


