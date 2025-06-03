<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaHorario.aspx.cs" Inherits="BJJGerenciamento.UI.ListaHorario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.16/jquery.mask.min.js"></script>

    <h2 class="mb-4">Gerenciar Horários</h2>

    <asp:Button ID="btnNovo" runat="server" Text="Novo Horário" OnClick="btnNovo_Click" CssClass="btn btn-primary mb-3" />

    <asp:Label ID="lblMensagem" runat="server" ForeColor="Red" CssClass="form-text mb-3"></asp:Label>

    <asp:GridView ID="gvHorario" runat="server" AutoGenerateColumns="false" DataKeyNames="IdHora" OnRowCommand="gvHorario_RowCommand" CssClass="table table-striped table-bordered">
        <Columns>
            <asp:BoundField DataField="HorarioInicioStr" HeaderText="Início" />
            <asp:BoundField DataField="HorarioFimStr" HeaderText="Fim" />
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Button ID="btnAtivarDesativar" runat="server" 
                        Text='<%# (bool)Eval("Ativa") ? "Desativar" : "Ativar" %>' 
                        CommandName="ToggleStatus" 
                        CommandArgument='<%# Eval("IdHora") %>' CssClass="btn btn-sm btn-outline-primary" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Editar">
                <ItemTemplate>
                    <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandName="Editar" CommandArgument='<%# Eval("IdHora") %>' CssClass="btn btn-sm btn-outline-secondary" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Panel ID="pnlCadastro" runat="server" Visible="false" CssClass="border rounded p-3 mt-4" style="max-width:320px;">
        <asp:Label ID="lblId" runat="server" Visible="false" Text="0" />
        <div class="form-group mb-3">
            <asp:Label runat="server" AssociatedControlID="txtHorarioInicio" Text="Horário Início:" CssClass="form-label" />
            <asp:TextBox ID="txtHorarioInicio" runat="server" CssClass="form-control" placeholder="HH:mm"
                MaxLength="5"
                pattern="^\d{2}:\d{2}$"
                title="Informe o horário no formato HH:mm"></asp:TextBox>
        </div>
        <div class="form-group mb-3">
            <asp:Label runat="server" AssociatedControlID="txtHorarioFim" Text="Horário Fim:" CssClass="form-label" />
            <asp:TextBox ID="txtHorarioFim" runat="server" CssClass="form-control" placeholder="HH:mm"
                MaxLength="5"
                pattern="^\d{2}:\d{2}$"
                title="Informe o horário no formato HH:mm"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar"
                OnClick="btnSalvar_Click"
                CssClass="btn btn-success" />

            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" CssClass="btn btn-danger ms-2" />
        </div>
        <asp:Label ID="lblMensagemCadastro" runat="server" ForeColor="Red" CssClass="form-text mt-2"></asp:Label>
    </asp:Panel>

    <script type="text/javascript">
        function confirmarSalvar(isNovo) {
            var inicio = document.getElementById('<%= txtHorarioInicio.ClientID %>').value.trim();
            var fim = document.getElementById('<%= txtHorarioFim.ClientID %>').value.trim();

            // Validação simples no client
            if (inicio === "" || fim === "") {
                alert("Preencha os campos de horário antes de salvar.");
                return false;
            }

            const regex = /^\d{2}:\d{2}$/;
            if (!regex.test(inicio) || !regex.test(fim)) {
                alert("Informe os horários no formato HH:mm.");
                return false;
            }

            var mensagem = "";
            if (isNovo === "true") {
                mensagem = "Tem certeza que deseja adicionar um novo horário das " + inicio + " até " + fim + "?";
            }
            else
            {
                mensagem = "Tem certeza que deseja salvar as alterações para o horário das " + inicio + " até " + fim + "?";
            }

            return confirm(mensagem);
        }

        $(document).ready(function () {
            $('#<%= txtHorarioInicio.ClientID %>').mask('00:00');
            $('#<%= txtHorarioFim.ClientID %>').mask('00:00');
        });
    </script>

</asp:Content>
