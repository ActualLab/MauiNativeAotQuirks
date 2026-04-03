namespace MauiNativeAotQuirks;

public static class Log
{
    public static string? Path { get; set; }

    public static void Append(string message)
    {
        if (Path == null) return;
        try {
            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(Path)!);
            var line = $"[{DateTime.Now:HH:mm:ss.fff}] {message}{Environment.NewLine}";
            File.AppendAllText(Path, line);
        }
        catch { }
    }
}
