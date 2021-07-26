using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GeometryMath
{
    public static float VectorLength(Vector3 vector)
    {
        return Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
    }

    public static float VectorLength(Vector2 vector)
    {
        return Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y);
    }

    public static float distanceBetweenPoints(Vector3 pt1, Vector3 pt2)
    {
        return VectorLength(new Vector3(
            pt2.x - pt1.x,
            pt2.y - pt1.y,
            pt2.z - pt1.z
            ));
    }

    public static bool itIsCollision(Vector3 pt1, Vector3 pt2, float xDiff, float yDiff, float zDiff)
    {
        if ((Mathf.Abs(pt1.x - pt2.x) < xDiff) &&
            (Mathf.Abs(pt1.y - pt2.y) < yDiff) &&
            (Mathf.Abs(pt1.z - pt2.z) < zDiff))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool itIsCollision(Vector3 pt1, Vector3 pt2, float Diff)
    {
        return itIsCollision(pt1, pt2, Diff, Diff, Diff);
    }

    public static Vector3 goToPosition(Vector3 oldPosition, Vector3 directionVector, float koef)
    {         
        Vector3 finalPosition = new Vector3(
            oldPosition.x + directionVector.x * koef,
            oldPosition.y + directionVector.y * koef,
            oldPosition.z + directionVector.z * koef);
        return finalPosition;
    }

    public static Vector3 goToPosition(Vector3 oldPosition, Vector3 directionVector, Vector3 destinationPoint, float koef, out float distanceCutted)
    {
        Vector3 vectorToDestinationPoint = destinationPoint - oldPosition;
        Vector3 vectorPassed = directionVector * koef;
        distanceCutted = VectorLength(vectorPassed) - VectorLength(vectorToDestinationPoint);
        Vector3 finalPosition = new Vector3();
        if (distanceCutted > 0)
        {
            finalPosition = oldPosition + vectorToDestinationPoint;
        }
        else
        {
            finalPosition = oldPosition + vectorPassed;
        }
        return finalPosition;
    }

    public static Vector3 normilizeVector(Vector3 vector)
    {
        Vector3 newVector = vector;
        float length = VectorLength(vector);
        if (length == 0)
        {
            return new Vector3(0, 0, 0);
        }
        newVector.x /= length;
        newVector.y /= length;
        newVector.z /= length;
        return newVector;
    }

    public static bool ItIsNullVector(Vector3 vector)
    {
        return ((vector.x == 0) && (vector.y == 0) && (vector.z == 0));
    }

    public static float DistanceFromPointToLine(Vector3 point, Vector3 lineVector)
    {
        Vector3 vectorToPoint = lineVector - point;
        return VectorLength(Vector3.Cross(vectorToPoint, lineVector)) / VectorLength(lineVector);       
    }
}
