using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BMI.WPF.App
{
    /// <summary>
    /// Name: I don't know yet
    /// </summary>
    enum bmiType { Underweight, Normal, Overweight, Obese }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Patient> patients = new List<Patient>();

        private string patientName = "";
        public MainWindow()
        {
            InitializeComponent();

            CreateData();

            BindList();


        }
        /// <summary>
        /// Initistanting the date
        /// </summary>
        private void CreateData()
        {
            patients.Add(new Patient { Name = "James Bond", Weight = 150, Height = 65, BMI = 18.7, Status = 1 });
            patients.Add(new Patient { Name = "Mary Bolyen", Weight = 110, Height = 55, BMI = 18.7, Status = 1 });
            patients.Add(new Patient { Name = "Harry Potter", Weight = 105, Height = 55, BMI = 18.7, Status = 1 });
        }

        private void BindList()
        {
            // DRY
            //Bind ComboBox cbxEmp to class object data, use LINQ to sort Name by descending order
            var bmiPatients = from patient in patients
                              orderby patient.Name
                        select patient.Name;
            cbxPatients.ItemsSource = bmiPatients;

            btnUpdate.IsEnabled = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            ComboBox comboBox = (ComboBox)sender;

            patientName = (string)cbxPatients.SelectedItem;

            var result = from p in patients
                         where p.Name.Equals(patientName)
                         select p;

            foreach (var item in result)
            {
                lblRWeight.Content = "Patient Weight: "+ item.Weight.ToString() + " lbs";
                lblRHeight.Content = "Patient Height: " + item.Height.ToString() +" inches";

                txtHeight.Text = item.Height.ToString();
                txtWeight.Text = item.Weight.ToString();

                txtblkReport.Text = $"BMI result of {item.BMI} category is {item.getStatus()}!";
                btnUpdate.IsEnabled = true;
            }
        }
        /// <summary>
        /// Reset Combox to unselected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var validPatient = from p in patients
                                 where p.Name.Equals(patientName)
                                 select p;
            foreach (var patient in validPatient)
            {
                patient.Height = double.Parse(txtHeight.Text);
                patient.Weight = double.Parse(txtWeight.Text);
                patient.BMI = patient.getBMI();

                cbxPatients.SelectedIndex = -1;
                txtHeight.Text = "";
                txtWeight.Text = "";
                txtblkReport.Text = $"BMI result:";
                lblRWeight.Content = "Patient Weight:";
                lblRHeight.Content = "Patient Height:";
                btnUpdate.IsEnabled = false;
            }


        }

        /// <summary>
        /// Clear all Textbox input
        /// </summary>
        private void ClearInputField()
        {
            txtPName.Text = "";
            txtPHeight.Text = "";
            txtPWeight.Text = "";

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearInputField();
        }
        /// <summary>
        /// Add New Patient
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddPatient_Click(object sender, RoutedEventArgs e)
        {
                if (txtPName.Text == string.Empty)
            {
                MessageBox.Show("Please enter a name!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Hand);

            }

                var patient = new Patient();
            
                patient.Name = txtPName.Text;
                patient.Weight = double.Parse(txtPWeight.Text);
                patient.Height = double.Parse(txtPHeight.Text);
                patient.BMI = patient.getBMI();
                patient.getStatus();

                patients.Add(patient);
                ClearInputField();
                BindList();                    
            

        }
        /// <summary>
        /// Delete user from the list,no longer a patient
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var item = patients.Single(x => x.Name == patientName);
            patients.Remove(item);
            BindList();
        }
    }


}
