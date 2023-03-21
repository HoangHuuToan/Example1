using Example01.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Microsoft.JSInterop;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Data;
using System.Xml.Linq;

namespace Example01.Pages
{
    public partial class MyPage
    {
        [Parameter]
        public string name { get; set; }

        public List<User> List_User { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            var builder = WebApplication.CreateBuilder();

            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=127.0.0.1;uid=root;pwd=20150601;database=user_db";

            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = builder.Configuration.GetConnectionString("ConnectTo_DB");
            conn.Open();


            MySqlCommand cmd = new MySqlCommand("SELECT * FROM user", conn);

            MySqlDataReader reader = cmd.ExecuteReader();


            

            while (reader.Read())
            {   
                User user = new User(reader.GetInt32("id"),reader.GetString("name"), reader.GetString("email"), reader.GetString("number_phone"), reader.GetString("role"));
                List_User.Add(user);
            }
            
            
        }
    }
}
