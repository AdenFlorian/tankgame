using System.Collections;
using UnityEngine;

public static class ExtTransform
{
    public static float PosX(this Transform transform)
    {
        return transform.position.x;
    }

    // <summary>Sets position and rotation to all 0s and scale to all 1s</summary>
    public static void SetToDefaults(this Transform transform)
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }
}
