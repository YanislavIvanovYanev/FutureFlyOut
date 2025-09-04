using UnityEngine;

public static class MathUtil
{
    public static float RandomSign(float input = 1f) => input * Random.value < .5f ? -1f : 1f;
}
