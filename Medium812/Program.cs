using System;
using System.Collections.Generic;

namespace Medium812
{
    public class MyObjectsController
    {
        private readonly Random _random = new Random();
        private readonly List<MyObject> _myObjects = new List<MyObject>();

        public MyObjectsController()
        {
            for (int i = 1; i < 4; i++)
                _myObjects.Add(new MyObject(i.ToString(), 5 * i, 5 * i));
        }

        public void CheckPositionMyObjects()
        {
            foreach (var firstMyObject in _myObjects)
            {
                foreach (var secondMyObject in _myObjects)
                {
                    if (firstMyObject.Name != secondMyObject.Name && firstMyObject.EqualsPosition(secondMyObject))
                    {
                        firstMyObject.IsAlive = false;
                        secondMyObject.IsAlive = false;
                    }
                }
            }
        }

        public void UpdatePositionMyObjects()
        {
            foreach (var myObject in _myObjects)
            {
                myObject.SetPosition(_random);
            }
        }

        public IEnumerable<MyObject> GetAliveMyObjects()
        {
            foreach (var myObject in _myObjects)
            {
                if (myObject.IsAlive)
                    yield return myObject;
            }
        }
    }

    public static class Extensions
    {
        public static bool EqualsPosition(this MyObject firstMyObject, MyObject secondMyObject)
        {
            return firstMyObject.X == secondMyObject.X && firstMyObject.Y == secondMyObject.Y;
        }
    }

    public class MyObject
    {
        public bool IsAlive { get; set; } = true;
        public string Name { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        public MyObject(string name, int x, int y)
        {
            Name = name;
            X = x;
            Y = y;
        }

        public void SetPosition(Random random)
        {
            X += random.Next(-1, 1);
            Y += random.Next(-1, 1);
            if (X < 0)
                X = 0;
            if (Y < 0)
                Y = 0;
        }
    }

    class Program
    {
        private static MyObjectsController _controller;

        static void Main(string[] args)
        {
            Initialize();

            while (true)
            {
                _controller.CheckPositionMyObjects();
                _controller.UpdatePositionMyObjects();

                foreach (var myObject in _controller.GetAliveMyObjects())
                {
                    Console.SetCursorPosition(myObject.X, myObject.Y);
                    Console.Write(myObject.Name);
                }
            }
        }

        private static void Initialize()
        {
            _controller = new MyObjectsController();
        }
    }
}
