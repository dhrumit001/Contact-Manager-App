﻿

namespace App.Core.Domain.Contacts
{
    public class Contact : BaseEntity
    {
        #region Fields

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the phone number
        /// </summary>

        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the email address
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the date and time of entity creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of last update of entity
        /// </summary>
        public DateTime? UpdatedOnUtc { get; set; }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// Gets addresse of contact
        /// </summary>
        public virtual Address ContactAddress { get; }

        #endregion

    }
}
