using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace A3_Vova
{
    class BMICalculator
    {
        private string name;
        private double height;
        private double weight;
        private UnitTypes unit;
        
        public void SetHeight(double value) //a setter method for height
        {
            height = value;
        }

        public void SetWeight(double value) //a setter method for weight
        {
            weight = value;
        }

        public void SetName(string value) //a setter method for name
        {
            if (string.IsNullOrEmpty(value))
                name = "No Name";
            else
                name = value;

        }

        public void SetUnit(UnitTypes value) //a setter method for unit types
        {
           unit = value;
        }

        public double GetHeight() //a getter method for height
        {
            return height;
        }

        public double GetWeight() //a getter method for weight
        {
            return weight;
        }

        public string GetName() //a getter method for name
        {
            return name;
        }
        
        public double BMIMetricCalc(double number1, double number2) //calculations for metric units
        {
            double result; //introducing an instance variable to store results in

            number2 /= 100; //converting cm into meters

            result = number1 / (number2 * number2); //calculating BMI

            return result; //returning result to be displayed in the MainForm
        }

        public double BMIImperialCalc(double number1, double number2) //calculations for imperial units
        {
            double result; //introducing an instance variable to store results in

            number2 *= 12.00;

            result = 703*(number1 / (number2 * number2)); //calculating BMI

            return result; //returning result to be displayed in the MainForm
        }

        public double CalculateBMI() //calculates the bmr and calls upon appropriate methods depending on units
        {
            double result;

            if (unit == UnitTypes.Metric)
                result = BMIMetricCalc(weight, height);
            else
                result = BMIImperialCalc(weight, height);

            return result;
        }

        public string WeightCategory() //determins the weight category depending on which range the BMI is in
        {
            double bmi = CalculateBMI();
            string stringout = String.Empty;
            if (bmi < 18.5)
            {
                stringout = "Underweight";
            }
            else if (bmi >= 18.5 && bmi <= 24.9)
            {
                stringout = "Normal weight";
            }
            else if (bmi >= 25.0 && bmi <= 29.9)
            {
                stringout = "Overweight (Pre-obesity)";
            }
            else if (bmi >= 30.0 && bmi <= 34.9)
            {
                stringout = "Obesity class I";
            }
            else if (bmi >= 35.0 && bmi <= 39.9)
            {
                stringout = "Obesity class II";
            }
            else if (bmi >= 40.0)
            {
                stringout = "Obesity class III";
            }

            return stringout;
        }
            


    }
}
