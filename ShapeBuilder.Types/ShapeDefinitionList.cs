using ShapeBuilder.Types.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeBuilder.Types
{
    public class ShapeDefinitionList
    {
        public static Dictionary<string, ShapeDefinition> Mappings = new Dictionary<string, ShapeDefinition>()
        {
            {"isoscelestriangle", new ShapeDefinition{ Name = "Isosceles Triangle", Type="custom", Vertices = 3, Parameters="width,height" } },
            {"square", new ShapeDefinition{ Name = "Square", Type="rectangle", Vertices = 1, Parameters="side"} },
            {"scalenetriangle", new ShapeDefinition{ Name = "Scalene Triangle", Type="custom", Vertices = 3, Parameters="width,height"} },
            {"parallelogram", new ShapeDefinition{ Name = "Parallelogram", Type="custom", Vertices = 4, Parameters="width,height"}},
            {"equilateraltriangle", new ShapeDefinition { Name = "Equilateral Triangle", Type = "pointarray", Vertices = 3, Parameters="side" } },
            {"pentagon", new ShapeDefinition { Name = "Pentagon", Type = "pointarray", Vertices = 5, Parameters="side" } },
            {"rectangle", new ShapeDefinition { Name = "Rectangle", Type = "rectangle", Vertices = 1, Parameters="width,height" } },
            {"hexagon", new ShapeDefinition { Name = "Hexagon", Type = "pointarray", Vertices = 6, Parameters="side" } },
            {"heptagon", new ShapeDefinition { Name = "Heptagon", Type = "pointarray", Vertices = 7, Parameters="side" } },
            {"octagon", new ShapeDefinition { Name = "Octagon", Type = "pointarray", Vertices = 8, Parameters="side" } },  
            {"circle", new ShapeDefinition { Name = "Circle", Type = "circle", Parameters="radius", Vertices = 1 } },
            {"oval", new ShapeDefinition { Name = "Oval", Type = "ellipse", Parameters="radius", Vertices = 1 } }
        };
    }
}
