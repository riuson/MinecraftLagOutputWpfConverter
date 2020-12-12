using System;
using System.IO;
using System.Reflection;
using System.Windows.Media;
using Leopold95.Parser;
using NUnit.Framework;

namespace Leopold95.Tests
{
    public class ParserTests
    {
        private string _sample1;
        private string _sample2;

        [SetUp]
        public void Setup()
        {
            _sample1 = ReadResource("Leopold95.Tests.Resources.sample1.txt");

            // sample2:
            // §6Uptime:§c 58 minutes 14 seconds\n
            // §6Current TPS = §a20
            _sample2 = ReadResource("Leopold95.Tests.Resources.sample2.txt");
        }

        [Test]
        public void CanSplit()
        {
            // Arrange.
            var parser = new Parser.Parser();
            var expected = new[]
                {"§6", "Uptime:", "§c", " 58 minutes 14 seconds", "\r\n", "§6", "Current TPS = ", "§a", "20"};

            // Act.
            var actual = parser.Split(_sample2);

            // Assert.
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase("§6", ExpectedResult = typeof(IColorPart))]
        [TestCase("§c", ExpectedResult = typeof(IColorPart))]
        [TestCase("Uptime", ExpectedResult = typeof(ITextPart))]
        [TestCase("\n", ExpectedResult = typeof(INewLineResetPart))]
        [TestCase("\r", ExpectedResult = typeof(INewLineResetPart))]
        [TestCase("\r\n", ExpectedResult = typeof(INewLineResetPart))]
        public Type CanConvertPart(string part)
        {
            // Arrange.
            var parser = new Parser.Parser();

            // Act.
            var parsedPart = parser.ConvertPart(part);

            // Assert.
            return parsedPart.GetType();
        }

        [TestCase("§0", "Black")]
        [TestCase("§1", "Blue")]
        [TestCase("§2", "Green")]
        [TestCase("§3", "Cyan")]
        [TestCase("§4", "Red")]
        [TestCase("§5", "Magenta")]
        [TestCase("§6", "Brown")]
        [TestCase("§7", "LightGray")]
        [TestCase("§8", "DarkGray")]
        [TestCase("§9", "LightBlue")]
        [TestCase("§a", "LightGreen")]
        [TestCase("§b", "LightCyan")]
        [TestCase("§c", "LightRed")]
        [TestCase("§d", "LightMagenta")]
        [TestCase("§e", "Yellow")]
        [TestCase("§f", "White")]
        public void CanConvertColor(string part, string colorName)
        {
            // Arrange.
            var parser = new Parser.Parser();
            var color = Colors.Black;
            var colors = typeof(Colors);

            foreach (var prop in colors.GetProperties())
                if (prop.Name == colorName)
                {
                    color = (Color) prop.GetValue(null, null);
                    break;
                }

            // Act.
            var parsedPart = parser.ConvertPart(part);

            // Assert.
            Assert.That(parsedPart, Is.AssignableTo<IColorPart>());
            var colorPart = parsedPart as IColorPart;
            Assert.That(colorPart.Color, Is.EqualTo(color));
        }

        [TestCase("§l", true)]
        [TestCase("§n", false)]
        public void CanConvertBold(string part, bool isBold)
        {
            // Arrange.
            var parser = new Parser.Parser();

            // Act.
            var parsedPart = parser.ConvertPart(part);

            // Assert.
            Assert.That(parsedPart is IBoldPart, Is.EqualTo(isBold));
        }

        [TestCase("§l", false)]
        [TestCase("§n", true)]
        public void CanConvertUnderline(string part, bool isUnderline)
        {
            // Arrange.
            var parser = new Parser.Parser();

            // Act.
            var parsedPart = parser.ConvertPart(part);

            // Assert.
            Assert.That(parsedPart is IUnderlinePart, Is.EqualTo(isUnderline));
        }

        private string ReadResource(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}