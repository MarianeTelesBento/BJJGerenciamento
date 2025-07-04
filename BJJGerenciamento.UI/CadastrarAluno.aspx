﻿<%@ Page Title="Cadastrar aluno" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastrarAluno.aspx.cs" Inherits="BJJGerenciamento.UI.CadastrarAluno" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <ContentTemplate>
            <style>
                    .btn-custom {
                    width: 250px;
                    font-size: 16px;
                    border: none;
                    color: white;
                    border-radius: 6px;
                    font-family: -apple-system, Roboto, Arial, sans-serif;
                    padding: 0px 14px; /* padding lateral */
                    height: 35px;
                    line-height: 35px;
                    text-align: center;

                    }
                    .btn-custom2 {
                    width: 90px;
                    font-size: 12px;
                    border: none;
                    color: white;
                    border-radius: 4px;
                    font-family: -apple-system, Roboto, Arial, sans-serif;
                    padding: 12px;
                    height: 16px;
                    line-height: 3px;
                    text-align: center;
                }
                    .btn-custom1 {
                    width: 250px;
                    font-size: 16px;
                    border: none;
                    color: white;
                    border-radius: 6px;
                    font-family: -apple-system, Roboto, Arial, sans-serif;
                    padding: 0px 14px; 
                    height: 35px;
                    line-height: 35px;
                    text-align: center;

                    }
            </style>

            <main aria-labelledby="title">
            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" CssClass="btn btn-danger" style="height:35px" Visible="false"/>
            <asp:Panel ID="pnlInformacoesPessoaisAluno" runat="server" CssClass="container mt-4">
                    <h2>Cadastro de Alunos</h2>
                        <div class="container mt-4">
                            <div class="card mb-4">
                                <div class="card-header">
                                    Informações Pessoais
                                </div>
                                <div class="card-body">
                                    <div class="row mb-3">
                                        <div class="col-md-6">
                                            <label for="txtNome" class="form-label">Nome</label>
                                            <asp:TextBox ID="nomeAluno" CssClass="form-control" runat="server" />
                                        </div>
                                        <div class="col-md-6">
                                            <label for="txtSobrenome" class="form-label">Sobrenome</label>
                                            <asp:TextBox ID="sobrenomeAluno" CssClass="form-control" runat="server" />
                                        </div>
                                    </div>

                                    <div class="row mb-3">
                                        <div class="col-md-6">
                                            <label for="txtCpf" class="form-label">CPF</label>
                                            <asp:TextBox ID="cpfAluno" CssClass="form-control" runat="server" OnTextChanged="cpfAluno_TextChanged" AutoPostBack="true"/>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="txtDataNascimento" class="form-label">Data de Nascimento</label>
                                            <asp:TextBox ID="dataNascimentoAluno" CssClass="form-control" TextMode="Date" runat="server" />
                                        </div>
                                    </div>

                                    <div class="row mb-3">
                                        <div class="col-md-6">
                                            <label for="txtTelefone" class="form-label">Telefone</label>
                                            <asp:TextBox ID="telefoneAluno" CssClass="form-control" runat="server" />
                                        </div>
                                        <div class="col-md-6">
                                            <label for="txtFpjj" class="form-label">Carteira FPJJ</label>
                                            <asp:TextBox ID="carteiraFPJJAluno" CssClass="form-control" runat="server" />
                                        </div>
                                    </div>

                                    <div class="row mb-3">
                                        <div class="col-md-6">
                                            <label for="txtEmail" class="form-label">Email</label>
                                            <asp:TextBox ID="emailAluno" CssClass="form-control" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card mb-4">
                                <div class="card-header">
                                    Informações de Logradouro
                                </div>
                                <div class="card-body">
                                    <div class="row mb-3">
                                        <div class="col-md-6">
                                            <label for="txtCep" class="form-label">CEP</label>
                                            <div class="d-flex gap-2">
                                        <asp:TextBox ID="cepAluno" runat="server" CssClass="form-control" />
                                                <asp:Button ID="Button1" runat="server" OnClick="buscarCepAluno_Click" 
                                                    Text="Buscar" CssClass="btn btn-danger btn-custom2" UseSubmitBehavior="false" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row mb-3">
                                        <div class="col-md-6">
                                            <label for="txtCidade" class="form-label">Cidade</label>
                                            <asp:TextBox ID="cidadeAluno" CssClass="form-control" runat="server" />
                                        </div>
                                        <div class="col-md-6">
                                            <label for="txtBairro" class="form-label">Bairro</label>
                                            <asp:TextBox ID="bairroAluno" CssClass="form-control" runat="server" />
                                        </div>
                                    </div>

                                    <div class="row mb-3">
                                        <div class="col-md-6">
                                            <label for="txtRua" class="form-label">Rua</label>
                                            <asp:TextBox ID="ruaAluno" CssClass="form-control" runat="server" />
                                        </div>
                                        <div class="col-md-6">
                                            <label for="txtNumero" class="form-label">Número</label>
                                            <asp:TextBox ID="numeroCasaAluno" CssClass="form-control" runat="server" />
                                        </div>
                                    </div>

                                    <div class="row mb-3">
                                        <div class="col-md-6">
                                            <label for="estadoAluno" class="form-label">Estado</label>
                                            <asp:TextBox ID="estadoAluno" CssClass="form-control" runat="server" />
                                        </div>
                                        <div class="col-md-6">
                                            <label for="txtComplemento" class="form-label">Complemento</label>
                                            <asp:TextBox ID="complementoAluno" CssClass="form-control" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer text-end">
                                <asp:Button ID="btnProximoResponsavel" Text="Próximo" OnClick="btnProximoResponsavel_Click" runat="server"
                                    CssClass="btn btn-danger btn-custom" />
                            </div>
                        </div>
            </asp:Panel>

            <asp:Panel ID="pnlInformacoesResponsavelAluno" runat="server" Visible="false" CssClass="container mt-4">
                <h2>Cadastro de Responsável</h2>  
                    <div class="container mt-4">
                        <div class="card mb-4">
                            <div class="card-header">Informações Pessoais</div>
                            <div class="card-body">
                                <div class="row mb-3">
                                    <div class="col-md-6">
                                        <label for="nomeResponsavel" class="form-label">Nome</label>
                                        <asp:TextBox ID="nomeResponsavel" CssClass="form-control" runat="server" />
                                    </div>
                                    <div class="col-md-6">
                                        <label for="sobrenomeResponsavel" class="form-label">Sobrenome</label>
                                        <asp:TextBox ID="sobrenomeResponsavel" CssClass="form-control" runat="server" />
                                    </div>
                                </div>
                                <div class="row mb-3">
                                    <div class="col-md-6">
                                        <label for="cpfResponsavel" class="form-label">CPF</label>
                                        <asp:TextBox ID="cpfResponsavel" CssClass="form-control" runat="server" OnTextChanged="cpfResponsavel_TextChanged" AutoPostBack="true" />
                                    </div>
                                    <div class="col-md-6">
                                        <label for="dataNascimentoResponsavel" class="form-label">Data de Nascimento</label>
                                        <asp:TextBox ID="dataNascimentoResponsavel" CssClass="form-control" TextMode="Date" runat="server" />
                                    </div>
                                </div>
                                <div class="row mb-3">
                                    <div class="col-md-6">
                                        <label for="telefoneResponsavel" class="form-label">Telefone</label>
                                        <asp:TextBox ID="telefoneResponsavel" CssClass="form-control" runat="server" />
                                    </div>
                                    <div class="col-md-6">
                                        <label for="emailResponsavel" class="form-label">Email</label>
                                        <asp:TextBox ID="emailResponsavel" CssClass="form-control" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card mb-4">
                            <div class="card-header">
                                Informações de Logradouro
                            </div>
                            <div class="card-body">
                                <div class="row mb-3">
                                    <div class="col-md-6">
                                        <label for="cepResponsavel" class="form-label">CEP</label>
                                        <div class="d-flex gap-2">
                                                <asp:TextBox ID="cepResponsavel" CssClass="form-control" runat="server" />
                                                <asp:Button ID="buscarCepResponsavel" runat="server" OnClick="buscarCepResponsavel_Click"
                                                    Text="Buscar" CssClass="btn btn-danger btn-custom2" UseSubmitBehavior="false" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row mb-3">
                                    <div class="col-md-6">
                                        <label for="cidadeResponsavel" class="form-label">Cidade</label>
                                        <asp:TextBox ID="cidadeResponsavel" CssClass="form-control" runat="server" />
                                    </div>
                                    <div class="col-md-6">
                                        <label for="bairroResponsavel" class="form-label">Bairro</label>
                                        <asp:TextBox ID="bairroResponsavel" CssClass="form-control" runat="server" />
                                    </div>
                                </div>
                                <div class="row mb-3">
                                    <div class="col-md-6">
                                        <label for="ruaResponsavel" class="form-label">Rua</label>
                                        <asp:TextBox ID="ruaResponsavel" CssClass="form-control" runat="server" />
                                    </div>
                                    <div class="col-md-6">
                                        <label for="numeroCasaResponsavel" class="form-label">Número</label>
                                        <asp:TextBox ID="numeroCasaResponsavel" CssClass="form-control" runat="server" />
                                    </div>
                                </div>
                                <div class="row mb-3">
                                    <div class="col-md-6">
                                        <label for="complementoResponsavel" class="form-label">Complemento</label>
                                        <asp:TextBox ID="complementoResponsavel" CssClass="form-control" runat="server" />
                                    </div>
                                    <div class="col-md-6">
                                        <label for="estadoResponsavel" class="form-label">Estado</label>
                                        <asp:TextBox ID="estadoResponsavel" CssClass="form-control" runat="server" />
                                    </div>
                                </div>       
                            </div>
                        </div>
                        <div class="modal-footer text-end">
                            <asp:Button ID="btnProximoConfimar" Text="Próximo" OnClick="btnProximoConfimar_Click" runat="server"
                            CssClass="btn btn-danger btn-custom" />
                        </div>
                    </div>
            </asp:Panel>

         <asp:Panel ID="pnlConfirmarAluno" runat="server" Visible="false" CssClass="container mt-4">
                <h2 class="text-center">Confirmar Cadastro</h2>

                <div class="card mb-4">
                    <div class="card-header">
                        Informações do Aluno
                    </div>
                    <div class="card-body">
                        <div class="row mb-2">
                            <div class="col-md-6">
                                <strong>Nome Completo:</strong> <asp:Label ID="lblNomeCompletoAlunoConfirm" runat="server" />
                            </div>
                            <div class="col-md-6">
                                <strong>CPF:</strong> <asp:Label ID="lblCpfAlunoConfirm" runat="server" />
                            </div>
                        </div>
                        <div class="row mb-2">
                            <div class="col-md-6">
                                <strong>Data de Nascimento:</strong> <asp:Label ID="lblDataNascimentoAlunoConfirm" runat="server" />
                            </div>
                            <div class="col-md-6">
                                <strong>Telefone:</strong> <asp:Label ID="lblTelefoneAlunoConfirm" runat="server" />
                            </div>
                        </div>
                        <div class="row mb-2">
                            <div class="col-md-6">
                                <strong>Carteira FPJJ:</strong> <asp:Label ID="lblCarteiraFpjjAlunoConfirm" runat="server" />
                            </div>
                            <div class="col-md-6">
                                <strong>Email:</strong> <asp:Label ID="lblEmailAlunoConfirm" runat="server" />
                            </div>
                        </div>
                        <div class="row mb-2">
                            <div class="col-md-12">
                                <strong>Endereço:</strong> <asp:Label ID="lblEnderecoAlunoConfirm" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="card mb-4">
                    <div class="card-header">
                        Informações do Responsável
                    </div>
                    <div class="card-body">
                        <div class="row mb-2">
                            <div class="col-md-6">
                                <strong>Nome Completo:</strong> <asp:Label ID="lblNomeCompletoResponsavelConfirm" runat="server" />
                            </div>
                            <div class="col-md-6">
                                <strong>CPF:</strong> <asp:Label ID="lblCpfResponsavelConfirm" runat="server" />
                            </div>
                        </div>
                        <div class="row mb-2">
                            <div class="col-md-6">
                                <strong>Data de Nascimento:</strong> <asp:Label ID="lblDataNascimentoResponsavelConfirm" runat="server" />
                            </div>
                            <div class="col-md-6">
                                <strong>Telefone:</strong> <asp:Label ID="lblTelefoneResponsavelConfirm" runat="server" />
                            </div>
                        </div>
                        <div class="row mb-2">
                            <div class="col-md-6">
                                <strong>Email:</strong> <asp:Label ID="lblEmailResponsavelConfirm" runat="server" />
                            </div>
                        </div>
                        <div class="row mb-2">
                            <div class="col-md-12">
                                <strong>Endereço:</strong> <asp:Label ID="lblEnderecoResponsavelConfirm" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal-footer text-end">      <asp:Button ID="EnviarInformacoes" runat="server" Text="Confirmar e Cadastrar" OnClick="btnEnviarInformacoes_Click" CssClass="btn btn-danger btn-custom" />
                </div>
            </asp:Panel>
        </main>
    </ContentTemplate>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.16/jquery.mask.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.16/jquery.mask.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // Máscara para o telefone
            $('#<%= telefoneAluno.ClientID %>').mask('(00) 00000-0000');

            // Máscara para o CPF
            $('#<%= cpfAluno.ClientID %>').mask('000.000.000-00');


            $('#<%= cepAluno.ClientID %>').mask('00000-000')

            $('#<%= numeroCasaAluno.ClientID %>').mask('00000')

            $('#<%= telefoneAluno.ClientID %>').mask('(00) 00000-0000');

            $('#<%= cpfResponsavel.ClientID %>').mask('000.000.000-00');


            $('#<%= cepResponsavel.ClientID %>').mask('00000-000')

            $('#<%= numeroCasaResponsavel.ClientID %>').mask('00000')

            $('#<%= telefoneResponsavel.ClientID %>').mask('(00) 00000-0000');

        });

        function formatarCep(campo) {
            let cep = campo.value.replace(/\D/g, "");

            if (cep.length > 8) cep = cep.substring(0, 8);

            cep = cep.replace(/^(\d{5})(\d)/, "$1-$2");

            campo.value = cep;
        }
    </script>
    

</asp:Content>






