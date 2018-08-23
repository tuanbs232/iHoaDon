using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Transactions;
using iHoaDon.Entities;
using iHoaDon.Infrastructure;

namespace iHoaDon.Business
{
    /// <summary>
    /// 
    /// </summary>
   public class ProductService:Service
    {
           private readonly IRepository<Product> _product;
        public ProductService(IUnitOfWork context)
            : base(context)
        {
            _product = Context.GetRepository<Product>();
        }

        public Product GetById(int id)
        {
            return _product.One(ProductQuery.WithById(id));
        }
        public Product GetByCode(string code)
        {
            return _product.Find(ProductQuery.WithByCode(code)).FirstOrDefault();
        }
        public Product GetByProName(string proName)
        {
            return _product.One(ProductQuery.WithByProName(proName));
        }

        public IEnumerable<Product> GelAll()
        {
            return _product.Find();
        }

        public IEnumerable<Product> GetByAccountId(int accountId)
        {
            return _product.Find(ProductQuery.WithByAccountId(accountId));
        }

        public IEnumerable<Product> GetAllProduct(out int totalRecords,
                                                                    int currentPage = 1,
                                                                    int pageSize = 25,
                                                                    string sortBy = "Id",
                                                                    bool descending = true)
        {
            var spec = ProductQuery.WithAll();

            totalRecords = _product.Count(spec);
            var sort = Context.Filters.Sort<Product, int>(ti => ti.Id, true);
            switch (sortBy)
            {
                case "Id":
                    sort = Context.Filters.Sort<Product, int>(ti => ti.Id, descending);
                    break;
                default:
                    break;
            }
            var pager = Context.Filters.Page<Product>(currentPage, pageSize);
            return _product.Find(spec, sort, pager);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        public int CreateProducts(Product product)
        {
            _product.Create(product);
            Context.SaveChanges();
            return product.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        public int UpdateProducts(Product product)
        {
            _product.Update(product);
            return Context.SaveChanges();
        }

        public void Create(Product product)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    if (product.Id == 0)
                        _product.Create(product);
                    else
                        _product.Update(product);
                    Context.SaveChanges();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw ex;
                }
            }
        }
    }
}
