<%@ Page Title="Lista de Turmas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaTurmas.aspx.cs" Inherits="BJJGerenciamento.UI.ListaTurmas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<main>

    <h1>Lista de Turmas</h1>

    <asp:GridView CssClass="table table-striped table-bordered table-hover" ID="GridViewTurmas" runat="server" AutoGenerateColumns="False"
        DataKeyNames="IdPlano,Ativo,Nome"
        OnSelectedIndexChanged="GridViewTurmas_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="IdPlano" HeaderText="ID" />
            <asp:BoundField DataField="Nome" HeaderText="Nome da Turma" />
            <asp:TemplateField HeaderText="Ativo">
                <ItemTemplate>
                    <asp:CheckBox ID="chkAtivo" runat="server" Enabled="false"
                        Checked='<%# Convert.ToBoolean(Eval("Ativo")) %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ação">
                <ItemTemplate>
                    <asp:Button ID="btnDetalhes" runat="server" Text="Mais"
                        CommandName="Detalhes"
                        OnClick="btnDetalhes_Click" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <!-- Modal Detalhes -->
    <div id="modalDetalhes" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Detalhes da Turma</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="fecharModal()">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">

                    <div class="form-group">
                        <asp:Label for="modalIdPlano">ID:</asp:Label>
                        <asp:TextBox ID="modalIdPlano" runat="server" ReadOnly CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group d-flex align-items-center gap-2">
                        <asp:Label ID="lblmodalAtivo" runat="server" CssClass="mb-0">Turma ativa:</asp:Label>
                        <asp:CheckBox ID="modalAtivo" runat="server" />
                    </div>
                    <div class="form-group">
                        <asp:Label for="modalNome">Nome:</asp:Label>
                        <asp:TextBox ID="modalNome" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lblDiasEdit" runat="server" Text="Dias da Semana:" AssociatedControlID="cblDiasEdit" />
                        <asp:UpdatePanel ID="updDiasHorarios" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:CheckBoxList ID="cblDiasEdit" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cblDiasEdit_SelectedIndexChanged" RepeatDirection="Horizontal" CssClass="d-flex gap-3 flex-wrap">
                                </asp:CheckBoxList>
                                <br />
                                <asp:PlaceHolder ID="phHorariosEdit" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cblDiasEdit" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                    <div class="modal-footer text-center">
                        <asp:Button ID="SalvarTurma" OnClick="SalvarTurma_Click" runat="server" CssClass="asp-button btn btn-primary" Text="Salvar" />
                    </div>

                </div>
            </div>
        </div>
    </div>

    <script>
        function abrirModal() {
            document.getElementById("modalDetalhes").style.display = "block";
        }

        function fecharModal() {
            document.getElementById("modalDetalhes").style.display = "none";
        }
    </script>

    <style>
        #GridViewTurmas {
            text-align: center;
        }

        .asp-button {
            display: inline-block;
            width: auto;
            padding: 8px 16px;
            font-size: 16px;
            font-weight: 400;
            line-height: 1.5;
            color: #fff;
            background-color: #0d6efd;
            border: 1px solid #0d6efd;
            border-radius: 5px;
            cursor: pointer;
            text-align: center;
            text-decoration: none;
            transition: background-color 0.2s ease-in-out;
            height: 38px;
        }

        .asp-button:hover {
            background-color: #0b5ed7;
            border-color: #0a58ca;
        }

        .modal {
            display: none;
            position: fixed;
            z-index: 1050;
            padding-top: 100px;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            overflow: auto;
            background-color: rgba(0,0,0,0.5);
        }

        .modal-content {
            background-color: #fff;
            margin: auto;
            border: 1px solid #888;
            width: 80%;
            max-width: 500px;
            padding: 20px;
            border-radius: 10px;
        }

        .modal-header, .modal-footer {
            border-bottom: none;
            border-top: none;
        }
    </style>

</main>
</asp:Content>
