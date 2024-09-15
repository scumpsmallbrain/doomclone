using Raylib_CSharp.Windowing;

namespace ScumpDoom;

/* SD : ScumpDoom */
public partial class SD
{

    public const string V_NUMBER = "v0.0.0";
    public const string V_TITLE = $"ScumpDoom -- {V_NUMBER}";

    public static void Main(string[] args)
    {
        V.Init();

        DoomLoop();
    }

    static void DoomLoop() {
        while ( !Window.ShouldClose() )
        {
            V.Draw();
        }
        Window.Close();
    }

}
