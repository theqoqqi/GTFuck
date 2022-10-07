using System;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.UI;

namespace My {
    public class MainScript : Script {

        private static readonly Random Random = new();

        public MainScript() {
            Tick += OnTick;
            Interval = 100;
            KeyDown += OnKeyDown;
        }

        private void OnTick(object sender, EventArgs e) {

        }

        void OnKeyDown(object sender, KeyEventArgs e) {
            if (Keys.J == e.KeyCode) {
                StartRandomZaruba();
            }

            if (e.KeyCode == Keys.NumPad0) {
                ExplodeAnything();
            }

            if (e.KeyCode == Keys.NumPad1) {
                SummonAnvil();
            }

            if (e.KeyCode == Keys.NumPad2) {
                LaunchElonMusk();
            }
        }

        private void StartRandomZaruba() {
            // Функция возвращает tuple из двух педов - (Ped, Ped). Либо null, если ничего не нашлось.
            var tuple = Finder.GetRandomPairNearPlayer(50, 25, IsNotPlayer);

            // Если найти пару не получилось, возвращается null
            if (tuple == null) {
                Notification.Show("Нет прохожих");
                return;
            }

            // Далее этот tuple можно разбить на две переменные, чтобы было удобно.
            var (first, second) = tuple.Value;
            // Это то же самое:
            // var first = tuple.Value.Item1;
            // var second = tuple.Value.Item2;

            if (first.IsInVehicle()) {
                first.Task.LeaveVehicle();
            }

            if (second.IsInVehicle()) {
                second.Task.LeaveVehicle();
            }

            first.Task.FightAgainst(second);

            Notification.Show("Кому-то пизда :)");
        }

        private void ExplodeAnything() {
            RandomUtils.RunRandomFunction(() => {
                Finder.GetRandomVehicleNearPlayer(50)?.Explode();
            }, () => {
                var radius = RandomUtils.NextFloat(2, 5);
                
                World.AddExplosion(Finder.PlayerPosition, ExplosionType.Barrel, radius, 0);
            });
        }

        private void SummonAnvil() {
            var targetPosition = GetRandomAnvilTargetPosition(Finder.PlayerPosition, 50);
            var heading = RandomUtils.NextFloat(360);

            if (targetPosition == null) {
                return;
            }

            var height = RandomUtils.NextFloat(5, 15);
            var position = targetPosition.Value + Vector3.WorldUp * height;

            World.CreateRandomVehicle(position, heading, IsModelCanBeAnvil);
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

        private void LaunchElonMusk() {
            var ped = Finder.GetRandomPed(20, p => p.IsOnScreen);
            var force = RandomUtils.NextFloat(5, 15);
            
            ped?.ApplyForce(Vector3.WorldUp * force);
        }

        /**
         * Проверяет, что пед не игрок
         */
        private static bool IsNotPlayer(Ped ped) {
            return ped != Game.Player.Character;
        }
    }
}