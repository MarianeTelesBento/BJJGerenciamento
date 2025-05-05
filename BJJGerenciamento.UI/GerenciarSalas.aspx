<%@ Page Title="Gerenciar Salas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GerenciarSalas.aspx.cs" Inherits="BJJGerenciamento.UI.GerenciarSalas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin: 20px;">
        <asp:Label ID="lblMensagem" runat="server" ForeColor="Green" />
        
        <br /><br />
        
        <asp:Button ID="btnAdicionarSala" runat="server" Text="Gerar Número da Sala"
            OnClick="btnGerarNumeroSala_Click" />
        
        <br /><br />
        
        <asp:Label ID="lblNumeroSala" runat="server" Text="" ForeColor="Black" />
        
        <br /><br />
        
        <asp:Button ID="btnConfirmarAdicao" runat="server" Text="Confirmar Adição da Sala"
            OnClick="btnConfirmarAdicao_Click" Visible="false" />
        
        <br /><br />
        
        <asp:GridView ID="gvSalas" runat="server" AutoGenerateColumns="False" DataKeyNames="IdSala" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
            <AlternatingRowStyle BackColor="White" />
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
                            OnClientClick="return confirm('Tem certeza que deseja alterar o status desta sala?');" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Excluir">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnExcluir" runat="server" Text="Excluir"
                            CommandArgument='<%# Eval("IdSala") %>' OnClick="btnExcluir_Click"
                            OnClientClick="return confirm('Tem certeza que deseja excluir esta sala? Esta ação não poderá ser desfeita.');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD3" />
            <SortedDescendingHeaderStyle BackColor="#575357" />
        </asp:GridView>
    </div>
</asp:Content>
