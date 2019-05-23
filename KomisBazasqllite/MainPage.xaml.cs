using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.IO;
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
        
        List<Cars> carsList = new List<Cars>();
        Cars selected;
        public MainPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path,"cars.db");
            var conn = new SQLiteAsyncConnection(path, true);
            conn.CreateTableAsync<Cars>();
            reload();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "cars.db");
            var conn = new SQLiteAsyncConnection(path, true);
            var car = new Cars {
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
            selected = (Cars)ListView.SelectedItem;
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "cars.db");
            var conn = new SQLiteAsyncConnection(path, true);
            conn.DeleteAsync(selected);
            reload();
        }
        async void reload()
        {
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "cars.db");
            var conn = new SQLiteAsyncConnection(path, true);
            var query = conn.Table<Cars>();
            carsList = await query.ToListAsync();
            ListView.ItemsSource = carsList;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListView.SelectedItem != null)
            {
                selected = (Cars)ListView.SelectedItem;
                marka.Text = selected.Marka;
                model.Text = selected.Model;
                rok.Text = selected.Rok.ToString();
            }

        }
    }

}
