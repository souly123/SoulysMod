using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SoulysMod.Content.Items.Weapons
{
    public class SoulyBlaster : ModItem
    {
        public override void SetDefaults()
        {
            // Item properties
            Item.damage = 25;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 40;
            Item.height = 20;
            Item.useTime = 20; // How fast the weapon fires
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true; // This weapon doesn't do melee damage
            Item.knockBack = 4;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item11; // Gun sound
            Item.autoReuse = true; // Can hold down to keep firing
            Item.shoot = ModContent.ProjectileType<Projectiles.IronBolt>();
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet; // Uses bullets as ammo
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Override the projectile type to always use our custom projectile
            type = ModContent.ProjectileType<Projectiles.IronBolt>();

            // Add some spread for realism
            Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(5));

            Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            return false; // Return false to prevent the default projectile from spawning
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DirtBlock, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
        public override string Texture => "SoulysMod/Assets/Textures/Items/Weapons/Range/IronBlaster";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Souly Blaster");
            // Tooltip.SetDefault("A blaster made of souly metal.");
            // The following line is optional and will automatically assign the item to the "Weapons" category in the inventory.
            // ItemID.Sets.ItemNoGravity[Item.type] = true;
        }
    }
}
