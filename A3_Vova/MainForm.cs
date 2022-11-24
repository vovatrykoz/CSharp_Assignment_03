using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A3_Vova
{
    public partial class MainForm : Form
    {
        FuelCalculator fuelCalc = new FuelCalculator(); //creating an instance of the FuelCalculator class
        BMICalculator bmiCalc = new BMICalculator();
        BMRCalculator bmrCalc = new BMRCalculator();

        bool buttonBmiWasClicked;
        double odometerCurr;
        double odometerPrev;

        public MainForm()
        {
            InitializeComponent();
            InitializeGUI();
            
        }

        private void InitializeGUI()
        {
            //clearing all the labels that will be used for output

            lblResultFuelKmLit.Text = String.Empty;
            lblResultFuelLitKm.Text = String.Empty;
            lblResultFuelLitMetMil.Text = String.Empty;
            lblResultFuelLitSweMil.Text = String.Empty;
            lblResultFuelCost.Text = String.Empty;
            lblBMI.Text = String.Empty;
            lblCategory.Text = String.Empty;

            //Pre-checks the radio buttons

            rbtnMetric.Checked = true;
            rbtnFemale.Checked = true;
            rbtActive0.Checked = true;

        }


        private bool ReadFuelInput() //reads and checks the input in the fuel boxes. Returns either true or false, which is used later
        {
            bool currentOdometerOk = ReadCurrentOdometer();
            bool prevOdometerOk = ReadPreviousOdometer();
            bool currentFuelOk = ReadCurrentFuel();
            bool priceOk = ReadFuelPrice();

            return currentOdometerOk && prevOdometerOk && currentFuelOk && priceOk;
        }

        private bool ReadCurrentOdometer()  //reads current odometer reading and checks the reading for validity
        {
            //checks if the input is valid and stores the value in the number variable

            double number;
            bool success = double.TryParse(txtOdometerCurrent.Text, out number) && (number >= 0);

            if (success)
            {
                fuelCalc.SetCurrOdometer(number);
                odometerCurr = number;
            }
            else
                MessageBox.Show("Invalid odometer input! Please, check your values!");

            return success; //returns the value so that it can be read in a separate method
        }

        private bool ReadPreviousOdometer()  //reads previous odometer reading and checks the reading for validity (works the same way as ReadCurrentOdometer, lines 33-41)
        {
            double number;
            bool success = double.TryParse(txtOdometerPrev.Text, out number) && (number >= 0);

            if (success)
            {
                fuelCalc.SetPrevOdometer(number);
                odometerPrev = number;
            }

            else
                MessageBox.Show("Invalid odometer input! Please, check your values!");

            return success;
        }

        private bool ReadCurrentFuel()  //reads current fuel amount and checks the reading for validity
        {
            double number;
            bool success = double.TryParse(txtCurrentFuel.Text, out number) && (number >= 0);

            if (success)
                fuelCalc.SetCurrFuel(number);
            else
                MessageBox.Show("Invalid fuel input! Please check your values!");


            return success;
        }

        private bool ReadFuelPrice()  //reads current fuel price and checks the reading for validity
        {
            double number;
            bool success = double.TryParse(txtFuelPrice.Text, out number) && (number >= 0);

            if (success)
                fuelCalc.SetPrice(number);
            else
                MessageBox.Show("Invalid price! Please, check your values!");

            return success;
        }

        private void btnCalcFuel_Click(object sender, EventArgs e)
        {
            bool ok = ReadFuelInput(); //receieves the value from the ReadFuelInput() variable and assigns it to a local one

            if (ok) //this part will only start if the value received from ReadFuelInput() is true
            {
                //introducing variables in which the results of calculations will be stored in and calling the methods from FuelCalculator.cs to calculate them in

                double resultKmLit = fuelCalc.CalculateFuelKmPerLit();
                double resultLitKm = fuelCalc.CalculateFuelLitPerKm();
                double resultLitMetMiL = fuelCalc.CalculateFuelLitPerMetMil();
                double resultLitSweMil = fuelCalc.CalculateFuelLitPerSweMil();
                double resultCost = fuelCalc.CalculateFuelCost();

                //writing results in corresponding labels, rounded to 2 sig. figs.

                lblResultFuelKmLit.Text = resultKmLit.ToString("f2");
                lblResultFuelLitKm.Text = resultLitKm.ToString("f2");
                lblResultFuelLitMetMil.Text = resultLitMetMiL.ToString("f2");
                lblResultFuelLitSweMil.Text = resultLitSweMil.ToString("f2");
                lblResultFuelCost.Text = resultCost.ToString("f2");
            }
            else
                MessageBox.Show("Invalid input! Please, check your values!");

        }


        private bool ReadBmiInput() //reads and checks the input in the BMI text boxes. Returns either true or false, which is used later
        {
            //height
            bool heightOk = ReadHeight();
            //weight
            bool weightOk = ReadWeight();

            return heightOk && weightOk;
        }

        private void ReadName() //reads the name the user provides and uses a setter to transfer the name into the BMICalculator
        {
            string name = txtName.Text;

            bmiCalc.SetName(name);
        }

        /// <summary>
        /// The way I wanted to go about creating a BMR calculator is for it to get values of height and weight from BMICalculator.cs using getter methods
        /// This way I could simply call on the established setter metods in BMICalculator.cs and the use getter methods to bring the values to BMRCalculator.cs
        /// And I would have to load the memory with an additional setter method in BMRCalculator.cs
        /// Unfortunatelly, the getters didn't work and the value returned would always be zero.
        /// Therefore there is an additional if-statement which cheks which button has been pressed and sends the values either to BMICalculator.cs or BMRCalculator.cs
        /// </summary>

        private bool ReadHeight()  //reads height reading and checks the reading for validity 
        {
            double number;
            bool success;

            success = double.TryParse(txtHeight.Text, out number) && (number >= 0); //checks if the value is valid and returns either true or false

            if (success)
            {
                if (buttonBmiWasClicked) //checks which burron has been pressed before and either sends the values to BMICalculator.cs or BMRCalculator.cs
                    bmiCalc.SetHeight(number);
                else
                    bmrCalc.SetHeight(number);
            }
            else
                MessageBox.Show("Invalid input! Check your values for height!"); //returns an error message if the values are invalid

            return success;
        }

        private bool ReadWeight()//reads weight reading and checks the reading for validity 
        {
            double number;
            bool success;

            success = double.TryParse(txtWeight.Text, out number) && (number >= 0); //checks if the value is valid and returns either true or false

            if (success)
            {
                if (buttonBmiWasClicked) //checks which burron has been pressed before and either sends the values to BMICalculator.cs or BMRCalculator.cs
                    bmiCalc.SetWeight(number);
                else
                    bmrCalc.SetWeight(number);
            }

            else
                MessageBox.Show("Invalid input! Check your values for weight!");//returns an error message if the values are invalid

            return success;
        }

        private void rbtnMetric_CheckedChanged(object sender, EventArgs e) //used to indicate which system has been chosen by changing GUI and setting the enum to metric
        {
            bmiCalc.SetUnit(UnitTypes.Metric);
            lblHeight.Text = "Height (cm)";
            lblWeight.Text = "Weight (kg)";
        }

        private void rbtnImperial_CheckedChanged(object sender, EventArgs e) //used to indicate which system has been chosen by changing GUI and setting the enum to imperial
        {

            bmiCalc.SetUnit(UnitTypes.Imperial);
            lblHeight.Text = "Height (foot)";
            lblWeight.Text = "Weight (lbs)";


        }

        private void btnCalcBMI_Click(object sender, EventArgs e)  //this method is called upon when the button for BMI is pressed
        {
            buttonBmiWasClicked = true; //allows ReadHeight() and ReadWeight() to determine which class will be used to set the data
            bool ok = ReadBmiInput(); //reads the numerical input
            ReadName(); //reads the name

            if (ok)
            {
                double bmi = bmiCalc.CalculateBMI(); //calculates the numerical value for bmi using pre-written methods in BMICalculator class
                string name = bmiCalc.GetName(); //gets the name (or no name if one hasn't been provided)
                string category = bmiCalc.WeightCategory(); //gets the weight category using pre-written methods in BMICalculator class

                //displays results in corresponding boxes (rounded to 2 sig figs if the answers are numerical)

                gbxResultsName.Text = ("Results for " + name);
                lblBMI.Text = bmi.ToString("f2");
                lblCategory.Text = category;

            }

        }


        private void rbtnFemale_CheckedChanged(object sender, EventArgs e) //sets the gender to Female if the corresponding radio button has been checked
        {
            bmrCalc.SetGender(Gender.Female);
        }

        private void rbtnMale_CheckedChanged(object sender, EventArgs e) //sets the gender to male if the corresponding radio button has been checked
        {
            bmrCalc.SetGender(Gender.Male);
        }

        private void rbtActive0_CheckedChanged(object sender, EventArgs e) //the next five methods set the activity level depending on which radio button is checked
        {
            bmrCalc.SetActivity(ActivityLevel.level0);
        }

        private void rbtActive1_CheckedChanged(object sender, EventArgs e)
        {
            bmrCalc.SetActivity(ActivityLevel.level1);
        }

        private void rbtActive2_CheckedChanged(object sender, EventArgs e)
        {
            bmrCalc.SetActivity(ActivityLevel.level2);
        }

        private void rbtActive3_CheckedChanged(object sender, EventArgs e)
        {
            bmrCalc.SetActivity(ActivityLevel.level3);
        }

        private void rbtActive4_CheckedChanged(object sender, EventArgs e)
        {
            bmrCalc.SetActivity(ActivityLevel.level4);
        }

        private bool ReadAge() //checks if the age has been a proper value
        {
            int number; //used to store the input in
            bool success = int.TryParse(txtAge.Text, out number) && (number >= 0); //checks if the input is valid

            if (success)
                bmrCalc.SetAge(number); //sets the age value into the BMRCalculator.cs
            else
                MessageBox.Show("Invalid age input! Check your values!"); //displays an error message

            return success; //returns the value that can be read in other methods
        }

        public bool ReadBMRInput() //gathers all the inputs for BMR and checks them for validity
        {
            bool ageOk = ReadAge();
            bool heightOk = ReadHeight();
            bool weightOk = ReadWeight();

            return ageOk && heightOk && weightOk;
        }


        private void btnCalcBMR_Click(object sender, EventArgs e)
        {

            buttonBmiWasClicked = false; //allows ReadHeight() and ReadWeight() to determine which class will be used to set the data
            bool ok = ReadBMRInput(); //reads the numerical input
            ReadName(); //reads the name

            if (ok)
            {
                //extracts the values calculated using methodes in BMRCalculator.cs and stores them in local variables

                string name = bmiCalc.GetName();
                double bmr = bmrCalc.BMRFemaleOrMale();
                double caloriesMaintain = bmrCalc.MaintainWeightCalories();
                double caloriesLoseHalf = bmrCalc.LoseHalf();
                double caloriesLoseKilo = bmrCalc.LoseKilo();
                double caloriesGainHalf = bmrCalc.GainHalf();
                double caloriesGainKilo = bmrCalc.GainKilo();

                //displays the items in the listbox


                lstbxBMR.Items.Clear(); //updates the form every time the values are caculated
                lstbxBMR.Items.Add("BMR results for " + name);
                lstbxBMR.Items.Add("");
                lstbxBMR.Items.Add("Your BMR (calories/day)                 " + bmr.ToString("f1"));
                lstbxBMR.Items.Add("Calories to maintain your weight        " + caloriesMaintain.ToString("f1"));
                lstbxBMR.Items.Add("Calories to lose 0,5 kg per week            " + caloriesLoseHalf.ToString("f1"));
                lstbxBMR.Items.Add("Calories to lose 1,0 kg per week            " + caloriesLoseKilo.ToString("f1"));
                lstbxBMR.Items.Add("Calories to gain 0,5 kg per week            " + caloriesGainHalf.ToString("f1"));
                lstbxBMR.Items.Add("Calories to gain 1,0 kg per week            " + caloriesGainKilo.ToString("f1"));
                lstbxBMR.Items.Add("");
                lstbxBMR.Items.Add("Losing more than 1000 calories per day is to be avoided");
            }


        }


            private void btnUnselect_Click(object sender, EventArgs e) //unselects listbox items
        {
            lstbxBMR.ClearSelected();
        }
    }
}
