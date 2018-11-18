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
        loadCursosTable();
    }

    private void loadCursosTable()
    {
        btnCadastrar.Visible = true;
        btnEditar.Visible = false; 
        using (MySqlConnection sqlCon = new MySqlConnection())
        {
            connection.Open();
            MySqlDataAdapter sqlDa = new MySqlDataAdapter("SELECT * FROM cursos", connection);
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            cursoList.DataSource = dtbl;
            cursoList.DataBind();
            connection.Close();
        }
    }

    private void saveCurso(string nome)
    {
        using (MySqlConnection sqlCon = new MySqlConnection())
        {
            connection.Open();
            MySqlCommand comm = connection.CreateCommand();
            comm.CommandText = "INSERT INTO `universistema`.`cursos` (`nome`) VALUES(@nomeCurso)";
            comm.Parameters.AddWithValue("@nomeCurso", nome);

            comm.ExecuteNonQuery();
            connection.Close();
            nomeCurso.Text = "";
            loadCursosTable();

        }
    }

    protected void editSelect_Click(object sender, EventArgs e)
    {
        int idCurso = Convert.ToInt32((sender as LinkButton).CommandArgument);
        btnCadastrar.Visible = false;
        btnEditar.Visible = true;
        btnEditar.CommandArgument = idCurso.ToString();

        using (MySqlConnection sqlCon = new MySqlConnection())
        {
            MySqlDataReader reader = null;
            connection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM cursos WHERE idCursos='" + idCurso + "'", connection);
            DataTable dtbl = new DataTable();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                statusLabel.Text = "Editar curso";
                nomeCurso.Text = (string)reader["nome"];

            }
            connection.Close();
        }

    }

    protected void apagarSelect_Click(object sender, EventArgs e)
    {
        int idCurso = Convert.ToInt32((sender as LinkButton).CommandArgument);
        using (MySqlConnection sqlCon = new MySqlConnection())
        {
            connection.Open();
            MySqlCommand comm = connection.CreateCommand();
            comm.CommandText = " DELETE FROM `universistema`.`cursos` WHERE(`idcursos` = '"+ idCurso + "');";

            comm.ExecuteNonQuery();
            connection.Close();
            nomeCurso.Text = "";
            loadCursosTable();
        }
    }

    protected void btnCadastrar_Click(object sender, EventArgs e)
    {
        if(nomeCurso.Text != "")
        {
            saveCurso(nomeCurso.Text);
        }
        else
        {
            Response.Write("<div class='alert alert-danger' role='alert'> Inisira um nome para o curso</div>");
        }
    }

    protected void btnEditar_Click(object sender, EventArgs e)
    {
        var argument = ((Button)sender).CommandArgument;
        string idCurso = argument.ToString();
        using (MySqlConnection sqlCon = new MySqlConnection())
        {
            MySqlDataReader reader = null;
            connection.Open();
            MySqlCommand command = new MySqlCommand("UPDATE `universistema`.`cursos` SET `nome` = '"+ nomeCurso.Text + "' WHERE(`idcursos` = '"+ idCurso + "')", connection);
            DataTable dtbl = new DataTable();
            command.ExecuteNonQuery();
            connection.Close();
            nomeCurso.Text = "";
            loadCursosTable();
            btnCadastrar.Visible = true;
            btnEditar.Visible = false;
            statusLabel.Text = "Novo Curso";

        }
    }
}