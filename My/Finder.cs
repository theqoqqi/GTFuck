using System;
using GTA;
using GTA.Math;

namespace My {
    public static class Finder {

        public static Ped PlayerPed => Game.Player.Character;

        public static Vector3 PlayerPosition => PlayerPed.Position;

        /**
         * Рандомная техника рядом с игроком.
         * 
         * radius - как далеко от игрока искать технику
         * filter - функция, которая будет фильтровать технику
         */
        public static Vehicle? GetRandomVehicleNearPlayer(float radius, Func<Vehicle, bool>? filter = null) {
            return GetRandomVehicle(PlayerPosition, radius, filter);
        }

        /**
         * Рандомная техника рядом с игроком.
         * 
         * origin - вокруг какой позиции искать
         * radius - как далеко от этой позиции искать технику
         * filter - функция, которая будет фильтровать технику
         */
        public static Vehicle? GetRandomVehicle(Vector3 origin, float radius, Func<Vehicle, bool>? filter = null) {
            var vehicles = World.GetNearbyVehicles(origin, radius);
            
            return RandomUtils.GetRandomItem(vehicles, filter);
        }

        /**
         * Рандомная пара педов рядом с игроком.
         * 
         * radiusAroundPlayer - как далеко от игрока искать первого педа
         * pairRadius - как далеко от первого педа искать второго
         * filter - функция, которая будет фильтровать педов
         */
        public static (Ped, Ped)? GetRandomPairNearPlayer(float radiusAroundPlayer, float pairRadius, Func<Ped, bool>? filter = null) {
            return GetRandomPair(PlayerPosition, radiusAroundPlayer, pairRadius, filter);
        }

        /**
         * Рандомная пара педов рядом с позицией.
         * 
         * origin - вокруг какой позиции искать
         * originRadius - как далеко от этой позиции искать первого педа
         * pairRadius - как далеко от первого педа искать второго
         * filter - функция, которая будет фильтровать педов
         */
        public static (Ped, Ped)? GetRandomPair(Vector3 origin, float originRadius, float pairRadius, Func<Ped, bool>? filter = null) {
            var firstPed = GetRandomPed(origin, originRadius, filter);

            if (firstPed == null) {
                return null;
            }
            
            var secondPed = GetRandomPed(firstPed, pairRadius, filter);

            if (secondPed == null) {
                return null;
            }

            return (firstPed, secondPed);
        }

        /**
         * Рандомный пед рядом с игроком
         */
        public static Ped? GetRandomPedNearPlayer(float radius, Func<Ped, bool>? filter = null) {
            return GetRandomPed(PlayerPed, radius, filter);
        }

        /**
         * Рандомный пед рядом с позицией
         */
        public static Ped? GetRandomPed(Vector3 position, float radius, Func<Ped, bool>? filter = null) {
            var peds = World.GetNearbyPeds(position, radius);

            return RandomUtils.GetRandomItem(peds, filter);
        }

        /**
         * Рандомный пед рядом с другим педом
         */
        public static Ped? GetRandomPed(Ped aroundPed, float radius, Func<Ped, bool>? filter = null) {
            var peds = World.GetNearbyPeds(aroundPed, radius);

            return RandomUtils.GetRandomItem(peds, filter);
        }
    }
}