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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace WpfSharp
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        string insertQuery = "";
        string country;
        string city;
        string people;
        string value;
        public AddWindow(string value, string selectedCountry, string selectedCity, string selectedPeople)
        {
            InitializeComponent();
            AddLblName.Content = "Add " + value;
            country = selectedCountry;
            city = selectedCity;
            people = selectedPeople;
            this.value = value;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (value)
            {
                case "Country":
                    insertQuery = "INSERT INTO `places`.`countrys` ( `country_name`) VALUES ('" + AddNameTextField.Text + "');";
                    break;
                case "City":
                    insertQuery = "INSERT INTO `places`.`citys` ( `city_name`, `country_name`) VALUES ('" + AddNameTextField.Text + "', '" + country + "');";
                    break;
                case "People":
                    insertQuery = "INSERT INTO `places`.`peoples` (`people_name`, `people_city`, `people_country`) VALUES ('" + AddNameTextField.Text + "', '" +city + "', '" + country + "');";
                    break;
                case "Phone":
                    insertQuery = "INSERT INTO `places`.`phone_numbers` (`people_name`, `city_name`, `country_name`, `phone_number`) VALUES ('" + people + "', '" + city + "', '" + country + "', '" + AddNameTextField.Text + "');";
                    break;
            }
            MySqlConnection connection = new MySqlConnection("server=localhost;user=admin;database=places;password=admin");

            connection.Open();

            MySqlCommand command = new MySqlCommand(insertQuery, connection);

            try
            {
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Succes");
                }
                else
                {
                    MessageBox.Show("FAK");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            connection.Close();

            Close();
        }
    }
}
