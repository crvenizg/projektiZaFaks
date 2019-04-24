using SQLite;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for DelWindow.xaml
    /// </summary>
    public partial class DelWindow : Window
    {
        DataClass newData;
        public DelWindow(DataClass newData)
        {
            InitializeComponent();

            this.newData = newData;
            opisTextBox.Text = newData.opis;
            zavrsavaDate.SelectedDate = newData.timeEnding;
        }

        private void buttonDel(object sender, RoutedEventArgs e)
        {
            List<DataClass> noviPodatci;
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<DataClass>();
                connection.Delete(newData);
            }
            
            Close();
        }

        private void buttonUpdate(object sender, RoutedEventArgs e)
        {
            newData.opis = opisTextBox.Text;
            newData.timeEnding = zavrsavaDate.SelectedDate.Value;
            newData.broj = (zavrsavaDate.SelectedDate.Value - DateTime.Now).Days + 1;

            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<DataClass>();
                connection.Update(newData);
            }

            Close();
        }

    }
}
