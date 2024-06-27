using System.ComponentModel.DataAnnotations;

namespace App.Web.Models.Contact
{
    /// <summary>
    /// Represents a contact address model
    /// </summary>
    public record ContactAddressModel : BaseEntityModel
    {
        public int ContactId { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        [RegularExpression(@"^\+?\d{0,10}$", ErrorMessage = "Invalid zip code")]
        public string ZipPostalCode { get; set; }

        public bool HasEmptyAddress()
        {
            return string.IsNullOrWhiteSpace(Country)
                && string.IsNullOrWhiteSpace(State)
                && string.IsNullOrWhiteSpace(City)
                && string.IsNullOrWhiteSpace(Street)
                && string.IsNullOrWhiteSpace(ZipPostalCode);
        }
    }
}
