using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;


public partial class View_CarregaCurriculo : System.Web.UI.UserControl
{
    private int cursoId;
    static string connString = "Server=localhost;Database=universistema;Uid=root;Pwd=admin";
    MySqlConnection connection = new MySqlConnection(connString);
    MySqlConnection connection2 = new MySqlConnection(connString);

    public int CursoId
    {
        get { return cursoId; }
        set {
            cursoId = value;
            mostrarCurriculo();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private void mostrarCurriculo()
    {
        loadCursosTable();
    }


    private void loadCursosTable()
    {

        using (MySqlConnection sqlCon = new MySqlConnection())
        {
            connection.Open();
            MySqlDataAdapter sqlDa = new MySqlDataAdapter("SELECT * FROM cursos WHERE idcursos='"+ CursoId + "'", connection);
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            cursoList.DataSource = dtbl;
            cursoList.DataBind();
            connection.Close();
        }

        //tabelaGeral

        MySqlDataReader reader = null;
        connection.Open();
        MySqlCommand command = new MySqlCommand("SELECT * FROM periodos WHERE cursoId='" + cursoId.ToString() + "'", connection);
        reader = command.ExecuteReader();
        
        while (reader.Read())
        {
            TableRow tRow = new TableRow();
            tabelaGeral.Rows.Add(tRow);

            TableCell tCell = new TableCell();
            var temp = reader["numPeriodo"];
            string stringText = "<b>Periodo " + Convert.ToString(temp)+"<b>";
            
            tCell.Text = stringText;
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            temp = reader["at"];
            stringText = Convert.ToString(temp);
            tCell.Text = stringText;
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            temp = reader["ap"];
            stringText = Convert.ToString(temp);
            tCell.Text = stringText;
            tRow.Cells.Add(tCell);


            tCell = new TableCell();
            temp = reader["cred"];
            stringText = Convert.ToString(temp);
            tCell.Text = stringText;
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            temp = reader["hr"];
            stringText = Convert.ToString(temp);
            tCell.Text = stringText;
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            temp = reader["ha"];
            stringText = Convert.ToString(temp);
            tCell.Text = stringText;
            tRow.Cells.Add(tCell);

 
            temp = reader["idPeriodos"];
            var idPeriodo = Convert.ToString(temp);


            MySqlDataReader reader2 = null;
            connection2.Open();
            MySqlCommand command2 = new MySqlCommand("SELECT * FROM disciplinas WHERE periodoId='" + idPeriodo + "'", connection2);
            reader2 = command2.ExecuteReader();

            while (reader2.Read())
            {
                TableRow tRow2 = new TableRow();
                tabelaGeral.Rows.Add(tRow2);

                TableCell tCell2 = new TableCell();
                var temp2 = reader2["nome"];
                string stringText2 = Convert.ToString(temp2);

                tCell2.Text = stringText2;
                tRow2.Cells.Add(tCell2);

                tCell2 = new TableCell();
                temp2 = reader2["at"];
                stringText2 = Convert.ToString(temp2);
                tCell2.Text = stringText2;
                tRow2.Cells.Add(tCell2);

                tCell2 = new TableCell();
                temp2 = reader2["ap"];
                stringText2 = Convert.ToString(temp2);
                tCell2.Text = stringText2;
                tRow2.Cells.Add(tCell2);


                tCell2 = new TableCell();
                temp2 = reader2["cred"];
                stringText2 = Convert.ToString(temp2);
                tCell2.Text = stringText2;
                tRow2.Cells.Add(tCell2);

                tCell2 = new TableCell();
                temp2 = reader2["hr"];
                stringText2 = Convert.ToString(temp2);
                tCell2.Text = stringText2;
                tRow2.Cells.Add(tCell2);

                tCell2 = new TableCell();
                temp2 = reader2["ha"];
                stringText2 = Convert.ToString(temp2);
                tCell2.Text = stringText2;
                tRow2.Cells.Add(tCell2);
            }
            connection2.Close();


        }
        connection.Close();



    }
}