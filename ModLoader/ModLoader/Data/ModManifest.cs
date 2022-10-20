using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SUCC;

namespace ModLoader.Data
{
    // Copied from LogicAPI.Dll!
    public class ModManifest
    {
        /// <summary>The mod's ID.</summary>
        [SaveThis]
        public string ID { get; protected set; }

        /// <summary>The mod's name.</summary>
        [SaveThis]
        public string Name { get; protected set; }

        /// <summary>The mod's author.</summary>
        [SaveThis]
        public string Author { get; protected set; }

        /// <summary>The mod's version.</summary>
        [SaveThis]
        public Version Version { get; protected set; }

        /// <summary>Whether or not this mod is a client-only mod.</summary>
        [SaveThis]
        public bool ClientOnly { get; protected set; }

        /// <summary>
        /// The mod's loading priority, between -100 and 100. Mods with a higher priority will be loaded first.
        /// </summary>
        [SaveThis]
        public int Priority { get; protected set; }

        /// <summary>The IDs of the mods that this mod depends on.</summary>
        [SaveThis]
        public string[] Dependencies { get; protected set; } = Array.Empty<string>();
    }
}
