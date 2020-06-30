using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationDirection
{
    Right,
    Left
}

public static class RotationDirectionExtensions
{
    private static float[] _rotationValues = {
        -5f,
        5f
    };

    public static float GetTorque(this RotationDirection torqueDirection) {
        return _rotationValues[(int)torqueDirection];
    }
}
