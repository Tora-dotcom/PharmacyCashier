namespace PharmacyCashier.Service
{
    using PharmacyCashier.Data;
    using PharmacyCashier.Models;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class ProductService
        {
            private readonly ApplicationDbContext _context;

            public ProductService(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<Product>> GetAllProductsAsync()
            {
                return await _context.Products.ToListAsync();
            }

            public async Task<Product?> GetProductByIdAsync(int id)
            {
                return await _context.Products.FindAsync(id);
            }

            public async Task AddProductAsync(Product product)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateProductAsync(int id, Product updatedProduct)
            {
                var product = await _context.Products.FindAsync(id);
                if (product != null)
                {
                    product.Name = updatedProduct.Name;
                    product.Quantity = updatedProduct.Quantity;
                    product.Category = updatedProduct.Category;
                    product.RequiresPrescription = updatedProduct.RequiresPrescription;
                    product.ExpiryDate = updatedProduct.ExpiryDate;

                    _context.Products.Update(product);
                    await _context.SaveChangesAsync();
                }
            }

            public async Task DeleteProductAsync(int id)
            {
                var product = await _context.Products.FindAsync(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                }
            }
        public async Task<List<Product>> FilterProductsAsync(string? category, bool? requiresPrescription)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Category == category);
            }

            if (requiresPrescription.HasValue)
            {
                query = query.Where(p => p.RequiresPrescription == requiresPrescription.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<List<Product>> GetProductsPaginatedAsync(int page, int pageSize, string? sortBy = null, bool ascending = true)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(sortBy))
            {
                query = ascending
                    ? query.OrderBy(e => EF.Property<object>(e, sortBy))
                    : query.OrderByDescending(e => EF.Property<object>(e, sortBy));
            }

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<bool> ValidatePrescriptionAsync(int productId, int prescriptionId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null || !product.RequiresPrescription)
                return true; // No prescription needed or invalid product.

            var prescription = await _context.Prescriptions.FindAsync(prescriptionId);
            return prescription != null;
        }

    }
}


