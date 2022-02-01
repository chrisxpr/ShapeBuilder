using ShapeBuilder.Types.Models;

namespace ShapeBuilder.Types.Behaviours
{
    public interface IShapeHelper
    {
        ShapeDefinition MapToDefinition(string description);
        string CleanDescription(string description);
        ShapeData GenerateShapeData(string description, ShapeDefinition definition);
        ShapeData CreateShapeData(string shapePhrase);
    }
}
;