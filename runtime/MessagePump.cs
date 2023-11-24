using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace PublishSubscribeSystem
{
    public class MessagePump
    {
        Dictionary<Type, Subscription> subscriptions = new Dictionary<Type, Subscription>();

        public void Publish()
        {
            foreach (KeyValuePair<Type, Subscription> subscription in subscriptions)
            {
                subscription.Value.Publish();
            }
        }

        public void Enqueue<T>(in T item) where T : unmanaged, IMessage<T>, IEquatable<T>, IComparable<T>
        {
            if (subscriptions.ContainsKey(typeof(T)))
            {
                (subscriptions[typeof(T)] as Subscription<T>).Enqueue(item);
            }
        }

        public void Subscribe<T>(ISubscriber<T> subscriber) where T : unmanaged, IMessage<T>, IEquatable<T>, IComparable<T>
        {
            if (!subscriptions.ContainsKey(typeof(T)))
            {
                subscriptions.Add(typeof(T), new Subscription<T>());
            }

            Subscription<T> subscription = subscriptions[typeof(T)] as Subscription<T>;
            subscription.Subscribe(subscriber);
        }

        public void Clear<T>() where T : unmanaged, IMessage<T>, IEquatable<T>, IComparable<T>
        {
            if (subscriptions.ContainsKey(typeof(T)))
            {
                subscriptions[typeof(T)].Clear();
            }
        }

        public void UnsubscribeAll<T>() where T : unmanaged, IMessage<T>, IEquatable<T>, IComparable<T>
        {
            if (subscriptions.ContainsKey(typeof(T)))
            {
                subscriptions[typeof(T)].UnsubscribeAll();
            }
        }

        public void Unsubscribe<T>(ISubscriber<T> subscriber) where T : unmanaged, IMessage<T>, IEquatable<T>, IComparable<T>
        {
            if (subscriptions.ContainsKey(typeof(T)))
            {
                subscriptions[typeof(T)].Unsubscribe(subscriber);
            }
        }
    }
}
