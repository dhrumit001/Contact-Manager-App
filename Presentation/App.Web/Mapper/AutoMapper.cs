using App.Core.Domain.Contacts;
using App.Web.Models.Contact;
using AutoMapper;

namespace App.Web.Mapper
{
    /// <summary>
    /// Represent class for defining mapping between related entities
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ContactMap();
        }

        /// <summary>
        /// Define map of contact entities with it's realated model
        /// </summary>
        private void ContactMap()
        {
            CreateMap<Contact, ContactModel>()
            .ForMember(dest=>dest.Address,opt=>opt.MapFrom(src=>src.ContactAddress))
            .ReverseMap();

            CreateMap<Address, ContactAddressModel>()
            .ReverseMap();
        }
    }

}
