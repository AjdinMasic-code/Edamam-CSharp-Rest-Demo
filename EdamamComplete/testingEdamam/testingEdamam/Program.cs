using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestRequest;

namespace testingEdamam
{
    class Program
    {

        private static List<string> recipeList = new List<string>();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Console.WriteLine("------------------------");

            GetRecipes("chicken").Wait();

            foreach (var rec in recipeList)
            {
                Console.WriteLine(rec);
            }

            Console.ReadKey();
        }

        private static async Task GetRecipes(string recipe)
        {
            //This is how I have set up calls to the RestApp
            //PARAMS: string method, string query
            Response response = await RestApp.Request("GET", recipe);

            //serialize the response to a string
            string jsonResponse = await response.Content.ReadAsStringAsync();

            //Create a JObject from the string jsonResponse
            JObject recipes = JObject.Parse(jsonResponse);

            //Create a JArray from a key element in the JSON(hits)
            //hits is a JSON Array of objects hence, the need to convert the JObject into an Array
            JArray recipeArr = JArray.Parse(recipes["hits"].ToString());

           
            //Loop through all of the objects found in recipes["hits"]
            for (var i = 0; i < recipeArr.Count(); ++i)
            {
                //Due to our set toLimit in the RestRequest application we will at maximum only get 25 recipes back.
                recipeList.Add(recipeArr[i]["recipe"]["label"].ToString());
            }

        }
    }
}
