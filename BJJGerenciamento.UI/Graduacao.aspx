<%@ Page Title="Graduacao" Language="C#" AutoEventWireup="true" CodeBehind="Graduacao.aspx.cs" Inherits="BJJGerenciamento.UI.Graduacao" MasterPageFile="~/Site.Master"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .btn-custom {
        padding: 0.5rem 1rem;
        font-size: 15px;
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

    /* Estilização dos campos do modal */
    #divAluno .form-control {
        margin-bottom: 0.75rem;
        padding: 0.6rem 1rem;
        font-size: 15px;
        border-radius: 6px;
        border: 1px solid #ccc;
    }

    #divAluno .form-group {
        display: flex;
        flex-direction: column;
    }

    #divAluno label {
        margin-top: 0.5rem;
        font-weight: 500;
        color: #333;
    }

    /* Botão "Salvar" do modal */
    .modal-footer .asp-button {
        padding: 0.5rem 1.2rem;
        font-size: 16px;
        border-radius: 6px;
        background-color: #28a745;
        color: #fff;
        border: none;
        cursor: pointer;
        height: 35px;
    }


    .modal-footer .asp-button:hover {
        background-color: #218838;
    }

    /* Botões da GridView */
    .aspNet-GridView button,
    .aspNet-GridView input[type="submit"] {
        padding: 0.4rem 0.8rem;
        font-size: 14px;
        background-color: #007bff;
        color: #fff;
        border: none;
        border-radius: 4px;
        cursor: pointer;
        height: 36px;
    }

    .aspNet-GridView button:hover,
    .aspNet-GridView input[type="submit"]:hover {
        background-color: #0056b3;
    }

    @keyframes highlightRow {
        0%   { background-color: transparent; }
        25%  { background-color: rgba(0, 123, 255, 0.3); } 
        50%  { background-color: transparent; }
        75%  { background-color: rgba(0, 123, 255, 0.3); }
        100% { background-color: transparent; }
    }

    tr.highlight-row > td {
        animation: highlightRow 1s ease-in-out 3;
    }

</style>

<contentTemplate>
    <main>
        <h1>Graduação</h1>

        <!-- Filtros -->
        <div class="flex-container">
            <asp:ImageButton ID="btnFiltro" runat="server" ImageUrl="~/Images/filtro.png" OnClick="btnFiltro_Click" AlternateText="Filtrar" CssClass="btn btn-light icon-btn" />

            <asp:DropDownList ID="ddPlanos" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddPlanos_SelectedIndexChanged" Visible="false" CssClass="form-select-custom" />
            <asp:DropDownList ID="ddHorarios" runat="server" AutoPostBack="false" CssClass="form-select-custom" Visible="false" />

            <asp:TextBox ID="TxtTermoPesquisa" runat="server" Visible="false" CssClass="form-control" placeholder="Pesquisar..." />

            <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" OnClick="btnPesquisar_Click" Visible="false" CssClass="btn btn-primary btn-custom" style="background-color:blue" />

            <asp:Button ID="btnLimpar" runat="server" Text="Limpar filtros" OnClick="btnLimpar_Click" Visible="false" CssClass="btn btn-danger btn-custom" />
        </div>

        <!-- Tabela -->
        <div class="grid-responsive-container">
            <asp:GridView CssClass="aspNet-GridView table table-striped table-bordered table-hover" ID="GridView1" runat="server" AutoGenerateColumns="False"
                DataKeyNames="IdMatricula" 
                OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                OnRowDataBound="GridView1_RowDataBound">
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
                                OnClick="btnGraduar_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div> 

        <!-- Modal -->
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
                            <asp:Label ID="lblMatricula" runat="server" Text="Matrícula:" />
                            <asp:TextBox ID="modalMatricula" runat="server" CssClass="form-control" ReadOnly></asp:TextBox>

                            <asp:Label ID="lblNome" runat="server" Text="Nome:" />
                            <asp:TextBox ID="modalNome" runat="server" CssClass="form-control" ReadOnly></asp:TextBox>

                            <asp:Label ID="lblSobrenome" runat="server" Text="Sobrenome:" />
                            <asp:TextBox ID="modalSobrenome" runat="server" CssClass="form-control" ReadOnly></asp:TextBox>

                            <asp:Label ID="lblObservacao" runat="server" Text="Observação:" />
                            <asp:TextBox ID="modalObservacaoAluno" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="modal-footer text-center">
                        <asp:Button ID="btnSalvarGraduacao" runat="server" CssClass="asp-button btn btn-primary" Text="Salvar" OnClick="btnSalvarGraduacao_Click" />
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

        
        function highlightAndScrollToRow(idMatricula) {
            const gridView = document.getElementById('<%= GridView1.ClientID %>');
            if (!gridView) {
                console.warn("GridView1 não encontrado no DOM.");
                return;
            }

            const dataRows = gridView.querySelectorAll('tbody > tr');

            for (let i = 0; i < dataRows.length; i++) {
                const row = dataRows[i];
                const rowMatriculaId = row.getAttribute('data-matricula-id');

                if (rowMatriculaId === idMatricula.toString()) {
                    row.classList.add('highlight-row');

                    row.scrollIntoView({ behavior: 'smooth', block: 'center' });

                    setTimeout(() => {
                        row.classList.remove('highlight-row');
                    }, 3500);

                    return; 
                }
            }
            console.log(`Aluno com matrícula ${idMatricula} não encontrado no GridView.`);
        }
    </script>
</contentTemplate>
</asp:Content>
