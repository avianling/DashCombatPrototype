using System;
using UnityEngine;

/// <summary>
///  Contains extension methods and static helpers for 
/// </summary>

// extension methods
public static class VectorUtil
{
    //////////////////////
    // Extension Methods:
    //////////////////////

    // Extension Method
    public static Vector2 Clone(this Vector2 self)
    {
        return new Vector2(self.x, self.y);
    }

    // Extension Method
    public static Vector3 Clone(this Vector3 self)
    {
        return new Vector3(self.x, self.y, self.z);
    }

    // Extension Method
    public static float GetAngle(this Vector2 self)
    {
        return Mathf.Atan2(self.y, self.x);
    }

    // Extension Method
    /// <summary>
    /// get the angle on the 2D plane 
    /// </summary>
    /// <returns>float</returns>
    public static float GetAngle2D(this Vector3 self)
    {
        return Mathf.Atan2(self.y, self.x);
    }

    // Extension Method
    public static float GetAngleDeg(this Vector2 self)
    {
        return Mathf.Atan2(self.y, self.x) * 57.2957f;
    }

    // Extension Method
    public static Vector2 GetPerpendicular(this Vector2 self)
    {
        return new Vector2(-self.y, self.x);
    }

    // Extension Method
    public static float DistanceTo(this Vector2 self, Vector2 v)
    {
        Vector2 distanceVector = new Vector2(v.x - self.x, v.y - self.y);
        return distanceVector.magnitude;
    }

    // extension method
    public static float Dot(this Vector2 self, Vector2 v)
    {
        return Vector2.Dot(self, v); // self.x * v.x + self.y * v.y;
    }

    // extension method
    public static float Cross(this Vector2 self, Vector2 v)
    {
        return (self.x * v.y) - (self.y * v.x);
    }

    //////////////////////
    // static helpers:
    //////////////////////

    public static Vector2 SetLength(Vector2 vec, float l)
    {
        if (l == 0)
        {
            return Vector2.zero;
        }

        float length = vec.magnitude; // Mathf.Sqrt(self.x * self.x + self.y * self.y);
        if (length == 0)
        {
            vec.y = 1;
            length = 1;
        }

        vec.x = (vec.x / length) * l;
        vec.y = (vec.y / length) * l;
        return vec;
    }

    /// <summary>
    /// Performs a length operation on only x and y, as if it were a Vector2d
    /// </summary>
    public static Vector3 Set2DLength(Vector3 vec, float l)
    {
        float length2D = Mathf.Sqrt(vec.x * vec.x + vec.y * vec.y);
        if (length2D == 0) vec.y = 1;
        vec.x = (vec.x / length2D) * l;
        vec.y = (vec.y / length2D) * l;
        // do nothing with z
        return vec;
    }

    public static Vector2 SetAngle(Vector2 vec, float a)
    {
        float length = Mathf.Sqrt(vec.x * vec.x + vec.y * vec.y);
        vec.x = Mathf.Cos(a) * length;
        vec.y = Mathf.Sin(a) * length;
        return vec;
    }

    public static Vector2 SetAngleDeg(Vector2 vec, float a)
    {
        float length = Mathf.Sqrt(vec.x * vec.x + vec.y * vec.y);
        a *= 0.0174532925f;
        vec.x = Mathf.Cos(a) * length;
        vec.y = Mathf.Sin(a) * length;
        return vec;
    }

    public static Vector2 RotateByDegrees(Vector2 vec, float degrees)
    {
        // convert to radians. TODO: make this all work end to end
        return RotateByRad(vec, degrees * Mathf.Deg2Rad);
    }

    /// <summary>
    /// Rotate in radians
    /// </summary>
    //public static Vector2 RotateBy(Vector2 vec, float radians)
    //{
    //    float angle = vec.GetAngle(); 
    //    float length = Mathf.Sqrt(vec.x * vec.x + vec.y * vec.y);
    //    vec.x = Mathf.Cos(radians + angle) * length;
    //    vec.y = Mathf.Sin(radians + angle) * length;

    //    return vec;
    //}


    public static Vector2 RotateByRad(Vector2 v, float radians)
    {
        float sin = Mathf.Sin(radians);
        float cos = Mathf.Cos(radians);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    //If acos recieves is over 1.0, which sometimes happens because of floating point inaccuracy, it breaks
    public static float AngleTo(this Vector2 self, Vector2 other)
    {
        float angle = Vector2.Angle(self, other);

        float cross = self.Cross(other);

        if (cross < 0)
        {
            angle = -angle;
        }

        return angle * Mathf.Deg2Rad;
    }


    /// <summary>
    /// Rotates the vector toward a Point(ie a partial "Look At")
    /// NOTE: angles are in degrees
    /// </summary>
    public static Vector2 RotateTowardPoint(Vector2 vec, Vector2 point, float maxRotation)
    {
        float angle = vec.AngleTo(point);

        if (Mathf.Abs(angle) > maxRotation)
        {
            angle = maxRotation * ((angle > 0f) ? 1.0f : -1.0f);
        }

        return VectorUtil.RotateByRad(vec, angle);
    }

    /// <summary>
    /// Rotates the vector toward a Point(ie a partial "Look At")
    /// </summary>
    public static Vector2 RotateTowardPoint(Vector2 vec, Vector3 point, float maxRotation)
    {
        return VectorUtil.RotateTowardPoint(vec, new Vector2(point.x, point.y), maxRotation);
    }

    public static Vector2 RotateTowardPoint(Vector2 vec, float x, float y, float maxRotation)
    {
        return VectorUtil.RotateTowardPoint(vec, new Vector2(x, y), maxRotation);
    }

    public static Vector2 RotateTowardAngle(Vector2 vec, float targetAngle, float maxRotation)
    {
        float currentAngle = vec.GetAngle();
        float diffAngle = Mathf.Atan2(
            Mathf.Sin(targetAngle - currentAngle),
            Mathf.Cos(targetAngle - currentAngle));

        if (Mathf.Abs(diffAngle) > maxRotation)
        {
            diffAngle = maxRotation * ((diffAngle > 0f) ? 1.0f : -1.0f);
        }

        return VectorUtil.SetAngle(vec, currentAngle + diffAngle);
    }

    //void rotateTowardVector(Vector2D otherVector, float maxRotation)
    //{

    //    float angle = angleTo(otherVector);
    //    if (fabs(angle) > maxRotation)
    //    {
    //        angle = maxRotation * ((angle > 0) ? 1.0 : -1.0);
    //    }

    //    rotateBy(angle);
    //}


    /// <summary>
    /// Rotates the vector away from point
    /// </summary>
    public static Vector2 RotateAwayFromPoint(Vector2 vec, Vector2 point, float maxRotation)
    {
        // get the negative of the angle
        float angleToTarget = vec.AngleTo(point);
        float angle;
        if (angleToTarget <= 0)
        {
            angle = maxRotation;
        }
        else
        {
            angle = -maxRotation;
        }

        // no not look so far away that it comes back around        
        // (assumes this is in degrees, not radians)
        //if (Mathf.Abs(angleToTarget) > (Mathf.PI - maxRotation))
        //{
        //    maxRotation = Mathf.PI - Mathf.Abs(angleToTarget);
        //}

        return VectorUtil.RotateByRad(vec, angle);
    }

    /// <summary>
    /// Rotates the vector away from point
    /// </summary>
    public static Vector2 RotateAwayFromPoint(Vector2 vec, Vector3 point, float maxRotation)
    {
        return VectorUtil.RotateAwayFromPoint(vec, new Vector2(point.x, point.y), maxRotation);
    }

    /// <summary>
    /// Rotates the vector away from point
    /// </summary>
    public static Vector2 RotateAwayFromPoint(Vector2 vec, float x, float y, float maxRotation)
    {
        return VectorUtil.RotateAwayFromPoint(vec, new Vector2(x, y), maxRotation);
    }


    public static Vector2 LookAt(Vector2 vec, Vector2 v)
    {
        Vector2 vectorToTarget = new Vector2(v.x - vec.x, v.y - vec.y);
        return VectorUtil.SetAngle(vec, vectorToTarget.GetAngle());
    }

    public static void Reflect(this Vector2 self, Vector2 v)
    {
        //Vector2 unitNormal = v.Unit();
        //*this = *this - (unitNormal * ((unitNormal.dot(*this)) * 2));
    }

    public static Vector2 Unit(this Vector2 self)
    {
        Vector2 unit = self.Clone();
        unit.Normalize();
        return unit;
    }

    public static Vector2 Invert(Vector2 vec)
    {
        vec.x = -vec.x;
        vec.y = -vec.y;
        return vec;
    }
    

}

