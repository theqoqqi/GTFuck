using System.Windows.Forms;
using GTA;
using GTA.Math;

namespace My.Scripts {
    public class LaunchElonMusk : Script {

        public LaunchElonMusk() {
            KeyDown += OnKeyDown;
        }

        void OnKeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.NumPad2) {
                Run();
            }
        }

        private void Run() {
            var ped = Finder.GetRandomPed(20, p => p.IsOnScreen);
            var force = RandomUtils.NextFloat(20, 50);
            
            ped?.ApplyForce(Vector3.WorldUp * force);
        }
    }
}