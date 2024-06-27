namespace App.Web.Models.Contact
{
    /// <summary>
    /// Represents a contact search model
    /// </summary>
    public record ContactSearchModel : BaseSearchModel
    {
        public string SearchName { get; set; }

        public string SearchEmail { get; set; }

        public string SearchPhoneNumber { get; set; }
    }
}
