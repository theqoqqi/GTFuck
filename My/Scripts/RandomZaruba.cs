using System.Windows.Forms;
using GTA;
using GTA.UI;

namespace My.Scripts {
    public class RandomZaruba : Script {

        public RandomZaruba() {
            KeyDown += OnKeyDown;
        }

        void OnKeyDown(object sender, KeyEventArgs e) {
            if (Keys.J == e.KeyCode) {
                StartRandomZaruba();
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

        private static bool IsNotPlayer(Ped ped) {
            return ped != Game.Player.Character;
        }
    }
}