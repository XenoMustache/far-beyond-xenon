using SFML.Graphics;
using SFML.Graphics.Glsl;
using SFML.System;
using Xenon.Client;

namespace FarBeyond.Effects {
	public class EffectExplosion : Effect {
		Shader shader;
		Clock clock;

		public EffectExplosion(float seed, float particleCount, float pixelResolution, float spread, float travelSpeed, float fadeIntesity, Vec2 direction, Vec4 color) {
			clock = new Clock();
			shader = GameRegistry.explosion;

			Vec2[] iDirection = new Vec2[] { direction };
			Vec3[] iResolution = new Vec3[] { new Vec3(800, 450, 0) };
			Vec4[] iColor = new Vec4[] { color };

			shader.SetUniformArray("iDirection", iDirection);
			shader.SetUniformArray("iResolution", iResolution);
			shader.SetUniformArray("iColor", iColor);
			shader.SetUniform("iSeed", seed);
			shader.SetUniform("iParticles", particleCount);
			shader.SetUniform("iPixelResolution", pixelResolution);
			shader.SetUniform("iSpread", spread);
			shader.SetUniform("iTravelSpeed", travelSpeed);
			shader.SetUniform("iFadeIntensity", fadeIntesity);
		}

		protected override void OnUpdate(double deltaTime) {
			var time = clock.ElapsedTime.AsSeconds() * (float)deltaTime;

			shader.SetUniform("iTime", time);
		}

		protected override void OnDraw(RenderTarget target, RenderStates states) { }
	}
}
