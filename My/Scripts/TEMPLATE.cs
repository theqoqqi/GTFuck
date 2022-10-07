using System;
using System.Windows.Forms;
using GTA;

namespace My.Scripts {
    public class TEMPLATE : Script {

        public TEMPLATE() {
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