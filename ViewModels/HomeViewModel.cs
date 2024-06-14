

using ProjektProgBD.Models;
using System.Net.Http;
using System.Windows.Threading;
using Newtonsoft.Json.Linq;
using System.Windows;
using System.Windows.Input;
using XPlat.Device.Geolocation;
using System.Windows.Media.Imaging;

namespace ProjektProgBD.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel(User user)
        {
            CurrentUser = user;
            CurrentDate = DateTime.Now.ToString("dd.MM.yyyy");
            CurrentTime = DateTime.Now.ToString("HH:mm");
            
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMinutes(1)
            };
            _timer.Tick += TimerTick;
            _timer.Start();

            InitializeAsync();
        }

        #region properties
        private User _currentUser;
        private string _currentDate;
        private string _currentTime;
        private string _weatherCondition;
        private BitmapImage _weatherImage;
        private double _temperature;
        private DispatcherTimer _timer;
        #endregion

        #region accessors
        public User CurrentUser {
            get { return _currentUser; }
            set 
            {
                _currentUser = value;
                onPropertyChanged(nameof(CurrentUser)); 
            }
        }

        public string CurrentDate
        {
            get
            {
                return _currentDate;
            }
            set
            {
                _currentDate = value;
                onPropertyChanged(nameof(CurrentDate));
            }
        }

        public string CurrentTime
        {
            get
            {
                return _currentTime;
            }
            set
            {
                _currentTime = value;
                onPropertyChanged(nameof(CurrentTime));
            }
        }

        public string WeatherCondition
        {
            get => _weatherCondition;
            set
            {
                _weatherCondition = value;
                onPropertyChanged(nameof(WeatherCondition));
            }
        }
        public BitmapImage WeatherImage
        {
            get => _weatherImage;
            set
            {
                _weatherImage = value;
                onPropertyChanged(nameof(WeatherImage));
            }
        }

        public double Temperature
        {
            get => _temperature;
            set
            {
                _temperature = value;
                onPropertyChanged(nameof(Temperature));
            }
        }


        #endregion

        #region commands


        #endregion

        #region functions
        private void TimerTick(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToString("HH:mm");
        }

        private async void InitializeAsync()
        {
            await GetWeatherDataAsync();
        }

        public async Task GetWeatherDataAsync()
        {
            string apiKey = "fa418d9978c099f0c7c3739558cdff67";
            string url = $"http://api.openweathermap.org/data/2.5/weather?q=Gliwice&appid={apiKey}&units=metric";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    JObject weatherData = JObject.Parse(result);
                    WeatherCondition = weatherData["weather"][0]["main"].ToString();
                    Temperature = Convert.ToDouble(weatherData["main"]["temp"]);
                    UpdateWeatherImage(WeatherCondition);
                }
            }
        }


        private void UpdateWeatherImage(string condition)
        {
            string imagePath = condition.ToLower() switch
            {
                "clear" => "/Images/clear.png",
                "clouds" => "/Images/clouds.png",
                "rain" => "/Images/rain.png",
                "snow" => "/Images/snow.png",
                _ => "Images/default.png"
            };

            WeatherImage = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
        }


        #endregion






    }
}
