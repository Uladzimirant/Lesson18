using Microsoft.AspNetCore.Mvc;
using Lesson6.ProductsClassRealization;
using Lesson16.Models.Request;
using System.Xml.Linq;
using Lesson17.Models;
using System.Diagnostics;

namespace Lesson16.Controllers
{
    [ApiController]
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

        [HttpGet("{name}")]
        public IActionResult Print([FromRoute] string? name) 
        { 
            var p = _productBasket.Get(name);
            return p != null ? Ok(p) : NotFound();
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

        private string Add(ProductDto pdto)
        {
            var p = pdto.ToProduct();
            _productBasket.Add(p);
            _log.LogInformation($"Added product: {p}");
            return "List";
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
        public IActionResult AddGeneral(ProductDto p) => View(Add(p));

        [HttpPost]
        public IActionResult AddFood(FoodDto p) => View(Add(p));

        [HttpPost]
        public IActionResult AddElectricalAppliance(ElectricalApplianceDto p) => View(Add(p));

        [HttpPost]
        public IActionResult AddChemical(ChemicalDto p) => View(Add(p));

        [HttpPut]
        public IActionResult ChangeAmount(string name, float amount)
        {
            if (_productBasket.SetAmount(name, amount))
            {
                _log.LogInformation($"Changed amount: {name}-{amount}");
                return Ok(_productBasket.Get(name));
            }
            else return NotFound($"No such element - {name}");
        }

        [HttpDelete]
        public IActionResult Delete(string name)
        {
            if (_productBasket.Remove(name)) _log.LogInformation($"Product {name} removed");
            return NoContent();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}