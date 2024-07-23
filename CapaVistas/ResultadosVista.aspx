<%@ Page Title="" Language="C#" MasterPageFile="~/CapaVistas/Menu.Master" AutoEventWireup="true" CodeBehind="ResultadosVista.aspx.cs" Inherits="ExamenKendall.CapaVistas.ResultadosVista" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/Paginas.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="form-container">
        <h1>Resultados de Votación</h1>
        
        <div class="form-row">
            <label for="Tganador">Ganador Elecciones:</label>
            <asp:TextBox ID="Tganador" runat="server" ReadOnly="True"></asp:TextBox>
        </div>
        
        <div class="form-row">
            <label for="Tporcentaje">Porcentaje:</label>
            <asp:TextBox ID="Tporcentaje" runat="server" ReadOnly="True"></asp:TextBox>
        </div>
        
        <div class="form-row">
            <asp:Button ID="Bingresar" runat="server" Text="Mostrar Resultados" OnClick="Bingresar_Click" />
        </div>
        
        <div class="form-row">
            <asp:Label ID="lblMensaje" runat="server" Text="" EnableViewState="false"></asp:Label>
        </div>

        <div class="form-row">
            <asp:GridView ID="GridViewResultados" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre del Candidato" />
                    <asp:BoundField DataField="Votos" HeaderText="Votos" />
                    <asp:BoundField DataField="Porcentaje" HeaderText="Porcentaje" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>