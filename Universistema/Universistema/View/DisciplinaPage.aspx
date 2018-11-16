<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeFile="DisciplinaPage.aspx.cs" Inherits="View_Disciplina" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1><asp:Label Text="Administração de Disciplinas" ID="statusLabel" runat="server"/></h1>
        <p>
            Selecione Um Curso:  
            <asp:DropDownList 
                id="cursoList" 
                DataTextField="nome" 
                DataValueField="idcursos"  
                runat="server"  
                onselectedindexchanged="cursoList_SelectedIndexChanged"
                AutoPostBack="true">
            </asp:DropDownList>

            <asp:DropDownList 
                id="periodosList" 
                DataTextField="numPeriodo" 
                DataValueField="idperiodos"  
                runat="server"  
                onselectedindexchanged="periodosList_SelectedIndexChanged"
                AutoPostBack="true">
            </asp:DropDownList>
        </p>
        <p>
            <asp:Panel id="PainelCadastro" runat="server">
                    Nome Disciplina:<br />
                    <asp:TextBox runat="server" ID="nomeDisciplina" CssClass="form-group"/><br />
                    AT:<br />
                    <asp:TextBox runat="server" ID="at" CssClass="form-group"/><br />
                    AP:<br />
                    <asp:TextBox runat="server" ID="ap" CssClass="form-group"/><br />
                    Creditos:<br />
                    <asp:TextBox runat="server" ID="cred" CssClass="form-group"/><br />
                    Horas Relógio:<br />
                    <asp:TextBox runat="server" ID="hr" CssClass="form-group"/><br />
                    Horas Aula:<br />
                    <asp:TextBox runat="server" ID="ha" CssClass="form-group"/><br /> 
                    
                    <asp:Button runat="server" ID="btnCadastrar" Text="Adicionar" OnClick="btnCadastrar_Click" />
                    <asp:Button runat="server" ID="btnEditar" Text="Editar"  OnClick="btnEditar_Click" />
            </asp:Panel>
        </p>
    </div>

     <div>
        <h1>Lista de Disciplinas</h1>
        <asp:GridView id="disciplinasList" runat="server" AutoGenerateColumns="false" CssClass="table table-striped">
            <Columns>
                <asp:BoundField DataField="nome" HeaderText="Nome" />
                <asp:BoundField DataField="at" HeaderText="AT" />
                <asp:BoundField DataField="ap" HeaderText="AP" />
                <asp:BoundField DataField="cred" HeaderText="CRED" />
                <asp:BoundField DataField="hr" HeaderText="HR" />
                <asp:BoundField DataField="ha" HeaderText="HA" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton  ID="editSelect" text="Editar" runat="server"  CommandArgument='<%# Eval("idDisciplinas") %>' OnClick="editSelect_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton  ID="apagarSelect" text="Apagar" runat="server"  CommandArgument='<%# Eval("idDisciplinas") %>' OnClick="apagarSelect_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>