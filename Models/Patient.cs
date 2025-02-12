using System;
using BMI.WPF.App.Models;

namespace BMI.WPF.App
{
    /// <summary>
    /// Name: Joseph Jean-Charles
    /// Program Course:
    /// </summary>
    public class Patient : PatientBase
    {
        public string Name { get; set; } = String.Empty;
        public double Weight { get; set; } // in pounds

        public double Height { get; set; } // in inches
        public override double BMI { get; set; }

        public int Status { get; set; }

        private readonly double KILOGRAMS_PER_POUND = 0.45359237; 
        private readonly double METERS_PER_INCH = 0.0254;

        /// <summary>
        /// Calculate Patient BMI 
        /// Return double
        /// </summary>
        /// <returns></returns>
        public override double getBMI()
        {
            double bmi = Weight * KILOGRAMS_PER_POUND /
              ((Height * METERS_PER_INCH) * (Height * METERS_PER_INCH));
            return Math.Round(bmi * 100) / 100.0;
        }

        /// <summary>
        /// Return BMI status based on the 
        /// value of calculated BMI
        /// </summary>
        /// <returns></returns>
        public String getStatus()
        {
            double bmi = getBMI();

            if (bmi < 18.5)
            {
                Status = (int)bmiType.Underweight;
                return bmiType.Underweight.ToString();
            }
            else if (bmi < 25) { 
                Status = (int)bmiType.Underweight;
            return bmiType.Normal.ToString();
            }
            else if (bmi < 30)
            {
                Status = (int)bmiType.Underweight;
                return bmiType.Overweight.ToString();
            }
            else
            {
                Status = (int)bmiType.Underweight;
                return bmiType.Obese.ToString();
            }
                
        }
    }
}
