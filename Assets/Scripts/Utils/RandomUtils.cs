using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomUtils
{
    public static int GetRandomValueFromRange(int minValue, int maxValueExclusive)
    {
        return Random.Range(minValue, maxValueExclusive);
    }
}
