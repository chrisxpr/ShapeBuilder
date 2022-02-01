using ShapeBuilder.Types;
using ShapeBuilder.Types.Behaviours;
using ShapeBuilder.Types.Models;

namespace ShapeBuilder.Helper
{
    public class ShapeHelper : IShapeHelper
    {
        public ShapeDefinition MapToDefinition(string description)
        {
            var parseItems = description.Split(" ");
            ShapeDefinition definition = null;
            var candidateKey = parseItems[0];
            if (ShapeDefinitionList.Mappings.ContainsKey(candidateKey))
            {
                definition = ShapeDefinitionList.Mappings[candidateKey];
            }

            if (definition == null && parseItems.Length > 1)
            {
                candidateKey = parseItems[0] + parseItems[1];
                if (ShapeDefinitionList.Mappings.ContainsKey(candidateKey))
                {
                    definition = ShapeDefinitionList.Mappings[candidateKey];
                }
            }

            return definition;
        }

        public ShapeData GenerateShapeData(string description, ShapeDefinition definition)
        {
            var shapeData = new ShapeData();

            // create parameter data
            if (definition?.Name == null || string.IsNullOrEmpty(description))
            {
                shapeData.Message = "Unable to parse shape description";
                return shapeData;
            }
            shapeData.Name = definition.Name;
            shapeData.Type = definition.Type;

            var parameterValues = description.ToLower().Replace(definition.Name.ToLower(), "").Trim();

            switch(definition.Type)
            {
                case ShapeTypes.POINTARRAY:
                    shapeData.DataPoints = CreatePointArrayData(definition.Vertices, definition.Parameters, parameterValues);
                    break;
                case ShapeTypes.CIRCLE:
                    shapeData.DataPoints = CreateCircleData(definition.Parameters, parameterValues);
                    break;
                case ShapeTypes.ELLIPSE:
                    shapeData.DataPoints = CreateEllipseData(definition.Parameters, parameterValues);
                    break;
                case ShapeTypes.RECTANGLE:
                    shapeData.DataPoints = CreateRectangleData(definition.Parameters, parameterValues);
                    break;
                case ShapeTypes.CUSTOM:
                    shapeData.DataPoints = CreateCustomData(definition, parameterValues);
                    break;
            }

            shapeData.Match = shapeData?.DataPoints?.Count > 0;

            if (!shapeData.Match)
            {
                shapeData.Message = "Unable to parse shape description";
            }

            return shapeData;
        }

        public string CleanDescription(string description)
        {
            var parsedDescription = description.ToLower().Replace("draw an ", "");
            parsedDescription = parsedDescription.ToLower().Replace("draw a ", "");
            parsedDescription = parsedDescription.ToLower().Replace("with a ", "");
            parsedDescription = parsedDescription.ToLower().Replace("and a ", "");
            parsedDescription = parsedDescription.ToLower().Replace("length ", "");
            parsedDescription = parsedDescription.ToLower().Replace("of ", "");

            return parsedDescription;
        }

        private List<DataPoint> CreatePointArrayData(int vertices, string parameters, string parameterValues)
        {
            var dataPoints = new List<DataPoint>();
            var measure = parameterValues.Replace(parameters, "").Trim();
            if (int.TryParse(measure, out var measureVal))
            {
                for (var i = 0; i < vertices; i++)
                {
                    var offset = 0.0d;
                    if (vertices % 4 == 0)
                        offset = Math.PI;
                    if (vertices % 3 == 0)
                        offset = -Math.PI / 2;
                    if (vertices % 5 == 0)
                        offset = 2 * Math.PI / 5;

                    var x = Math.Cos(((2 * i * Math.PI) - offset) / vertices) * measureVal;
                    var y = Math.Sin(((2 * i * Math.PI) - offset) / vertices) * measureVal;
                    dataPoints.Add(new DataPoint { X = x, Y = y });
                }
            }

            return dataPoints;

        }

        private List<DataPoint> CreateCircleData(string parameters, string parameterValues)
        {
            var dataPoints = new List<DataPoint>();
            var measure = parameterValues.Replace(parameters, "").Trim();
            if (int.TryParse(measure, out var measureVal))
            {
                dataPoints.Add(new DataPoint { R1 = measureVal });
            }

            return dataPoints;

        }

        private List<DataPoint> CreateEllipseData(string parameters, string parameterValues)
        {
            var dataPoints = new List<DataPoint>();
            var measure = parameterValues.Replace(parameters, "").Trim();
            if (int.TryParse(measure, out var measureVal))
            {
                dataPoints.Add(new DataPoint { R1 = measureVal, R2 = measureVal / 2 });
            }

            return dataPoints;
        }

        private List<DataPoint> CreateRectangleData(string parameters, string parameterValues)
        {
            var dataPoints = new List<DataPoint>();
            var paramList = parameters.Split(",");
            foreach(var p in paramList)
            {
                var boundary = parameterValues.IndexOf(p);
                if (boundary < 0)
                    return dataPoints;
                if (boundary == 0)
                    continue;

                parameterValues = parameterValues.Substring(0, boundary-1) + ";"+ parameterValues.Substring(boundary);

            }

            parameterValues = parameterValues.Replace(" ", "=");

            var dictionary = new Dictionary<string, int>();
            var pairs = parameterValues.Split(";");
            foreach (var pair in pairs)
            {
                var keyValues = pair.Split("=");
                if (int.TryParse(keyValues[1], out var keyVal))
                {
                    dictionary.Add(keyValues[0], keyVal);
                }
            }
           
            dataPoints.Add(new DataPoint { D = dictionary });
            
            return dataPoints;

        }

        private List<DataPoint> CreateCustomData(ShapeDefinition definition, string parameterValues)
        {
            var dataPoints = new List<DataPoint>();
            var paramList = definition.Parameters.Split(",");
            foreach (var p in paramList)
            {
                var boundary = parameterValues.IndexOf(p);
                if (boundary < 0)
                    return dataPoints;
                if (boundary == 0)
                    continue;

                parameterValues = parameterValues.Substring(0, boundary - 1) + ";" + parameterValues.Substring(boundary);

            }

            parameterValues = parameterValues.Replace(" ", "=");

            var dictionary = new Dictionary<string, int>();
            var pairs = parameterValues.Split(";");
            foreach (var pair in pairs)
            {
                var keyValues = pair.Split("=");
                
                //Expecting a pair of values
                if (keyValues.Length < 2)
                    return dataPoints;

                if (int.TryParse(keyValues[1], out var keyVal))
                {
                    dictionary.Add(keyValues[0], keyVal);
                }
            }

            if (!dictionary.ContainsKey("width") || !dictionary.ContainsKey("height"))
            {
                return dataPoints;
            }

            if (definition.Name == ShapeConstants.PARALLELOGRAM)
            {
                var width = dictionary["width"];
                var height = dictionary["height"];
                var offset = width / 5;

                dataPoints.Add(new DataPoint { X = 0, Y = height });
                dataPoints.Add(new DataPoint { X = width, Y = height });
                dataPoints.Add(new DataPoint { X = width + offset, Y = 0 });
                dataPoints.Add(new DataPoint { X = offset, Y = 0 });
            }
            else if (definition.Name == ShapeConstants.ISOSCELES_TRIANGLE)
            {
                var width = dictionary["width"];
                var height = dictionary["height"];
                
                dataPoints.Add(new DataPoint { X = 0, Y = height });
                dataPoints.Add(new DataPoint { X = width, Y = height });
                dataPoints.Add(new DataPoint { X = width / 2, Y = 0 });
            }
            else if (definition.Name == ShapeConstants.SCALENE_TRIANGLE)
            {
                var width = dictionary["width"];
                var height = dictionary["height"];
                
                dataPoints.Add(new DataPoint { X = 0, Y = height });
                dataPoints.Add(new DataPoint { X = width, Y = height });
                dataPoints.Add(new DataPoint { X = width / 4, Y = 0 });
            }

            return dataPoints;

        }

        public ShapeData CreateShapeData(string shapePhrase)
        {
            var cleanPhrase = CleanDescription(shapePhrase);
            var definition = MapToDefinition(cleanPhrase);
            var shapeData = GenerateShapeData(cleanPhrase, definition);

            return shapeData;
        }
    }
}