using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up,
    Right,
    Bottom,
    Left
}

public static class DirectionExtensions
{
    private static Vector3[] movements = {
        new Vector3(0f, 0.1f, 0f),
        new Vector3(0.1f, 0f, 0f),
        new Vector3(0f, -0.1f, 0f),
        new Vector3(-0.1f, 0f, 0f)
    };

    public static Vector3 GetMovement(this Direction direction)
    {
        return movements[(int)direction];
    }
}
