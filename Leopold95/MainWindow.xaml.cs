using System.IO;
using System.Reflection;
using System.Windows;

namespace Leopold95
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var parser = new Parser.Parser();
            var sample = ReadResource("Leopold95.Resources.sample1.txt");
            var paragraph = parser.Convert(sample);
            FlowDocumentConsoleOutput.Blocks.Add(paragraph);
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