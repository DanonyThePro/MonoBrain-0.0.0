
using System.Numerics;

namespace MonoBrain.GameEngineScripts;

public class Square
{
    public Vector Postion;
    public Vector Scale;
    

    public Square(Vector postion, Vector scale)
    {
        Postion = postion;
        Scale = scale;
    }
    
    public double Left => Postion.X - Scale.X / 2;
    public double Right => Postion.X + Scale.X / 2;
    public double Up => Postion.Y - Scale.Y / 2;
    public double Down => Postion.Y + Scale.Y / 2;
}