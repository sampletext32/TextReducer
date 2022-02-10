using System.Text;
using TextReducer;

string[] keywords =
{
    "должны", "должен", "должно", "необходимо", "необходимы", "обязательно", "обязательны", "обязательный",
    "обязательные", "использовать", "надлежит"
};

Console.Write("Enter input file path: ");
var inputPath = Console.ReadLine().Replace("\\", "/");
if (!File.Exists(inputPath))
{
    Console.WriteLine("File not found");
    Console.WriteLine("press any key to exit");
    Console.ReadKey();
    return;
}

var inputText = File.ReadAllText(inputPath);

var outputBuilder = new StringBuilder();

var paragraphs = inputText.Split("\n");

foreach (var paragraph in paragraphs)
{
    if (paragraph.StartsWith("\t"))
    {
        var hasKeyword = paragraph
            .ReplaceWithPredicate(' ', c => !char.IsLetterOrDigit(c))
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(w => w.ToLower()).Any(w => keywords.Contains(w));

        if (hasKeyword)
        {
            outputBuilder.AppendLine(paragraph);
        }
    }
}

Console.WriteLine(outputBuilder.ToString());