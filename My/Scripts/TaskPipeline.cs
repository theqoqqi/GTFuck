using System;
using System.Collections.Generic;
using GTA;

namespace My.Scripts {
    public class TaskPipeline {

        private readonly List<Task> actions = new List<Task>();
        
        public void Tick() {
            var actionsToExecute = actions.FindAll(Task.TimeHasCome);
            actions.RemoveAll(Task.TimeHasCome);
            
            actionsToExecute.ForEach(Task.Execute);
        }

        public void DelayedTask(float delaySeconds, Action action) {
            var executeAt = (int) (Game.GameTime + delaySeconds * 1000);
            var task = new Task(executeAt, action);

            actions.Add(task);
        }

        private class Task {
            private readonly int executeAt;
            
            private readonly Action action;

            public Task(int executeAt, Action action) {
                this.executeAt = executeAt;
                this.action = action;
            }

            public static bool TimeHasCome(Task task) {
                return task.executeAt <= Game.GameTime;
            }

            public static void Execute(Task task) {
                task.action();
            }
        }
    }
}