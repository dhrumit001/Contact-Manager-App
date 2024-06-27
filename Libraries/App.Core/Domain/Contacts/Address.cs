using System.ComponentModel.DataAnnotations.Schema;


namespace App.Core.Domain.Contacts
{
    [Table(nameof(Address))]
    public class Address : BaseEntity
    {
        #region Fields

        /// <summary>
        /// Gets or sets the contact id for address
        /// </summary>
        public int ContactId { get; set; }

        /// <summary>
        /// Gets or sets the country name
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the state name
        /// </summary>

        public string State { get; set; }

        /// <summary>
        /// Gets or sets the city name
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the street name
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the zip postal code
        /// </summary>
        public string ZipPostalCode { get; set; }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// Gets the contact of address
        /// </summary>
        public virtual Contact Contact { get; }

        #endregion
    }
}
