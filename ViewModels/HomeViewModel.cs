

using ProjektProgBD.Models;
using System.Net.Http;
using System.Windows.Threading;
using Newtonsoft.Json.Linq;

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
        }

        #region properties
        private User _currentUser;
        private string _currentDate;
        private string _currentTime;
        private string _weatherCondition;
        private string _weatherImage;
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
        public string WeatherImage
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

        public async Task GetWeatherDataAsync()
        {
            string apiKey = "YOUR_API_KEY";
            string url = $"http://api.openweathermap.org/data/2.5/weather?q=YOUR_CITY_NAME&appid={apiKey}&units=metric";

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
            switch (condition.ToLower())
            {
                case "clear":
                    WeatherImage = "Images/clear.png";
                    break;
                case "clouds":
                    WeatherImage = "Images/clouds.png";
                    break;
                case "rain":
                    WeatherImage = "Images/rain.png";
                    break;
                case "snow":
                    WeatherImage = "Images/snow.png";
                    break;
                default:
                    WeatherImage = "Images/default.png";
                    break;
            }
        }


        #endregion






    }
}
