using static System.Environment;

namespace EntityModels;

public class NorthwindProtocol
{
    public static void WriteLine(string text)
    {
        string path = Path.Combine(GetFolderPath(SpecialFolder.DesktopDirectory),"northwindlog.txt");

        StreamWriter textFile = File.AppendText(path);
        textFile.WriteLine(text);
        textFile.Close();
    }
}