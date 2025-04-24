<%@ Page Title="Lista de Alunos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaAlunos.aspx.cs" Inherits="BJJGerenciamento.UI._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <h1 id="aspnetTitle">Lista de Alunos</h1>    
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
                                <Button OnClick="painelAluno()">Aluno</button>
                                <Button OnClick="painelResponsavel()">Responsável</button>
                                <Button OnClick="painelPlano()">Plano</button>

                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="fecharModal()">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <asp:Panel ID="pnlAluno"  runat="server">
        <%--                            <div class="modal-header">
                                        <h5 class="modal-title">Detalhes do Aluno</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="fecharModal()">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>--%>
                                    <div class="modal-body">

                                        <div class="form-group">
                                            <asp:Label for="modalIdMatricula">Número da Matricula:</asp:Label>
                                            <asp:TextBox ID="modalIdMatricula" runat="server" Text="modalId" ReadOnly CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label for="modalDataMatricula">Data da Matricula:</asp:Label>
                                            <asp:TextBox ID="modalDataMatricula" runat="server" Text="modalId" ReadOnly CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group d-flex align-items-center gap-2">
                                            <asp:Label for="modalStatusMatricula" AssociatedControlID="modalStatusMatricula" runat="server" CssClass="mb-0">Status da matrícula:</asp:Label>
                                            <asp:CheckBox ID="modalStatusMatricula" runat="server" />
                                        </div>
                                        <div class="form-group">
                                            <label for="modalNome">Nome:</label>
                                            <asp:TextBox ID="modalNome" runat="server" Text="modalNome" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="modalSobrenome">Sobrenome:</label>
                                            <asp:TextBox ID="modalSobrenome" runat="server" Text="modalSobrenome" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="modalCpf">CPF:</label>
                                            <asp:TextBox ID="modalCpf" runat="server" Text="modalCpf" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="modalEmail">Email:</label>
                                            <asp:TextBox ID="modalEmail" runat="server" Text="modalEmail" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="modalTelefone">Telefone:</label>
                                            <asp:TextBox ID="modalTelefone" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="modalDataNascimento">Data de Nascimento:</label>
                                            <asp:TextBox ID="modalDataNascimento" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="modalCep">CEP:</label>
                                            <asp:TextBox ID="modalCep" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="modalRua">Rua:</label>
                                            <asp:TextBox ID="modalRua" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="modalBairro">Bairro:</label>
                                            <asp:TextBox ID="modalBairro" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="modalCidade">Cidade:</label>
                                            <asp:TextBox ID="modalCidade" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="modalEstado">Estado:</label>
                                            <asp:TextBox ID="modalEstado" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="modalNumero">Número:</label>
                                            <asp:TextBox ID="modalNumero" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="modalComplemento">Complemento:</label>
                                            <asp:TextBox ID="modalComplemento" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="modalCarteiraFpjj">CarteiraFpjj:</label>
                                            <asp:TextBox ID="modalCarteiraFpjj" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="modal-footer text-center">
                                            <asp:Button ID="SalvarAluno" OnClick="SalvarAluno_Click" runat="server" CssClass="asp-button btn btn-primary" Text="Salvar" />
                                        </div>

                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlResponsavel"  runat="server">
                                    <div class="modal-body">
                                        <div class="form-group">
                                            <label for="modalNomeResponsavel">Nome:</label>
                                            <asp:TextBox ID="modalNomeResponsavel" runat="server" Text="modalNome" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="modalSobrenomeResponsavel">Sobrenome:</label>
                                            <asp:TextBox ID="modalSobrenomeResponsavel" runat="server" Text="modalSobrenome" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="modalCpfResponsavel">CPF:</label>
                                            <asp:TextBox ID="modalCpfResponsavel" runat="server" Text="modalCpf" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="modalEmailResponsavel">Email:</label>
                                            <asp:TextBox ID="modalEmailResponsavel" runat="server" Text="modalEmail" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="modalTelefoneResponsavel">Telefone:</label>
                                            <asp:TextBox ID="modalTelefoneResponsavel" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="modalDataNascimentoResponsavel">Data de Nascimento:</label>
                                            <asp:TextBox ID="modalDataNascimentoResponsavel" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="modalCepResponsavel">CEP:</label>
                                            <asp:TextBox ID="modalCepResponsavel" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="modalRuaResponsavel">Rua:</label>
                                            <asp:TextBox ID="modalRuaResponsavel" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="modalBairroResponsavel">Bairro:</label>
                                            <asp:TextBox ID="modalBairroResponsavel" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="modalCidadeResponsavel">Cidade:</label>
                                            <asp:TextBox ID="modalCidadeResponsavel" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="modalEstadoResponsavel">Estado:</label>
                                            <asp:TextBox ID="modalEstadoResponsavel" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="modalNumeroResponsavel">Número:</label>
                                            <asp:TextBox ID="modalNumeroResponsavel" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="modalComplementoResponsavel">Complemento:</label>
                                            <asp:TextBox ID="modalComplementoResponsavel" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="modal-footer text-center">
                                            <asp:Button ID="SalvarResponsavel" runat="server" CssClass="asp-button btn btn-primary" Text="Salvar" />
                                        </div>

                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlPlano" runat="server">
                                    <div class="modal-body">

                                        <div class="modal-footer text-center">
                                            <asp:Button ID="SalvarPlano" runat="server" CssClass="asp-button btn btn-primary" Text="Salvar" />
                                        </div>
                               </div>
                            </asp:Panel>
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

        function painelAluno() {
            document.getElementById("pnlAluno").style.display = "block";
            document.getElementById("pnlResponsavel").style.display = "none";
            document.getElementById("pnlPlano").style.display = "none";
        }

        function painelResponsavel() {
            document.getElementById("pnlAluno").style.display = "none";
            document.getElementById("pnlResponsavel").style.display = "block";
            document.getElementById("pnlPlano").style.display = "none";
        }

        function painelPlano() {
            document.getElementById("pnlAluno").style.display = "none";
            document.getElementById("pnlResponsavel").style.display = "none";
            document.getElementById("pnlPlano").style.display = "block";
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
            display = "block";
        }
/*      .form-group {
            display: flex;
            align-items: center;
            gap:8px; 
            padding: 4px;
        }
*/

    </style>
</asp:Content>
