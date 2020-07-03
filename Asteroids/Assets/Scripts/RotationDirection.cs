public enum RotationDirection
{
    Right,
    Left
}

public static class RotationDirectionExtensions
{
    private static float[] _rotationValues = {
        -1f,
        1f
    };

    public static float GetRoataion(this RotationDirection rotationDirection) {
        return _rotationValues[(int)rotationDirection];
    }
}
