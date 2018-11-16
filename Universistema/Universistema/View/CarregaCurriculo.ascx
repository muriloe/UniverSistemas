<%@ Control Language="C#"  AutoEventWireup="true" CodeFile="CarregaCurriculo.ascx.cs" Inherits="View_CarregaCurriculo" %>

<asp:GridView id="cursoList" runat="server" AutoGenerateColumns="false" CssClass="table table-striped">
    <Columns>
        <asp:BoundField DataField="nome" HeaderText="CURSO" />
        <asp:BoundField DataField="at" HeaderText="AT" />
        <asp:BoundField DataField="ap" HeaderText="AP" />
        <asp:BoundField DataField="cred" HeaderText="CRED" />
        <asp:BoundField DataField="hr" HeaderText="HR" />
        <asp:BoundField DataField="ha" HeaderText="HA" />
    </Columns>
</asp:GridView>

<asp:Table ID="tabelaGeral" runat="server" CssClass="table table-striped">
    <asp:TableHeaderRow>
        <asp:TableHeaderCell Text="Nome"></asp:TableHeaderCell>
        <asp:TableHeaderCell Text="AT"></asp:TableHeaderCell>
        <asp:TableHeaderCell Text="AP"></asp:TableHeaderCell>
        <asp:TableHeaderCell Text="CRED"></asp:TableHeaderCell>
        <asp:TableHeaderCell Text="HR"></asp:TableHeaderCell>
        <asp:TableHeaderCell Text="HA"></asp:TableHeaderCell>
    </asp:TableHeaderRow>
</asp:Table>



