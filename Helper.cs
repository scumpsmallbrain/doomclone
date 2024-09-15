namespace ScumpDoom;

public partial class SD {
    public static void WriteColoredLine(ConsoleColor bg, ConsoleColor color, string text)
    {
        Console.BackgroundColor = bg;
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }
}