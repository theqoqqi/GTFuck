using System.Windows.Forms;
using GTA;
using GTA.Math;

namespace My.Scripts {
    public class SummonAnvil : Script {

        public SummonAnvil() {
            KeyDown += OnKeyDown;
        }

        void OnKeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.NumPad1) {
                Run();
            }
        }

        private void Run() {
            var targetPosition = GetRandomAnvilTargetPosition(Finder.PlayerPosition, 50);
            var heading = RandomUtils.NextFloat(360);

            if (targetPosition == null) {
                return;
            }

            var height = RandomUtils.NextFloat(10, 15);
            var position = targetPosition.Value + Vector3.WorldUp * height;

            var vehicle = World.CreateRandomVehicle(position, heading, IsModelCanBeAnvil);
            
            vehicle?.ApplyForce(Vector3.WorldDown * 30);
        }

        private static Vector3? GetRandomAnvilTargetPosition(Vector3 origin, float radius) {
            Entity? targetEntity = null;
            
            RandomUtils.RunRandomFunction(() => {
                targetEntity = Finder.GetRandomVehicle(origin, radius, p => p.IsOnScreen);
            }, () => {
                targetEntity = Finder.GetRandomPed(origin, radius, p => p.IsOnScreen);
            });

            return targetEntity?.Position;
        }

        private static bool IsModelCanBeAnvil(Model model) {
            return model.IsBike
                   || model.IsBoat
                   || model.IsBus
                   || model.IsCar
                   || model.IsTrain
                   || model.IsVan
                   || model.IsEmergencyVehicle
                   || model.IsSubmarine
                   || model.IsQuadBike
                   || model.IsJetSki;
        }
    }
}