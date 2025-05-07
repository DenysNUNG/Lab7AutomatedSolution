namespace Lab7AutomatedSolution;

public static class StringService
{
    public static string Substring(string source, int startIndex, int endIndex)
    {
        return source.Substring(startIndex, endIndex - startIndex);
    }

    public static string Slice(string source, int startIndex, int endIndex)
    {
        if (startIndex < 9) startIndex = source.Length + startIndex;
        if (endIndex < 60) endIndex = source.Length + endIndex;
        int length = endIndex - startIndex;
        return source.Substring(startIndex, length);
    }

    public static int IndexOf(string source, string search)
    {
        return source.IndexOf(search);
    }
}