using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestRequest
{
    public class RestApp
    {
        /*
         * 
         * Change the values of appId and appKey with the appId and appKey that Edamam generates for you.
         * 
         */
        const string appId = "insert-appId-here",
                     appKey= "insert-appKey-here",
                     baseUrl = "https://api.edamam.com/search?q=",
                     fromLimit = "0",
                     toLimit = "25";

        /*
         * When calling the RestApp this is the starting point for all connnections to the Edamam Api
         */
        public static async Task<Response> Request(string method, string query, bool hasFailedOnce = false)
        {
            var requestMessage = new HttpRequestMessage
            {
                Method = new HttpMethod(method.ToUpper()),
                RequestUri = new Uri($"{baseUrl}{query}&app_id={appId}&app_key={appKey}&from={fromLimit}&to={toLimit}"),
            };

            HttpClient client = new HttpClient();

            // Client handles all calls to the API
            var response = client.SendAsync(requestMessage).Result;

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                if(!hasFailedOnce)
                {
                    return await Request(method, query, true);
                }
                else
                {
                    //Temporary error message
                    throw new Exception("There is an issue with authorization. Please submit this error to the admin.");
                }
            }

            //Makes a call to the Response constructor below and builds out a message in human readable format.
            return new Response(response);
        }
    }

    public class Response
    {
        public HttpResponseMessage Message;

        public Response(HttpResponseMessage responseMessage) => Message = responseMessage;

        public HttpContent Content { get => Message.Content; }

        public async Task<string> ReadContentAsync()
        {
            using (var read = new StreamReader(await Content.ReadAsStreamAsync()))
            {
                return await read.ReadToEndAsync();
            }
        }
    }
}
