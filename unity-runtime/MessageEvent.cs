using System;

using UnityEngine;
using UnityEngine.Events;

namespace PublishSubscribeSystem
{
    [Serializable]
    public class MessageEvent<T>: UnityEvent<T> where T : unmanaged, IEquatable<T>, IComparable<T>
    {

    }
}
