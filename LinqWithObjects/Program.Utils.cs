partial class Program
{
    private static void SectionTitle(string title)
    {
        ConsoleColor previousColor = ForegroundColor;
        ForegroundColor = ConsoleColor.DarkYellow;
        WriteLine($"*** title ***");
        ForegroundColor = previousColor;
    }

    static void OutputText(IEnumerable<string> group, string description = "")
    {
        if (!string.IsNullOrEmpty(description))
        {
            WriteLine(description);
        }
        Write(" ");
        WriteLine(string.Join(" ", group.ToArray() ));
        WriteLine("");
    }
    

}