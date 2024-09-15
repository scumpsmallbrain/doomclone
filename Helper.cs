/*==== Helper.cs        < coded with love by scump smallbrain />
    static functions in the SD class for general use.
============================================================== */

namespace ScumpDoom;

public static partial class SD {
    public static void WriteColoredLine(ConsoleColor bg, ConsoleColor color, string text)
    {
        Console.BackgroundColor = bg;
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }
}