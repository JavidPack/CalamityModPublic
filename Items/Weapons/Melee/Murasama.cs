using CalamityMod.Projectiles.Melee;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee
{
	public class Murasama : ModItem
	{
		private int frameCounter = 0;
		private int frame = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Murasama");
			Tooltip.SetDefault("There will be blood!\n" +
				"ID and power-level locked\n" +
				"Prove your strength or have the correct user ID to wield this sword");
		}

		public override void SetDefaults()
		{
			item.height = 128;
			item.width = 56;
			item.damage = 20001;
			item.crit += 30;
			item.melee = true;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.channel = true;
			item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 5;
			item.knockBack = 6.5f;
			item.autoReuse = false;
			item.value = Item.buyPrice(2, 50, 0, 0);
			item.rare = 10;
			item.shoot = ModContent.ProjectileType<MurasamaSlash>();
			item.shootSpeed = 24f;
			item.Calamity().customRarity = CalamityRarity.Violet;
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 14));
		}

		internal Rectangle GetCurrentFrame()
		{
			//0 = 6 frames, 8 = 3 frames]
			int applicableCounter = frame == 0 ? 36 : frame == 8 ? 24 : 6;
			
			if (frameCounter >= applicableCounter)
			{
				frameCounter = -1;
				frame = frame == 13 ? 0 : frame + 1;
			}
			frameCounter++;
			return new Rectangle(0, item.height * frame, item.width, item.height);
		}

		public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			Texture2D texture = ModContent.GetTexture(Texture);
			spriteBatch.Draw(texture, position, GetCurrentFrame(), Color.White, 0f, origin, scale, SpriteEffects.None, 0);
			return false;
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			Texture2D texture = ModContent.GetTexture(Texture);
			spriteBatch.Draw(texture, item.position - Main.screenPosition, GetCurrentFrame(), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
			return false;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.ownedProjectileCounts[item.shoot] > 0)
				return false;
			return CalamityWorld.downedYharon || player.name == "Sam" || player.name == "Samuel Rodrigues";
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, 0f, 0f);
			return false;
		}
	}
}
