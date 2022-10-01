using System;
using System.Data;
using System.IO;
namespace ConsoleApp3
{
    delegate void func(string name);
    class Program
    {
        
        static void Main(string[] args)
        {
            Person Hei1 = new Person("黑人小哥1");
            Person Hei2 = new Person("黑人小哥2");
            Person Hei3 = new Person("黑人小哥3");
            Person Hei4 = new Person("黑人小哥4");
            Person ming = new Person("小明");
            ming.died += Hei1.taiGuan;
            ming.died += Hei2.taiGuan;
            ming.died += Hei3.taiGuan;
            ming.died += Hei4.taiGuan;
            ming.fellFull += Hei1.seeFull;
            ming.fellFull += Hei2.seeFull;
            ming.fellFull += Hei3.seeFull;
            ming.fellFull += Hei4.seeFull;
            ming.fellHungry += Hei1.seeHungry;
            ming.fellHungry += Hei2.seeHungry;
            ming.fellHungry += Hei3.seeHungry;
            ming.fellHungry += Hei4.seeHungry;
            ming.EatDonut(10);
            Console.ReadKey();
        }
    }
    class Person{
        string name;
        public event func died;
        public event func fellFull;
        public event func fellHungry;
        public Person(string name) {
            this.name = name;
        }
        
        public void EatDonut(int i){
            if (i <= 10)
            {
               fellHungry(name);
            }
            else {
                if (i <= 100)
                {
                    fellFull(name);
                }
                else {
                    if (i > 100) {
                        died(name);
                    }
                }
            }
        }
        public void taiGuan(string name) {
            Console.WriteLine(this.name+"收到"+name+"死了，开始抬棺！奏乐！！");
        }
        public void seeHungry(string name)
        {
            Console.WriteLine(this.name + "看到" + name + "还很饿");
        }
        public void seeFull(string name)
        {
            Console.WriteLine(this.name + "看到" + name + "吃饱了");
        }
    }
}