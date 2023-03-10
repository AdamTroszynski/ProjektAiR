using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace MultiViewApp.Model
{
    public class IoTServer
    {
        private string ip;

        public IoTServer(string _ip)
        {
            ip = _ip;
        }

        /**
         * @brief obtaining the address of the PHP script from IoT server IP.
         * @return LED display control script URL
         */
        public string ScriptUrl
        {
            get => "http://" + ip + "/server/led_display.php";
        }

        /**
         * @brief Send control request using HttpClient with POST method
         * @param data Control data in JSON format
         */
        public async Task<string> PostControlRequest(List<KeyValuePair<string, string>> data)
        {
            string responseText = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var requestData = new FormUrlEncodedContent(data);
                    // Sent POST request
                    var result = await client.PostAsync(ScriptUrl, requestData);
                    // Read response content
                    responseText = await result.Content.ReadAsStringAsync();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("NETWORK ERROR");
                Debug.WriteLine(e);
            }

            return responseText;
        }
    }
}
