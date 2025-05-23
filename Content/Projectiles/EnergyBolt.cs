using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace SoulysMod.Content.Projectiles
{
    public class EnergyBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = 1; // Arrow-like movement
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1; // How many enemies it can hit
            Projectile.timeLeft = 600; // How long it lasts (10 seconds at 60 FPS)
            Projectile.alpha = 255; // Transparency (255 = invisible, 0 = opaque)
            Projectile.light = 0.5f; // Light emission
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1; // Makes projectile move faster/smoother

            AIType = ProjectileID.Bullet; // Copies AI from vanilla bullet
        }

        public override void AI()
        {
            // Custom AI behavior
            Projectile.alpha -= 15;
            if (Projectile.alpha < 0)
                Projectile.alpha = 0;

            // Add some visual effects
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height,
                    DustID.Electric, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // What happens when hitting a tile
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            // Correct way to play sounds in current tModLoader
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            return true; // true = destroy projectile, false = don't destroy
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // What happens when hitting an NPC
            target.AddBuff(BuffID.Electrified, 120); // Apply electrified debuff for 2 seconds
        }
        
        public override string Texture => "SoulysMod/Assets/Textures/Items/Projectiles/EnergyBolt"; // Path to the texture
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Energy Bolt");
            // Tooltip.SetDefault("A bolt of energy.");
            // The following line is optional and will automatically assign the item to the "Weapons" category in the inventory.
            // ItemID.Sets.ItemNoGravity[Item.type] = true;
        }
    }
}
