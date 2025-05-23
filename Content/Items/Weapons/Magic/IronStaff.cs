using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SoulysMod.Content.Items.Weapons
{
    public class MagicStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 35;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 10; // Mana cost
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 5;
            Item.value = Item.buyPrice(gold: 2);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item20; // Magic sound
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.EnergyBolt>();
            Item.shootSpeed = 12f;
        }

        public override Vector2? HoldoutOffset()
        {
            // Adjust the position of the item in the player's hand
            // This is optional and can be customized to fit your design
            return new Vector2(-4f, 0); // tweak these to adjust positioning
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Fire 3 projectiles in a spread
            int numberOfProjectiles = 3;
            float spreadAngle = MathHelper.ToRadians(5); // 5 degrees spread

            for (int i = 0; i < numberOfProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-spreadAngle, spreadAngle, i / (float)(numberOfProjectiles - 1)));
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }

            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DirtBlock, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }

        public override string Texture => "SoulysMod/Assets/Textures/Items/Weapons/Magic/IronStaff";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Magic Staff");
            // Tooltip.SetDefault("A staff imbued with magical energy.");
            // The following line is optional and will automatically assign the item to the "Weapons" category in the inventory.
            // ItemID.Sets.ItemNoGravity[Item.type] = true;
        }
        
    }
}