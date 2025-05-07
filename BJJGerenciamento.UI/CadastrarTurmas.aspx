<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastrarTurmas.aspx.cs" Inherits="BJJGerenciamento.UI.CadastrarTurmas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Cadastrar Turma</h2>

    <asp:Label ID="lblPlano" runat="server" Text="Nome do Novo Plano: " />
    <asp:TextBox ID="txtNomeNovoPlano" runat="server" Placeholder="Digite o nome do novo plano" />
    <br />

    <asp:Label ID="lblDias" runat="server" Text="Escolha os Dias: " />
    <asp:CheckBoxList ID="cblDias" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cblDias_SelectedIndexChanged">
    </asp:CheckBoxList>
    <br />

    <asp:Label ID="lblHorarios" runat="server" Text="Escolha os Horários: " />
    <asp:PlaceHolder ID="phHorarios" runat="server">
    </asp:PlaceHolder>
    <br />

    <asp:Label ID="Label1" runat="server" Text="Mensalidade: " />
<asp:TextBox ID="txtMensalidade" runat="server" />
<asp:Button ID="btnCalcularMensalidade" runat="server" Text="Calcular" OnClick="btnCalcularMensalidade_Click" />
<br />

    <br />

    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
</asp:Content>
