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

        [TestCase("§6", typeof(IColorPart))]
        [TestCase("§c", typeof(IColorPart))]
        [TestCase("Uptime", typeof(ITextPart))]
        [TestCase("\n", typeof(INewLineResetPart))]
        [TestCase("\r", typeof(INewLineResetPart))]
        [TestCase("\r\n", typeof(INewLineResetPart))]
        public void CanConvertPart(string part, Type type)
        {
            // Arrange.
            var parser = new Parser.Parser();

            // Act.
            var parsedPart = parser.ConvertPart(part);

            // Assert.
            Assert.That(parsedPart, Is.AssignableTo(type));
        }

        [TestCase("§0", "Black")]
        [TestCase("§1", "DarkBlue")]
        [TestCase("§2", "DarkGreen")]
        [TestCase("§3", "DarkCyan")]
        [TestCase("§4", "DarkRed")]
        [TestCase("§5", "DarkMagenta")]
        [TestCase("§6", "Brown")]
        [TestCase("§7", "Gray")]
        [TestCase("§8", "DarkGray")]
        [TestCase("§9", "Blue")]
        [TestCase("§a", "Green")]
        [TestCase("§b", "Cyan")]
        [TestCase("§c", "Red")]
        [TestCase("§d", "Magenta")]
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

        [Test]
        public void CanConvertString()
        {
            // Arrange.
            var parser = new Parser.Parser();
            /*
             * §6Uptime:§c 58 minutes 14 seconds
             * §6Current TPS = §a20
             */
            var expected = new IPart[]
            {
                new ColorPart(Colors.Brown),
                new TextPart("Uptime:"),
                new ColorPart(Colors.Red),
                new TextPart(" 58 minutes 14 seconds"),
                new NewLineResetPart(),
                new ColorPart(Colors.Brown),
                new TextPart("Current TPS = "),
                new ColorPart(Colors.Green),
                new TextPart("20")
            };

            // Act.
            var actual = parser.ConvertParts(parser.Split(_sample2));

            // Assert.
            Assert.That(actual, Is.EqualTo(expected));
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