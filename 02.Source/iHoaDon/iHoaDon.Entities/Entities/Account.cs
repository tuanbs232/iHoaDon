using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace iHoaDon.Entities
{
    public partial class Account
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// gets or sets the LoginName
        /// </summary>
        [Column]
        public int ProfileId { get; set; }
        /// <summary>
        /// gets or sets the password
        /// </summary>
        [Column]
        public string CompanyCode { get; set; }

        /// <summary>
        /// gets or sets the password
        /// </summary>
        [Column]
        public byte[] PasswordHash { get; set; }

       /// <summary>
       /// 
       /// </summary>
        [Column]
        public byte[] PasswordSalt { get; set; }
        /// <summary>
        /// gets or sets the FullName
        /// </summary>
        [Column]
        public string CompanyName { get; set; }
        /// <summary>
        /// gets or sets the email
        /// </summary>
        [Column]
        public string Representative { get; set; }
        /// <summary>
        /// gets or sets the status
        /// </summary>
        [Column]
        public string Address { get; set; }
        /// <summary>
        /// gets or sets the role
        /// </summary>
        [Column]
        public string Phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string AccountType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string BankAccount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string BankName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public DateTime ActivationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public DateTime DateExpired { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public DateTime DeactivationTime { get; set; }

        /// <summary>
        /// gets or sets the role
        /// </summary>
        [Column]
        public byte RoleCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Permissions")]
        public long PermissionFlags { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public int? MasterAccountId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public byte Status { get; set; }

        [InverseProperty("Account")]
        public virtual ICollection<Customer> Customer { get; set; }



        [InverseProperty("Account")]
        public virtual ICollection<Currency> Currency { get; set; }
    }
}
