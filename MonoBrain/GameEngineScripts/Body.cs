using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;


namespace MonoBrain.GameEngineScripts
{
    public class Body
    {
        public static List<Body> Bodies = new();

        // sprite settings
        public Texture2D Sprite;
        public string Directory;
        public Vector Position;
        public Vector PrePosition = Vector.Zero;
        public Vector DPosition = Vector.Zero;
        public Vector Scale;
        public double Rotation;


        // physics
        public Vector Velocity;
        public Vector Force;
        public double Mass;

        public Vector Offset;
        
        public Body(string directory, Vector position, Vector scale = null, double mass = 1, double rotation = 0 , Vector offset = null)
        {
            // sprite settings
            Directory = directory;
            Position = position;
            Rotation = rotation;
            Scale = scale ?? new Vector(5 , 5);
            Offset = offset ?? Vector.Zero; // for the hit-box primarily
            
            
            // physics
            Velocity = Vector.Zero;
            Force = Vector.Zero;
            Mass = mass;

            Bodies.Add(this);
        }


        public Vector Size => new Vector(Math.Abs(Sprite.Width * Scale.X), Math.Abs(Sprite.Height * Scale.Y));
        public Square Square => new Square(Position , new Vector(Size.X , Size.Y));
        
        /// <summary>
        /// If set to false makes an Object disappear by removing it from the rendered list.
        /// </summary>
        /// <param name="active"></param>
        public void SetActive(bool active = true)
        {
            if (active == Bodies.Contains(this)) return;
            if (active)
            {
                Bodies.Add(this);
            }
            else
            {
                Bodies.Remove(this);
            }
        }
        
        /// <summary>
        /// adds a physical force to the body (e.g. gravity)
        /// </summary>
        public void AddForce(Vector force)
        {
            Force = force;
            Vector acceleration = force / Mass;
            Velocity += Force * 10;
        }

        /// <summary>
        /// AABB collision detection.
        /// I don't even know what AABB stands for :)
        /// </summary>
        /// <returns>boolean (True or False)</returns>
        public bool Collision2D()
        {
            bool Collision = false;
            foreach (Body body in Bodies)
            {
                if (Square.Right > body.Square.Left &&
                    Square.Left < body.Square.Right &&
                    Square.Down > body.Square.Up &&
                    Square.Up < body.Square.Down &&
                    body != this)
                {
                    Collision = true;
                }
            }

            return Collision;
        }
        // ! ! ! add collision / sprite mask ! ! !
        // it'll make your collisions a LOT more accurate
        // if you want you can try and make a square of the mask that is just :
        // up = highest pixel that's not transparent
        // down = lowest pixel that's not transparent
        // left = most left pixel that's not transparent (leftest jk)
        // right = most right pixel that's not transparent (rightest lol)
        
        // use the starbound collisions only if necessary
        
        /// <summary>
        /// Detects collision with another object
        /// </summary>
        /// <param name="body"></param>
        /// <returns>boolean (True or False)</returns>
        public bool CollisionB(Body body) => Square.Right > body.Square.Left &&
                                         Square.Left < body.Square.Right &&
                                         Square.Up < body.Square.Down &&
                                         Square.Down > body.Square.Up;
    }
}
