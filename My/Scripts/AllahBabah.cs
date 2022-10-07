using System.Windows.Forms;
using GTA;

namespace My.Scripts {
    public class AllahBabah : Script {

        public AllahBabah() {
            KeyDown += OnKeyDown;
        }

        void OnKeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.NumPad9) {
                Run();
            }
        }

        private void Run() {
            var radius = RandomUtils.NextFloat(3, 5);
            
            World.AddExplosion(Finder.PlayerPosition, ExplosionType.Barrel, radius, 0);
        }
    }
}