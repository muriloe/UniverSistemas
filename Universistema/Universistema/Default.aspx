<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register Src="~/View/CarregaCurriculo.ascx" TagPrefix="uc" TagName="CarregaCurriculo" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <p>
        Selecione um curso para visualizar o curriculo completo:  
        <asp:DropDownList 
            id="cursoList" 
            DataTextField="nome" 
            DataValueField="idcursos"  
            runat="server"  
            onselectedindexchanged="cursoList_SelectedIndexChanged"
            AutoPostBack="true">
        </asp:DropDownList>
    </p>

    <div class="jumbotron">
        <uc:CarregaCurriculo runat="server" id="CarregaCurriculo1" />
    </div>


</asp:Content>
