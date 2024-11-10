using System;
using System.Numerics;
using Microsoft.Xna.Framework.Input;

namespace MonoBrain.GameEngineScripts
{
    public class Input : IMonoBrain
    {
        public static KeyboardState CurrentKeyBoardState;
        public static KeyboardState PreviousKeyBoardState;
        public static MouseState CurrentMouseState;
        public static MouseState PreviousMouseState;

        public void Start() { }
        public void Update()
        {
            PreviousKeyBoardState = CurrentKeyBoardState;
            PreviousMouseState = CurrentMouseState;

            CurrentKeyBoardState = Keyboard.GetState();
            CurrentMouseState = Mouse.GetState();
        }

        public static bool KeyPressed(Keys key) => CurrentKeyBoardState.IsKeyDown(key) && !PreviousKeyBoardState.IsKeyDown(key);
        public static bool KeyDown(Keys key) => CurrentKeyBoardState.IsKeyDown(key);


        public static bool IsMousePressed(int button)
        {
            switch(button)
            {
                case 0:
                    return CurrentMouseState.LeftButton == ButtonState.Pressed 
                        && PreviousMouseState.LeftButton == ButtonState.Released;
                case 1:
                    return CurrentMouseState.RightButton == ButtonState.Pressed 
                        && PreviousMouseState.RightButton == ButtonState.Released;
                case 2:
                    return CurrentMouseState.MiddleButton == ButtonState.Pressed 
                        && PreviousMouseState.MiddleButton == ButtonState.Released;
                default: 
                    return false;

            }
        }

        public static Vector MousePosition() => new (CurrentMouseState.Position.X, CurrentMouseState.Position.Y);

        public static double GetInputAxis(string input)
        {
            switch (input.ToLower())
            {
                case "horizontal":
                    if (KeyDown(Keys.D)) return 1;
                    if (KeyDown(Keys.A)) return -1;
                    break;

                case "vertical":
                    if (KeyDown(Keys.W)) return -1;
                    if (KeyDown(Keys.S)) return 1;
                    break;
                default:
                    Console.WriteLine($"{input} is not a valid input");
                    break;
            }

            return 0;
        }

        public static Vector GetInputButton(string input)
        {
            switch (input.ToLower())
            {
                case "jump" :
                    if (KeyDown(Keys.Space)) return new Vector(0, -4);
                    break;
            }
            return Vector.Zero;
        }
    }
}
