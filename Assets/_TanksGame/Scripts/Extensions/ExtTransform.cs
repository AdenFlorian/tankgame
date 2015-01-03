using UnityEngine;
using System.Collections;

namespace Extensions
{
    public static class ExtTransform
    {
        public static float PosX(this Transform transform)
        {
            return transform.position.x;
        }
    }
}