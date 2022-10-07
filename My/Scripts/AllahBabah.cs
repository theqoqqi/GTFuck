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
            Audio.PlaySoundAt(Finder.PlayerPed, "Monkey_Scream", "FBI_05_SOUNDS");
            
            Wait(3000);
            
            var radius = RandomUtils.NextFloat(10, 15);
            
            World.AddExplosion(Finder.PlayerPosition, ExplosionType.Barrel, radius, 0);
        }
    }
}