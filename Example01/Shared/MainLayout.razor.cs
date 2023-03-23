
using Example01.Data;
using Example01.Pages;
using Microsoft.AspNetCore.Components;
using MySql.Data.MySqlClient;
using System.Data;

namespace Example01.Shared
{   
    public partial class MainLayout
    {
        [Parameter]
        public bool status { get; set; } = false;

        public string cate { get; set; }="";

        public List<User> User_Update { get; set; } = new List<User>();

        public List<User> List_User { get; set; } = new();

        protected override Task OnInitializedAsync()
        {
            get_data();
            return base.OnInitializedAsync();
        }
        public void get_data()
        {
            this.List_User.Clear();
            var builder = WebApplication.CreateBuilder();
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=root;pwd=20150601;database=user_db";
            MySqlConnection conn = new MySqlConnection(builder.Configuration.GetConnectionString("ConnectTo_DB"));
            conn.Open();


            MySqlCommand cmd = new MySqlCommand("SELECT * FROM user,role WHERE user.role = id_role", conn);

            MySqlDataReader reader = cmd.ExecuteReader();




            while (reader.Read())
            {
                User user = new User(reader.GetInt32("id"), reader.GetString("name"), reader.GetString("email"), reader.GetString("number_phone"), reader.GetString("name_role"));
                this.List_User.Add(user);
            }
            conn.Close();

        }
        public void show_add_form() 
        {
            status = true;
            cate = "add";
            AdditonRef.id = null;
            AdditonRef.name = "";
            AdditonRef.email = "";
            AdditonRef.number_phone = "";
            AdditonRef.role = "";
        }

        public Addition AdditonRef { get; set; } = new();

        public void show_update_form(int user_ind)
        {
            User_Update.Clear();
            status = true;
            cate = "update";

            var builder = WebApplication.CreateBuilder();
            //MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=127.0.0.1;uid=root;pwd=20150601;database=user_db";

            MySqlConnection conn = new MySqlConnection(builder.Configuration.GetConnectionString("ConnectTo_DB"));
            //conn.ConnectionString = builder.Configuration.GetConnectionString("ConnectTo_DB");
            conn.Open();


            MySqlCommand cmd = new MySqlCommand("SELECT * FROM user WHERE id = " + user_ind, conn);

            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                User user = new User(reader.GetInt32("id"), reader.GetString("name"), reader.GetString("email"), reader.GetString("number_phone"), reader.GetString("role"));
                User_Update.Add(user);
            }
            conn.Close();
            AdditonRef.assign_data();
        }

        public void close_form() { status = false; }

        public void update_user(int user_id) 
        {
            var builder = WebApplication.CreateBuilder();
            //MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=127.0.0.1;uid=root;pwd=20150601;database=user_db";

            MySqlConnection conn = new MySqlConnection(builder.Configuration.GetConnectionString("ConnectTo_DB"));
            //conn.ConnectionString = builder.Configuration.GetConnectionString("ConnectTo_DB");
            conn.Open();


            MySqlCommand cmd = new MySqlCommand("Update user SET id =" + AdditonRef.id + " , name = '" +AdditonRef.name + "' , email = '" + AdditonRef.email + "' , number_phone ='" + AdditonRef.number_phone + "' , role ='" + AdditonRef.role + "' WHERE id = " + AdditonRef.id, conn);

            cmd.ExecuteReader();
            conn.Close();
            get_data();
        }

        public void add_user() 
        {
            var builder = WebApplication.CreateBuilder();

            if (AdditonRef.name == null || AdditonRef.email == null || AdditonRef.number_phone == null || AdditonRef.role == null)
            {
                // Không cho phép trể trống
            }
            else
            {
                string sql = "insert into user  values(@id,@name,@email,@number_phone,@role)";
                // Create the connection (and be sure to dispose it at the end)
                var a = builder.Configuration.GetConnectionString("ConnectTo_DB");
                using (MySqlConnection cnn = new MySqlConnection(builder.Configuration.GetConnectionString("ConnectTo_DB")))
                {
                    cnn.Open();
                    // Open the connection to the database. 
                    // This is the first critical step in the process.
                    // If we cannot reach the db then we have connectivity problems

                    // Prepare the command to be executed on the db
                    using (MySqlCommand cmd = new MySqlCommand(sql, cnn))
                    {

                        // Create and set the parameters values 
                        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = AdditonRef.id;
                        cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = AdditonRef.name;
                        cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = AdditonRef.email;
                        cmd.Parameters.Add("@number_phone", MySqlDbType.VarChar).Value = AdditonRef.number_phone;
                        cmd.Parameters.Add("@role", MySqlDbType.VarChar).Value = AdditonRef.role;

                        // Let's ask the db to execute the query
                        cmd.ExecuteNonQuery();

                    }
                    cnn.Close();
                    get_data();
                }
                
            }
        }

        public void del_user(int id)
        {
            var builder = WebApplication.CreateBuilder();

            //MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=127.0.0.1;uid=root;pwd=20150601;database=user_db";

            MySqlConnection conn = new MySqlConnection(builder.Configuration.GetConnectionString("ConnectTo_DB"));
            //conn.ConnectionString = builder.Configuration.GetConnectionString("ConnectTo_DB");
            conn.Open();


            MySqlCommand cmd = new MySqlCommand("DELETE FROM user WHERE id = " + id, conn);

            cmd.ExecuteNonQuery();
            conn.Close();
            get_data();
            //StateHasChanged();
        }
    }
}
