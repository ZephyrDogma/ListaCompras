<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Import.aspx.cs" Inherits="WebFormCompras.Import" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <br />

    <asp:Label ID="Label1" runat="server" Text="Importação XML"></asp:Label>



    <asp:FileUpload ID="FileUpload1" runat="server" />
    <br />
    <asp:Button ID="btnImport" runat="server" Text="Importar" OnClick="btnImport_Click" />





    <br />
    <br />
    <asp:Label ID="resultado" runat="server" Text="À espera de Importação"></asp:Label>





<br />
<br />
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





</asp:Content>
