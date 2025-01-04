namespace PharmacyCashier.Service
{
    using PharmacyCashier.Data;
    using PharmacyCashier.Models;
    using Microsoft.EntityFrameworkCore;
    using PharmacyCashier.Data;
    using PharmacyCashier.Models;

    namespace PharmacySystem.Services
    {
        public class CustomerService
        {
            private readonly ApplicationDbContext _context;

            public CustomerService(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<Customer>> GetAllCustomersAsync()
            {
                return await _context.Customers.ToListAsync();
            }

            public async Task<Customer?> GetCustomerByIdAsync(int id)
            {
                return await _context.Customers.FindAsync(id);
            }

            public async Task AddCustomerAsync(Customer customer)
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateCustomerAsync(int id, Customer updatedCustomer)
            {
                var customer = await _context.Customers.FindAsync(id);
                if (customer != null)
                {
                    customer.Name = updatedCustomer.Name;
                    customer.Contact = updatedCustomer.Contact;
                    customer.LoyaltyPoints = updatedCustomer.LoyaltyPoints;

                    _context.Customers.Update(customer);
                    await _context.SaveChangesAsync();
                }
            }

            public async Task DeleteCustomerAsync(int id)
            {
                var customer = await _context.Customers.FindAsync(id);
                if (customer != null)
                {
                    _context.Customers.Remove(customer);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }

}
