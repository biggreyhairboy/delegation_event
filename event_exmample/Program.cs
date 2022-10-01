using System;

namespace event_example
{
    class Program
    {
        static void Main(string[] args)
        {
            var dealer = new CarDealer();

            var michael = new Consumer("Michael");
            dealer.NewCarInfo += michael.NewCarIsHere;
            dealer.NewCar("Mercedes");
            dealer.NewCarInfo -= michael.NewCarIsHere;
            CarDealer.newCarInfo("Toyotattt");
            dealer.NewCar("Toyota");
        }
    }
}