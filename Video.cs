/*==== Video.cs        < coded with love by scump smallbrain />
    handles the game window, with code to resize the virtual
    screen to the size of the window
============================================================== */
using System.Numerics;
using Raylib_CSharp.Camera.Cam2D;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Textures;
using Raylib_CSharp.Transformations;
using Raylib_CSharp.Windowing;

namespace ScumpDoom;

/* V : Video */
public class V
{ 
    /* SCR : Screen : refers to the low-res virtual screen */
    const int SCR_WIDTH = 320;
    const int SCR_HEIGHT = 240;
    /* WIN : Window */
    const int WIN_WIDTH = SCR_WIDTH * 3;
    const int WIN_HEIGHT = SCR_HEIGHT * 3;
    const float virtualRatio = (float)WIN_WIDTH/(float)SCR_WIDTH;

    static Camera2D virtualCamera = new();
    static Camera2D screenCamera  = new();
    static RenderTexture2D renderTexture = new();

    static Rectangle renderTextureSourceRectangle = new();
    static Rectangle renderTextureDestRectangle = new();


    public static void Init()
    {
        SD.WriteColoredLine(ConsoleColor.Cyan, ConsoleColor.White, "V.Init: Working...");

        /* initialize window, virtual cameras, render texture */
        Window.Init( WIN_WIDTH, WIN_HEIGHT, SD.V_TITLE );
        // TODO: make window resizable w/ black bars
        virtualCamera.Zoom = 1;
        screenCamera.Zoom = 1;
        renderTexture = RenderTexture2D.Load(SCR_WIDTH, SCR_HEIGHT);
        /* i don't exactly understand this, but it works [[https://raw.githubusercontent.com/raysan5/raylib/refs/heads/master/examples/core/core_smooth_pixelperfect.c]] */
        renderTextureSourceRectangle = new( 0f, 0f, (float)renderTexture.Texture.Width, -(float)renderTexture.Texture.Height );
        renderTextureDestRectangle = new( -virtualRatio, -virtualRatio, WIN_WIDTH + (virtualRatio*2), WIN_HEIGHT + (virtualRatio*2) );
        
        SD.WriteColoredLine(ConsoleColor.Cyan, ConsoleColor.White, "V.Init: Done!");
    }

    public static void Draw()
    {
        /* render our low-res screen to the texture */
        Graphics.BeginTextureMode(renderTexture);
            Graphics.ClearBackground(Color.Black);
            Graphics.BeginMode2D(virtualCamera);
                //TODO: Rendering Code
                Graphics.DrawCircle(120, 200, 40, Color.White);
                Graphics.DrawCircle(200, 120, 30, Color.White);
            Graphics.EndMode2D();
        Graphics.EndTextureMode();

        /* draw the texture at window size */
        Graphics.BeginDrawing();
            Graphics.ClearBackground(Color.Red);
            Graphics.BeginMode2D(screenCamera);
                Graphics.DrawTexturePro(renderTexture.Texture, renderTextureSourceRectangle, renderTextureDestRectangle, Vector2.Zero, 0f, Color.White);
            Graphics.EndMode2D();
        Graphics.EndDrawing();
    }

}