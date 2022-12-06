using Lesson17.Models.Response;
using Lesson6.ProductsClassRealization;
using System.Runtime.CompilerServices;

namespace Lesson17.Models
{
    public static class DtoConvertExtentions
    {
        private static string GetInfo(this Food o) => $"Expiration Time: {o.ExpirationTime} days";
        private static string GetInfo(this Chemical o) => $"Danger Level: {o.DangerLevel}";
        private static string GetInfo(this ElectricalAppliance o) => $"{o.Voltage} V, {o.Amperage} A, {o.Power} W";

        public static ProductOutDto ToOutDto(this Product o)
        {
            ProductOutDto dto = new ProductOutDto();
            dto.Type = o.Type.ToString();
            dto.Price = o.Price;
            dto.Name = o.Name;
            dto.Amount = o.Amount;
            dto.AdditionalInfo = 
                (o as Food)?.GetInfo() ??
                (o as Chemical)?.GetInfo() ??
                (o as ElectricalAppliance)?.GetInfo() ??
                null;
            return dto;
        }
    }
}
