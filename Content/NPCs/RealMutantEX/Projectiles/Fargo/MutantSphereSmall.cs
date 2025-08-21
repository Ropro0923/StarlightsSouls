using System;
using FargowiltasSouls;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.RealMutantEX.Projectiles;

public class MutantSphereSmall : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_454";
	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Projectile.type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 10;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 46;
		base.Projectile.height = 46;
		base.Projectile.aiStyle = -1;
		base.Projectile.hostile = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 120;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 200;
		base.CooldownSlot = 1;
	}

	public override bool CanHitPlayer(Player target)
	{
		return target.hurtCooldowns[1] == 0;
	}

	public override void AI()
	{
		if (base.Projectile.ai[0] > -1f && base.Projectile.ai[0] < 255f && (base.Projectile.ai[1] += 1f) > 20f)
		{
			int foundTarget = (int)base.Projectile.ai[0];
			Player p = Main.player[foundTarget];
			Vector2 desiredVelocity = base.Projectile.DirectionTo(p.Center) * 5f;
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, desiredVelocity, 0.05f);
		}
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 20;
			if (base.Projectile.alpha < 0)
			{
				base.Projectile.alpha = 0;
			}
		}
		base.Projectile.scale = (1f - (float)base.Projectile.alpha / 255f) * 0.75f;
		if (++base.Projectile.frameCounter >= 6)
		{
			base.Projectile.frameCounter = 0;
			if (++base.Projectile.frame > 1)
			{
				base.Projectile.frame = 0;
			}
		}
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		target.GetModPlayer<FargoSoulsPlayer>().MaxLifeReduction += 100;
		target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400);
		target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
		target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 360);
	}

	public override void Kill(int timeleft)
	{
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 208);
		base.Projectile.Center = base.Projectile.position;
		for (int index1 = 0; index1 < 3; index1++)
		{
			int index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, DustID.Vortex, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[index2].position = new Vector2(base.Projectile.width / 2, 0f).RotatedBy(6.28318548202515 * Main.rand.NextDouble()) * (float)Main.rand.NextDouble() + base.Projectile.Center;
		}
		for (int index1 = 0; index1 < 10; index1++)
		{
			int index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, DustID.Vortex, 0f, 0f, 0, default(Color), 2.5f);
			Main.dust[index2].position = new Vector2(base.Projectile.width / 2, 0f).RotatedBy(6.28318548202515 * Main.rand.NextDouble()) * (float)Main.rand.NextDouble() + base.Projectile.Center;
			Main.dust[index2].noGravity = true;
			Main.dust[index2].velocity *= 1f;
			int index3 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, DustID.Vortex, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[index3].position = new Vector2(base.Projectile.width / 2, 0f).RotatedBy(6.28318548202515 * Main.rand.NextDouble()) * (float)Main.rand.NextDouble() + base.Projectile.Center;
			Main.dust[index3].velocity *= 1f;
			Main.dust[index3].noGravity = true;
		}
		if (Main.netMode != 1)
		{
			Projectile.NewProjectile(Terraria.Entity.InheritSource(base.Projectile), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<MutantBombSmall>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, 0f);
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White * base.Projectile.Opacity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D glow = ModContent.Request<Texture2D>("ssm/Assets/ExtraTextures/RealMutantEX/MutantSphereGlow", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
		int rect1 = glow.Height;
		int rect2 = 0;
		Rectangle glowrectangle = new Rectangle(0, rect2, glow.Width, rect1);
		Vector2 gloworigin2 = glowrectangle.Size() / 2f;
		Color glowcolor = Color.Lerp(new Color(196, 247, 255, 0), Color.Transparent, 0.85f);
		for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[base.Projectile.type]; i++)
		{
			Color color27 = glowcolor;
			color27 *= (float)(ProjectileID.Sets.TrailCacheLength[base.Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type];
			float scale = base.Projectile.scale * (float)(ProjectileID.Sets.TrailCacheLength[base.Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type];
			Vector2 value4 = base.Projectile.oldPos[i] - Vector2.Normalize(base.Projectile.velocity) * i * 2f;
			Main.EntitySpriteDraw(glow, value4 + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)glowrectangle, color27, base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f, gloworigin2, scale * 1.5f, SpriteEffects.None, 0);
		}
		glowcolor = Color.Lerp(new Color(255, 255, 255, 0), Color.Transparent, 0.8f);
		Main.EntitySpriteDraw(glow, base.Projectile.position + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)glowrectangle, glowcolor, base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f, gloworigin2, base.Projectile.scale * 1.5f, SpriteEffects.None, 0);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		Texture2D texture2D13 = TextureAssets.Projectile[base.Projectile.type].Value;
		int num156 = TextureAssets.Projectile[base.Projectile.type].Value.Height / Main.projFrames[base.Projectile.type];
		int y3 = num156 * base.Projectile.frame;
		Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
		Vector2 origin2 = rectangle.Size() / 2f;
		Main.EntitySpriteDraw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin2, base.Projectile.scale, SpriteEffects.None, 0);
	}
}
