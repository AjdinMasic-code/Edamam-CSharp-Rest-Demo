<h1>Edamam REST API Demo</h1>

<p>
  This project contains the source code for communicating to the Edamam Recipe API(EdamamComplete/RestRequest) and how to make calls to the Rest App.
</p>

<p>
  Rest Request was set up as a class Library so you can compile the whole project into a convenient .dll file and reference it in your future projects. You can see this in action within EdamamComplete/testingEdamam after cloning and opening the solution file in Visual Studio under references in the solution explorer.
</p>

<p>
  In order to make this project work, after cloning/downloading the project, you will need to go open the RestApp.cs file in Rest Request. Change the appId and appKey variables to the values that Edamam provided you. While you're there you can also modify the toLimit variable from 25 to a number you want so you can get more recipes.

  After you have done so make sure that you rebuild or run the solution. This will generate the .dll within your-project-directory/EdamamComplete\RestRequest\RestRequest\bin\Debug(Either Debug or Release depending on what options you have selected)
</p>
<p>
  How to call the Rest Request App:
  
  <div>
    Response response = await RestApp.Request("GET", "search term(example: chicken)");
  </div>
  
  That's pretty much all you have to do to communicate with Edamam API. However, if you want to further process the data as you'll see in the testingEdamam Project, you'll have to take additional steps in order to achieve that.
</p>

<p>
  <strong>What I did to process the information</strong>
</p>
<p>
  In the case below I needed to get the hits key in the response that Edamam sends back to me. Hits contains all of the recipe information that I need.
</p>
<pre><code class='language-cs'>
    //Create a JObject from the string jsonResponse
    JObject recipes = JObject.Parse(jsonResponse);
    
    //Create a JArray from a key element in the JSON(hits)
    //hits is a JSON Array of objects
    JArray recipeArr = JArray.Parse(recipes["hits"].ToString());
    
    //Loop through all of the objects found in recipes["hits"]
    for (var i = 0; i < recipeArr.Count(); ++i)
    {
        //Due to our set toLimit in the RestRequest application we will at maximum only get 25 recipes back.
        recipeList.Add(recipeArr[i]["recipe"]["label"].ToString());
    }
</code></pre>
