using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3_Vova
{
    class BMRCalculator
    {

        private int age;
        private double height;
        private double weight;
        private Gender gender;
        private ActivityLevel activityLevel;
        private UnitTypes unit;
        private const double activity0 = 1.32;
        private const double activity1 = 1.375;
        private const double activity2 = 1.550;
        private const double activity3 = 1.725;
        private const double activity4 = 1.9;




        public void SetGender(Gender value) //setter method for gender
        {
            gender = value;
        }

        public void SetActivity(ActivityLevel value) //setter method for activity levels
        {
            activityLevel = value;
        }

        public void SetAge(int value) //setter method for age
        {
            age = value;
        }

        public void SetHeight(double value) //setter method for height
        {
            height = value;
        }

        public void SetWeight(double value) //setter method for weight
        {
            weight = value;
        }


        /// <summary>
        /// In the following method i wanted to use the getter methods from the BMICalculator.cs to set the values for height
        /// I created an instanse of BMICalculator.cs and used the getters, however, it would always return the value of zero
        /// This is why I used setters here
        /// </summary>

        public double BMRBaseResult() //calculates the inital BMR result result using a pre-established formula
        {
            double result;

            if(unit == UnitTypes.Imperial)
            {
                weight /= 2.205; //converting the input to the metric system
                height /= 0.0254;

                result = 10 * weight + 6.25 * height - 5 * age;
            }
            else
                result = 10 * weight + 6.25 * height - 5 * age;


            return result;
        }


        public double BMRFemaleOrMale() //does some additional calculation depending on the gender provided
        {
            double result;

            if (gender == Gender.Female)
                result = BMRBaseResult() - 161;
            else
                result = BMRBaseResult() + 5;

            return result;
        }

        /// <summary>
        /// Calculates values for how to maintan calories weight by referring to the previously calculated BMR and enums which represent activity levels
        /// </summary>

        public double MaintainWeightCalories()
        {
            double result = 0.0;

            if (activityLevel == ActivityLevel.level0)
                result = BMRFemaleOrMale() * activity0;
            else if (activityLevel == ActivityLevel.level1)
                result = BMRFemaleOrMale() * activity1;
            else if (activityLevel == ActivityLevel.level2)
                result = BMRFemaleOrMale() * activity2;
            else if (activityLevel == ActivityLevel.level3)
                result = BMRFemaleOrMale() * activity3;
            else if (activityLevel == ActivityLevel.level4)
                result = BMRFemaleOrMale() * activity4;

            return result;

        }

        //the next four methods are simple calculations showing how many calories should be consumed/lost to gain/lose a 0.5 or 1 kilo

        public double LoseHalf()
        {
            double result = MaintainWeightCalories() - 500;

            return result;
        }

        public double LoseKilo()
        {
            double result = MaintainWeightCalories() - 1000;

            return result;
        }

        public double GainHalf()
        {
            double result = MaintainWeightCalories() + 500;

            return result;
        }

        public double GainKilo()
        {
            double result = MaintainWeightCalories() + 1000;

            return result;
        }
    }
}
