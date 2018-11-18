using Data.DTOs;
using Data.Entities;

namespace Data.ExtensionsDTO
{
    public static class DistrictLoader
    {
        public static DistrictDTO CreateDto(this District entity)
        {
            return new DistrictDTO
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}
