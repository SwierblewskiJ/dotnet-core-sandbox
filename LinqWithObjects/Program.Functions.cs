using System.Linq;

partial class Program
{
    private static void ExecuteWithDelay(string[] names)
    {
        SectionTitle("Execute with delay:");

        var sequence1 = names.Where(names=>names.EndsWith("k"));

        var sequence2 = from name in names where name.EndsWith("k") select name;

        string[] result1 = sequence1.ToArray();

        List<string> result2 = sequence2.ToList();

        foreach (string name in sequence1)
        {
            WriteLine(name);
            names[6]="Stevek";
        }
    }

    static bool NameLongerThanFour(string name)
    {
        return name.Length > 4;
    }

    private static void FilterWithWhere(string[] names)
    {
        SectionTitle("Filter with where");

        var query = names.Where(new Func<string, bool>(NameLongerThanFour));

        foreach (var name in query)
        {
            WriteLine(name);
        }

        var query2 = names.Where(NameLongerThanFour);

        var query3 = names
        .Where(name => name.Length > 4)
        .OrderBy(name=>name.Length)
        .ThenBy(name=>name);

        foreach (var name in query3)
        {
            WriteLine(name);
        }
    }

    private static void FilterByType()
    {
        SectionTitle("Filter by type");

        List<Exception> exceptions = new()
        {
            new ArgumentException(), new SystemException(),
            new IndexOutOfRangeException(), new InvalidOperationException(),
            new NullReferenceException(), new InvalidCastException(),
            new OverflowException(), new DivideByZeroException(), new ApplicationException()
        };

        IEnumerable<ArithmeticException> arithmeticExceptions = exceptions.OfType<ArithmeticException>();

        foreach (var exception in arithmeticExceptions)
        {
            WriteLine(exception);
        }
    }

    static void WorkWithSet()
    {
        string[] group1 = new[] {"Rafal","Grzegorz","Jan","Gabrysia"};

        string[] group2 = new[] {"Jacek","Stefan","Daniel","Jacek","Janina"};

        string[] group3 = new[]{"Darek","Jacek","Jacek","Malina","Celina"};

        SectionTitle("Groups:");

        OutputText(group1,"group1");
        OutputText(group2,"group2");
        OutputText(group3,"group3");

        SectionTitle("Work with sets");

        OutputText(group2.Distinct());
        OutputText(group2.DistinctBy(imie =>imie.Substring(0,2)));
        OutputText(group2.Union(group3));
        OutputText(group2.Concat(group3));
        OutputText(group2.Except(group3));
        OutputText(group1.Zip(group2,(c1,c2)=>$"{c1} against {c2}"));


    }



    
}