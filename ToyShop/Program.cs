using System;

namespace ToyShop
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Controller.MakeRelationToyToPart();

            Controller.ParseInput();

            while(Controller.Possible())
            {
                Controller.MakeToys();
            }

            Controller.GetUnusedData();
        }
    }
}
