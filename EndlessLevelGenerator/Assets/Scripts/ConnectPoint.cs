// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public enum Edge
    {
        Left = 0,
        Right = 1,
    }

    public sealed class ConnectPoint : MonoBehaviour
    {
        public Edge edge;
    }
}