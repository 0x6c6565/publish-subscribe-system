using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace PublishSubscribeSystem
{
    public interface ISubscriber<T> : ISubscriber where T : unmanaged, IEquatable<T>, IComparable<T>
    {
        void OnNotify(in T message);
    }

    public interface ISubscriber { }
}
