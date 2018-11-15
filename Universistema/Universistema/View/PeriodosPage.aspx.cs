using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

public partial class View_PeriodosPage : System.Web.UI.Page
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
        btnCadastrar.Visible = true;
        btnEditar.Visible = false;
        using (MySqlConnection sqlCon = new MySqlConnection())
        {
            connection.Open();
            MySqlDataReader reader = null;

            MySqlCommand command = new MySqlCommand("SELECT * FROM cursos", connection);

            cursoList.DataSource = command.ExecuteReader();
            cursoList.DataBind();
        }
    }


    protected void btnCadastrar_Click(object sender, EventArgs e)
    {
        int idDoCurso = Convert.ToInt32(cursoList.SelectedItem.Value);

        if (numeroPeriodo.Text != null)
        {
            try
            {
                int valor = Convert.ToInt32(numeroPeriodo.Text);
                savePeriodo(valor, idDoCurso);
            }catch{

            }
            
        }
    }

    private void savePeriodo(int periodo, int cursoId)
    {


        using (MySqlConnection sqlCon = new MySqlConnection())
        {
            connection.Open();
            MySqlCommand comm = connection.CreateCommand();
            comm.CommandText = "INSERT INTO `universistema`.`periodos` (`numPeriodo`, `cursoId`) VALUES(@numPeriodo, @cursoId)";
            comm.Parameters.AddWithValue("@numPeriodo", periodo);
            comm.Parameters.AddWithValue("@cursoId", cursoId);

            comm.ExecuteNonQuery();
            connection.Close();
            numeroPeriodo.Text = "";
            loadCursos();

        }
    }

    protected void btnEditar_Click(object sender, EventArgs e)
    {
        var argument = ((Button)sender).CommandArgument;
        string idPeriodo = argument.ToString();
        using (MySqlConnection sqlCon = new MySqlConnection())
        {
            MySqlDataReader reader = null;
            connection.Open();
            MySqlCommand command = new MySqlCommand("UPDATE `universistema`.`periodos` SET `numperiodo` = '" + numeroPeriodo.Text + "' WHERE(`idperiodos` = '" + idPeriodo + "')", connection);
            DataTable dtbl = new DataTable();
            command.ExecuteNonQuery();
            connection.Close();
            numeroPeriodo.Text = "";
            loadPeriodos(Convert.ToInt32(cursoList.SelectedItem.Value));
            btnCadastrar.Visible = true;
            btnEditar.Visible = false;
            statusLabel.Text = "Novo Periodo";

        }
    }

    protected void cursoList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idDoCurso = Convert.ToInt32(cursoList.SelectedItem.Value);
        loadPeriodos(idDoCurso);
    }

    private void loadPeriodos(int numCurso)
    {
        btnCadastrar.Visible = true;
        btnEditar.Visible = false;
        using (MySqlConnection sqlCon = new MySqlConnection())
        {
            connection.Open();
            MySqlDataAdapter sqlDa = new MySqlDataAdapter("SELECT * FROM periodos where cursoId='"+ numCurso + "'", connection);
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            periodoList.DataSource = dtbl;
            periodoList.DataBind();
            connection.Close();
        }
    }

    protected void editSelect_Click(object sender, EventArgs e)
    {
        int idPeriodo = Convert.ToInt32((sender as LinkButton).CommandArgument);
        btnCadastrar.Visible = false;
        btnEditar.Visible = true;
        btnEditar.CommandArgument = idPeriodo.ToString();


        using (MySqlConnection sqlCon = new MySqlConnection())
        {
            MySqlDataReader reader = null;
            connection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM periodos WHERE idPeriodos='" + idPeriodo + "'", connection);
            DataTable dtbl = new DataTable();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                statusLabel.Text = "Editar periodo";
                var temp = reader["numPeriodo"];
                string aaa = Convert.ToString(temp);
                numeroPeriodo.Text = aaa;

            }
            connection.Close();
        }
    }

    protected void apagarSelect_Click(object sender, EventArgs e)
    {
        int idPeriodo = Convert.ToInt32((sender as LinkButton).CommandArgument);
        using (MySqlConnection sqlCon = new MySqlConnection())
        {
            connection.Open();
            MySqlCommand comm = connection.CreateCommand();
            comm.CommandText = " DELETE FROM `universistema`.`periodos` WHERE(`idperiodos` = '" + idPeriodo + "');";

            comm.ExecuteNonQuery();
            connection.Close();
            Response.Write("PERIODO DELETADO");
            loadPeriodos(Convert.ToInt32(cursoList.SelectedItem.Value));
        }
    }
}

