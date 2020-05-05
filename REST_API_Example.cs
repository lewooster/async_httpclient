using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;



namespace REST_API_Example
{

    public class User
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }

    class Program
    {

        static HttpClient client = new HttpClient();


        static void ShowUsers(List<User> user)
        {
            for (int i = 0; i < user.Count; i++)
            {
                Console.WriteLine($"UserId: {user[i].UserId}\tPrice: " +
                $"{user[i].Title}\tCategory: {user[i].Body}");
            }
          
        }


        static async Task<List<User>> GetUsersAsync(string path)
        {
            
            List<User> user = null;
            
            var response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
               user = await response.Content.ReadAsAsync<List<User>>();
                //var jsonString = await response.Content.ReadAsStringAsync();
                //user = JsonConvert.DeserializeObject<List<User>>(jsonString);
                //returnView(user);

            }
            return user;
        }


        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }



        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/posts");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {

                List<User> user = null;

                user = await GetUsersAsync("https://jsonplaceholder.typicode.com/posts");
                ShowUsers(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

      
    }
}
