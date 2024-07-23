<%@ Page Title="" Language="C#" MasterPageFile="~/CapaVistas/Menu.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="ExamenKendall.CapaVistas.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/Paginas.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="form-container">
        <h1>Inicio</h1>
        
        <div class="form-row">
            <label for="Tid">Identificación:</label>
            <asp:TextBox ID="Tid" runat="server"></asp:TextBox>
        </div>
        
        <div class="form-row">
            <label for="Tnombre">Nombre:</label>
            <asp:TextBox ID="Tnombre" runat="server"></asp:TextBox>
        </div>
        
        <div class="form-row">
            <label for="Tapellido1">Apellido:</label>
            <asp:TextBox ID="Tapellido1" runat="server"></asp:TextBox>
        </div>
        
        <div class="form-row">
            <label for="Tapellido2">Segundo Apellido:</label>
            <asp:TextBox ID="Tapellido2" runat="server"></asp:TextBox>
        </div>
        
        <div class="form-row">
            <label for="Tcorreo">Correo:</label>
            <asp:TextBox ID="Tcorreo" runat="server"></asp:TextBox>
        </div>

        <div class="form-row">
            <asp:Button ID="Bingresar" runat="server" Text="Ingresar Datos" OnClick="Bingresar_Click" />
        </div>
        
        <div class="form-row">
            <asp:Label ID="lblMensaje" runat="server" Text="" EnableViewState="false"></asp:Label>
        </div>
    </div>
</asp:Content>
