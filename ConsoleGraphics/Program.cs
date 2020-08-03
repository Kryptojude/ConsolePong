using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Diagnostics;
using System.Windows.Input;

namespace ConsoleGraphics
{
    class Program
    {
        public class Ball
        {
            static public int X = 50;
            static public int Y = 2;
            static public int[] Vector = new int[2] { 1, 1 };
            static public int interval = 100;

            static public void Move()
            {
                //Erase
                Console.SetCursorPosition(X, Y);
                Console.Write(" ");
                //Write
                Console.SetCursorPosition(X + Vector[0], Y + Vector[1]);
                Console.Write("●");

                X += Vector[0];
                Y += Vector[1];
            }

        }

        public class Player
        {
            static private int x = 50;
            static public int X
            {
                get { return x; }
                set
                {
                    if (value > 0 && value < Console.WindowWidth - Width)
                    {
                        if (value > x) //Um 1 nach rechts
                        {
                            //Erase
                            Console.SetCursorPosition(x, Y);
                            Console.Write(" ");
                            //Write
                            Console.SetCursorPosition(value + Width - 1, Y);
                            Console.Write("█");
                        }
                        else //Um 1 nach links
                        {
                            //Write
                            Console.SetCursorPosition(value, Y);
                            Console.Write("●");
                            //Erase
                            Console.SetCursorPosition(x + Width - 1, Y);
                            Console.Write(" ");
                        }

                        x = value;
                    }
                }
            }
            public const int Y = 25;
            public const int Width = 8;
        }

        [STAThread]
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BufferWidth = Console.WindowWidth;
            Console.BufferHeight =  Console.WindowHeight;
            Console.CursorVisible = false;

            //Initial player drawing
            for (int x = Player.X; x < Player.X + Player.Width; x++)
            {
                Console.SetCursorPosition(x, Player.Y);
                Console.Write("●");
            }

            //Initial ball drawing
            Console.SetCursorPosition(Ball.X, Ball.Y);
            Console.Write("●");

            DrawBorders();

            Movement();

        }

        private static void DrawBorders()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("╔");
            for (int x = 1; x < Console.WindowWidth - 1; x++)
                Console.Write("═");
            Console.Write("╗");
            Console.SetCursorPosition(0, 28);
            Console.Write("╚");
            for (int x = 1; x < Console.WindowWidth - 1; x++)
                Console.Write("═");
            Console.Write("╝");
            for (int y = 1; y < Console.WindowHeight - 2; y++)
            {
                Console.SetCursorPosition(0, y);
                Console.Write("║");
            }
            for (int y = 1; y < Console.WindowHeight - 2; y++)
            {
                Console.SetCursorPosition(Console.WindowWidth - 1, y);
                Console.Write("║");
            }
        }

        private static void Movement()
        {
            Stopwatch watch1 = new Stopwatch();
            Stopwatch watch2 = new Stopwatch();
            watch1.Start();
            watch2.Start();
            while (true)
            {
                if (watch1.ElapsedMilliseconds >= 50)
                {
                    if (Keyboard.IsKeyDown(Key.Left))
                        Player.X--;
                    if (Keyboard.IsKeyDown(Key.Right))
                        Player.X++;

                    watch1.Restart();
                }
                if (watch2.ElapsedMilliseconds >= Ball.interval)
                {
                    if (Ball.X == 1 || Ball.X == Console.WindowWidth - 2) //Left right bounds
                        Ball.Vector[0] *= -1;
                    if (Ball.Y == 1) //top bounds
                        Ball.Vector[1] *= -1;
                    if (Ball.Y == Player.Y - 1 && Ball.X >= Player.X && Ball.X <= Player.X + Player.Width) //player bounds
                    {
                        Ball.Vector[1] *= -1;
                        Ball.interval -= Ball.interval/10;
                    }

                    Ball.Move();

                    watch2.Restart();
                }
            }
        }

        
    }
}
