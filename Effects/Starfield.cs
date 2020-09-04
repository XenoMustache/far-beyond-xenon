using SFML.Graphics;
using SFML.Graphics.Glsl;
using SFML.System;
using Xenon.Client;

namespace FarBeyond.Effects {
	public class Starfield : Effect {
		public Vector2u resolution;
		public Vector2f position, parallax;
		public Clock shaderClock;
		public RectangleShape rect;
		private Shader shader;

		public Starfield(float layers, float fade, float flickerSpeed, float depth) {
			shader = GameRegistry.starfield;

			rect = new RectangleShape(new Vector2f(1920, 1080));

			rect.Origin = rect.Size / 2;

			Vec3[] iResolution = new Vec3[] { new Vec3(800, 450, 0) };

			shader.SetUniformArray("iResolution", iResolution);
			shader.SetUniform("iNumLayers", layers);
			shader.SetUniform("iFade", fade);
			shader.SetUniform("iFlickerSpeed", flickerSpeed);
			shader.SetUniform("iFloatDepth", depth);
		}

		protected override void OnUpdate(float time) {
			rect.Position = position;
			Vec4[] iMouse = new Vec4[] { new Vec4(-parallax.X, parallax.Y, 0, 0) };

			shader.SetUniform("iTime", time);
			shader.SetUniformArray("iMouse", iMouse);
		}

		protected override void OnDraw(RenderTarget target, RenderStates states) {
			states = new RenderStates(states) { Shader = shader };
			target.Draw(rect, states);
		}
	}
}
