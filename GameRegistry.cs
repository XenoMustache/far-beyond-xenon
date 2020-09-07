using SFML.Graphics;

namespace FarBeyond {
	public class GameRegistry {
		public static Texture civShipsTexture, bulletsTexture, pirateShipsTexture;
		public static Shader starfield, explosion;

		public static void Init() {
			civShipsTexture = new Texture("Resources\\Textures\\civ_ships.png");
			pirateShipsTexture = new Texture("Resources\\Textures\\pirate_ships.png");
			bulletsTexture = new Texture("Resources\\Textures\\bullets.png");

			starfield = new Shader(null, null, "Resources\\Shaders\\starfield.glsl");
			explosion = new Shader(null, null, "Resources\\Shaders\\explode.glsl");
		}
	}
}
