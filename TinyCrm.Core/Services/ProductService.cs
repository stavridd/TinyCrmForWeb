﻿using System;
using System.Linq;
using System.Collections.Generic;

using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly TinyCrmDbContext context;

        public ProductService(TinyCrmDbContext ctx)
        {
            context = ctx ??
                throw new ArgumentNullException(nameof(ctx));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public bool AddProduct(AddProductOptions options)
        {
            if (options == null) {
                return false;
            }

            var product = GetProductById(options.Id); 

            if (product != null) {
                return false;
            }

            if (string.IsNullOrWhiteSpace(options.Name)) {
                return false;
            }

            if (options.Price <= 0) {
                return false;
            }

            if (options.ProductCategory ==
              ProductCategory.Invalid) {
                return false;
            }

            product = new Product() {
                Id = options.Id,
                Name = options.Name,
                Price = options.Price,
                ProductCategory = options.ProductCategory
            };

            context.Add(product);

            var success = false;

            try {
                success = context.SaveChanges() > 0;
            } catch (Exception) {
                // log
            }

            return success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public bool UpdateProduct(string productId,
            UpdateProductOptions options)
        {
            if (options == null) {
                return false;
            }

            var product = GetProductById(productId);
            if (product == null) { 
                return false; 
            }

            if (!string.IsNullOrWhiteSpace(options.Description)) {
                product.Description = options.Description;
            }

            if (options.Price != null &&
              options.Price <= 0) {
                return false;
            }

            if (options.Price != null) {
                if (options.Price <= 0) {
                    return false;
                } else {
                    product.Price = options.Price.Value;
                }
            }

            if (options.Discount != null &&
              options.Discount < 0) {
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProductById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) {
                return null;
            }

            return context
                .Set<Product>()
                .SingleOrDefault(s => s.Id == id);
        }

        public ICollection<Product>
            SearchProduct(SearchProductOptions option)
        {
            if (option == null) {
                return null;
            }
            var productList = context.Set<Product>().ToList();
            if (!string.IsNullOrWhiteSpace(option.Id)) {
                productList = context.Set<Product>()
                    .Where(i => i.Id == option.Id)
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(option.Name)) {
                productList = context.Set<Product>()
                    .Where(i => i.Name == option.Name)
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(option.Description)) {
                productList = context.Set<Product>()
                    .Where(i => i.Description == option.Description)
                    .ToList();
            }

            if (option.Price != null) {
                productList = context.Set<Product>()
                    .Where(i => i.Price == option.Price)
                    .ToList();
            }

            if (option.Discount != null) {
                productList = context.Set<Product>()
                    .Where(i => i.Discount == option.Discount)
                    .ToList();
            }

            if (option.ProductCategory != null) {
                productList = context.Set<Product>()
                    .Where(i => i.ProductCategory
                    == option.ProductCategory)
                    .ToList();
            }

            return productList;
        }

    }
}
