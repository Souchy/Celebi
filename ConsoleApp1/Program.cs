﻿using Celebi.common;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            //new Celebi();
            //Celebi.common.Celebi.main(null);
            var c = new PlayfabClient();
            c.thing();


            Console.ReadLine();
        }
    }
}