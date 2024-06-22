using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EmiasWPF10.ViewModel.Helpers
{
    internal class ApiHelper
    {
        private static string Url = "https://localhost:7221/api/";

        public static string Put(string nameTable, string json, int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage message = client.PutAsync(Url + nameTable + "/" + id, content).Result;
                return message.Content.ReadAsStringAsync().Result;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public static string Get(string nameTable)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage message = client.GetAsync(Url + nameTable + "/").Result;
                return message.Content.ReadAsStringAsync().Result;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public static string GetByID(string nameTable, string id)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage message = client.GetAsync(Url + nameTable + "/" + id).Result;
                return message.Content.ReadAsStringAsync().Result;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public static string Post(string nameTable, string json)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage message = client.PostAsync(Url + nameTable + "/", content).Result;
                return message.Content.ReadAsStringAsync().Result;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public static string Delete(string nameTable, int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage message = client.DeleteAsync(Url + nameTable + "/" + id).Result;
                return message.Content.ReadAsStringAsync().Result;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
