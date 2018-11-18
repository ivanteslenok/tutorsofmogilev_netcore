using Data.DTOs;
using Data.Entities;

namespace Data.ExtensionsDTO
{
    public static class ContactLoader
    {
        public static ContactDTO CreateDto(this Contact entity)
        {
            return new ContactDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Value = entity.Value,
                ContactType = entity.ContactType?.CreateDto()
            };
        }
    }
}
