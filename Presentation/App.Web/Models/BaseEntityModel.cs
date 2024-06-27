namespace App.Web.Models
{
    /// <summary>
    /// Represents the base model for all entity model
    /// </summary>
    public abstract record BaseEntityModel
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public int Id { get; set; }
    }
}
