using Data.DTOs;
using Data.Entities;

namespace Data.ExtensionsDTO
{
    public static class SubjectLoader
    {
        public static SubjectDTO CreateDto(this Subject entity)
        {
            return new SubjectDTO
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}
