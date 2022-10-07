using System;
using System.Windows.Forms;
using GTA;
using GTA.Math;

namespace My.Scripts {
    public class AllahBabah : Script {

        public AllahBabah() {
            KeyDown += OnKeyDown;
        }

        void OnKeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.NumPad9) {
                Babah();
            }
        }

        private void Babah() {
            var peds = World.GetNearbyPeds(Finder.PlayerPed, 10);
            var vehicles = World.GetNearbyVehicles(Finder.PlayerPed, 10);
            
            BabahPeds(peds, 40, 10);
            BabahVehicles(vehicles, 30, 8);
        }
        
        private void BabahPeds(Ped[] peds, float basePower, float verticalPower) {
            BabahEntities(peds, basePower, verticalPower, (ped, force) => {
                ped.Ragdoll(10000);
            });
        }
        
        private void BabahVehicles(Vehicle[] vehicles, float basePower, float verticalPower) {
            BabahEntities(vehicles, basePower, verticalPower, (vehicle, force) => {
                vehicle.ApplyForce(force);
            });
        }
        
        private void BabahEntities<T>(T[] entities, float basePower, float verticalPower, Action<T, Vector3> forceApplier) where T : Entity {
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