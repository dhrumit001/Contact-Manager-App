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

        public string ZipPostalCode { get; set; }
    }
}
