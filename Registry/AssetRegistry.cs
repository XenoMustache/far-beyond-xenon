using SFML.Graphics;

namespace FarBeyond.Registry {
	public class AssetRegistry {
		public static Texture civShipsTexture, bulletsTexture;

		public static void Init() {
			civShipsTexture = new Texture("Resources\\Textures\\civ_ships.png");
			bulletsTexture = new Texture("Resources\\Textures\\bullets.png");
		}
	}
}
