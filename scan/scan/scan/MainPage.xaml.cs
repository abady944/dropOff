using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;


namespace scan
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]

    public partial class MainPage : ContentPage
    {
        //  private List<string> trackingData;
        public ObservableCollection<string> trackingData = new ObservableCollection<string>();

        public static void Run()
        {
            /*               
               // database connection //
               FbConnectionStringBuilder csb = new FbConnectionStringBuilder();
               csb.DataSource = "192.168.13.134";
               csb.Port = 3050;
               csb.Database = "PCS";
               csb.UserID = "SYSDBA";
               csb.Password = "3k7rur9e";
               csb.ServerType = FbServerType.Default;

               FbConnection db = new FbConnection(csb.ToString());
               db.Open();
               //FbCommand cmd = new FbCommand("select *", db);
              */
        }
        ZXingScannerPage scanPage;

        public MainPage()
        {
            //            trackingData = new List<string>();
            InitializeComponent();
            trackerlist.ItemsSource = trackingData;
            ButtonScanDefault.Clicked += ButtonScanDefault_Clicked;
            ButtonSubmitNum.Clicked += ButtonSubmitNum_Clicked;
            ButtonNext.Clicked += ButtonNext_Clicked;
        }

        // 
        private void ButtonSubmitNum_Clicked(object sender, EventArgs e)
        {
            var list = new List<string>();
            list.Add(trackingtxt.Text);
            for (int i = 0; i < list.Count; i++)
            {
                trackingData.Add(list[i]);
                trackingtxt.Text = "";
            }
        }

        private void ButtonNext_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Next button clicked", "Clicked", "OK");
        }

        private async void ButtonScanDefault_Clicked(object sender, EventArgs e)
        {
            scanPage = new ZXingScannerPage();
            scanPage.OnScanResult += (result) =>
            {
                scanPage.IsScanning = false;

                //Do something with result 

                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PopModalAsync();
                    DisplayAlert("Barcode was scanned", result.Text, "OK");
             //       trackingtxt.Text = result.Text;
                    trackingData.Add(result.Text);

                    // FbCommand cmd = new FbCommand("INSERT INTO package VALUES" + result.Text , db);

                });
                //trackiong.text = result.text;
                //                FbCommand cmd = new FbCommand("SELECT*", db); 

            };
            await Navigation.PushModalAsync(scanPage);
        }
    }
}