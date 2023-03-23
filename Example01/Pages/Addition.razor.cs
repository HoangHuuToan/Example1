using Example01.Data;
using Example01.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;

namespace Example01.Pages
{
    public partial class Addition
    {

        [Parameter]
        public EventCallback clb { get; set; }

        [Parameter]
        public EventCallback<int> clb_update_user { get; set; }

        [Parameter]
        public EventCallback<int> clb_add_user { get; set; }
        public int? id { get; set; }
        public string name { get; set; }

        public string email { get; set; }

        public string number_phone { get; set; }

        public string role { get; set; }

        [Parameter]
        public string cate { get; set; } = "";

        public int user_ind { get; set; } = 0;

        [Parameter]
        public List<User> List_User { get; set; } = new List<User>();

        protected override async Task OnInitializedAsync()
        {
            if (List_User.Any())
            {
                id = List_User[0].Id;
                name = List_User[0].Name;
                email = List_User[0].Email;
                number_phone = List_User[0].NumberPhone;
                role = List_User[0].Role;
            }
        }
        public void assign_data()
        {
            if (List_User.Any())
            {
                id = List_User[0].Id;
                name = List_User[0].Name;
                email = List_User[0].Email;
                number_phone = List_User[0].NumberPhone;
                role = List_User[0].Role;
            }
        }

        private async void add_user()
        {
            clb_add_user.InvokeAsync();
            clb.InvokeAsync();
            
        }

        // Prepare a proper parameterized query 


        public void update_user(int user_ind)
        {
            clb_update_user.InvokeAsync(user_ind);
        }

    }
}
