using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x415

namespace KomisBazasqllite
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        
        List<Car> carsList = new List<Car>();
        Car selected;
        public MainPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path,"cars.db");
            var conn = new SQLiteAsyncConnection(path, true);
            conn.CreateTableAsync<Car>();
            reload();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "cars.db");
            var conn = new SQLiteAsyncConnection(path, true);
            var car = new Car {
                Marka = marka.Text,
                Model = model.Text,
                Rok=Convert.ToInt32(rok.Text)
            };
            conn.InsertAsync(car);
            reload();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            selected.Marka = marka.Text;
            selected.Model = model.Text;
            selected.Rok = Convert.ToInt32(rok.Text);
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "cars.db");
            var conn = new SQLiteAsyncConnection(path, true);
            conn.UpdateAsync(selected);
            reload();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            selected = (Car)ListView.SelectedItem;
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "cars.db");
            var conn = new SQLiteAsyncConnection(path, true);
            conn.DeleteAsync(selected);
            reload();
        }
        async void reload()
        {
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "cars.db");
            var conn = new SQLiteAsyncConnection(path, true);
            var query = conn.Table<Car>();
            carsList = await query.ToListAsync();
            ListView.ItemsSource = carsList;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListView.SelectedItem != null)
            {
                selected = (Car)ListView.SelectedItem;
                marka.Text = selected.Marka;
                model.Text = selected.Model;
                rok.Text = selected.Rok.ToString();
            }

        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchValue = search.Text;
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "cars.db");
            var conn = new SQLiteAsyncConnection(path, true);
            var car = await conn.Table<Car>().Where(x => x.Marka == searchValue).ToListAsync(); //dopisac filtrowanie xD
                                                                                                 // int Id = car.Id;
                                                                                                 //var carInList = carsList.Select(n=>n).Where(n => n.Id == Id).ToList<Car>();
            ListView.ItemsSource = car;
        }

        private void CancelSearch_Click(object sender, RoutedEventArgs e)
        {
            ListView.ItemsSource = carsList;
        }
    }

}
