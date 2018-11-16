using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

public partial class View_Disciplina : System.Web.UI.Page
{
    static string connString = "Server=localhost;Database=universistema;Uid=root;Pwd=admin";
    MySqlConnection connection = new MySqlConnection(connString);

    protected void Page_Load(object sender, EventArgs e)
    {
        btnEditar.Visible = false;
        PainelCadastro.Visible = false;

        if (!IsPostBack)
        {

            loadCursos();
            cursoList.SelectedIndex = 0;
            loadPeriodos(Convert.ToInt32(cursoList.SelectedItem.Value));
            periodosList.SelectedIndex = 0;
            loadDisciplinas(Convert.ToInt32(periodosList.SelectedItem.Value));


        }
        else
        {
            var indiceCurso = cursoList.SelectedIndex;
            var indicePeriodo = periodosList.SelectedIndex;
            if(indiceCurso != -1 && indicePeriodo != -1)
            {
                loadCursos();
                PainelCadastro.Visible = true;
                periodosList.Visible = true;
                cursoList.SelectedIndex = indiceCurso;
                periodosList.SelectedIndex = indicePeriodo;
                try
                {
                    loadDisciplinas(Convert.ToInt32(periodosList.SelectedItem.Value));
                }
                catch
                {
                }
                
            }

        }
    }

    private void loadCursos()
    {
        periodosList.Visible = false;
        using (MySqlConnection sqlCon = new MySqlConnection())
        {
            connection.Open();
            MySqlDataReader reader = null;

            MySqlCommand command = new MySqlCommand("SELECT * FROM cursos", connection);

            cursoList.DataSource = command.ExecuteReader();
            cursoList.DataBind();
            connection.Close();
        }
    }

    private void loadPeriodos(int idCurso)
    {
        periodosList.Visible = false;
        using (MySqlConnection sqlCon = new MySqlConnection())
        {
            connection.Open();
            try
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM periodos WHERE cursoId='" + idCurso + "'", connection);
                periodosList.Visible = true;
                periodosList.DataSource = command.ExecuteReader();
                periodosList.DataBind();
                connection.Close();
            }
            catch
            {
                Response.Write("Não Encontrado");
                cursoList.SelectedIndex = 0;
                periodosList.SelectedIndex = 0;

            }

        }
    }

    private void loadDisciplinas(int idPeriodo)
    {
        using (MySqlConnection sqlCon = new MySqlConnection())
        {
            connection.Open();

            MySqlCommand command = new MySqlCommand("SELECT * FROM disciplinas WHERE periodoId='" + idPeriodo + "'", connection);
            disciplinasList.DataSource = command.ExecuteReader();
            disciplinasList.DataBind();
            connection.Close();
        }
    }

    protected void cursoList_SelectedIndexChanged(object sender, EventArgs e)
    {
        periodosList.Visible = true;
        int idDoCurso = Convert.ToInt32(cursoList.SelectedItem.Value);
        loadPeriodos(idDoCurso);
    }

    protected void periodosList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idPeriodo = Convert.ToInt32(periodosList.SelectedItem.Value);
        loadDisciplinas(idPeriodo);
        PainelCadastro.Visible = true;
    }

    protected void btnCadastrar_Click(object sender, EventArgs e)
    {
        int idDoCurso = Convert.ToInt32(cursoList.SelectedItem.Value);
        int idDoPeriodo = Convert.ToInt32(periodosList.SelectedItem.Value);

        using (MySqlConnection sqlCon = new MySqlConnection())
        {
            connection.Open();
            MySqlCommand comm = connection.CreateCommand();
            comm.CommandText = "INSERT INTO `universistema`.`disciplinas` (`nome`, `at`, `ap`, `cred`, `hr`, `ha`, `periodoId`) " +
                                "VALUES(@nome, @at, @ap, @cred, @ht, @ha, @idDoPeriodo)";

            comm.Parameters.AddWithValue("@nome", nomeDisciplina.Text);
            comm.Parameters.AddWithValue("@at", at.Text);
            comm.Parameters.AddWithValue("@ap", ap.Text);
            comm.Parameters.AddWithValue("@cred", cred.Text);
            comm.Parameters.AddWithValue("@ht", hr.Text);
            comm.Parameters.AddWithValue("@ha", ha.Text);
            comm.Parameters.AddWithValue("@idDoPeriodo", idDoPeriodo);
            comm.ExecuteNonQuery();
            connection.Close();
            //numeroPeriodo.Text = "";


        }
        loadDisciplinas(Convert.ToInt32(periodosList.SelectedItem.Value));
        clearFields();

    }

    private void clearFields()
    {
        nomeDisciplina.Text = null;
        at.Text = null;
        ap.Text = null;
        cred.Text = null;
        hr.Text = null;
        ha.Text = null;
    }

    protected void btnEditar_Click(object sender, EventArgs e)
    {
        var argument = ((Button)sender).CommandArgument;
        string idCurso = argument.ToString();
        using (MySqlConnection sqlCon = new MySqlConnection())
        {
            MySqlDataReader reader = null;
            connection.Open();
            MySqlCommand command = new MySqlCommand("UPDATE `universistema`.`disciplinas` SET `nome` = '" + nomeDisciplina.Text + 
                                                    "', `at` = '" + at.Text +
                                                    "', `ap` = '" + ap.Text +
                                                    "', `cred` = '" + cred.Text +
                                                    "', `ha` = '" + ha.Text +
                                                    "', `hr` = '" + hr.Text +
                                                    "' WHERE(`iddisciplinas` = '" + idCurso + "')", connection);
            DataTable dtbl = new DataTable();
            command.ExecuteNonQuery();
            connection.Close();
            clearFields();
            loadDisciplinas(Convert.ToInt32(periodosList.SelectedItem.Value));
            btnCadastrar.Visible = true;
            btnEditar.Visible = false;
            statusLabel.Text = "Novo Periodo";

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
            MySqlCommand command = new MySqlCommand("SELECT * FROM disciplinas WHERE iddisciplinas='" + idCurso + "'", connection);
            DataTable dtbl = new DataTable();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                statusLabel.Text = "Editar periodo";
                var temp = reader["nome"];
                string stringText = Convert.ToString(temp);
                nomeDisciplina.Text = stringText;

                temp = reader["at"];
                stringText = Convert.ToString(temp);
                at.Text = stringText;

                temp = reader["ap"];
                stringText = Convert.ToString(temp);
                ap.Text = stringText;

                temp = reader["cred"];
                stringText = Convert.ToString(temp);
                cred.Text = stringText;

                temp = reader["hr"];
                stringText = Convert.ToString(temp);
                hr.Text = stringText;

                temp = reader["ha"];
                stringText = Convert.ToString(temp);
                ha.Text = stringText;

            }
            connection.Close();
        }
    }

    protected void apagarSelect_Click(object sender, EventArgs e)
    {
        int idDisciplina = Convert.ToInt32((sender as LinkButton).CommandArgument);
        using (MySqlConnection sqlCon = new MySqlConnection())
        {
            connection.Open();
            MySqlCommand comm = connection.CreateCommand();
            comm.CommandText = " DELETE FROM `universistema`.`disciplinas` WHERE(`iddisciplinas` = '" + idDisciplina + "');";

            comm.ExecuteNonQuery();
            connection.Close();
            Response.Write("DISCIPLINA DELETADA");
            loadDisciplinas(Convert.ToInt32(periodosList.SelectedItem.Value));
        }
    }
}