/*==== Map.cs        < coded with love by scump smallbrain />
    data structure representing the 3d space of a level
============================================================== */

namespace ScumpDoom;

public struct Vert(Int16 x, Int16 y)
{
	public Int16 X = x;
	public Int16 Y = y;
}

public struct LineDef
		(	UInt16 start, UInt16 end, LineDef.Flag flags,
			LineDef.Special specials, UInt16 targetSector,
			UInt16 frontSideDef, UInt16 backSideDef 		)
{
	public UInt16 Start = start;
	public UInt16 End = end;
	public Flag Flags = flags;
	public Special Specials = specials;
	public UInt16 TargetSector = targetSector;
	public UInt16 FrontSideDef = frontSideDef;
	public UInt16 BackSideDef = backSideDef;

	[Flags]
	public enum Flag
	{
		Impassable = 1,
		BlockMonsters = 2,
		TwoSided = 4,
		UpperUnpegged = 8,
		LowerUnpegged = 16,
		Secret = 32,
		BlockSound = 64,
		NoAutomap = 128,
		AlwaysAutomap = 256
	}
	[Flags]
	public enum Special
	{
		None = 0
	}
}

public struct SideDef
{
	public UInt16 OffsetX;
	public UInt16 OffsetY;
	public String UpperTex;
	public String MiddleTex;
	public String LowerTex;
	public UInt16 Sector;
}

public class Map(Vert[] verts, LineDef[] lineDefs)
{
	public Vert[] Verts = verts;
	public LineDef[] LineDefs = lineDefs;
}