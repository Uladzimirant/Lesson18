using Lesson6.ProductsClassRealization;
using static Lesson6.ProductsClassRealization.Chemical;

namespace Lesson16.Models.Request
{
    public class ChemicalDto : ProductDto
    {
        public enum DangerLevelType {
            Safe,
            Slight,
            Dangerous,
            Extreme
        }
         
        public DangerLevelType DangerLevel { get; set; }

        public override Product ToProduct()
        {
            if (Enum.TryParse<Chemical.DangerLevelType>(DangerLevel.ToString(), out var level))
            {
                return new Chemical(Name, Price, level, Amount);
            }
            else throw new ArgumentException(HelperClass.EnumMessage<Chemical.DangerLevelType>(DangerLevel.ToString()));
        }
    }
}
