using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MultiViewApp.Model
{
    class Server
    {
        private static string currIp = "192.168.0.18:8080";
        private static string getDataPath = "/AiR_Projekt/get_chart_mes.php";
        //private static string setDataPath = "/AiR_Projekt/write_to_file.php";
        private static string setDataPath = "/AiR_Projekt/led_display.php";
        private static string getDataURL = "http://" + currIp.ToString() + getDataPath;
        private static string sendDataURL = "http://" + currIp.ToString() + setDataPath;

        public static string GetIp()
        {
            return currIp;
        }

        public static void SetUrls(string ip)
        {
            getDataURL = "http://" + ip.ToString() + getDataPath;
            sendDataURL = "http://" + ip.ToString() + setDataPath;
            currIp = ip;
        }

        public static async Task<string> GetData()
        {
            string responseString = "";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsync(getDataURL, null);
                    responseString = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("NETWORK ERROR");
                Debug.WriteLine(e);
            }

            return responseString;
        }

        public static async Task<string> SendData(string postName, string data)
        {
            var values = new Dictionary<string, string>
            {
                { postName, data }
            };
            var content = new FormUrlEncodedContent(values);

            string responseString = "";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsync(sendDataURL, content);
                    responseString = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception e)
            {               
                Debug.WriteLine("NETWORK ERROR");
                Debug.WriteLine(e);
            }

            return responseString;
        }

        public static async Task<string> SendData(List<KeyValuePair<string, string>> data)
        {
            var content = new FormUrlEncodedContent(data);

            string responseString = "";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsync(sendDataURL, content);
                    responseString = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("NETWORK ERROR");
                Debug.WriteLine(e);
            }

            return responseString;
        }
    }    
}
