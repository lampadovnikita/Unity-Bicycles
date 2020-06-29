using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TorqueDirection
{
    Right,
    Left
}

public static class TorqueDirectionExtensions
{
    private static float[] _torqueValues = {
        -1f,
        1f
    };

    public static float GetTorque(this TorqueDirection torqueDirection) {
        return _torqueValues[(int)torqueDirection];
    }
}
