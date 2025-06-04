<%@ Page Title="Lista de Alunos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaAlunos.aspx.cs" Inherits="BJJGerenciamento.UI._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <h1 id="aspnetTitle">Lista de Alunos</h1>    
        
       <div class="flex-container">
            <asp:ImageButton ID="btnFiltro" runat="server" ImageUrl="~/Images/filtro.png" OnClick="btnFiltro_Click" AlternateText="Filtrar" CssClass="btn btn-light icon-btn" />

            <asp:DropDownList ID="ddPlanos" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddPlanos_SelectedIndexChanged" Visible="false" CssClass="form-select-custom" />

<%--            <asp:DropDownList ID="ddHorarios" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddHorarios_SelectedIndexChanged" Visible="false" CssClass="form-select-custom" />--%>

            <asp:TextBox ID="TxtTermoPesquisa" runat="server" Visible="false" CssClass="form-control" placeholder="Pesquisar..." />

            <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" OnClick="btnPesquisar_Click" Visible="false" CssClass="btn btn-primary btn-custom" style="background-color:blue" />

            <asp:Button ID="btnLimpar" runat="server" Text="Limpar filtros" OnClick="btnLimpar_Click" Visible="false" CssClass="btn btn-danger btn-custom" />
        </div>

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
                        <asp:Button ID="btnDetalhes" runat="server" Text="Mais" 
                            CommandName="Detalhes" 
                            OnClick="btnDetalhes_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    <!-- Modal (Mini Tela) -->

        <div id="modalDetalhes" class="modal" tabindex="-1" role="dialog">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <asp:Button ID="btnDetalhesAluno" OnClick="btnDetalhesAluno_Click" runat="server" Text="Aluno"/>
                        <asp:Button ID="btnDetalhesResponsavel" OnClick="btnDetalhesResponsavel_Click" runat="server" Text="Responsavel"/>
                        <asp:Button ID="btnDetalhesPlano" OnClick="btnDetalhesPlano_Click" runat="server" Text="Plano"/>

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
                                <label">Sobrenome:</label>
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

                        <asp:Literal ID="litDadosPlano" runat="server" Mode="PassThrough"></asp:Literal>

                        <div class="modal-footer text-center">
                        <asp:Button ID="btnModificarPlano" runat="server" Text="Modificar Plano"
                            CssClass="asp-button btn btn-primary"
                            OnClientClick="return confirmarMudancaPlano();" />

                        </div>
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

        function exibirAba(nomeAba) {
            document.getElementById("divAluno").style.display = "none";
            document.getElementById("divResponsavel").style.display = "none";
            document.getElementById("divPlano").style.display = "none";

            document.getElementById("div" + nomeAba).style.display = "block";
            abrirModal();
        }

        function confirmarMudancaPlano() {
            const idAluno = document.getElementById("modalIdMatriculaAluno").value;

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
                    Swal.fire(
                        'Confirmado!',
                        'Você será redirecionado.',
                        'success'
                    ).then(() => {
                        window.location.href = 'CadastrarPlano.aspx?idAluno=' + idAluno;
                    });
                } else {
                    Swal.fire(
                        'Cancelado',
                        'A ação foi cancelada.',
                        'error'
                    );
                }
            });

            return false;
        }


    </script>

    <style>
        #GridView1 {
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


        #pnlAluno{
            display: "block";
        }

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
</asp:Content>
