<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastrarUsuario.aspx.cs" Inherits="BJJGerenciamento.UI.CadastrarUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%-- Inclua o jQuery se ainda não estiver no seu MasterPage. Se já estiver, pode remover. --%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    
    <%-- LINK PARA BOOTSTRAP ICONS (essencial para os ícones bi-eye e bi-eye-slash) --%>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css">

    <style>
        /* Estilos para centralizar os campos de input e o botão */
        .card-body .form-control,
        .card-body .input-group { /* Aplica a largura máxima tanto para form-control avulso quanto para input-group */
            max-width: 300px; /* Ajuste conforme a largura desejada para os campos */
            margin-left: auto;
            margin-right: auto;
        }

        /* === NOVO CSS PARA AUMENTAR A ALTURA DOS CAMPOS === */
        .form-control {
            height: calc(2.25rem + 2px); /* Altura padrão de um input do Bootstrap. 2.25rem é 36px, +2px de borda */
            padding: 0.375rem 0.75rem; /* Padding padrão para centralizar o texto verticalmente */
            font-size: 1rem; /* Tamanho da fonte padrão */
            line-height: 1.5; /* Altura da linha padrão */
        }

        /* Garante que o input-group também tenha a altura correta */
        .input-group > .form-control {
            height: calc(2.25rem + 2px); /* A mesma altura para o input dentro do grupo */
            border-right: 0; /* Remove a borda direita do input para que fique colado no botão */
        }
        
        .input-group-text {
            height: calc(2.25rem + 2px); /* Garante que o addon do botão tenha a mesma altura */
            border-left: 0; /* Remove a borda esquerda do input-group-text */
            background-color: #e9ecef; /* Cor de fundo para o "addon" do botão, similar ao Bootstrap padrão */
            border-color: #ced4da; /* Cor da borda, similar ao Bootstrap padrão */
            padding: 0.375rem 0.75rem; /* Padding para o addon */
            cursor: pointer; /* Indica que é clicável */
            display: flex;
            align-items: center; /* Centraliza o ícone verticalmente */
        }

        /* Ajuste para o botão dentro do input-group-text */
        .input-group-text .btn {
            height: 100%; /* Faz o botão preencher toda a altura do input-group-text */
            display: flex;
            align-items: center;
            justify-content: center;
        }
        /* === FIM DO NOVO CSS === */

        .card-body .btn-custom {
            max-width: 200px; /* Ajuste conforme o tamanho do botão Cadastrar */
            margin-left: auto;
            margin-right: auto;
            display: block; /* Garante que as margens automáticas funcionem para centralizar */
            height: calc(2.25rem + 2px); /* Garante que o botão Cadastrar também tenha a mesma altura */
        }

        /* Ajuste para o ícone dentro do botão, se necessário, para que não fique muito grande */
        .input-group-text i {
            font-size: 1rem; /* Ajuste o tamanho do ícone se ele parecer muito grande ou pequeno */
        }
    </style>

    <body>
        <div class="container mt-5">
            <div class="row justify-content-center">
                <div class="col-md-6">
                    <div class="card shadow rounded-4">
                        <div class="card-header bg-primary text-white text-center">
                            <h4>Cadastro de Usuário</h4>
                        </div>
                        <div class="card-body">

                            <div class="mb-3"> 
                                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="Nome de Usuário"></asp:TextBox>
                            </div>

                            <div class="mb-3">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
                            </div>

                            <div class="mb-3">
                                <div class="input-group">
                                    <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" CssClass="form-control" placeholder="Senha"></asp:TextBox>
                                    <div class="input-group-text">
                                        <button type="button" class="btn btn-link p-0 border-0" onclick="togglePasswordVisibility('<%= txtSenha.ClientID %>', 'eyeIconSenha')">
                                            <i id="eyeIconSenha" class="bi bi-eye"></i>
                                        </button>
                                    </div>
                                </div>
                            </div>

                            <div class="mb-3">
                                <div class="input-group">
                                    <asp:TextBox ID="txtConfirmarSenha" runat="server" TextMode="Password" CssClass="form-control" placeholder="Confirmar Senha"></asp:TextBox>
                                    <div class="input-group-text">
                                        <button type="button" class="btn btn-link p-0 border-0" onclick="togglePasswordVisibility('<%= txtConfirmarSenha.ClientID %>', 'eyeIconConfirmarSenha')">
                                            <i id="eyeIconConfirmarSenha" class="bi bi-eye"></i>
                                        </button>
                                    </div>
                                </div>
                            </div>

                            <div class="d-grid d-flex justify-content-center">
                                <asp:Button ID="btnCadastrar" runat="server" Text="Cadastrar" CssClass="btn btn-custom btn-primary" OnClick="btnCadastrar_Click" /> <%-- Removi Style="height:35px" Width="150px" pois será controlado pelo CSS --%>
                            </div>

                            <div class="mt-3 text-center">
                                <asp:Label ID="lblMensagem" runat="server" CssClass="text-danger"></asp:Label>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <script type="text/javascript">
            function togglePasswordVisibility(inputElementId, iconElementId) {
                const inputField = document.getElementById(inputElementId);
                const eyeIcon = document.getElementById(iconElementId);

                if (inputField.type === "password") {
                    inputField.type = "text";
                    eyeIcon.classList.remove('bi-eye');
                    eyeIcon.classList.add('bi-eye-slash');
                } else {
                    inputField.type = "password";
                    eyeIcon.classList.remove('bi-eye-slash');
                    eyeIcon.classList.add('bi-eye');
                }
            }
        </script>

    </body>
</asp:Content>