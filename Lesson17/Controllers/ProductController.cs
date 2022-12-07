using Microsoft.AspNetCore.Mvc;
using Lesson6.ProductsClassRealization;
using Lesson16.Models.Request;
using System.Xml.Linq;
using Lesson17.Models;
using System.Diagnostics;

namespace Lesson16.Controllers
{
    [Controller]
    [Route("[controller]/[action]")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _log;
        private ProductBasketService _productBasket;

        public ProductController(ProductBasketService productBasket, ILogger<ProductController> log)
        {
            _productBasket = productBasket;
            _log = log;
        }

        [HttpGet("")]
        [HttpGet("/")]
        public IActionResult List()
        {
            return View(_productBasket.GetListOfProducts().Select(DtoConvertExtentions.ToOutDto));
        }


        [HttpGet]
        public IActionResult PrintByType(string type)
        {
            if (Enum.TryParse<Product.ProductType>(type, out var productType)) 
            {
                return Ok(_productBasket.PrintProductsByType(productType));
            } 
            else
            {
                return NotFound($"No such type - {type}");
            }
        }

        private IActionResult AddAndRedirect(ProductDto pdto)
        {
            var p = pdto.ToProduct();
            _productBasket.Add(p);
            _log.LogInformation($"Added product: {p}");
            return Redirect("List");
        }

        [HttpGet]
        public IActionResult AddGeneral() => View();

        [HttpGet]
        public IActionResult AddFood() => View();

        [HttpGet]
        public IActionResult AddElectricalAppliance() => View();

        [HttpGet]
        public IActionResult AddChemical() => View();


        [HttpPost]
        public IActionResult AddGeneral(ProductDto p) => AddAndRedirect(p);

        [HttpPost]
        public IActionResult AddFood(FoodDto p) => AddAndRedirect(p);

        [HttpPost]
        public IActionResult AddElectricalAppliance(ElectricalApplianceDto p) => AddAndRedirect(p);

        [HttpPost]
        public IActionResult AddChemical(ChemicalDto p) => AddAndRedirect(p);


        [HttpGet]
        public IActionResult Delete(string itemName)
        {
            if (_productBasket.Remove(itemName)) _log.LogInformation($"Product {itemName} removed");
            return Redirect("List");
        }

        [HttpGet]
        public IActionResult Edit(string itemName)
        {
            var p = _productBasket.Get(itemName);
            switch (p.Type) {
                case Product.ProductType.General:
                    return View("EditGeneral", p.ToInDto());
                case Product.ProductType.Food:
                    return View("EditFood", (p as Food).ToInDto());
                case Product.ProductType.ElectricalAppliance:
                    return View("EditElectricalAppliance", (p as ElectricalAppliance).ToInDto());
                case Product.ProductType.Chemical:
                    return View("EditChemical", (p as Chemical).ToInDto());
                default:
                    return View("List");
            }
        }

        private IActionResult EditAndRedirect(ProductDto pdto)
        {
            var p = _productBasket.Get(pdto.Name);
            if (p != null)
            {
                _productBasket.Remove(pdto.Name);
                _productBasket.Add(pdto.ToProduct());
                _log.LogInformation($"Edit product: {p}");
            }
            return Redirect("List");
        }

        [HttpPost]
        public IActionResult EditGeneral(ProductDto p) => EditAndRedirect(p);

        [HttpPost]
        public IActionResult EditFood(FoodDto p) => EditAndRedirect(p);

        [HttpPost]
        public IActionResult EditElectricalAppliance(ElectricalApplianceDto p) => EditAndRedirect(p);

        [HttpPost]
        public IActionResult EditChemical(ChemicalDto p) => EditAndRedirect(p);

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}