using System;
using System.Collections.Generic;
using System.Data.Entity;
using iHoaDon.Entities;
using iHoaDon.Infrastructure;
using System.Data.Entity.Infrastructure;

namespace iHoaDon.DataAccess
{
    /// <summary>
    /// The context of  db session
    /// </summary>
    public class EWhiteHatContext : DbContext, IUnitOfWork
    {
        private readonly Lazy<EntityQueryFilterProvider> _filterProviderInitializer = new Lazy<EntityQueryFilterProvider>();

        /// <summary>
        /// Initializes a new instance of the <see cref="EWhiteHatContext"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public EWhiteHatContext(string connectionString)
            : base(connectionString)
        {
            // Get the ObjectContext related to this DbContext
            var objectContext = (this as IObjectContextAdapter).ObjectContext;

            // Sets the command timeout for all the commands
            objectContext.CommandTimeout = 120;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EWhiteHatContext"/> class.
        /// </summary>
        /// 
        public EWhiteHatContext()
        {

        }

        #region Define tables

        /// <summary>
        /// ActionLog Table
        /// </summary>
        public DbSet<ActionLog> ActionLogs { get; set; }

        /// <summary>
        /// Account Table
        /// </summary>
        public DbSet<Account> Accounts { get; set; }

        /// <summary>
        /// AccountLogins Table
        /// </summary>
        public DbSet<AccountLogin> AccountLogins { get; set; }


        /// <summary>
        /// Profiles Table
        /// </summary>
        public DbSet<Profile> Profile { get; set; }

        public DbSet<ListReleaseInvoice> ListReleaseInvoice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<TemplateInvoice> TemplateInvoice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<InvoiceNumber> InvoiceNumber { get; set; }

        public DbSet<Currency> Currency { get; set; }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<Invoice> Invoice { get; set; }

        public DbSet<InvoiceDetail> InvoiceDetail { get; set; }

        public DbSet<Banks> Banks { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<Unit> Unit { get; set; }

        public DbSet<Transaction> Transaction { get; set; }

        #endregion

        /// <summary>
        /// Override some of EF's default settings.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Stop pluralizing my table names :)
            modelBuilder.Conventions.Remove
                <System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Gets the repository for the specified type of entity.
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <returns></returns>
        public IRepository<T> GetRepository<T>() where T : class
        {
            return new EWhiteHatRepository<T>(this);
        }

        /// <summary>
        /// Send raw modification command to the datasource
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public int RawModify(string commandText, params object[] parameters)
        {
            int result;
            try
            {
                using (var command = Database.Connection.CreateCommand())
                {
                    //Bổ sung thời gian time out tăng lên 120s thay 30s
                    //command.CommandTimeout = 120;
                    command.CommandText = commandText;
                    command.AddParams(parameters);
                    Database.Connection.Open();
                    result = command.ExecuteNonQuery();
                }
            }
            finally
            {
                Database.Connection.Close();
            }
            return result;
        }

        /// <summary>
        /// Make a raw query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public IEnumerable<dynamic> RawQuery(string query, params object[] parameters)
        {
            try
            {
                using (var command = Database.Connection.CreateCommand())
                {
                    //Bổ sung thời gian time out tăng lên 120s thay 30s
                    //command.CommandTimeout = 120;
                    command.CommandText = query;
                    command.AddParams(parameters);
                    Database.Connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        return reader.ToExpandoList();
                    }
                }
            }
            finally
            {
                Database.Connection.Close();
            }
        }

        /// <summary>
        /// Send a raw scalar command to the db
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public object RawScalar(string query, params object[] parameters)
        {
            object result;
            try
            {
                using (var command = Database.Connection.CreateCommand())
                {
                    //Bổ sung thời gian time out tăng lên 120s thay 30s
                    //command.CommandTimeout = 120;
                    command.CommandText = query;
                    command.AddParams(parameters);
                    Database.Connection.Open();
                    result = command.ExecuteScalar();
                }
            }
            finally
            {
                Database.Connection.Close();
            }
            return result;
        }

        /// <summary>
        /// Gets the filters specific to the underlying data source.
        /// </summary>
        /// <value>The filters.</value>
        public QueryFilterProvider Filters
        {
            get { return _filterProviderInitializer.Value; }
        }
    }
}