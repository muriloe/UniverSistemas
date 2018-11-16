using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;


public partial class _Default : Page
{
    static string connString = "Server=localhost;Database=universistema;Uid=root;Pwd=admin";
    MySqlConnection connection = new MySqlConnection(connString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadCursos();
        }
    }

    private void loadCursos()
    {

        using (MySqlConnection sqlCon = new MySqlConnection())
        {
            connection.Open();
            MySqlDataReader reader = null;

            MySqlCommand command = new MySqlCommand("SELECT * FROM cursos", connection);

            cursoList.DataSource = command.ExecuteReader();
            cursoList.DataBind();
        }
    }

    protected void cursoList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idDoCurso = Convert.ToInt32(cursoList.SelectedItem.Value);

        CarregaCurriculo1.CursoId = idDoCurso;
    }
}