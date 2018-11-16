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
            try
            {
                connection.Open();
                MySqlCommand comm = connection.CreateCommand();
                comm.CommandText = " DELETE FROM `universistema`.`periodos` WHERE(`idperiodos` = '" + idPeriodo + "');";

                comm.ExecuteNonQuery();
               
                Response.Write("PERIODO DELETADO");
                loadPeriodos(Convert.ToInt32(cursoList.SelectedItem.Value));
            }
            catch
            {
                Response.Write("<div>VOCÊ NÃO PODE DELETAR UM PERIODO QUE TENHA DISCIPLINAS REGISTRADAS</div>");
            }
            connection.Close();



        }

        MySqlDataReader reader = null;

        int idDoCurso = 0;
        reader = null;
        connection.Open();
        MySqlCommand command = new MySqlCommand("SELECT * FROM periodos WHERE idperiodos='" + idPeriodo + "'", connection);
        reader = command.ExecuteReader();
        while (reader.Read())
        {
            var temp = reader["cursoId"];
            string stringText = Convert.ToString(temp);
            idDoCurso = Convert.ToInt32(stringText);
        }
        connection.Close();
        atualizarSomatorioCurso(idDoCurso);
    }

    private void atualizarSomatorioCurso(int cursoId)
    {
        int somatorioAT = 0;
        int somatorioAP = 0;
        int somatorioCred = 0;
        int somatorioHR = 0;
        int somatorioHA = 0;

        MySqlDataReader reader = null;


        connection.Open();
        MySqlCommand command = new MySqlCommand("SELECT * FROM periodos WHERE cursoId='" + cursoId.ToString() + "'", connection);
        reader = command.ExecuteReader();
        while (reader.Read())
        {
            var temp = reader["at"];
            string stringText = Convert.ToString(temp);
            if (stringText != "")
            {
                somatorioAT += Convert.ToInt32(stringText);
            }


            temp = reader["ap"];
            stringText = Convert.ToString(temp);
            if (stringText != "")
            {
                somatorioAP += Convert.ToInt32(stringText);
            }

            temp = reader["cred"];
            stringText = Convert.ToString(temp);
            if (stringText != "")
            {
                somatorioCred += Convert.ToInt32(stringText);
            }

            temp = reader["hr"];
            stringText = Convert.ToString(temp);
            if (stringText != "")
            {
                somatorioHR += Convert.ToInt32(stringText);
            }

            temp = reader["ha"];
            stringText = Convert.ToString(temp);
            if (stringText != "")
            {
                somatorioHA += Convert.ToInt32(stringText);
            }
        }
        connection.Close();

        connection.Open();
        command = new MySqlCommand("UPDATE `universistema`.`cursos` SET " +
                                                    " `at` = '" + somatorioAT +
                                                    "', `ap` = '" + somatorioAP +
                                                    "', `cred` = '" + somatorioCred +
                                                    "', `ha` = '" + somatorioHA +
                                                    "', `hr` = '" + somatorioHR +
                                                    "' WHERE(`idcursos` = '" + cursoId + "')", connection);

        command.ExecuteNonQuery();
        connection.Close();
    }
}

