using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModLoader
{
    public static class ModRepository
    {
        private static DirectoryInfo repositoryDirectory;

        public static void Init(string repositoryPath)
        {
            repositoryDirectory = new DirectoryInfo(repositoryPath);
        }

        public static IEnumerable<Mod> GetEnabledMods()
        {
            return GetInstalledMods().Where(ModFolder.IsModEnabled);
        }

        public static IEnumerable<Mod> GetInstalledMods()
        {
            var mods = new List<Mod>();
            var manifests = repositoryDirectory.GetFiles("manifest.succ", SearchOption.AllDirectories);
            return manifests.Where(e => e.DirectoryName != null).Select(e => new Mod(e.DirectoryName));
        }
    }
}
