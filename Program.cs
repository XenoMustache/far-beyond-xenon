using Xenon.Common.Utilities;

namespace FarBeyond {
	class Program {
		public static readonly string VERSION = "0.1 Pre-Alpha";

		static void Main(string[] args) {
			MiscUtils.HideConsole(false);
			new FarBeyond($"Far Beyond - Xenon - {VERSION}", new SFML.System.Vector2u(1920, 1080));
		}
	}
}
