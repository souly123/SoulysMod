using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader.IO;
using System.Linq;
using Terraria.Audio;
using SoulysMod.Content.Systems;

namespace SoulysMod.Content.Players
{
    public class ConjurorPlayer : ModPlayer
    {
        public Dictionary<int, SummonInfo> unlockedSummons = new();
        public int selectedSummonID = -1;

        public override void Initialize()
        {
            unlockedSummons.Clear();
            selectedSummonID = -1;
        }

        public void UnlockSummon(Item item)
        {
            // Check if it's a sentry by creating a dummy projectile
            int tempProj = Projectile.NewProjectile(
                Player.GetSource_Misc("CheckAbsorb"),
                Player.Center,
                Vector2.Zero,
                item.shoot,
                0, 0, Player.whoAmI);

            Projectile proj = Main.projectile[tempProj];
            if (proj.sentry)
            {
                Main.NewText($"{item.Name} is a sentry and cannot be absorbed.", Color.Red);
                proj.active = false;
                return;
            }

            proj.active = false;

            if (!unlockedSummons.ContainsKey(item.type))
            {
                unlockedSummons[item.type] = new SummonInfo(item.type, item.shoot, item.buffType);
                selectedSummonID = item.type;
                Main.NewText($"You have absorbed the essence of {item.Name}!", Color.LightGreen);
                item.TurnToAir();
            }
            else
            {
                Main.NewText("You already know this summon.", Color.Gray);
            }
        }

        public bool HasSummon(int itemID) => unlockedSummons.ContainsKey(itemID);

        public override void ProcessTriggers(Terraria.GameInput.TriggersSet triggersSet)
        {
            // Absorb Key (R)
            if (SoulysMod.AbsorbKey.JustPressed)
            {
                Item held = Player.HeldItem;
                if (held.DamageType == DamageClass.Summon && held.shoot > 0)
                {
                    UnlockSummon(held);
                }
            }

            // Conjure Key (Q)
            if (SoulysMod.ConjureKey.JustPressed &&
                selectedSummonID != -1 &&
                unlockedSummons.TryGetValue(selectedSummonID, out SummonInfo info))
            {
                // Check for sentries using a dummy
                int dummy = Projectile.NewProjectile(
                    Player.GetSource_Misc("CheckConjure"),
                    Player.Center,
                    Vector2.Zero,
                    info.ProjectileID,
                    0, 0, Player.whoAmI);

                Projectile proj = Main.projectile[dummy];
                bool isSentry = proj.sentry;
                proj.active = false;

                if (isSentry)
                {
                    Main.NewText("Sentry-type summons cannot be conjured.", Color.Red);
                    return;
                }

                if (Player.slotsMinions < Player.maxMinions)
                {
                    Player.AddBuff(info.BuffID, 3600);
                    Projectile.NewProjectile(
                        Player.GetSource_Misc("Conjure"),
                        Player.Center,
                        Vector2.Zero,
                        info.ProjectileID,
                        12, 1f, Player.whoAmI);

                    SoundEngine.PlaySound(SoundID.Item44, Player.position);
                    Main.NewText($"You conjure a {Lang.GetItemNameValue(selectedSummonID)}!", Color.LightCyan);
                }
                else
                {
                    Main.NewText("You’ve reached your summon limit!", Color.OrangeRed);
                }
            }

            // Cycle Summon Key (E)
            if (SoulysMod.CycleSummonKey.JustPressed && unlockedSummons.Count > 0)
            {
                var keys = unlockedSummons.Keys.ToList();
                keys.Sort();

                int currentIndex = selectedSummonID == -1 ? -1 : keys.IndexOf(selectedSummonID);
                int nextIndex = (currentIndex + 1) % keys.Count;

                selectedSummonID = keys[nextIndex];

                Main.NewText($"Selected: {Lang.GetItemNameValue(selectedSummonID)}", Color.Yellow);
            }
        }

        public override void SaveData(TagCompound tag)
        {
            tag["unlockedSummons"] = unlockedSummons.Values
                .Select(s => $"{s.ItemID},{s.ProjectileID},{s.BuffID}")
                .ToList();
            tag["selectedSummonID"] = selectedSummonID;
        }

        public override void LoadData(TagCompound tag)
        {
            unlockedSummons.Clear();
            foreach (var str in tag.GetList<string>("unlockedSummons"))
            {
                var parts = str.Split(',');
                int itemID = int.Parse(parts[0]);
                int projID = int.Parse(parts[1]);
                int buffID = int.Parse(parts[2]);
                unlockedSummons[itemID] = new SummonInfo(itemID, projID, buffID);
            }

            if (tag.ContainsKey("selectedSummonID"))
                selectedSummonID = tag.GetInt("selectedSummonID");
        }
    }
}
