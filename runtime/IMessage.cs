using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace PublishSubscribeSystem
{
    public interface IMessage<T> where T : unmanaged, IEquatable<T>, IComparable<T>
    {

    }
}
