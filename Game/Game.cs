using System;
using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoBrain.GameEngineScripts;
using Vector = MonoBrain.GameEngineScripts.Vector;

namespace Game;

public class Game : IMonoBrain
{
    private Body player = new Body("duck", new Vector(250, 500));
    private Body b = new Body("character", new Vector(250, 1000) , scale: new Vector(20 , 5));
    private Body b1 = new Body("character", new Vector(250, 0));

    private Vector Gravity = new Vector(0, 0.2);
    private Vector Jump = Vector.Zero;

    public void Start() { }

    public void Update()
    {
        

        player.Velocity.X = Input.GetInputAxis("Horizontal") * 10;
        Jump = Input.GetInputButton("Jump");
        

        if (player.Collision2D())
        {
            player.Velocity = Vector.Zero;
            player.AddForce(Jump);
        }
        else
        {        
            player.AddForce(Gravity);
        }
        

        if (Input.KeyPressed(Keys.E))
        {
            player.Scale.Y *= -1;
            Gravity *= -1;

        }

        Console.WriteLine(player.Collision2D());
    }
}