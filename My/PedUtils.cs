using System;
using GTA;

namespace My {
    public static class PedUtils {

        public static void PerformSequence(Ped ped, Action<TaskInvoker> addTasks) {
            TaskSequence sequence = new();

            addTasks(sequence.AddTask);

            ped.Task.PerformSequence(sequence);
        }
    }
}