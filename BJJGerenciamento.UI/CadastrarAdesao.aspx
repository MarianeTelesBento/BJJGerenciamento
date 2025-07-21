<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastrarAdesao.aspx.cs" Inherits="BJJGerenciamento.UI.CadastrarAdesao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

   
    <h2>Cadastro de Adesão</h2>

    <asp:Label ID="lblMensagem" runat="server" ForeColor="Green" />

    <div>
        <asp:Label runat="server" Text="Nome da Adesão:" />
        <asp:TextBox ID="txtNomeAdesao" runat="server" CssClass="form-control" />
    </div>

    <div>
        <asp:Label runat="server" Text="Qtd. Dias Permitidos:" />
        <asp:TextBox ID="txtQtdDias" runat="server" CssClass="form-control" TextMode="Number" />
    </div>

    <div>
        <asp:Label runat="server" Text="Mensalidade (R$):" />
        <asp:TextBox ID="txtMensalidade" runat="server" CssClass="form-control" />
    </div>
     <div>
    <asp:Label runat="server" Text="Turmas vinculadas:" />
    <asp:CheckBoxList ID="chkTurmas" runat="server" RepeatColumns="2" CssClass="form-check" />
</div>

    <div style="margin-top: 10px;">
        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn btn-primary" OnClick="btnSalvar_Click" />
    </div>

    <br />
    <asp:GridView ID="gridAdesoes" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
        DataKeyNames="IdAdesao" OnRowCommand="gridAdesoes_RowCommand">
        <Columns>
            <asp:BoundField DataField="NomeAdesao" HeaderText="Nome" />
            <asp:BoundField DataField="QtdDiasPermitidos" HeaderText="Dias Permitidos" />
            <asp:BoundField DataField="Mensalidade" HeaderText="Mensalidade (R$)" />
            <asp:ButtonField CommandName="Excluir" Text="Excluir" ButtonType="Button" />
        </Columns>
    </asp:GridView>
</asp:Content>

