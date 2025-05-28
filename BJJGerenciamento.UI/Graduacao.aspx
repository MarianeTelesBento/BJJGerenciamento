<%@ Page Title="Graduacao" Language="C#" AutoEventWireup="true" CodeBehind="Graduacao.aspx.cs" Inherits="BJJGerenciamento.UI.Graduacao" MasterPageFile="~/Site.Master"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .btn-custom {
        padding: 0.5rem 1rem;
        font-size: 15px;
       /* background-color: #3366ff;*/
        color: #fff;
        border: none;
        border-radius: 4px;
        height: 38px;
        line-height: 1.5;
        display: flex;
        align-items: center;
        justify-content: center;
        gap: 0.5rem;
        cursor: pointer;
        min-width: 120px;
    }

    .btn-danger-custom {
        background-color: #ff0000;
        color: #fff;
        padding: 0.5rem 1rem;
        font-size: 15px;
        border: none;
        border-radius: 4px;
        height: 38px;
        line-height: 1.5;
        display: flex;
        align-items: center;
        justify-content: center;
        gap: 0.5rem;
        cursor: pointer;
        min-width: 120px;
    }

    .form-select-custom,
    .form-control {
        height: 38px;
        font-size: 15px;
        padding: 0.45rem 1rem;
        color: #000 !important;
        background-color: #fff !important;
        border: 1px solid #ccc;
        border-radius: 4px;
        min-width: 180px;
    }

    .flex-container {
        display: flex;
        flex-wrap: wrap;
        align-items: center;
        gap: 0.5rem;
        margin-bottom: 1rem;
    }

    #aspnetTitle {
        font-size: 2rem;
        font-weight: bold;
        color: #3366ff;
        margin-bottom: 1rem;
    }

    main {
        padding: 20px;
    }

    .icon-btn {
        width: 38px;
        height: 38px;
        padding: 0;
        display: flex;
        align-items: center;
        justify-content: center;
        background-color: transparent;
        border: none;
    }

    .icon-btn img {
        max-width: 70%;
        max-height: 70%;
    }
</style>

<contentTemplate>
    <main>
        <h1>Graduacao</h1>

        <!-- Filtros -->
        <div class="flex-container">
            <asp:ImageButton ID="btnFiltro" runat="server" ImageUrl="~/Images/filtro.png" OnClick="btnFiltro_Click" AlternateText="Filtrar" CssClass="btn btn-light icon-btn" />

            <asp:DropDownList ID="ddPlanos" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddPlanos_SelectedIndexChanged" Visible="false" CssClass="form-select-custom" />

<%--            <asp:DropDownList ID="ddHorarios" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddHorarios_SelectedIndexChanged" Visible="false" CssClass="form-select-custom" />--%>

            <asp:TextBox ID="TxtTermoPesquisa" runat="server" Visible="false" CssClass="form-control" placeholder="Pesquisar..." />

            <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" OnClick="btnPesquisar_Click" Visible="false" CssClass="btn btn-primary btn-custom" style="background-color:blue" />

            <asp:Button ID="btnLimpar" runat="server" Text="Limpar filtros" OnClick="btnLimpar_Click" Visible="false" CssClass="btn btn-danger btn-custom" />
        </div>

        <!-- Tabela -->
        <asp:GridView CssClass="table table-striped table-bordered table-hover" ID="GridView1" runat="server" AutoGenerateColumns="False"
            OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="IdMatricula" HeaderText="Matrícula" />
                <asp:BoundField DataField="Nome" HeaderText="Nome" HtmlEncode="false" />
                <asp:BoundField DataField="Sobrenome" HeaderText="Sobrenome" HtmlEncode="false" />
                <asp:BoundField DataField="Cpf" HeaderText="CPF" />
                <asp:BoundField DataField="Telefone" HeaderText="Telefone" />

                <asp:TemplateField HeaderText="Status da Matrícula">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkStatusMatricula" runat="server" Enabled="false"
                            Checked='<%# Convert.ToBoolean(Eval("StatusMatricula")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:BoundField DataField="TotalPresencas" HeaderText="Total de Presenças" />

                <asp:TemplateField HeaderText="Ação">
                    <ItemTemplate>
                        <asp:Button ID="btnGraduar" runat="server" Text="Graduar" 
                            CommandName="Graduar" 
                             OnClick="btnGraduar_Click"/>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>

       <div id="modalDetalhes" class="modal" tabindex="-1" role="dialog">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="fecharModal()">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div id="divAluno" class="modal-body">
                        <div class="form-group">
                            <asp:Label>Observação:</asp:Label>
                            <asp:TextBox ID="modalObservacaoAluno" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="modal-footer text-center">
                        <asp:Button ID="btnSalvarGraduacao" runat="server" CssClass="asp-button btn btn-primary" Text="Salvar" OnClick="btnSalvarGraduacao_Click"/>
                    </div>
                </div>
            </div>
        </div>

    </main>

    <script>
        function abrirModal() {
            document.getElementById("modalDetalhes").style.display = "block";
        }

        function fecharModal() {
            document.getElementById("modalDetalhes").style.display = "none";
        }
    </script>
</contentTemplate>
</asp:Content>
