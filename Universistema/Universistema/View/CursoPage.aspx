<%@ Page Title="Cursos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  CodeFile="CursoPage.aspx.cs" Inherits="View_Curso" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1><asp:Label Text="Novo Curso" ID="statusLabel" runat="server"/></h1>
        <p>
            Nome Curso:
            <asp:TextBox runat="server" ID="nomeCurso" />
            <asp:Button runat="server" ID="btnCadastrar" Text="Adicionar" OnClick="btnCadastrar_Click" />
            <asp:Button runat="server" ID="btnEditar" Text="Editar"  OnClick="btnEditar_Click" />
        </p>
        
    </div>
    <div>
        <h1>Lista de Cursos</h1>
        <asp:GridView id="cursoList" runat="server" AutoGenerateColumns="false" CssClass="table table-striped">
            <Columns>
                <asp:BoundField DataField="idCursos" HeaderText="ID" />
                <asp:BoundField DataField="nome" HeaderText="Nome do curso" />
                <asp:BoundField DataField="at" HeaderText="AT" />
                <asp:BoundField DataField="ap" HeaderText="AP" />
                <asp:BoundField DataField="cred" HeaderText="CRED" />
                <asp:BoundField DataField="hr" HeaderText="HR" />
                <asp:BoundField DataField="ha" HeaderText="HA" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton  ID="editSelect" text="Editar" runat="server"  CommandArgument='<%# Eval("idCursos") %>' OnClick="editSelect_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton  ID="apagarSelect" text="Apagar" runat="server"  CommandArgument='<%# Eval("idCursos") %>' OnClick="apagarSelect_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>