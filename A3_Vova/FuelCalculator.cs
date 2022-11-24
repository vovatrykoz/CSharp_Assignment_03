using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace A3_Vova
{
    class FuelCalculator
    {

        private double currOdometer;
        private double prevOdometer;
        private double currFuel;
        private double price;

        public void SetCurrOdometer(double value)
        {
            currOdometer = value;
        }

        public void SetPrevOdometer(double value)
        {
            prevOdometer = value;
        }

        public void SetCurrFuel(double value)
        {
            currFuel = value;
        }

        public void SetPrice(double value)
        {
            price = value;
        }

        public double GetCurrOdometer()
        {
            return currOdometer;
        }

        public double GetPrevOdometer()
        {
            return prevOdometer;
        }

        public double GetCurrFuel()
        {
            return currFuel;
        }

        public double GetPrice()
        {
            return price;
        }

        //performes calculations using established formulas
        public double CalculateFuelKmPerLit() //numbers will be read out in exact order as listed here
        {
            double result; //creates an instance variable to store results in

            result = (GetCurrOdometer() - GetPrevOdometer()) / GetCurrFuel(); //calculates the result using numbers and stores it the "result" variable

            return result; //returns the resulting value, that could be read out elsewhere in the program
        }

        public double CalculateFuelLitPerKm() //numbers will be read out in exact order as listed here
        {
            double result; //creates an instance variable to store results in

            result = GetCurrFuel() / (GetCurrOdometer() - GetPrevOdometer()); //calculates the result using numbers and stores it the "result" variable

            return result; //returns the resulting value, that could be read out elsewhere in the program
        }



        public double CalculateFuelLitPerMetMil()
        {
            double result;  //creates an instance variable to store results in
            const double kmToMileFactor = 0.621371192; //creates a known constant to be used in further calculations

            result = (GetCurrFuel() / (GetCurrOdometer() - GetPrevOdometer())) / kmToMileFactor; //calculates the result using numbers and stores it the "result" variable

            return result; //returns the resulting value, that could be read out elsewhere in the program
        }

        public double CalculateFuelLitPerSweMil()
        {
            double result; //creates an instance variable to store results in

            result = (GetCurrFuel() / (GetCurrOdometer() - GetPrevOdometer())) * 10; //calculates the result using numbers and stores it the "result" variable
            //creating a constant here would be unnecesarry, since 10 is a simple number to work with

            return result; //returns the resulting value, that could be read out elsewhere in the program
        }

        public double CalculateFuelCost()
        {
            double result; //creates an instance variable to store results in

            result = (GetCurrFuel() / (GetCurrOdometer() - GetPrevOdometer())) * GetPrice(); //calculates the result using numbers and stores it the "result" variable

            return result; //returns the resulting value, that could be read out elsewhere in the program
        }


    }
}
