using System;
using System.Collections.Generic;
using System.IO;
using FargowiltasSouls;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.RealMutantEX.Projectiles;

public class MutantDestroyerBody : ModProjectile
{
    public override string Texture => "FargowiltasSouls/Assets/ExtraTextures/Resprites/NPC_135";
    public override void SetStaticDefaults()
	{
		Main.projFrames[base.Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 24;
		base.Projectile.height = 24;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 900;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 255;
		base.Projectile.netImportant = true;
		base.Projectile.hide = true;
		base.CooldownSlot = 1;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
		writer.Write(base.Projectile.localAI[1]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
		base.Projectile.localAI[1] = reader.ReadSingle();
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindProjectiles.Add(index);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D texture2D13 = TextureAssets.Projectile[base.Projectile.type].Value;
		int num214 = TextureAssets.Projectile[base.Projectile.type].Value.Height / Main.projFrames[base.Projectile.type];
		int y6 = num214 * base.Projectile.frame;
		Main.EntitySpriteDraw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, y6, texture2D13.Width, num214), base.Projectile.GetAlpha(Color.White), base.Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)num214 / 2f), base.Projectile.scale, (base.Projectile.spriteDirection != 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
		return false;
	}

	public override void AI()
	{
		if ((int)Main.time % 120 == 0)
		{
			base.Projectile.netUpdate = true;
		}
		int num1038 = 30;
		bool flag67 = false;
		Vector2 value67 = Vector2.Zero;
		_ = Vector2.Zero;
		float num1052 = 0f;
		if (base.Projectile.ai[1] == 1f)
		{
			base.Projectile.ai[1] = 0f;
			base.Projectile.netUpdate = true;
		}
		int byIdentity = FargoSoulsUtil.GetProjectileByIdentity(base.Projectile.owner, (int)base.Projectile.ai[0], base.Projectile.type, ModContent.ProjectileType<MutantDestroyerHead>());
		if (byIdentity >= 0 && Main.projectile[byIdentity].active)
		{
			flag67 = true;
			value67 = Main.projectile[byIdentity].Center;
			_ = Main.projectile[byIdentity].velocity;
			num1052 = Main.projectile[byIdentity].rotation;
			MathHelper.Clamp(Main.projectile[byIdentity].scale, 0f, 50f);
			_ = Main.projectile[byIdentity].alpha;
			Main.projectile[byIdentity].localAI[0] = base.Projectile.localAI[0] + 1f;
			if (Main.projectile[byIdentity].type != ModContent.ProjectileType<MutantDestroyerHead>())
			{
				Main.projectile[byIdentity].localAI[1] = base.Projectile.identity;
			}
			base.Projectile.timeLeft = Main.projectile[byIdentity].timeLeft;
		}
		if (flag67)
		{
			base.Projectile.alpha -= 42;
			if (base.Projectile.alpha < 0)
			{
				base.Projectile.alpha = 0;
			}
			base.Projectile.velocity = Vector2.Zero;
			Vector2 vector134 = value67 - base.Projectile.Center;
			if (num1052 != base.Projectile.rotation)
			{
				float num1056 = MathHelper.WrapAngle(num1052 - base.Projectile.rotation);
				vector134 = vector134.RotatedBy(num1056 * 0.1f);
			}
			base.Projectile.rotation = vector134.ToRotation() + (float)Math.PI / 2f;
			base.Projectile.position = base.Projectile.Center;
			base.Projectile.width = (base.Projectile.height = (int)((float)num1038 * base.Projectile.scale));
			base.Projectile.Center = base.Projectile.position;
			if (vector134 != Vector2.Zero)
			{
				base.Projectile.Center = value67 - Vector2.Normalize(vector134) * 36f;
			}
			base.Projectile.spriteDirection = ((vector134.X > 0f) ? 1 : (-1));
		}
	}

	//public override void Kill(int timeLeft)
	//{
	//	for (int i = 0; i < 20; i++)
	//	{
	//		int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 62, (0f - base.Projectile.velocity.X) * 0.2f, (0f - base.Projectile.velocity.Y) * 0.2f, 100, default(Color), 2f);
	//		Main.dust[dust].noGravity = true;
	//		Main.dust[dust].velocity *= 2f;
	//		dust = Dust.NewDust(base.Projectile.Center, base.Projectile.width, base.Projectile.height, 60, (0f - base.Projectile.velocity.X) * 0.2f, (0f - base.Projectile.velocity.Y) * 0.2f, 100);
	//		Main.dust[dust].velocity *= 2f;
	//	}
	//	if (!Main.dedServ)
	//	{
	//		int g = Gore.NewGore(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity / 2f, ModContent.Find<ModGore>(base.Mod.Name, "MutantDestroyerBody").Type, base.Projectile.scale);
	//		Main.gore[g].timeLeft = 20;
	//	}
	//}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		target.AddBuff(ModContent.BuffType<LightningRodBuff>(), Main.rand.Next(300, 1200));
		target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
	}
}
