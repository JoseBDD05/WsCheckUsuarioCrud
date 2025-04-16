<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/mpPrincipal.Master" AutoEventWireup="true" CodeBehind="Formulario web1.aspx.cs" Inherits="wsCheckUsuario.Formulario_web1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="App_themes/principal/principal.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <asp:Label ID="Label1" runat="server" Text="Reporte de Usuarios Registrados" cssClass="tituloContenido"></asp:Label>
    <br />
    <br />
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/imagenes/icon_logalum.GIF" />
    <br />
    <br />
    <asp:GridView ID="GridView2" runat="server" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" AllowPaging="True" PageSize="5">
        <AlternatingRowStyle BackColor="Aqua" ForeColor="Black" />
        <HeaderStyle BackColor="#0000CC" BorderColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="Medium" ForeColor="White" />
        <PagerStyle BackColor="Blue" Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="White" />
        <RowStyle BackColor="#3399FF" BorderColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="Black" />
    </asp:GridView>
</asp:Content>