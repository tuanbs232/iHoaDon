using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace iHoaDon.Entities
{
    /// <summary>
    /// An Account Log
    /// </summary>
    public class AccountLogin
    {
        #region Members
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the login name.
        /// </summary>
        /// <value>The name of the login.</value>
        [Column(TypeName = "varchar")]
        [Required]
        public string LoginName { get; set; }

        /// <summary>
        /// Gets or sets the ip login system (client).
        /// </summary>
        /// <value>The ip of the account login.</value>
        [Column(TypeName = "varchar")]
        [Required]
        public string LoginIP { get; set; }

        /// <summary>
        /// Gets or sets the time of account login system.
        /// </summary>
        /// <value>The time login.</value>
        [Column]
        [Required]
        public DateTime LoginTime { get; set; }

        /// <summary>
        /// Gets or sets status of account when login system (success or fail)
        /// </summary>
        /// <value>The status.</value>
        [Column]
        [Required]
        public bool Status { get; set; }
        #endregion
    }
}
