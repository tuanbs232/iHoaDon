using System;
using System.ComponentModel.DataAnnotations;

namespace iHoaDon.Entities
{
    /// <summary>
    /// Map Action Log
    /// </summary>
    public class ActionLog
    {
        ///<summary>
        /// Gets or sets the Id
        ///</summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the LoginName 
        /// </summary>
        [StringLength(32)]
        [Required]
        public string LoginName { get; set; }

        /// <summary>
        /// Gets or sets the action content
        /// </summary>
        [StringLength(200)]
        public string ActionContent { get; set; }

        /// <summary>
        /// Gets or sets data before change
        /// </summary>
        public string DataBeforeChange{ get; set; }

        /// <summary>
        /// Gets or sets data after change
        /// </summary>
        public string DataAfterChange { get; set; }
        
        /// <summary>
        /// Gets or sets time of action
        /// </summary>
        [Required]
        public DateTime ActionTime { get; set; }

        /// <summary>
        /// Gets or sets type of action
        /// </summary>
        [Required]
        public byte ActionType { get; set; }
    }
}
