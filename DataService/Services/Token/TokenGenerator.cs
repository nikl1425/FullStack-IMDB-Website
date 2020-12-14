using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace DataService.Services.Token
{
    public class TokenGenerator
    {
        private string authString { get; set; }
        private int retryCount { get; set; }
        private DateTime expiredDateTimeUtc { get; set; }
        private string username { get; set; }
        private string password { get; set; }
        private string getTokenUri { get; set; }
        
        public string AuthString 
        {
            get
            {
                if (expiredDateTimeUtc < DateTime.Now)
                {
                    authString = GetToken();
                }
                return authString;
            }
        }

        // Constructor that takes username, password, and endpoint for the Token API
        public TokenGenerator(string usernameParam, string passwordParam, string uri) 
        {
            // Set private properties
            getTokenUri = uri;
            username = usernameParam;
            password = passwordParam;
            authString = GetToken();
        }
						
        private string GetToken() 
        {
            string error;
            do
            {
                // Build authentication and store in variable
                string authenticationString = BuildBasicAuthenticationString(username, password);
                // Begin new HttpClient. Use "using" to ensure the resource is released
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authenticationString); 
                    // Begin getting the response from the REST API call, passing in the endpoint to the method
                    using (HttpResponseMessage response = client.GetAsync(getTokenUri).Result)
                    {
                        // True if HttpStatusCode was in the Successful range (200-299); otherwise false
                        if (response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("Token successfully retrieved");
                            Console.WriteLine(response.Content.ReadAsStringAsync());
                            expiredDateTimeUtc = DateTime.Now.AddMinutes(20);       
                            retryCount = 0;                                           
                            return response.Content.ReadAsStringAsync().Result;
                        }
                        retryCount++;
                        error = string.Format("{0}, {1}", (int)response.StatusCode, response.ReasonPhrase);
                        Console.WriteLine("Http Error {0}. Problem getting token", error);
                    }
                }
            } while (retryCount < 3);
            retryCount = 0;
            throw new Exception(string.Format("Failed 3 retries! Error {0}", error));
        }


        private static string BuildBasicAuthenticationString(string username, string password)
        {
            var byteArray = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", username, password));
            var convertedString = Convert.ToBase64String(byteArray);
            return convertedString;
        }
    }
}