using System;
using System.Linq;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using Screen = GTA.UI.Screen;

namespace My.Scripts {
    public class AllahBabah : Script {

        private bool enableAutoBabah;

        public AllahBabah() {
            Tick += OnTick;
            Interval = 100;
            KeyDown += OnKeyDown;
        }

        private void OnTick(object sender, EventArgs e) {
            if (enableAutoBabah) {
                Babah();
            }
        }

        void OnKeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.E) {
                enableAutoBabah = !enableAutoBabah;

                Screen.ShowHelpText("Автобабах " + (enableAutoBabah ? "включен" : "выключен"));
            }

            if (e.KeyCode == Keys.NumPad9) {
                Babah();
            }
        }

        private void Babah() {
            var peds = World.GetNearbyPeds(Finder.PlayerPed, 10);
            var vehicles = World.GetNearbyVehicles(Finder.PlayerPed, 10);
            var entities = World.GetNearbyEntities(Finder.PlayerPosition, 10)
                    .Where(entity => !(entity is Vehicle || entity is Ped)).ToArray();

            BabahPeds(peds, 40, 10);
            BabahVehicles(vehicles, 30, 8);
            BabahEntities(entities, 20, 6);

            Screen.ShowHelpText(
                    "Раскидано "
                    + peds.Length
                    + " педов, "
                    + vehicles.Length
                    + " техники и "
                    + entities.Length
                    + " энтитей",
                    10000
            );
        }

        private void BabahPeds(Ped[] peds, float basePower, float verticalPower) {
            BabahEntities(peds, basePower, verticalPower, (ped, force) => {
                ped.Ragdoll(10000);
                ped.ApplyForce(force);
            });
        }

        private void BabahVehicles(Vehicle[] vehicles, float basePower, float verticalPower) {
            BabahEntities(vehicles, basePower, verticalPower, (vehicle, force) => {
                vehicle.ApplyForce(force);
            });
        }

        private void BabahEntities<T>(T[] entities, float basePower, float verticalPower, Action<T, Vector3>? forceApplier = null) where T : Entity {
            forceApplier ??= (entity, force) => entity.ApplyForce(force);
            
            var origin = Finder.PlayerPosition;

            foreach (var entity in entities) {
                var difference = entity.Position - origin;
                var direction = difference.Normalized;
                var power = basePower - difference.Length();
                var force = direction * power + Vector3.WorldUp * verticalPower;

                forceApplier(entity, force);
            }
        }
    }
}