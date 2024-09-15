/*==== Wad.cs        < coded with love by scump smallbrain />
    code for reading id's WAD format. [[https://doomwiki.org/wiki/WAD]]
============================================================== */
using System.Text;

namespace ScumpDoom;

/* WAD : Where's All the Data?! */
public static class WAD
{
    // TODO: support PWADs
    //TODO: make this all an instance class
    static string IWADPath = "wad/DOOM.WAD";
    static Header     Head;
    static Directory  Dir;
    static WADReader Reader = new(IWADPath);

    public static void Init()
    {
        /* read header */
        byte[] head = Reader.ReadLump(0x0, 0xC);
        Head = new( Str(head, 0x0, 4), Int(head, 0x4), Int(head, 0x8) );
        /* read directory */
        Dir = new();
        
    }

    /* points to directory :3 */
    struct Header( string wad_type, int numlumps, int directory_offset )
    {
        public readonly string WadType = wad_type;
        public readonly Int32 NumLumps = numlumps;
        public readonly Int32 DirectoryOffset = directory_offset;
    }

    /* points to alllll da lumps in da file */
    struct Directory
    {
        readonly Entry[] list;
        public Directory()
        {
            byte[] buf = Reader.ReadLump(Head.DirectoryOffset, Head.NumLumps * 16);

            /* load lumps */
            list = new Entry[Head.NumLumps];
            int lump_counter = 0;
            for (int i = 0; i < Head.NumLumps * 16; i += 16) {
                string lump_name = Str(buf, i+0x8).TrimEnd('\0');
                list[lump_counter] = new Entry( Int(buf, i+0x0), Int(buf, i+0x4), lump_name );
                lump_counter++;
            }
            Console.WriteLine($"Loaded {lump_counter} lumps");
        }

        /* points to a Luuuump */
        public struct Entry(int offset, int size, string lump_name)
        {
            public readonly Int32 Offset = offset;
            public readonly Int32 Size = size;
            public readonly String LumpName = lump_name;
        }
        // TODO: getter logic, return abstract class Lump from which all lump types inherit
    }

    /* read int from bytes (endian-safe, i hope!!!) */
    public static int Int( byte[] arr, int offset )
    {   
        byte[] buf = arr[offset..(offset+4)];
        
        if ( !BitConverter.IsLittleEndian )
            Array.Reverse(buf);
        
        return BitConverter.ToInt32(buf, 0);
    }

    /* read ASCII string from bytes */
    public static string Str( byte[] arr, int offset, int len = 8 )
    {
        StringBuilder str = new(len);
        foreach ( byte b in arr[offset..(offset+len)] ) {
            str.Append((char)b);
        }
        return str.ToString();
    }
}


public class WADReader
{
    readonly FileStream wadFile;

    /* open file */
    public WADReader(string wad_path)
    {
        Console.WriteLine($"Loading WAD: ${wad_path}");
        wadFile = File.Open(wad_path, FileMode.Open, FileAccess.Read);
        if ( wadFile == null )
            throw new FileNotFoundException();
    }

    /* gives lump as a byte array given an offset and size */
    public byte[] ReadLump( int offset, int size )
    {
        byte[] buf = new byte[size];
        wadFile.Seek(offset, SeekOrigin.Begin);
        wadFile.Read(buf, 0, size);

        return buf;
    }

}