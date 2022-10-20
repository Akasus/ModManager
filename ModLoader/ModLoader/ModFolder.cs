using System.Runtime.CompilerServices;

namespace ModLoader;

public static class ModFolder
{
    private static DirectoryInfo gameDataDirectory;

    public static void Init(string gameDataFolder)
    {
        gameDataDirectory = new DirectoryInfo(gameDataFolder);
    }

    public static bool IsModEnabled(Mod mod)
    {
       return gameDataDirectory.EnumerateDirectories(mod.ModPath.Name)
            .Any(e => e.LinkTarget != null && e.LinkTarget == mod.ModPath.FullName);
    }

    public static bool EnableMod(Mod mod)
    {
        if(IsModEnabled(mod))
            return false;
        gameDataDirectory.CreateSubdirectory(mod.ModPath.Name).CreateAsSymbolicLink(mod.ModPath.FullName);
        return true;
    }

    public static DirectoryInfo GetGameDataPath(Mod mod)
    {
        return gameDataDirectory.EnumerateDirectories(mod.ModPath.Name)
            .First(e => e.LinkTarget != null && e.LinkTarget == mod.ModPath.FullName);
    }

    public static bool DisableMod(Mod mod)
    {
        if(!IsModEnabled(mod)) return false;
        GetGameDataPath(mod).Delete();
        return true;
    }


}