using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ModLoader.Data;
using SUCC;

namespace ModLoader
{
    public class Mod
    {
        public Mod(string modPath)
        {
            ModPath = new DirectoryInfo(modPath);
            _manifest = new DataFile(Path.Combine(modPath, "manifest.succ"));
            Name = _manifest.Get<string>(nameof(ModManifest.Name));
        }
        public string Name { get; set; }
        public DirectoryInfo ModPath { get; }
        
        public string Description { get; set; }

        private DataFile _manifest { get; set; }


        public ModManifest Manifest => _manifest.GetAsObject<ModManifest>();

        public void SetPriority(int prio)
        {
            _manifest.Set(nameof(ModManifest.Priority),prio);
        }
    }
}
