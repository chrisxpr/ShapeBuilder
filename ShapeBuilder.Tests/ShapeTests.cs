using NUnit.Framework;
using ShapeBuilder.Helper;
using ShapeBuilder.Types;
using ShapeBuilder.Types.Behaviours;

namespace ShapeBuilder.Tests
{
    [TestFixture]
    public class Tests
    {
        private IShapeHelper _shapeHelper;

        [SetUp]
        public void Setup()
        {
            _shapeHelper = new ShapeHelper();
        }

        [Test]
        public void TestSquare()
        {
            var testPhrase = "Draw a square with a side length of 200";
            var cleanPhrase = _shapeHelper.CleanDescription(testPhrase);
            Assert.IsTrue(cleanPhrase.Contains("square side 200"));
            var definition = _shapeHelper.MapToDefinition(cleanPhrase);
            Assert.IsTrue(definition.Name == ShapeConstants.SQUARE);
            Assert.IsTrue(definition.Type == ShapeTypes.RECTANGLE);

            var shapeData = _shapeHelper.GenerateShapeData(cleanPhrase, definition);

            Assert.IsNotNull(shapeData);
            Assert.IsNotNull(shapeData.DataPoints);
            Assert.IsTrue(shapeData.DataPoints?.Count == definition.Vertices);

        }

        [Test]
        public void TestParallelogram()
        {

            var testPhrase = "Draw a parallelogram with a width of 250 and a height of 400";

            var cleanPhrase = _shapeHelper.CleanDescription(testPhrase);
            Assert.IsTrue(cleanPhrase.Contains("parallelogram width 250 height 400"));

            var definition = _shapeHelper.MapToDefinition(cleanPhrase);
            Assert.IsTrue(definition.Name == ShapeConstants.PARALLELOGRAM);
            Assert.IsTrue(definition.Type == ShapeTypes.CUSTOM);

            var shapeData = _shapeHelper.GenerateShapeData(cleanPhrase, definition);

            Assert.IsNotNull(shapeData);
            Assert.IsNotNull(shapeData.DataPoints);
            Assert.IsTrue(shapeData.DataPoints?.Count == definition.Vertices);
        }

        [Test]
        public void TestRectangle()
        {
            var testPhrase = "Draw a rectangle with a width of 250 and a height of 400";
            var cleanPhrase = _shapeHelper.CleanDescription(testPhrase);

            Assert.IsTrue(cleanPhrase.Contains("rectangle width 250 height 400"));
            var definition = _shapeHelper.MapToDefinition(cleanPhrase);
            Assert.IsTrue(definition.Name == ShapeConstants.RECTANGLE);
            Assert.IsTrue(definition.Type == ShapeTypes.RECTANGLE);

            var shapeData = _shapeHelper.GenerateShapeData(cleanPhrase, definition);

            Assert.IsNotNull(shapeData);
            Assert.IsNotNull(shapeData.DataPoints);
            Assert.IsTrue(shapeData.DataPoints?.Count == definition.Vertices);
        }


        [Test]
        public void TestEquilateralTriangle()
        {
            var testPhrase = "Draw an equilateral triangle with a side length of 200";
            var cleanPhrase = _shapeHelper.CleanDescription(testPhrase);
            Assert.IsTrue(cleanPhrase.Contains("equilateral triangle side 200"));
            
            var definition = _shapeHelper.MapToDefinition(cleanPhrase);
            Assert.IsTrue(definition.Name == ShapeConstants.EQUILATERAL_TRIANGLE);
            Assert.IsTrue(definition.Type == ShapeTypes.POINTARRAY);

            var shapeData = _shapeHelper.GenerateShapeData(cleanPhrase, definition);

            Assert.IsNotNull(shapeData);
            Assert.IsNotNull(shapeData.DataPoints);
            Assert.IsTrue(shapeData.DataPoints?.Count == definition.Vertices);
        }

        [Test]
        public void TestPentagon()
        {
            var testPhrase = "Draw an pentagon with a side length of 200";
            var cleanPhrase = _shapeHelper.CleanDescription(testPhrase);
            Assert.IsTrue(cleanPhrase.Contains("pentagon side 200"));
            
            var definition = _shapeHelper.MapToDefinition(cleanPhrase);
            Assert.IsTrue(definition.Name == ShapeConstants.PENTAGON);
            Assert.IsTrue(definition.Type == ShapeTypes.POINTARRAY);

            var shapeData = _shapeHelper.GenerateShapeData(cleanPhrase, definition);

            Assert.IsNotNull(shapeData);
            Assert.IsNotNull(shapeData.DataPoints);
            Assert.IsTrue(shapeData.DataPoints?.Count == definition.Vertices);
        }

        [Test]
        public void TestHexagon()
        {
            var testPhrase = "Draw an hexagon with a side length of 200";
            var cleanPhrase = _shapeHelper.CleanDescription(testPhrase);

            Assert.IsTrue(cleanPhrase.Contains("hexagon side 200"));
            var definition = _shapeHelper.MapToDefinition(cleanPhrase);
            Assert.IsTrue(definition.Name == ShapeConstants.HEXAGON);
            Assert.IsTrue(definition.Type == ShapeTypes.POINTARRAY);

            var shapeData = _shapeHelper.GenerateShapeData(cleanPhrase, definition);

            Assert.IsNotNull(shapeData);
            Assert.IsNotNull(shapeData.DataPoints);
            Assert.IsTrue(shapeData.DataPoints?.Count == definition.Vertices);
        }

        [Test]
        public void TestHeptagon()
        {
            var testPhrase = "Draw an heptagon with a side length of 200";
            var cleanPhrase = _shapeHelper.CleanDescription(testPhrase);

            Assert.IsTrue(cleanPhrase.Contains("heptagon side 200"));

            var definition = _shapeHelper.MapToDefinition(cleanPhrase);
            Assert.IsTrue(definition.Name == ShapeConstants.HEPTAGON);
            Assert.IsTrue(definition.Type == ShapeTypes.POINTARRAY);

            var shapeData = _shapeHelper.GenerateShapeData(cleanPhrase, definition);

            Assert.IsNotNull(shapeData);
            Assert.IsNotNull(shapeData.DataPoints);
            Assert.IsTrue(shapeData.DataPoints?.Count == definition.Vertices);
        }

        [Test]
        public void TestOctagon()
        {
            var testPhrase = "Draw an octagon with a side length of 200";
            var cleanPhrase = _shapeHelper.CleanDescription(testPhrase);

            Assert.IsTrue(cleanPhrase.Contains("octagon side 200"));

            var definition = _shapeHelper.MapToDefinition(cleanPhrase);
            Assert.IsTrue(definition.Name == ShapeConstants.OCTAGON);
            Assert.IsTrue(definition.Type == ShapeTypes.POINTARRAY);

            var shapeData = _shapeHelper.GenerateShapeData(cleanPhrase, definition);

            Assert.IsNotNull(shapeData);
            Assert.IsNotNull(shapeData.DataPoints);
            Assert.IsTrue(shapeData.DataPoints?.Count == definition.Vertices);
        }

        [Test]
        public void TestCircle()
        {
            var testPhrase = "Draw a circle with a radius of 100";
            var cleanPhrase = _shapeHelper.CleanDescription(testPhrase);

            Assert.IsTrue(cleanPhrase.Contains("circle radius 100"));
            
            var definition = _shapeHelper.MapToDefinition(cleanPhrase);
            Assert.IsTrue(definition.Name == ShapeConstants.CIRCLE);
            Assert.IsTrue(definition.Type == ShapeTypes.CIRCLE);

            var shapeData = _shapeHelper.GenerateShapeData(cleanPhrase, definition);

            Assert.IsNotNull(shapeData);
            Assert.IsNotNull(shapeData.DataPoints);
            Assert.IsTrue(shapeData.DataPoints?.Count == definition.Vertices);
        }

        [Test]
        public void TestOval()
        {
            var testPhrase = "Draw an oval with a radius of 100";
            var cleanPhrase = _shapeHelper.CleanDescription(testPhrase);

            Assert.IsTrue(cleanPhrase.Contains("oval radius 100"));
            
            var definition = _shapeHelper.MapToDefinition(cleanPhrase);
            Assert.IsTrue(definition.Name == ShapeConstants.OVAL);
            Assert.IsTrue(definition.Type == ShapeTypes.ELLIPSE);

            var shapeData = _shapeHelper.GenerateShapeData(cleanPhrase, definition);

            Assert.IsNotNull(shapeData);
            Assert.IsNotNull(shapeData.DataPoints);
            Assert.IsTrue(shapeData.DataPoints?.Count == definition.Vertices);
        }

        [Test]
        public void TestIsocelesTriangle()
        {
            var testPhrase = "Draw an isosceles triangle with a height of 200 and a width of 100";
            var cleanPhrase = _shapeHelper.CleanDescription(testPhrase);
            Assert.IsTrue(cleanPhrase.Contains("isosceles triangle height 200 width 100"));
            var definition = _shapeHelper.MapToDefinition(cleanPhrase);
            Assert.IsTrue(definition.Name == ShapeConstants.ISOSCELES_TRIANGLE);
            Assert.IsTrue(definition.Type == ShapeTypes.CUSTOM);

            var shapeData = _shapeHelper.GenerateShapeData(cleanPhrase, definition);

            Assert.IsNotNull(shapeData);
            Assert.IsNotNull(shapeData.DataPoints);
            Assert.IsTrue(shapeData.DataPoints?.Count == definition.Vertices);
        }


        [Test]
        public void TestScaleneTriangle()
        {
            var testPhrase = "Draw a scalene triangle with a height of 200 and a width of 100";

            var cleanPhrase = _shapeHelper.CleanDescription(testPhrase);
            Assert.IsTrue(cleanPhrase.Contains("scalene triangle height 200 width 100"));

            var definition = _shapeHelper.MapToDefinition(cleanPhrase);
            Assert.IsTrue(definition.Name == ShapeConstants.SCALENE_TRIANGLE);
            Assert.IsTrue(definition.Type == ShapeTypes.CUSTOM);

            var shapeData = _shapeHelper.GenerateShapeData(cleanPhrase, definition);

            Assert.IsNotNull(shapeData);
            Assert.IsNotNull(shapeData.DataPoints);
            Assert.IsTrue(shapeData.DataPoints?.Count == definition.Vertices);
        }


    }
}