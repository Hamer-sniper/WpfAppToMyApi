﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppToMyApi
{
    public class DataBookDataApi
    {
        string baseUrl = @"https://localhost:7161/api/MyApi/";
        private HttpClient httpClient { get; set; }

        public DataBookDataApi()
        {
            httpClient = new HttpClient();
        }

        public void AddTokenToClient(string accessToken = "")
        {
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            }
        }

        public string GetToken(string UserName, string Password)
        {
            //var registerResult = Register(userName, password);

            //Console.WriteLine("Статусный код регистрации: {0}", registerResult);

            //Dictionary<string, string> tokenDictionary = GetTokenDictionary(userName, password);
            //token = tokenDictionary["access_token"];

            var token = httpClient.GetStringAsync($"https://localhost:7161/api/Token/{UserName}/{Password}").Result;

            AddTokenToClient(token);
            return token;
        }

        public string Register(string UserName, string Password)
        {
            var token = httpClient.GetStringAsync($"https://localhost:7161/api/Register/{UserName}/{Password}").Result;

            AddTokenToClient(token);
            return token;
        }


        /// <summary>
        /// Получить все записи из БД.
        /// </summary>
        /// <returns></returns>
        public List<DataBook> GetAllDatabooks()
        {
            string url = baseUrl;

            string json = httpClient.GetStringAsync(url).Result;

            return JsonConvert.DeserializeObject<List<DataBook>>(json);
        }

        /// <summary>
        /// Добавить запись в БД.
        /// </summary>
        /// <param name="dataBook">Запись</param>
        public void CreateDataBook(DataBook dataBook)
        {
            string url = baseUrl;

            var r = httpClient.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(dataBook), Encoding.UTF8,
                mediaType: "application/json")
                ).Result;
        }

        /// <summary>
        /// Получить запись из БД.
        /// </summary>
        /// <param name="dataBookId">Id записи</param>
        /// <returns>Запись</returns>
        public DataBook ReadDataBook(int dataBookId)
        {
            string url = baseUrl + dataBookId;

            string json = httpClient.GetStringAsync(url).Result;

            return JsonConvert.DeserializeObject<DataBook>(json);
        }

        /// <summary>
        /// Изменить запсиь в БД.
        /// </summary>
        /// <param name="dataBook">Запись</param>
        public void UpdateDataBook(DataBook dataBook)
        {
            UpdateDataBookById(dataBook.Id, dataBook);
        }

        /// <summary>
        /// Изменить запсиь в БД по Id.
        /// </summary>
        /// <param name="dataBookId">Id записи</param>
        /// <param name="dataBook">Запись</param>
        public void UpdateDataBookById(int dataBookId, DataBook dataBook)
        {
            string url = baseUrl + dataBookId;

            var dataBookToModify = ReadDataBook(dataBookId);

            if (dataBookToModify != null)
            {
                var r = httpClient.PutAsync(
                    requestUri: url,
                    content: new StringContent(JsonConvert.SerializeObject(dataBook), Encoding.UTF8,
                    mediaType: "application/json")
                    ).Result;
            }
        }

        /// <summary>
        /// Удалить запись из БД.
        /// </summary>
        /// <param name="dataBook">Запись</param>
        public void DeleteDataBook(DataBook dataBook)
        {
            DeleteDataBookById(dataBook.Id);
        }

        /// <summary>
        /// Удалить запись из БД по Id.
        /// </summary>
        /// <param name="dataBookId">Id записи</param>
        public void DeleteDataBookById(int dataBookId)
        {
            string url = baseUrl + dataBookId;

            var dataBookToDelete = ReadDataBook(dataBookId);

            if (dataBookToDelete != null)
            {
                var r = httpClient.DeleteAsync(requestUri: url).Result;
            }
        }
    }
}