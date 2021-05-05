<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ImportCSV.aspx.cs" Inherits="WebFormCompras.ImportCSV" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Importação CSV"></asp:Label>


    <asp:FileUpload ID="FileUpload1" runat="server" />
    <br />
    <asp:Button ID="btnImport" runat="server" Text="Importar" OnClick="btnImport_Click" />


    <br />
    <br />


    <asp:Label ID="resultado" runat="server" Text="À espera de Importação"></asp:Label>
</asp:Content>
