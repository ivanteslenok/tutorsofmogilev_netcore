using Data.DTOs;
using Data.Entities;

namespace Data.ExtensionsDTO
{
    public static class ContactTypeLoader
    {
        public static ContactTypeDTO CreateDto(this ContactType entity)
        {
            return new ContactTypeDTO
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}
