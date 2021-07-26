using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class defines in which way distance between points will be calculated - on Sphere, including Y coordinate, or on Cylinder, where
//Y coordinate is ignored

[System.Serializable]
public static class DistanceMeter
{
    public enum DistanceMode
    {
        Cylinder,
        Sphere
    };

    public static float CalculateLogicalDistance(Vector3 from, Vector3 to, DistanceMode shape)
    {
        switch (shape)
        {
            case DistanceMode.Cylinder:
                {
                    return CalculateCylinderDistance(from, to);
                }
            case DistanceMode.Sphere:
                {
                    return CalculateSphereDistance(from, to);
                }
            default:
                {
                    return 0;
                }
        }
    }

    private static float CalculateCylinderDistance(Vector3 from, Vector3 to)
    {
        return GeometryMath.VectorLength(new Vector3(to.x - from.x, 0, to.z - from.z));
    }

    private static float CalculateSphereDistance(Vector3 from, Vector3 to)
    {
        return GeometryMath.VectorLength(to - from);
    }

    public static float CalculateMaxDistanceWithinShape(Vector3 from, Vector3 to, float maxLogicalRange, DistanceMode shape)
    {
        float groundDistanceToTarget = CalculateLogicalDistance(from, to, shape);
        Vector3 direction = to - from;
        switch (shape)
        {
            case DistanceMode.Cylinder:
                {
                    //Return actual range which never will go outside the cylinder
                    return groundDistanceToTarget / Mathf.Sin(Vector3.Angle(direction, new Vector3(0, -1, 0)) * Mathf.PI / 180);
                }
            case DistanceMode.Sphere:
                {
                    return groundDistanceToTarget;
                }
            default:
                {
                    return 0;
                }
        }
    }
}
