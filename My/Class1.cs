using System;
using System.Linq;
using System.Windows.Forms;
using GTA;
using GTA.Math;

namespace My {
    public class CLass1 : Script {

        private readonly Random random = new Random();

        public CLass1() {
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
        }

        private void StartRandomZaruba() {
            var tuple = GetRandomPairNearPlayer(50, 25, IsNotPlayer);

            if (tuple == null) {
                UI.Notify("Нет прохожих");
                return;
            }
            
            var (first, second) = tuple.Value;
                        
            if (first.IsInVehicle()) {
                first.Task.LeaveVehicle();
            }
                    
            if (second.IsInVehicle()) {
                second.Task.LeaveVehicle();
            }
                        
            first.Task.FightAgainst(second);

            UI.Notify("Кому-то пизда :)");
        }

        /**
         * Проверяет, что пед не игрок
         */
        private bool IsNotPlayer(Ped ped) {
            return ped != Game.Player.Character;
        }

        /**
         * Рандомная пара педов рядом с игроком.
         * 
         * originRadius - как далеко от этой игрока искать первого педа
         * pairRadius - как далеко от первого педа искать второго
         */
        private (Ped, Ped)? GetRandomPairNearPlayer(float radiusAroundPlayer, float pairRadius, Func<Ped, bool>? filter = null) {
            var origin = Game.Player.Character.Position;

            return GetRandomPair(origin, radiusAroundPlayer, pairRadius, filter);
        }

        /**
         * Рандомная пара педов рядом с позицией.
         * 
         * origin - вокруг какой позиции искать
         * originRadius - как далеко от этой позиции искать первого педа
         * pairRadius - как далеко от первого педа искать второго
         */
        private (Ped, Ped)? GetRandomPair(Vector3 origin, float originRadius, float pairRadius, Func<Ped, bool>? filter = null) {
            var firstPed = GetRandomPedInRadius(origin, originRadius, filter);

            if (firstPed == null) {
                return null;
            }
            
            var secondPed = GetRandomPedInRadius(firstPed, pairRadius, filter);

            if (secondPed == null) {
                return null;
            }

            return (firstPed, secondPed);
        }

        /**
         * Рандомный пед рядом с позицией
         */
        private Ped? GetRandomPedInRadius(Vector3 position, float radius, Func<Ped, bool>? filter = null) {
            var peds = World.GetNearbyPeds(position, radius);

            return GetRandomPed(peds, filter);
        }

        /**
         * Рандомный пед рядом с другим педом
         */
        private Ped? GetRandomPedInRadius(Ped aroundPed, float radius, Func<Ped, bool>? filter = null) {
            var peds = World.GetNearbyPeds(aroundPed, radius);

            return GetRandomPed(peds, filter);
        }

        /**
         * Рандомный пед из массива
         */
        private Ped? GetRandomPed(Ped[] peds, Func<Ped, bool>? filter = null) {
            if (filter != null) {
                peds = peds.Where(filter).ToArray();
            }
            
            if (peds.Length == 0) {
                return null;
            }
            
            var index = random.Next(0, peds.Length);
            
            return peds[index];
        }
    }
}