using System;
using System.Collections.Generic;
using Contracts.Domain.Events;
using Crosscutting.IoC;

namespace Domain.Events
{
    /// <summary>
    /// http://msdn.microsoft.com/en-gb/magazine/ee236415.aspx#id0400046
    /// </summary>
    public static class DomainEvents
    {
        [ThreadStatic]
        static List<Delegate> actions;

        public static void Register<T>(Action<T> callback) where T : IDomainEvent
        {
            if (actions == null)
            {
                actions = new List<Delegate>();
            }
            actions.Add(callback);
        }

        public static void ClearCallbacks()
        {
            actions = null;
        }

        public static void Raise<T>(T args) where T : IDomainEvent
        {
            foreach (var handler in ContainerHolder.Container.ResolveAll<IHandle<T>>())
            {
                handler.Handle(args);
            }

            if (actions == null) return;

            foreach (var action in actions)
            {
                var action1 = action as Action<T>;
                action1?.Invoke(args);
            }
        }
    }
}
