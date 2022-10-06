using System;
using System.Linq;

namespace My {
    public static class RandomUtils {
        
        private static readonly Random Random = new();

        public static float NextFloat(float max) {
            return NextFloat(0, max);
        }

        public static float NextFloat(float min, float max) {
            return (float) (min + Random.NextDouble() * (max - min));
        }

        /**
         * Запускает рандомную функцию
         */
        public static void RunRandomFunction(params Action[] actions) {
            var index = Random.Next(0, actions.Length);
            
            actions[index].Invoke();
        }
        
        /**
         * Рандомный элемент из массива элементов любого типа
         */
        public static T? GetRandomItem<T>(T[] items, Func<T, bool>? filter = null) {
            if (filter != null) {
                items = items.Where(filter).ToArray();
            }
            
            if (items.Length == 0) {
                return default; // Считай null
            }
            
            var index = Random.Next(0, items.Length);
            
            return items[index];
        }
    }
}