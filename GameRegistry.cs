using SFML.Graphics;

namespace FarBeyond {
	public class GameRegistry {
		public static Texture civShipsTexture, bulletsTexture, pirateShipsTexture;
		public static Shader starfield, explosion;
		public static Font uiDefault, uiStylized;

		static string location = "Resources";

		public static void Init() {
			civShipsTexture = addTexture("civ_ships.png");
			pirateShipsTexture = addTexture("pirate_ships.png");
			bulletsTexture = addTexture("bullets.png");

			starfield = addFragShader("starfield.glsl");
			explosion = addFragShader("explode.glsl");

			uiDefault = addFont("Monaco.ttf");
			uiStylized = addFont("KennyRocket.ttf");
		}

		static Texture addTexture(string name) {
			var texture = new Texture($"{location}\\Textures\\{name}");
			return texture;
		}

		static Shader addFragShader(string fragment) {
			var locale = $"{location}\\Shaders\\";
			var shader = new Shader(null, null, $"{locale}{fragment}");
			return shader;
		}

		static Font addFont(string name) {
			var font = new Font($"{location}\\Fonts\\{name}");
			return font;
		}
	}
}
