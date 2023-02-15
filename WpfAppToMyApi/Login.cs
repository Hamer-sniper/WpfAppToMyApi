using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppToMyApi
{
    public class Login
    {
        private const string APP_PATH = "https://localhost:7065";
        private static string token;

        //static string First(string UserName, string Password)
        public static string First()
        {
            //string userName = UserName;
            //string password = Password;


            string userName = "Administrator";
            string password = "123456aA!";

            

            //var registerResult = Register(userName, password);

            //Console.WriteLine("Статусный код регистрации: {0}", registerResult);

            Dictionary<string, string> tokenDictionary = GetTokenDictionary(userName, password);
            token = tokenDictionary["access_token"];

            //Console.WriteLine();
            //Console.WriteLine("Access Token:");
            //Console.WriteLine(token);

            return token;

            //Console.WriteLine();
            //string values = GetValues(token);
            //Console.WriteLine("Values:");
            //Console.WriteLine(values);

            //Console.Read();
        }

        // регистрация
        static string Register(string name, string password)
        {
            var registerModel = new
            {
                Name = name,
                Password = password,
                ConfirmPassword = password
            };
            using (var client = new HttpClient())
            {
                var response = client.PostAsJsonAsync(APP_PATH + "/api/Account/Register", registerModel).Result;
                return response.StatusCode.ToString();
            }
        }
        // получение токена
        static Dictionary<string, string> GetTokenDictionary(string userName, string password)
        {
            var pairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>( "grant_type", "password" ),
                    new KeyValuePair<string, string>( "username", userName ),
                    new KeyValuePair<string, string> ( "Password", password )
                };
            var content = new FormUrlEncodedContent(pairs);

            using (var client = new HttpClient())
            {
                var response = client.PostAsync(APP_PATH + "/Token", content).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                // Десериализация полученного JSON-объекта
                Dictionary<string, string> tokenDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                return tokenDictionary;
            }
        }

        // создаем http-клиента с токеном 
        static HttpClient CreateClient(string accessToken = "")
        {
            var client = new HttpClient();
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            }
            return client;
        }

        // получаем информацию о клиенте 
        static string GetUserInfo(string token)
        {
            using (var client = CreateClient(token))
            {
                var response = client.GetAsync(APP_PATH + "/api/Account/UserInfo").Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        // обращаемся по маршруту api/values 
        static string GetValues(string token)
        {
            using (var client = CreateClient(token))
            {
                //var response = client.GetAsync(APP_PATH + "/api/values").Result;
                //return response.Content.ReadAsStringAsync().Result;

                var response = client.GetAsync(APP_PATH + "/api/MyApi").Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
