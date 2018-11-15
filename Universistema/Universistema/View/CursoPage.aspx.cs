using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;



public partial class View_Curso : System.Web.UI.Page
{
    static string connString = "Server=localhost;Database=universistema;Uid=root;Pwd=admin";
    MySqlConnection connection = new MySqlConnection(connString);
    //string command = connection.CreateCommand();

    protected void Page_Load(object sender, EventArgs e)
    {
        var command = connection.CreateCommand();
        MySqlDataReader reader = null;
        try
        {
            connection.Open();
            command.CommandText = "SELECT * FROM cursos";
            reader = command.ExecuteReader();
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }

        using (MySqlConnection sqlCon = new MySqlConnection())
        {
            connection.Open();
            MySqlDataAdapter sqlDa = new MySqlDataAdapter("SELECT * FROM cursos", connection);
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            cursoList.DataSource = dtbl;
            cursoList.DataBind();
        }

    }
}