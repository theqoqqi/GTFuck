using System.Windows.Forms;
using GTA;

namespace My.Scripts {
    public class ExplodeAnything : Script {

        public ExplodeAnything() {
            KeyDown += OnKeyDown;
        }

        void OnKeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.NumPad0) {
                Run();
            }
        }
        
        private void Run() {
            Finder.GetRandomVehicleNearPlayer(50)?.Explode();
        }
    }
}