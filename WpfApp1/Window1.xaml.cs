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
using SQLite;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // save
            DataClass noviPodatak = new DataClass()
            {
                opis = opisTextBox.Text,
                timeEnding = zavrsavaDate.SelectedDate.Value,
                broj = (zavrsavaDate.SelectedDate.Value - DateTime.Now).Days + 1
            };

            //SQLiteConnection connection = new SQLiteConnection(App.databasePath);
            //connection.CreateTable<DataClass>();
            //connection.Insert(noviPodatak);
            //connection.Close();

            //ili bolje ovo
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<DataClass>();
                connection.Insert(noviPodatak);
            }

            this.Close();
        }
    }
}
