using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace SoulysMod
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class SoulysMod : Mod
	{
		public static ModKeybind ConjureKey;
		public static ModKeybind AbsorbKey;
		public static ModKeybind CycleSummonKey;

		public override void Load()
		{
			ConjureKey = KeybindLoader.RegisterKeybind(this, "Conjure Spirit", "Q");
			AbsorbKey = KeybindLoader.RegisterKeybind(this, "Absorb Summon Weapon", "R");
			CycleSummonKey = KeybindLoader.RegisterKeybind(this, "Cycle Summon", "E");
		}

	}
}
