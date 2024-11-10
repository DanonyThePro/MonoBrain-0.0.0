using System;
using Microsoft.Xna.Framework;


namespace MonoBrain.GameEngineScripts;

public class Vector
{
    public double X { get; set; }
    public double Y { get; set; }

    public Vector(double x, double y)
    {
        X = x;
        Y = y;
    }

    public static Vector Zero => new (0, 0);
    public Vector2 ToVector2 => new ((float)X, (float)Y);

    
    public Vector Normalize()
    {
        double c = Math.Sqrt(X * X + Y * Y);
        if (c > 0)
        {
            return new Vector(X / c, Y / c);
        }
        return this;
    }
    
    
    public static Vector operator +(Vector v1, Vector v2) => new (v1.X + v2.X, v1.Y + v2.Y);
    public static Vector operator -(Vector v1, Vector v2) => new (v1.X - v2.X, v1.Y - v2.Y);
    public static Vector operator /(Vector v1, double d) => new (v1.X / d, v1.Y / d);
    public static Vector operator *(Vector v1 , double d) => new (v1.X * d, v1.Y * d);
}