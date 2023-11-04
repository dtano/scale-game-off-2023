using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EmployeeUtils
{
    public static int DetermineRandomWeight(int minWeight, int maxWeight)
    {
        int randomValue = Random.Range(minWeight, maxWeight);
        return randomValue;
    }
}
