namespace App.Web.Models.Contact
{
    /// <summary>
    /// Represents a contact model
    /// </summary>
    public record ContactModel : BaseEntityModel
    {
        public ContactModel()
        {
            Addresses = new List<ContactAddressModel>();
        }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public List<ContactAddressModel> Addresses { get; set; }
    }
}
