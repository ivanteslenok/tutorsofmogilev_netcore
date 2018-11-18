using Data.DTOs;
using Data.Entities;

namespace Data.ExtensionsDTO
{
    public static class PhoneLoader
    {
        public static PhoneDTO CreateDto(this Phone entity)
        {
            return new PhoneDTO
            {
                Id = entity.Id,
                Number = entity.Number,
                Operator = entity.Operator
            };
        }
    }
}
