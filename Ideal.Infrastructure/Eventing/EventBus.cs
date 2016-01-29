#region credits
// ***********************************************************************
// Assembly	: Ideal.Infrastructure
// Author	: Rod Johnson
// Created	: 02-24-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Ideal.Core.Eventing;

namespace Ideal.Infrastructure.Eventing
{
    #region

    

    #endregion

    public partial class MessageBus : IMessageBus
    {
        private static MessageBus _instance;

        private MessageBus() { }

        public static MessageBus Instance => _instance ?? (_instance = new MessageBus());

        private readonly Dictionary<Type, List<WeakReference>> _eventSubscriberLists =
            new Dictionary<Type, List<WeakReference>>();
        private readonly object _lock = new object();

        public void Subscribe(object subscriber)
        {
            lock (_lock)
            {
                var subscriberTypes =
                    subscriber.GetType().GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandles<>));

                var weakReference = new WeakReference(subscriber);
                foreach (var subscriberType in subscriberTypes)
                {
                    var subscribers = GetSubscribers(subscriberType);
                    subscribers.Add(weakReference);
                }
            }
        }

        public void Publish<TEvent>(TEvent eventToPublish)
        {
            var subscriberType = typeof(IHandles<>).MakeGenericType(typeof(TEvent));
            var subscribers = GetSubscribers(subscriberType);
            var subscribersToRemove = new List<WeakReference>();

            foreach (var weakSubscriber in subscribers)
            {
                if (weakSubscriber.IsAlive)
                {
                    var subscriber = (IHandles<TEvent>)weakSubscriber.Target;
                    var syncContext = SynchronizationContext.Current ?? new SynchronizationContext();

                    syncContext.Post(s => subscriber.OnEvent(eventToPublish), null);
                }
                else
                {
                    subscribersToRemove.Add(weakSubscriber);
                }
            }

            if (!subscribersToRemove.Any()) return;
            lock (_lock)
            {
                foreach (var remove in subscribersToRemove)
                    subscribers.Remove(remove);
            }
        }

        private List<WeakReference> GetSubscribers(Type subscriberType)
        {
            List<WeakReference> subscribers;
            lock (_lock)
            {
                var found = _eventSubscriberLists.TryGetValue(subscriberType, out subscribers);
                if (!found)
                {
                    subscribers = new List<WeakReference>();
                    _eventSubscriberLists.Add(subscriberType, subscribers);
                }
            }
            return subscribers;
        }
    }
}
