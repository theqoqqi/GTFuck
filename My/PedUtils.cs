using System;
using GTA;

namespace My {
    public static class PedUtils {

        public static void PerformSequence(Ped ped, Action<TaskSequence> addTasks) {
            TaskSequence sequence = new();

            addTasks(sequence);

            ped.Task.PerformSequence(sequence);
        }
    }
}