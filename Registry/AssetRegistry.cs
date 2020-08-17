using SFML.Graphics;

namespace Seed.Registry {
	public class AssetRegistry {
		public static Texture civShips, bullets;

		public static void Init() {
			civShips = new Texture("Resources\\Textures\\civ_ships.png");
			bullets = new Texture("Resources\\Textures\\bullets.png");
		}
	}
}
