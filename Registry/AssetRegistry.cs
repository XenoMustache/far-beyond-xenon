using SFML.Graphics;

namespace FarBeyond.Registry {
	public class AssetRegistry {
		public static Texture civShipsTexture, bulletsTexture, pirateShipsTexture;

		public static void Init() {
			civShipsTexture = new Texture("Resources\\Textures\\civ_ships.png");
			pirateShipsTexture = new Texture("Resources\\Textures\\pirate_ships.png");
			bulletsTexture = new Texture("Resources\\Textures\\bullets.png");
		}
	}
}
