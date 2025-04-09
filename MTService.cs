
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services
{
    public class SmsService 
    {
        public async Task<bool> SendText(string number, string message)
        {
            // Initialise
           
            var activationUrl = "https://rest.textmagic.com/api/v2/messages";
            
            if (string.IsNullOrWhiteSpace(message))
            {
                return false;
            }
            
            try
            {
                var postData = new Dictionary<string, string>
                {
                    { "text", message },
                    { "phones", number }
                };
                var content = new FormUrlEncodedContent(postData);

                using var httpClient = new HttpClient();
                
                httpClient.DefaultRequestHeaders.Add("X-TM-Username", "MT user name");
                httpClient.DefaultRequestHeaders.Add("X-TM-Key", "MT api secret key");
                    
                var response = await httpClient.PostAsync(activationUrl, content);
                var apiResponse = await response.Content.ReadAsStringAsync();
                
                if (!response.IsSuccessStatusCode)
                {
                    // process apiResponse
                }
                
            }
            catch(Exception)
            {
                return false;
            }
            
            return true;
        }
    }
}
