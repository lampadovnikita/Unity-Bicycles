using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ForceDirection
{
    Up,
    Down
}

public static class ForceDirectionExtensions
{
    private static Vector3[] _forceVectors = {
        new Vector3(0f, 30f, 0f),
        new Vector3(0f, -30f, 0f),
    };

    public static Vector3 GetForce(this ForceDirection forceDirection)
    {
        return _forceVectors[(int)forceDirection];
    }
}
