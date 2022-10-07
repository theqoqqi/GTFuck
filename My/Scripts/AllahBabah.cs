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
            
            BabahEntities(peds, 20, 5);
            BabahEntities(vehicles, 15, 4);
        }

        private void BabahEntities<T>(T[] entities, float basePower, float verticalPower) where T : Entity {
            var origin = Finder.PlayerPosition;
            
            foreach (var entity in entities) {
                var difference = entity.Position - origin;
                var direction = difference.Normalized;
                var power = basePower - difference.Length();
                var force = direction * power + Vector3.WorldUp * verticalPower;

                entity.ApplyForce(force);
            }
        }
    }
}