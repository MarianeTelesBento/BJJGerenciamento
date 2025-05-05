<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastrarTurmas.aspx.cs" Inherits="BJJGerenciamento.UI.CadastrarTurmas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Cadastrar Turma</h2>

    <asp:Label ID="lblPlano" runat="server" Text="Escolha o Plano: " />
    <asp:DropDownList ID="ddlPlano" runat="server">
    </asp:DropDownList>
    <br />

    <asp:Label ID="lblDias" runat="server" Text="Escolha os Dias: " />
    <asp:CheckBoxList ID="cblDias" runat="server">
    </asp:CheckBoxList>
    <br />

    <asp:Label ID="lblHorarios" runat="server" Text="Escolha os Horários: " />
    <asp:PlaceHolder ID="phHorarios" runat="server">

    </asp:PlaceHolder>
    <br />

    <asp:Label ID="lblMensalidade" runat="server" Text="Mensalidade: " />
    <asp:TextBox ID="txtMensalidade" runat="server" ReadOnly="true" />
    <br />

    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
</asp:Content>
