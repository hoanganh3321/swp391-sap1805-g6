using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using swp391_sap1805_g6.Entities;

namespace swp391_sap1805_g6.Reporitories
{

    public class ProductRepo
    {
        private static  BanhangContext? _context;

        public static bool ProsExist(int id)
        {
            _context = new();
            return _context.Products.Any(s => s.ProductId == id);
        }
        public static List<Product> GetAllProducts()
        {
            //_context = new();
            //return _context.Products.ToList();
            using (var context = new BanhangContext())
            {
                var products = context.Products
                                       .Select(p => new Product
                                       {
                                           ProductId = p.ProductId,
                                           Name = p.Name,
                                           Type = p.Type,
                                           Barcode = p.Barcode,
                                           Weight  = p.Weight,
                                           Price = p.Price,
                                           ManufacturingCost = p.ManufacturingCost,
                                           StoneCost = p.StoneCost,
                                           WarrantyInfo= p.WarrantyInfo,
                                           IsBuyback= p.IsBuyback,
                                          
                                       })
                                       .ToList();

                return products;
            }

        }

        public static Product? GetProductById(int id)
        {
            _context = new();
            return _context.Products.FirstOrDefault(x => x.ProductId == id);

        }
        public static void Add(Product product)
        {
            _context= new();
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public static void Update(Product product)
        {
            _context= new();          
            var productUpdate = _context.Products.First(x=> x.ProductId == product.ProductId);
            productUpdate.Name = product.Name;
            productUpdate.Type= product.Type;
            productUpdate.ManufacturingCost = product.ManufacturingCost;         
            productUpdate.IsBuyback= product.IsBuyback;
            productUpdate.StoneCost = product.StoneCost;
            productUpdate.Price= product.Price;
            productUpdate.Barcode= product.Barcode;
            productUpdate.Weight= product.Weight;
            _context.SaveChanges();

        }
        public static void Delete(int id)
        {
             var pro=GetProductById(id);
             var war=WarrantyRepo.GetWarrantyByProductById(id);
            if (pro != null && war!=null )
            {
                _context = new();
                _context.Warranties.Remove(war);
                _context.Products.Remove(pro);               
                _context.SaveChanges();
            }
          
        }

    }
}
