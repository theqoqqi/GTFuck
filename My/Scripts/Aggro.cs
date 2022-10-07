using System;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.UI;

namespace My.Scripts {
    public class Aggro : Script {

        public Aggro() {
            KeyDown += OnKeyDown;
        }

        void OnKeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.NumPad8) {
                Run();
            }
        }

        private void Run() {
            var playerPed = Finder.PlayerPed;
            var peds = World.GetNearbyPeds(playerPed, 40);

            foreach (var ped in peds) {
                PedUtils.PerformSequence(ped, tasks => {
                    if (ped.IsInVehicle()) {
                        tasks.LeaveVehicle();
                    }
                    
                    tasks.FightAgainst(playerPed);
                });
            }
        }
    }
}