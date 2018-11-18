using Data.DTOs;
using Data.Entities;

namespace Data.ExtensionsDTO
{
    public static class SpecializationLoader
    {
        public static SpecializationDTO CreateDto(this Specialization entity)
        {
            return new SpecializationDTO
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}
