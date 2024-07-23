<%@ Page Title="" Language="C#" MasterPageFile="~/CapaVistas/Menu.Master" AutoEventWireup="true" CodeBehind="VotacionesVista.aspx.cs" Inherits="ExamenKendall.CapaVistas.VotacionesVista" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/Paginas.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="form-container">
        <h1>Votos</h1>
        
        <div class="form-row">
            <label for="Tid">Seleccionar Identificación:</label>
            <asp:TextBox ID="Tid" runat="server"></asp:TextBox>
        </div>
        
        <div class="form-row">
            <label for="Tcandidato">Seleccionar Candidato:</label>
            <asp:TextBox ID="Tcandidato" runat="server"></asp:TextBox>
        </div>
        
        <div class="form-row">
            <asp:Button ID="Bingresar" runat="server" Text="Ingresar Datos" OnClick="Bingresar_Click" />
        </div>
        
        <div class="form-row">
            <asp:Label ID="lblMensaje" runat="server" Text="" EnableViewState="false"></asp:Label>
        </div>
    </div>
</asp:Content>
