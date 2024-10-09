/*==== AutoMap.cs        < coded with love by scump smallbrain />
    draw top-down map of a level
============================================================== */

using System.Numerics;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Rendering;

namespace ScumpDoom;

public static class AutoMap
{
	public static Map? CurrentMap;
	public static Vector2 ViewOffset = new Vector2(1024, -3488);
	public static void Init()
	{
		CurrentMap = MapLoader.Load(
			WAD.Dir.GetLump(7)
		);
	}
	public static void Update()
	{
		ViewOffset += new Vector2(
			Convert.ToSingle(Input.IsKeyDown(KeyboardKey.L))
					- Convert.ToSingle(Input.IsKeyDown(KeyboardKey.J)),
			Convert.ToSingle(Input.IsKeyDown(KeyboardKey.I))
					- Convert.ToSingle(Input.IsKeyDown(KeyboardKey.K)));
	}
	public static void Draw()
	{
		if (CurrentMap == null) return;

		foreach (LineDef ld in CurrentMap.LineDefs) {
			Vert start = CurrentMap.Verts[ld.Start];
			Vert end = CurrentMap.Verts[ld.End];
			Graphics.DrawLine(	start.X - (int)ViewOffset.X,
								-start.Y + (int)ViewOffset.Y,
								end.X - (int)ViewOffset.X,
								-end.Y + (int)ViewOffset.Y, Color.Red	);
		}
	}
}