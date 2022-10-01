using System;

namespace event_example
{
    public class CarInfoEventArgs : EventArgs
    {
        public CarInfoEventArgs(string car)
        {
            this.Car = car;
        }

        public string Car { get; set; }
    }
    
    public class CarDealer
    {
        public delegate EventHandler<CarInfoEventArgs> newCarInfo();

        public event EventHandler<CarInfoEventArgs> NewCarInfo;
        // {
        //     add
        //     {
        //         newCarInfo += value;
        //     }
        //     remove
        //     {
        //         newCarInfo -= value;
        //     }
        // }

        public void NewCar(string car)
        {
            Console.WriteLine("CarDealer, new car{0}", car);
            if (NewCarInfo != null)
            {
                NewCarInfo(this, new CarInfoEventArgs(car));
            }
        }
    }
}