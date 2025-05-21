<%@ Page Title="Gerenciar Salas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GerenciarSalas.aspx.cs" Inherits="BJJGerenciamento.UI.GerenciarSalas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    

    <div class="container mt-5">
        <asp:Label ID="lblMensagem" runat="server" CssClass="form-text text-success" />

        <h2 class="mb-4">Gerenciar Salas</h2>

      <asp:Button ID="btnAdicionarSala" runat="server" Text="Gerar Número da Sala"
    CssClass="btn btn-primary"
    Style="width: auto; padding-left: 12px; padding-right: 12px; white-space: nowrap; height: 40px;" 
    OnClick="btnGerarNumeroSala_Click" />


        <div class="mb-3">
            <asp:Label ID="lblNumeroSala" runat="server" CssClass="form-text fw-bold text-dark" />
        </div>

        <div class="mb-4">
            <asp:Button ID="btnConfirmarAdicao" runat="server" Text="Confirmar Adição da Sala"
                CssClass="btn btn-success" Style="height: 40px" OnClick="btnConfirmarAdicao_Click" Visible="false" />
        </div>

        <asp:GridView ID="gvSalas" runat="server" AutoGenerateColumns="False" DataKeyNames="IdSala"
            CssClass="table table-striped table-bordered" GridLines="None">
            <Columns>
                <asp:BoundField DataField="NumeroSala" HeaderText="Número da Sala" SortExpression="NumeroSala" />

                <asp:TemplateField HeaderText="Ativa">
                    <ItemTemplate>
                        <%# (Convert.ToBoolean(Eval("Ativa")) ? "Sim" : "Não") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Ação">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnToggleAtiva" runat="server"
                            Text='<%# Convert.ToBoolean(Eval("Ativa")) ? "Desativar" : "Ativar" %>'
                            CommandArgument='<%# Eval("IdSala") + "|" + Eval("Ativa") %>'
                            OnClick="btnToggleAtiva_Click"
                            CssClass="btn btn-warning btn-sm me-2"
                            OnClientClick="return confirm('Tem certeza que deseja alterar o status desta sala?');" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Excluir">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnExcluir" runat="server" Text="Excluir"
                            CommandArgument='<%# Eval("IdSala") %>'
                            OnClick="btnExcluir_Click"
                            CssClass="btn btn-danger btn-sm"
                            OnClientClick="return confirm('Tem certeza que deseja excluir esta sala? Esta ação não poderá ser desfeita.');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
