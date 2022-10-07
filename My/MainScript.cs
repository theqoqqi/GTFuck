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
            
        }
    }
}