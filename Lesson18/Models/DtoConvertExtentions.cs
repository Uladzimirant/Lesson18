using Lesson16.Models.Request;
using Lesson17.Models.Response;
using Lesson6.ProductsClassRealization;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

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

        private static T FillInProduct<T>(this T dto, Product o) where T : ProductDto
        {
            dto.Name = o.Name;
            dto.Price = o.Price;
            dto.Amount = o.Amount;
            return dto;
        }
        public static ProductDto ToInDto(this Product o) => 
            new ProductDto().FillInProduct(o);
        public static FoodDto ToInDto(this Food o) =>
            new FoodDto() { ExpirationTime = o.ExpirationTime }.FillInProduct(o);
        public static ElectricalApplianceDto ToInDto(this ElectricalAppliance o) =>
            new ElectricalApplianceDto() { Voltage = o.Voltage, Amperage = o.Amperage, Power = o.Power }.FillInProduct(o);
        public static ChemicalDto ToInDto(this Chemical o) =>
            new ChemicalDto() { DangerLevel = Enum.Parse<ChemicalDto.DangerLevelType>(o.DangerLevel.ToString()) }.FillInProduct(o);

    }
}
