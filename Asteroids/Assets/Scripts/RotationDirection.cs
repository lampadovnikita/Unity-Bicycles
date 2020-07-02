public enum RotationDirection
{
    Right,
    Left
}

public static class RotationDirectionExtensions
{
    private static float[] _rotationValues = {
        -3f,
        3f
    };

    public static float GetTorque(this RotationDirection torqueDirection) {
        return _rotationValues[(int)torqueDirection];
    }
}
