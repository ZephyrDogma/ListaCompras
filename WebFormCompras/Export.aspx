<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Export.aspx.cs" Inherits="WebFormCompras.Ws" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br/>
   <br/>
   <br/>
   Exportação da Base de Dados<br />
&nbsp;<br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource2">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
            <asp:BoundField DataField="Marca" HeaderText="Marca" SortExpression="Marca" />
            <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" />
            <asp:BoundField DataField="Tamanho" HeaderText="Tamanho" SortExpression="Tamanho" />
            <asp:BoundField DataField="Preco" HeaderText="Preco" SortExpression="Preco" />
        </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Roupa]"></asp:SqlDataSource>
    <br />
    <br />
&nbsp;
    <asp:Button ID="Export" runat="server" OnClick="BtnExportXML" Text="Export para XML" />
    <br />
    <br />


    &nbsp;


    <asp:Button ID="ExportCSV" runat="server" OnClick="BtnExportCSV" Text="Export to CSV" />


    <br />
    <asp:Label ID="resultado" runat="server" Text=""></asp:Label>


</asp:Content>
