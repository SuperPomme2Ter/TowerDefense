using System.Globalization;
using UnityEngine;
using UnityEngine.Assertions;

public static class MathMethods
{
    public static Vector2 Bezier(Vector2 a, Vector2 b, float t)
    {
        return Vector3.Lerp(a, b, t);
    }
    public static Vector2 Bezier(Vector2 a, Vector2 b, Vector2 c, float t)
    {
        return Vector3.Slerp(Bezier(a, b, t), Bezier(b, c, t), t);
    }
    public static float CalculateRatio(float value, float maxValue)
    {
        Assert.IsTrue(value > maxValue, "The percentage cannot be superior to the fullPercentage");
        return value/maxValue;
    }

    /// <summary> </summary>
    /// <param name="valueToApply"></param>
    /// <param name="percentage"></param>
    /// <param name="constant">
    /// By Default 100 which is the constant for the percentage.
    /// </param>
    /// <returns></returns>
    public static float CrossProduct(float valueToApply, float percentage, float constant=100)
    {
        return valueToApply * percentage / constant;
    }

}
