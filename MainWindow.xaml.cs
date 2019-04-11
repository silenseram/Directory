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
using MySql.Data.MySqlClient;


namespace WpfSharp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {

        string conStr = "server=localhost;user=admin;database=places;password=admin";
        private void updateLists()
        {
                MySqlConnection con = new MySqlConnection(conStr);
                con.Open();
                string value = "";
                string select = "SELECT country_name FROM countrys";

                MySqlCommand command = new MySqlCommand(select, con);
                MySqlDataReader reader = command.ExecuteReader();
                CountryList.Items.Clear();
                PeoplesList.Items.Clear();
                CityList.Items.Clear();
                PhonesList.Items.Clear();


                try
                {
                    while (reader.Read())
                    {
                        value = reader[0].ToString();
                        CountryList.Items.Add(value);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                try
                {
                    select = "SELECT city_name FROM citys WHERE country_name ='" + CountryList.SelectedValue + "');";
                    while (reader.Read())
                    {
                        value = reader[0].ToString();
                        CityList.Items.Add(value);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }


                try
                {
                    select = "SELECT people_name FROM peoples WHERE people_city='" + CityList.SelectedValue + "' AND people_country = '" + CountryList.SelectedValue + "');";
                    while (reader.Read())
                    {
                        value = reader[0].ToString();
                        PeoplesList.Items.Add(value);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                try
                {
                    select = "SELECT phone_number FROM places.phone_numbers WHERE people_city='" + CityList.SelectedValue + "' AND people_country = '" + CountryList.SelectedValue + "', AND people_name ='" + PeoplesList.SelectedValue + "');";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                con.Close();

        }
        private void updateCityList()
        {
            MySqlConnection con = new MySqlConnection(conStr);
            con.Open();
            string value = "";
            string select = "SELECT city_name FROM citys WHERE country_name ='" + CountryList.SelectedValue + "';";

            MySqlCommand command = new MySqlCommand(select, con);
            MySqlDataReader reader = command.ExecuteReader();
            CityList.Items.Clear();

                while (reader.Read())
                {
                    value = reader[0].ToString();
                    CityList.Items.Add(value);
                }
            con.Close();
        }
        private void updatePeoplesList()
        {
            MySqlConnection con = new MySqlConnection(conStr);
            con.Open();
            string value = "";
            string select = "SELECT people_name FROM peoples WHERE people_city='" + CityList.SelectedValue + "' AND people_country = '" + CountryList.SelectedValue + "';";

            MySqlCommand command = new MySqlCommand(select, con);
            MySqlDataReader reader = command.ExecuteReader();
            PeoplesList.Items.Clear();
            PhonesList.Items.Clear();
                while (reader.Read())
                {
                    value = reader[0].ToString();
                    PeoplesList.Items.Add(value);
                }

            con.Close();
        }
        private void updatePhoneNumbersList()
        {
            MySqlConnection con = new MySqlConnection(conStr);
            con.Open();
            string value = "";
            string select = "SELECT phone_number FROM places.phone_numbers WHERE country_name='" + CountryList.SelectedItem.ToString() + "' AND city_name='" + CityList.SelectedItem.ToString() + "' AND people_name='" + PeoplesList.SelectedItem.ToString() + "';";

            MySqlCommand command = new MySqlCommand(select, con);
            MySqlDataReader reader = command.ExecuteReader();
            PhonesList.Items.Clear();

            while (reader.Read())
            {
                value = reader[0].ToString();
                PhonesList.Items.Add(value);
            }

            con.Close();
        }

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object senser, RoutedEventArgs e)
        {
            MySqlConnection con = new MySqlConnection(conStr);
            con.Open();
            string value = "";
            string select = "SELECT country_name FROM countrys";

            MySqlCommand command = new MySqlCommand(select, con);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                value = reader[0].ToString();
                CountryList.Items.Add(value);
            }
            con.Close();
            
        }

        private void AddCountryBtn1_Click(object sender, RoutedEventArgs e)
        {
            AddWindow wind = new AddWindow("Country", "", "", "");
            wind.ShowDialog();
            updateLists();
        }

        private void AddPeopleBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddWindow wind = new AddWindow("People", CountryList.SelectedValue.ToString(), CityList.SelectedValue.ToString(), "");
                wind.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            updatePeoplesList();
        }

        private void AddCityBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddWindow wind = new AddWindow("City", CountryList.SelectedValue.ToString(), "", "");
                wind.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            updateCityList();
        }

        private void CountryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateCityList();
            PhonesList.Items.Clear();
            PeoplesList.Items.Clear();
            
        }

        private void CityList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PhonesList.Items.Clear();
            updatePeoplesList();
        }

        private void AddPhoneBtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                AddWindow wind = new AddWindow("Phone", CountryList.SelectedItem.ToString(), CityList.SelectedItem.ToString(), PeoplesList.SelectedItem.ToString());
                wind.ShowDialog();
            }
            catch
            {

            }
            updatePhoneNumbersList();
        }

        private void PeoplesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PeoplesList.SelectedItem != null)
            {
                updatePhoneNumbersList();
            }
        }
    }
}
