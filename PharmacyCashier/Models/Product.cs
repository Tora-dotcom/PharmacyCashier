using System.ComponentModel.DataAnnotations;

namespace PharmacyCashier.Models
{
    public class Product
 
        {
            public int ProductId { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
            public string Name { get; set; }

            [Required]
            [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
            public int Quantity { get; set; }

            [Required]
            [StringLength(50, ErrorMessage = "Category cannot be longer than 50 characters.")]
            public string Category { get; set; }

            [StringLength(500)]
            public string Description { get; set; }

            [Required]
            [StringLength(50)]
            public string BatchNumber { get; set; }

            [Required]
            public DateTime ExpiryDate { get; set; }

            [Required]
            [StringLength(100)]
            public string Manufacturer { get; set; }

            [Required]
            [StringLength(50)]
            public string StorageLocation { get; set; }

            [Required]
            public bool RequiresPrescription { get; set; }
        }


        // Functions
        //public void AddProduct(Product product);
        //public void UpdateProduct(int id, Product updatedProduct);
        //public Product GetProduct(int id);
        //public List<Product> GetAllProducts();
        //public bool CheckStockAvailability(int productId, int requiredQuantity);
    }


