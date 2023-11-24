using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace PublishSubscribeSystem
{
    internal class Subscription<T> : Subscription where T : unmanaged, IMessage<T>, IEquatable<T>, IComparable<T>
    {
        MessageHeap<T> messages = new MessageHeap<T>();
        List<ISubscriber<T>> subscribers = new List<ISubscriber<T>>();

        protected internal override int Count => messages.Count;

        protected internal override void Publish()
        {
            if (0 < messages.Count)
            {
                T message = messages.Dequeue();

                foreach (ISubscriber<T> subscriber in subscribers)
                {
                    subscriber.OnNotify(in message);
                }
            }
        }

        protected internal override void Clear() => messages.Clear();

        internal void Enqueue(T item) => messages.Enqueue(item);

        protected internal override void Subscribe(ISubscriber subscriber)
            => Subscribe(subscriber as ISubscriber<T>);
        internal void Subscribe(ISubscriber<T> subscriber)
        {
            if (null != subscriber && !subscribers.Contains(subscriber))
            {
                subscribers.Add(subscriber);
            }
        }

        protected internal override void Unsubscribe(ISubscriber subscriber)
        {
            if (subscriber is ISubscriber<T> sub && subscribers.Contains(sub))
            {
                subscribers.Remove(sub);
            }
        }

        protected internal override void UnsubscribeAll() => subscribers.Clear();
    }

    internal abstract class Subscription
    {
        protected internal abstract int Count { get; }

        protected internal abstract void Publish();

        protected internal abstract void Clear();

        protected internal abstract void Subscribe(ISubscriber subscriber);

        protected internal abstract void UnsubscribeAll();
        protected internal abstract void Unsubscribe(ISubscriber subscriber);
    }
}
