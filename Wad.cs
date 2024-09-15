/*==== Wad.cs        < coded with love by scump smallbrain />
    code for reading id's WAD format. [[https://doomwiki.org/wiki/WAD]]
============================================================== */
namespace ScumpDoom;

public partial class SD
{
    static void WADInit()
    {

    }
}


public class WADReader
{
    readonly FileStream? wadFile = null;

    public WADReader(string wad_path)
    {
        wadFile = File.Open(wad_path, FileMode.Open, FileAccess.Read);
        if ( wadFile == null )
            Environment.Exit(66);
        
    }

}