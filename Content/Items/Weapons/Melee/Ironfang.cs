using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SoulysMod.Content.Items
{
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class Ironfang : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.SoulysMod.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 24;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 15;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(silver: 1);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;

		}


		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		public override string Texture => "SoulysMod/Assets/Textures/Items/Weapons/Melee/Ironfang";

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ironfang");
			// Tooltip.SetDefault("A sharp fang made of iron.");
			// The following line is optional and will automatically assign the item to the "Weapons" category in the inventory.
			// ItemID.Sets.ItemNoGravity[Item.type] = true;
		}
	}
}
