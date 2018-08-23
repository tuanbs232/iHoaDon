using Newtonsoft.Json;

namespace iHoaDon.Entities
{
    /// <summary>
    /// A form's selection status
    /// </summary>
    public class PartSelection
    {
        /// <summary>
        /// Gets or sets the form key.
        /// </summary>
        /// <value>The key.</value>
        [JsonProperty("key")]
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [JsonProperty("title")]
        public string Title { get; set; }

        ///<summary>
        /// Gets or sets the description
        ///</summary>
        /// <value>The description.</value>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the template type code.
        /// </summary>
        /// <value>The template type code.</value>
        [JsonProperty("templatetype")]
        public byte TemplateTypeCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the form to be selected is a primary form.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is primary; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("primary",DefaultValueHandling=DefaultValueHandling.Ignore)]
        public bool IsPrimary { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is fixed.
        /// </summary>
        /// <value><c>true</c> if this instance is fixed; otherwise, <c>false</c>.</value>
        [JsonProperty("fixed", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsFixed { get; set; }

        private bool _isSelected;
        /// <summary>
        /// Gets or sets a value indicating whether this form is selected.
        /// NOTE: Integral forms (like _KHBS01 and _common) and the primary form are selected by default)
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("selected", DefaultValueHandling=DefaultValueHandling.Ignore)]
        public bool IsSelected
        {
            get { return IsPrimary || IsFixed || _isSelected; }
            set
            {
                if (CanChange)
                {
                    _isSelected = value;
                }
            }
        }

        ///<summary>
        ///</summary>
        [JsonProperty("isattachment", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsAttachmentPart { get; set; }

        /// <summary>
        /// Gets a value indicating whether this form part can be selected or deselected.
        /// NOTE: Integral form parts (like _KHBS01 and _common) and the primary form part cannot be deselected)
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can change; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool CanChange
        {
            get { return !(IsPrimary || IsFixed); }
        }

        /// <summary>
        /// Gets a value indicating whether this form part is integral.
        /// NOTE: by convention, integral form parts (like _KHBS01 and _common) are those whose keys begin with the underscore character _
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is integral; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool IsIntegral
        {
            get { return Key != null && Key.StartsWith("_"); }
        }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        [JsonIgnore]
        public int Order
        {
            get
            {
                return IsIntegral
                           ? -1
                           : IsPrimary
                                 ? 0
                                 : 1;
            }
        }

        /// <summary>
        /// Gets the index.
        /// </summary>
        /// <value>The index.</value>
        [JsonIgnore]
        public int Index
        {
            get
            {
                return IsIntegral
                           ? 2
                           : IsPrimary
                                 ? 0
                                 : 1;
            }
        }
    }
}