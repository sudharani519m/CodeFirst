using ConsumeWebAPI.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsumeWebAPI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Call the API and bind the data to the DataGridView
            List<OurHero> heroes = await GetOurHero();

            // Example: Bind the data to a DataGridView (assuming you have a DataGridView named dataGridView1)
            dataGridView1.DataSource = heroes;
        }

        public async Task<List<OurHero>> GetOurHero()
        {

            //Step-1: Call the auth api and get the token
            // step-2: Call the api with the token by passig it in the header   

            //string jsonString = "";
            //WebRequest request = WebRequest.Create("http://localhost:5134/api/OurHero?isActive=true");
            //request.Method = "GET";
            //request.ContentType = "application/x-www-form-urlencoded";
            //using (var response = request.GetResponse())
            //{
            //    using (var reader = new StreamReader(response.GetResponseStream()))
            //    {
            //        jsonString = reader.ReadToEnd();
            //    }
            //}

            //return jsonString;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5134/api/"); // Replace with your API's base URL and port
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // Add the token to the request headers
            var user = new UserModel
            {
                Username = textBox1.Text,
                Password = textBox2.Text,
                EmailAddress = textBox3.Text,
                DateOfJoing = DateTime.Now
            };
            txtToken.Text = await GetToken(user);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", txtToken.Text);
            // End the token to the request headers

            HttpResponseMessage response = await client.GetAsync("OurHero?isActive=true"); // "products" is the API endpoint
            if (response.IsSuccessStatusCode)
            {
                // Read response content as JSON string
                string jsonResponse = await response.Content.ReadAsStringAsync();

                // Deserialize JSON into a C# object (e.g., a list of products)
                List<OurHero> products = JsonConvert.DeserializeObject<List<OurHero>>(jsonResponse);

                // Update UI with the retrieved data
                // ...
                return products; // Return the JSON string for further processing
            }
            else
            {
                // Handle error, display message to the user
                MessageBox.Show($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
            return new List<OurHero>(); // Return an empty list in case of error
        }

        public async Task<string> GetToken(UserModel user)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5134/api/"); // Replace with your API's base URL and port
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var content = new StringContent(JsonConvert.SerializeObject(user), System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("Login", content); // "Auth" is the API endpoint for authentication
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                TokenModel token = JsonConvert.DeserializeObject<TokenModel>(jsonResponse);
                return token.Token;
            }
            else
            {
                MessageBox.Show($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return null;
            }
        }

        private async void GetToten_Click(object sender, EventArgs e)
        {
            var user = new UserModel
            {
                Username = textBox1.Text,
                Password = textBox2.Text,
                EmailAddress = textBox3.Text,
                DateOfJoing=DateTime.Now
            };
            txtToken.Text = await GetToken(user);
        }
    }
}
