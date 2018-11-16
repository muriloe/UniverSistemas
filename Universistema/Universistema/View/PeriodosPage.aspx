<%@ Page Title="Periodos" Language="C#" MasterPageFile="~/Site.Master"  CodeFile="PeriodosPage.aspx.cs" Inherits="View_PeriodosPage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1><asp:Label Text="Administração de Periodos" ID="statusLabel" runat="server"/></h1>
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
        </p>
        <p>
            Número Periodo:
            <asp:TextBox runat="server" ID="numeroPeriodo" TextMode="Number"/>
            <asp:Button runat="server" ID="btnCadastrar" Text="Adicionar" OnClick="btnCadastrar_Click" />
            <asp:Button runat="server" ID="btnEditar" Text="Editar"  OnClick="btnEditar_Click" />
        </p>  
    </div>

    <div>
        <h1>Lista de Periodos</h1>
        <asp:GridView id="periodoList" runat="server" AutoGenerateColumns="false" CssClass="table table-striped">
            <Columns>
                <asp:BoundField DataField="numPeriodo" HeaderText="Numero do periodo" />
                <asp:BoundField DataField="at" HeaderText="AT" />
                <asp:BoundField DataField="ap" HeaderText="AP" />
                <asp:BoundField DataField="cred" HeaderText="CRED" />
                <asp:BoundField DataField="hr" HeaderText="HR" />
                <asp:BoundField DataField="ha" HeaderText="HA" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton  ID="editSelect" text="Editar" runat="server"  CommandArgument='<%# Eval("idPeriodos") %>' OnClick="editSelect_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton  ID="apagarSelect" text="Apagar" runat="server"  CommandArgument='<%# Eval("idPeriodos") %>' OnClick="apagarSelect_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>