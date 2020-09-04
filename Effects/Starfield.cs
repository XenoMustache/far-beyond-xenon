using FarBeyond.Registry;
using SFML.Graphics;
using SFML.Graphics.Glsl;
using SFML.System;
using Xenon.Client;

namespace FarBeyond.Effects {
	public class Starfield : Effect {
		public float Layers = 2, Fade = 0, FlickerSpeed = 1, Depth = 0.3f;

		public Vector2u resolution;
		public Vector2f playerPos;
		public Clock shaderClock;
		public RectangleShape rect;
		private Shader shader;

		public Starfield() {
			shader = AssetRegistry.starfield;

			rect = new RectangleShape(new Vector2f(1920, 1080));

			rect.Origin = rect.Size / 2;

			Vec3[] iResolution = new Vec3[] { new Vec3(800, 450, 0) };

			shader.SetUniformArray("iResolution", iResolution);
			shader.SetUniform("iNumLayers", Layers);
			shader.SetUniform("iFade", Fade);
			shader.SetUniform("iFlickerSpeed", FlickerSpeed);
			shader.SetUniform("iFloatDepth", Depth);
		}

		protected override void OnUpdate(float time) {
			rect.Position = playerPos;
			Vec4[] iMouse = new Vec4[] { new Vec4(-playerPos.X, playerPos.Y, 0, 0) };

			shader.SetUniform("iTime", time);
			shader.SetUniformArray("iMouse", iMouse);
		}

		protected override void OnDraw(RenderTarget target, RenderStates states) {
			states = new RenderStates(states) { Shader = shader };
			target.Draw(rect, states);
		}
	}
}
