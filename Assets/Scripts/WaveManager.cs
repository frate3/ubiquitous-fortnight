using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static int startingAmount = 5;
    public static Vector3 randomVector3(Transform host, float min, float max)
    {
        Vector3 basePosition = host.position;
        float offsetX = Random.Range(min, max);
        float offsetZ = Random.Range(min, max);
        basePosition += new Vector3(offsetX, 0, offsetZ);
        return basePosition;

    }

}
