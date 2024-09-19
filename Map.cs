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
		(	UInt16 start, UInt16 end /*, LineDef.Flag flags,
			LineDef.Special specials, UInt16 targetSector,
			UInt16 frontSideDef, UInt16 backSideDef */ 		)
{
	public UInt16 Start = start;
	public UInt16 End = end;
	// public Flag Flags = flags;
	// public Special Specials = specials;
	// public UInt16 TargetSector = targetSector;
	// public UInt16 FrontSideDef = frontSideDef;
	// public UInt16 BackSideDef = backSideDef;

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

public static class MapLoader
{
	public static Map Load( Lump mapMarker )
	{
		Vert[] verts;
		if (WAD.Dir.SearchNextLump("VERTEXES", mapMarker.Index, out Lump vLump))
		{
			int vertCount = vLump.Buffer.Length / 4;
			verts = new Vert[vertCount];
			for ( int i = 0; i < vertCount; i++ ) {
				int ofs = i * 4;
				verts[i] = new Vert(
					WAD.I16(vLump.Buffer, ofs),
					WAD.I16(vLump.Buffer, ofs + 0x02)
				);
				Console.WriteLine($"V{i}:{{{verts[i].X},{verts[i].Y}}}");
			}
		}
		else throw new Exception($"VERTEXES lump not found in map {mapMarker.Name}");
		
		LineDef[] lineDefs;
		if (WAD.Dir.SearchNextLump("LINEDEFS", mapMarker.Index, out Lump lLump))
		{
			int ldCount = lLump.Buffer.Length / 14;
			lineDefs = new LineDef[ldCount];
			for ( int i = 0; i < ldCount; i++ ) {
				int ofs = i * 14;
				lineDefs[i] = new LineDef(
					WAD.U16(lLump.Buffer, ofs),
					WAD.U16(lLump.Buffer, ofs + 0x02)
				);
				Console.WriteLine($"L{i}:{{{lineDefs[i].Start},{lineDefs[i].End}}}");
			}
		}
		else throw new Exception($"LINEDEFS lump not found in map {mapMarker.Name}");

		return new Map(verts, lineDefs);
	}
}