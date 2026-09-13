using System.Globalization;

partial class Program
{
    private static void ConfigureConsole(string culture = "pl-PL", bool usePCCulture = false)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        if (!usePCCulture)
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(culture);
        }
        Console.WriteLine($"CurrentCulture: {CultureInfo.CurrentCulture.DisplayName}");
    }

    private static void OutputInColor(string text, ConsoleColor color)
    {
        ConsoleColor previousColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ForegroundColor = previousColor;
    }

    private static void SectionTitle(string title)
    {
        OutputInColor($"*** {title} ***", ConsoleColor.DarkYellow);
    }

    private static void Error(string message)
    {
        OutputInColor($"Error > {message}",ConsoleColor.Red);
    }

    private static void Info(string message)
    {
        OutputInColor($"Info > {message}",ConsoleColor.Cyan);
    }
}