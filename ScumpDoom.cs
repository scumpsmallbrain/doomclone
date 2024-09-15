/*==== Program.cs        < coded with love by scump smallbrain />
    entry-point of the program. hosts the init process & main loop
============================================================== */
using Raylib_CSharp.Windowing;

namespace ScumpDoom;

/* SD : ScumpDoom */
public static partial class SD
{

    public const string V_NUMBER = "v0.0.0";
    public const string V_TITLE = $"ScumpDoom -- {V_NUMBER}";

    public static void Main(string[] args)
    {
        WAD.Init();
        V.Init();

        /* all systems nominal*/

        /* thunderbirds are go */
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
