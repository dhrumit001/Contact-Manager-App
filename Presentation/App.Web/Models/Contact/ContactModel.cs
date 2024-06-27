using System.ComponentModel.DataAnnotations;

namespace App.Web.Models.Contact
{
    /// <summary>
    /// Represents a contact model
    /// </summary>
    public record ContactModel : BaseEntityModel
    {
        public ContactModel()
        {
            Address = new ContactAddressModel();
        }

        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^\+?\d{0,13}$", ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter valid email")]
        public string EmailAddress { get; set; }

        public ContactAddressModel Address { get; set; }
    }
}
