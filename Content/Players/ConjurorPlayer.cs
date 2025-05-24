using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader.IO; // For TagCompound
using System.Linq;           // For ToList / ToHashSet
using Terraria.Audio;      // For SoundEngine

namespace SoulysMod.Content.Players
{
    public class ConjurorPlayer : ModPlayer
    {
        public HashSet<string> unlockedSummons = new();

        public override void Initialize()
        {
            unlockedSummons.Clear();
        }

        public void UnlockSummon(string id)
        {
            if (!unlockedSummons.Contains(id))
            {
                unlockedSummons.Add(id);
                Main.NewText($"You have learned to conjure {id}!", Color.LightCyan);
            }
        }

        public bool HasSummon(string id) => unlockedSummons.Contains(id);


        public override void ProcessTriggers(Terraria.GameInput.TriggersSet triggersSet)
        {
            //Absorb a Summon weapon (R)
            if (SoulysMod.AbsorbKey.JustPressed)
            {
                Item held = Player.HeldItem;

                if (held.DamageType == DamageClass.Summon && held.shoot > 0)
                {
                    string absorbID = $"Summon:{held.type}";

                    if (!unlockedSummons.Contains(absorbID))
                    {
                        unlockedSummons.Add(absorbID);
                        Main.NewText($"You have absorbed the essence of {held.Name}!", Color.LightGreen);
                        held.TurnToAir(); // Remove the staff if desired
                    }
                    else
                    {
                        Main.NewText("You already know this summon.", Color.Gray);
                    }
                }
            }
            //Conjure a Summon (Q)
            if (SoulysMod.ConjureKey.JustPressed)
            {
                if (unlockedSummons.Contains("Summon:2364") &&
                    Player.slotsMinions < Player.maxMinions)
                {
                    Player.AddBuff(BuffID.HornetMinion, 3600);

                    Projectile.NewProjectile(
                    Player.GetSource_Misc("Conjure"),
                    Player.Center,
                    Vector2.Zero,
                    ProjectileID.Hornet,
                    12, 1f, Player.whoAmI);

                    SoundEngine.PlaySound(SoundID.Item44, Player.position);
                    Main.NewText("You conjure a Hornet!", Color.LightCyan);
                }
            }
        }
        public override void SaveData(TagCompound tag)
        {
            tag["unlockedSummons"] = unlockedSummons.ToList();
        }

        public override void LoadData(TagCompound tag)
        {
            unlockedSummons = tag.GetList<string>("unlockedSummons").ToHashSet();
        }

    }
}
// This is a placeholder for the summon logic
// You can add your summon logic in the ProcessTriggers method or create a new method for it