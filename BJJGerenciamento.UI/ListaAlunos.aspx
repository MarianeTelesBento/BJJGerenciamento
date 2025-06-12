<%@ Page Title="Lista de Alunos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaAlunos.aspx.cs" Inherits="BJJGerenciamento.UI._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <h1 id="aspnetTitle">Lista de Alunos</h1>    
        
        <div class="flex-container">
            <asp:ImageButton ID="btnFiltro" runat="server" ImageUrl="~/Images/filtro.png" OnClick="btnFiltro_Click" AlternateText="Filtrar" CssClass="btn btn-light icon-btn" />

            <asp:DropDownList ID="ddPlanos" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddPlanos_SelectedIndexChanged" Visible="false" CssClass="form-select-custom input-grande" />

            <asp:TextBox ID="TxtTermoPesquisa" runat="server" Visible="false" CssClass="form-control input-grande" placeholder="Pesquisar..." />

          <asp:CheckBox ID="chkApenasAtivos" runat="server" Text="Apenas Ativos" AutoPostBack="true" OnCheckedChanged="chkApenasAtivos_CheckedChanged" Visible="false"/>

          <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" OnClick="btnPesquisar_Click" Visible="false" CssClass="btn btn-primary btn-custom" Style="background-color: blue" />
          <asp:Button ID="btnLimpar" runat="server" Text="Limpar filtros" OnClick="btnLimpar_Click" Visible="false" CssClass="btn btn-danger btn-custom" />

        </div>

        <div class="grid-responsive-container ">
            <asp:GridView CssClass="table table-striped table-bordered table-hover" ID="GridView1" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="Email,Rg,DataNascimento,Cep,Rua,Bairro,Cidade,Estado,NumeroCasa,Complemento,CarteiraFPJJ,DataMatricula,IdAlunos"
                OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                <Columns>

                    <asp:BoundField DataField="IdMatricula" HeaderText="Matrícula" />
                    <asp:BoundField DataField="Nome" HeaderText="Nome" HtmlEncode="false"/>
                    <asp:BoundField DataField="Sobrenome" HeaderText="Sobrenome" HtmlEncode="false"/>
                    <asp:BoundField DataField="Cpf" HeaderText="CPF" />
                    <asp:BoundField DataField="Telefone" HeaderText="Telefone"/>
                            
                    <asp:TemplateField HeaderText="Status da Matrícula">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkStatusMatricula" runat="server" Enabled="false"
                                Checked='<%# Convert.ToBoolean(Eval("StatusMatricula")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Ação">
                        <ItemTemplate>
                            <asp:HiddenField ID="hfIdAluno" runat="server" Value='<%# Eval("IdAlunos") %>' />
                            <asp:Button ID="btnDetalhes" runat="server" Text="Mais" 
                                CommandName="Detalhes" 
                                OnClick="btnDetalhes_Click" CssClass="" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        

    <!-- Modal (Mini Tela) -->

        <div id="modalDetalhes" class="modal" tabindex="-1" role="dialog">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">

                        <asp:HiddenField ID="hfIdAlunoModal" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hfMatriculaAlunoModal" runat="server" ClientIDMode="Static" />

                        <asp:Button ID="btnDetalhesAluno" OnClick="btnDetalhesAluno_Click" runat="server" Text="Aluno"/>
                        <asp:Button ID="btnDetalhesResponsavel" OnClick="btnDetalhesResponsavel_Click" runat="server" Text="Responsavel"/>
                        <asp:Button ID="btnDetalhesPlano" OnClick="btnDetalhesPlano_Click" runat="server" Text="Plano"/>
                        <asp:Button ID="btnDetalhesGraduacao" OnClick="btnDetalhesGraduacao_Click" runat="server" Text="Graduacao"/>

                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="fecharModal()">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div id="divAluno" class="modal-body">
                            <div class="form-group">
                                <asp:Label>Número da Matricula:</asp:Label>
                                <asp:TextBox ID="modalIdMatriculaAluno" runat="server" Text="modalId" ReadOnly CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label>Data da Matricula:</asp:Label>
                                <asp:TextBox ID="modalDataMatriculaAluno" runat="server" Text="modalId" ReadOnly CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group d-flex align-items-center gap-2">
                                <asp:Label AssociatedControlID="modalStatusMatricula" runat="server" CssClass="mb-0">Status da matrícula:</asp:Label>
                                <asp:CheckBox ID="modalStatusMatricula" runat="server" />
                            </div>
                            <div class="form-group">
                                <label>Nome:</label>
                                <asp:TextBox ID="modalNomeAluno" runat="server" Text="modalNome" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Sobrenome:</label>
                                <asp:TextBox ID="modalSobrenomeAluno" runat="server" Text="modalSobrenome" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>CPF:</label>
                                <asp:TextBox ID="modalCpfAluno" runat="server" Text="modalCpf" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Email:</label>
                                <asp:TextBox ID="modalEmailAluno" runat="server" Text="modalEmail" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Telefone:</label>
                                <asp:TextBox ID="modalTelefoneAluno" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Data de Nascimento:</label>
                                <asp:TextBox ID="modalDataNascimentoAluno" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>CEP:</label>
                                <asp:TextBox ID="modalCepAluno" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Rua:</label>
                                <asp:TextBox ID="modalRuaAluno" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Bairro:</label>
                                <asp:TextBox ID="modalBairroAluno" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Cidade:</label>
                                <asp:TextBox ID="modalCidadeAluno" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Estado:</label>
                                <asp:TextBox ID="modalEstadoAluno" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Número:</label>
                                <asp:TextBox ID="modalNumeroAluno" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Complemento:</label>
                                <asp:TextBox ID="modalComplementoAluno" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>CarteiraFpjj:</label>
                                <asp:TextBox ID="modalCarteiraFpjjAluno" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="modal-footer text-center">
                                <asp:Button ID="SalvarAluno" OnClick="SalvarAluno_Click" runat="server" CssClass="asp-button btn btn-primary" Text="Salvar" />
                            </div>

                    </div>

                    <div ID="divResponsavel" class="modal-body" style="display: none;">
                            <div class="form-group">
                                <asp:Label>Número do ID:</asp:Label>
                                <asp:TextBox ID="ModalIdResponsavel" runat="server" ReadOnly CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Nome:</label>
                                <asp:TextBox ID="modalNomeResponsavel" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label">Sobrenome:</label>
                                <asp:TextBox ID="modalSobrenomeResponsavel" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>CPF:</label>
                                <asp:TextBox ID="modalCpfResponsavel" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Email:</label>
                                <asp:TextBox ID="modalEmailResponsavel" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Telefone:</label>
                                <asp:TextBox ID="modalTelefoneResponsavel" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Data de Nascimento:</label>
                                <asp:TextBox ID="modalDataNascimentoResponsavel" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>CEP:</label>
                                <asp:TextBox ID="modalCepResponsavel" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Rua:</label>
                                <asp:TextBox ID="modalRuaResponsavel" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Bairro:</label>
                                <asp:TextBox ID="modalBairroResponsavel" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Cidade:</label>
                                <asp:TextBox ID="modalCidadeResponsavel" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label">Estado:</label>
                                <asp:TextBox ID="modalEstadoResponsavel" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Número:</label>
                                <asp:TextBox ID="modalNumeroResponsavel" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Complemento:</label>
                                <asp:TextBox ID="modalComplementoResponsavel" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="modal-footer text-center">
                                <asp:Button ID="SalvarResponsavel" runat="server" CssClass="asp-button btn btn-primary" Text="Salvar" OnClick="SalvarResponsavel_Click"/>
                            </div>

                    </div>
                            
                    <div ID="divPlano" class="modal-body" style="display: none;">

                            <asp:Literal ID="LitDadosPlano" runat="server" Mode="PassThrough"></asp:Literal>

                    
                            <asp:Button ID="btnModificarPlano" runat="server" Text="Modificar Plano"
                                CssClass="asp-button btn btn-primary"
                                OnClientClick="return confirmarMudancaPlano();" />

                    </div>

                    <div ID="divGraduacao" class="modal-body" style="display: none;">
                        <h3>Graduações do Aluno</h3>
                        <asp:Label ID="modalNomeGraducaoHeader" runat="server" CssClass="form-group"></asp:Label>
                        <hr/>

                        <asp:Repeater ID="rptGraduacoes" runat="server" OnItemCommand="rptGraduacoes_ItemCommand">
                            <ItemTemplate>
                                <div class='card p-2 mb-2'>
                                    <asp:HiddenField ID="hdnIdGraduacao" runat="server" Value='<%# Eval("idGraduacao") %>' />
                                    <strong>Observação:</strong> <asp:Label ID="lblObservacao" runat="server" Text='<%# Eval("observacao") %>' /><br/>
                                    <strong>Data de Graduação:</strong> <asp:Label ID="lblDataGraduacao" runat="server" Text='<%# Eval("dataGraduacao", "{0:dd/MM/yyyy}") %>' /><br/>
                                    <div class="text-right mt-2">
                                        <asp:Button ID="btnExcluirGraduacao" runat="server" CssClass="btn btn-danger btn-sm" Text="Excluir"
                                                    CommandName="Excluir" CommandArgument='<%# Eval("idGraduacao") %>'
                                                    OnClientClick="return confirm('Tem certeza que deseja excluir esta graduação?');" />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                        <asp:Panel ID="pnlNoGraduations" runat="server" Visible="false">
                            <p>Nenhuma graduação encontrada para este aluno.</p>
                        </asp:Panel>


                        <div class="modal-footer text-center">
                            <asp:Button ID="btnAdicionarGraduacao" runat="server" CssClass="asp-button btn btn-primary" Text="Adicionar Graduação" OnClientClick="return adicionarGraduacao()" />
                        </div>

                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfAlunoPossuiPlano" runat="server" ClientIDMode="Static"/>


    </main>

    <script>
        
        function abrirModal() {
            document.getElementById("modalDetalhes").style.display = "block";
        }


        function fecharModal() {
            document.getElementById("modalDetalhes").style.display = "none";
        }

        function exibirAba(nomeAba) {
            document.getElementById("divAluno").style.display = "none";
            document.getElementById("divResponsavel").style.display = "none";
            document.getElementById("divPlano").style.display = "none";
            document.getElementById("divGraduacao").style.display = "none";

            document.getElementById("div" + nomeAba).style.display = "block";
        }


        function confirmarMudancaPlano() {
            const hf = document.getElementById('hfIdAlunoModal');
            const hfPossuiPlano = document.getElementById('hfAlunoPossuiPlano');

            if (!hf || !hfPossuiPlano) {
                alert("Campos ocultos não encontrados na modal.");
                return false;
            }

            const idAluno = hf.value;
            const possuiPlano = hfPossuiPlano.value === "true";

            Swal.fire({
                title: 'Tem certeza?',
                text: "Você não poderá desfazer esta ação!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Sim, confirmar',
                cancelButtonText: 'Não, cancelar',
                reverseButtons: true
            }).then((result) => {
                if (result.isConfirmed) {
                    Swal.fire('Confirmado!', 'Você será redirecionado.', 'success').then(() => {
                        const url = 'CadastrarPlano.aspx?idAluno=' + idAluno +
                            (possuiPlano ? '&excluirAnterior=true' : '');
                        window.location.href = url;
                    });
                } else {
                    Swal.fire('Cancelado', 'A ação foi cancelada.', 'error');
                }
            });

            return false;
        }

        function adicionarGraduacao() {
            const hf = document.getElementById('hfMatriculaAlunoModal');
            const idMatricula = hf.value;

            Swal.fire({
                title: 'Tem certeza?',
                text: "Você será redirecionado!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Sim, confirmar',
                cancelButtonText: 'Não, cancelar',
                reverseButtons: true
            }).then((result) => {
                if (result.isConfirmed) {           
                    const url = 'Graduacao.aspx?idMatricula=' + idMatricula;
                    window.location.href = url;             
                } else {
                    Swal.fire('Cancelado', 'A ação foi cancelada.', 'error');
                }
            });

            return false;
        }






    </script>

    <style>
/* Layout geral */
main {
    padding: 20px;
}

/* Título */
#aspnetTitle {
    font-size: 2rem;
    margin-bottom: 20px;
    color: #333;
}

/* Container dos filtros */
.flex-container {
    display: flex;
    gap: 10px;
    flex-wrap: wrap;
    margin-bottom: 20px;
}

/* Estilo dos botões */
.btn-custom, .asp-button {
    padding: 8px 16px;
    border-radius: 8px;
    border: none;
    cursor: pointer;
    transition: background-color 0.3s ease;
    height:35px;
}

.btn-custom:hover {
    opacity: 0.9;
}

.btn-primary {
    background-color: #007bff;
    color: white;
}

.btn-danger {
    background-color: #dc3545;
    color: white;
}

.btn-light {
    background-color: #f8f9fa;
    color: #333;
}

.icon-btn {
    width: 40px;
    height: 40px;
    padding: 4px;
}

/* DropDown e TextBox */
.form-select-custom, .form-control {
    padding: 8px;
    border-radius: 8px;
    border: 1px solid #ccc;
    min-width: 180px;
}

.form-control {
    width: 100%;
}

/* Tabela */
.table {
    width: 100%;
    border-collapse: collapse;
}

.table th {
    background-color: #007bff;
    color: white;
    padding: 8px;
}

.table td {
    padding: 8px;
    border: 1px solid #ddd;
}

.table-striped tbody tr:nth-child(odd) {
    background-color: #f9f9f9;
}

.table-hover tbody tr:hover {
    background-color: #f1f1f1;
}

/* Modal */
.modal {
    display: none; 
    position: fixed; 
    z-index: 1000; 
    padding-top: 60px; 
    left: 0;
    top: 0;
    width: 100%; 
    height: 100%; 
    overflow: auto; 
    background-color: rgba(0,0,0,0.5); 
}

.modal-dialog {
    background-color: white;
    margin: auto;
    padding: 20px;
    border-radius: 10px;
    max-width: 600px;
    box-shadow: 0 5px 15px rgba(0,0,0,0.3);
}

.modal-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    border-bottom: 1px solid #dee2e6;
    padding-bottom: 10px;
}

.modal-header .close {
    background: none;
    border: none;
    font-size: 1.5rem;
}

.modal-body {
    margin-top: 10px;
}

.modal-footer {
    display: flex;
    justify-content: center;
    gap: 10px;
}

/* Labels */
label, asp\:Label {
    font-weight: 500;
    margin-bottom: 5px;
}

/* CheckBox alinhado */
.d-flex {
    display: flex;
    align-items: center;
    gap: 10px;
}

/* Responsivo */
@media (max-width: 600px) {
    .flex-container {
        flex-direction: column;
    }

    .modal-dialog {
        width: 90%;
    }
    .input-grande {
    width: 250px; /* Aumenta o tamanho */
    height: 35px; /* Altura opcional */
    font-size: 16px;
}

}



    </style>
</asp:Content>
