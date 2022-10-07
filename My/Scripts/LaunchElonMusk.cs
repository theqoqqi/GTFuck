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
                RandomUtils.RunRandomFunction(LaunchPed, LaunchVehicle);
            }
        }

        private void LaunchPed() {
            var ped = Finder.GetRandomPedNearPlayer(20, p => p.IsOnScreen);
            
            LaunchEntity(ped, 20, 50);

            if (RandomUtils.FlipCoin()) {
                Wait(5000);
            
                ped?.Task.UseParachute();
            }
        }

        private void LaunchVehicle() {
            var vehicle = Finder.GetRandomVehicleNearPlayer(20, v => v.IsOnScreen);
            
            LaunchEntity(vehicle, 50, 100);
            
            if (RandomUtils.FlipCoin()) {
                Wait(3000);
            
                vehicle?.Explode();
            }
        }

        private static void LaunchEntity(Entity? vehicle, float minForce, float maxForce) {
            var force = RandomUtils.NextFloat(minForce, maxForce);

            vehicle?.ApplyForce(Vector3.WorldUp * force);
        }
    }
}