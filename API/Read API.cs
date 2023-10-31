using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BitBot.API.API_Model;

namespace BitBot.API
{
    public class Read_API
    {
        public static async Task<List<Result>> Response()
        {
            HttpClient httpClient = new HttpClient();
            string stringAPI = "https://api.wallex.ir/v1/currencies/stats";

            HttpResponseMessage response = await httpClient.GetAsync(stringAPI);
            if (response.IsSuccessStatusCode)
            {
                string apiresponse = await response.Content.ReadAsStringAsync();

                // Deserialize the entire JSON response into ApiResponseWrapper
                Root apiWrapper = JsonConvert.DeserializeObject<Root>(apiresponse);

                // Return the 'result' list
                return apiWrapper.result;
            }

            // If the response is not successful, return an empty list or handle the error as needed
            return new List<Result>();
        }
    }
}
