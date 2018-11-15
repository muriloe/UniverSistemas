<%@ Page Title="Cursos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  CodeFile="CursoPage.aspx.cs" Inherits="View_Curso" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        asdasd
    </div>
    <div>
        <asp:GridView id="cursoList" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="idCursos" HeaderText="ID" />
                <asp:BoundField DataField="nome" HeaderText="Nome do curso" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>