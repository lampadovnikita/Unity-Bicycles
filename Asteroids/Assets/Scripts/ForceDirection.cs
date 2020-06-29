using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ForceDirection
{
    Up,
    Down
}

public static class DirectionExtensions
{
    private static Vector3[] _movements = {
        new Vector3(0f, 50f, 0f),
        new Vector3(0f, -50f, 0f),
    };

    public static Vector3 GetForce(this ForceDirection direction)
    {
        return _movements[(int)direction];
    }
}
