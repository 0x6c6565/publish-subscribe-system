using System;
using System.Collections.Generic;

namespace PublishSubscribeSystem
{
    public class MessagePump
    {
        SortedList<int, Subscription> subscriptions = new SortedList<int, Subscription>();

        public bool Publish()
        {
            bool hasMessages = false;
            for (int n = 0; n < subscriptions.Values.Count; ++n)
            {
                subscriptions.Values[n].Publish();
                hasMessages |= 0 < subscriptions.Values[n].Count;
            }

            return hasMessages;
        }

        public void Enqueue<T>(in T item) where T : unmanaged, IMessage<T>, IEquatable<T>, IComparable<T>
        {
            if (subscriptions.ContainsKey(typeof(T).GetHashCode()))
            {
                (subscriptions[typeof(T).GetHashCode()] as Subscription<T>).Enqueue(item);
            }
        }

        public void Subscribe<T>(ISubscriber<T> subscriber) where T : unmanaged, IMessage<T>, IEquatable<T>, IComparable<T>
        {
            if (!subscriptions.ContainsKey(typeof(T).GetHashCode()))
            {
                subscriptions.Add(typeof(T).GetHashCode(), new Subscription<T>());
            }

            Subscription<T> subscription = subscriptions[typeof(T).GetHashCode()] as Subscription<T>;
            subscription.Subscribe(subscriber);
        }

        public void Clear<T>() where T : unmanaged, IMessage<T>, IEquatable<T>, IComparable<T>
        {
            if (subscriptions.ContainsKey(typeof(T).GetHashCode()))
            {
                subscriptions[typeof(T).GetHashCode()].Clear();
            }
        }

        public void UnsubscribeAll<T>() where T : unmanaged, IMessage<T>, IEquatable<T>, IComparable<T>
        {
            if (subscriptions.ContainsKey(typeof(T).GetHashCode()))
            {
                subscriptions[typeof(T).GetHashCode()].UnsubscribeAll();
            }
        }

        public void Unsubscribe<T>(ISubscriber<T> subscriber) where T : unmanaged, IMessage<T>, IEquatable<T>, IComparable<T>
        {
            if (subscriptions.ContainsKey(typeof(T).GetHashCode()))
            {
                subscriptions[typeof(T).GetHashCode()].Unsubscribe(subscriber);
            }
        }
    }
}
