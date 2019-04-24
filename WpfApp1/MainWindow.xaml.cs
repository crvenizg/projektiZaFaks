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
using SQLite;
using System.ComponentModel;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MinimizeToTray(); // dodati refence za system drawing i wind forms, desni klik add reference na applikaciju
            ReadDatabase(); //hmm dodati beskonacnu petlju da on sam ovo updatea ?
        }


        void MinimizeToTray()
        {
            System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = new System.Drawing.Icon("Main.ico");
            ni.Visible = true;
            ni.DoubleClick +=
                delegate (object sender, EventArgs args)
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                };
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == System.Windows.WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 newDodaj = new Window1();
            newDodaj.ShowDialog();

            ReadDatabase();
            //UpdateDatabase(noviPodatci);
        }

        void ReadDatabase()
        {
            List<DataClass> noviPodatci; //stvaramo listu
            using (SQLiteConnection conn = new SQLiteConnection(App.databasePath)) // spajamoo se na bazu
            {
                conn.CreateTable<DataClass>();
                noviPodatci = conn.Table<DataClass>().ToList();
            }

            UpdateDatabase(noviPodatci);
        }

        void UpdateDatabase(List<DataClass> noviPodatci)
        {
            //updataj vrijeme, stavi u tablicu i soortiraj
            for (var i = 0; i < noviPodatci.Count; i++)
            {
                noviPodatci[i].broj = (noviPodatci[i].timeEnding - DateTime.Now).Days + 1;
            }
            if (noviPodatci != null)
            {
                podaciListView.ItemsSource = noviPodatci;
            }
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(podaciListView.ItemsSource);  //sluzi za sortiranje
            view.SortDescriptions.Add(new SortDescription("broj", ListSortDirection.Ascending));                    //sluzi za sortiranje
        }

        private void PodaciListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataClass selectedData = (DataClass)podaciListView.SelectedItem;

            if(selectedData != null)
            {
                DelWindow newDodajDel = new DelWindow(selectedData);
                newDodajDel.ShowDialog();

                ReadDatabase(); // ovime kad se zatvori onaj prozor odmah updatea i vidi se kako izgleda kad se obrise ili izmjeni nestoo :)
            }

        }



    //    if (noviPodatci != null)
    //        {
    //            foreach(var pod in noviPodatci)
    //            {
    //                podaciListView.Items.Add(new ListViewItem()
    //    {
    //        Content = pod
    //                });
    //            }
    //podaciListView.ItemsSource = noviPodatci;
    //        }
}
}
