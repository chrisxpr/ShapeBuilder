using Microsoft.AspNetCore.Mvc;
using ShapeBuilder.Types.Behaviours;

namespace ShapeBuilder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShapeController : ControllerBase
    {
        private readonly IShapeHelper _shapeHelper;
        public ShapeController(IShapeHelper shapeHelper)
        {
            _shapeHelper = shapeHelper;
        }

        [HttpGet("{definition}")]
        public IActionResult Get(string definition)
        {
            if (string.IsNullOrEmpty(definition))
            {
                return BadRequest();
            }

            var response = _shapeHelper.CreateShapeData(definition);
            return Ok(response);
            
        }
    }
}