using System;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.UI;

namespace My.Scripts {
    public class LaunchElonMusk : Script {

        private readonly TaskPipeline tasks = new TaskPipeline();

        public LaunchElonMusk() {
            Tick += OnTick;
            Interval = 100;
            KeyDown += OnKeyDown;
        }

        private void OnTick(object sender, EventArgs e) {
            tasks.Tick();
        }

        void OnKeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.NumPad2) {
                RandomUtils.RunRandomFunction(LaunchPed, LaunchVehicle);
            }
        }

        private void LaunchPed() {
            var ped = Finder.GetRandomPedNearPlayer(50, p => p.IsOnScreen);

            if (ped == null) {
                GTA.UI.Screen.ShowHelpText("На экране не нашлось пешеходов", 5000);
                return;
            }
            
            ped.Ragdoll(10000);
            LaunchEntity(ped, 20, 50);

            if (RandomUtils.FlipCoin()) {
                tasks.DelayedTask(5, () => {
                    ped.Task.UseParachute();
                });
            }
        }

        private void LaunchVehicle() {
            var vehicle = Finder.GetRandomVehicleNearPlayer(50, v => v.IsOnScreen);
            
            if (vehicle == null) {
                GTA.UI.Screen.ShowHelpText("На экране не нашлось техники", 5000);
                return;
            }

            LaunchEntity(vehicle, 20, 50);
            
            if (RandomUtils.FlipCoin()) {
                tasks.DelayedTask(3, () => {
                    vehicle.Explode();
                });
            }
        }

        private static void LaunchEntity(Entity? vehicle, float minForce, float maxForce) {
            var force = RandomUtils.NextFloat(minForce, maxForce);
            var planeImpulse = Vector3.RandomXY() * (force / 2 * RandomUtils.NextFloat());

            vehicle?.ApplyForce(Vector3.WorldUp * force + planeImpulse);
        }
    }
}