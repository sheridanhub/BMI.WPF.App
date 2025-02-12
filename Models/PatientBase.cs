namespace BMI.WPF.App.Models
{
    public abstract class PatientBase
    {
        public abstract double BMI
        {
            get; set;
        }
        public abstract double getBMI();
    }
}