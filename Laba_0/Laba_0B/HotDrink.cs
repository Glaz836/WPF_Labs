﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba_0B
{
    abstract class HotDrink
    {
        protected int sugar = 3;
        public int Sugar {
            get { return sugar; }
            set {
                if (value < 0) sugar = 0;
                if (value > 10) sugar = 10;
                if (value >= 0 && value <= 10) sugar = value;
            }
        }

        protected int milk = 3;
        public int Milk {
            get { return milk ; }
            set {
                if (value < 0) milk = 0;
                if (value > 5) milk = 5;
                if (value >= 0 && value <= 5) milk = value;
            }
        }

        public void Drink()
        {
            Console.WriteLine("Чашка опустошена\n");
        }
        public void AddMilk(int milk)
        {
            Milk = milk;
        }
        public void AddSugar(int sugar)
        {
            Sugar = sugar;
        }
    }
}
