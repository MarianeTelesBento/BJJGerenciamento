<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastrarProfessor.aspx.cs" Inherits="BJJGerenciamento.UI.CadastrarProfessor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Cadastrar Professor</h2>
 <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />


   <style>
    .btn-custom {
        width: 250px;
        font-size: 16px;
        border: none;
        color: white;
        border-radius: 6px;
        font-family: -apple-system, Roboto, Arial, sans-serif;
        padding: 6px 0;
    }

    .btn-custom2 {
        width: 90px;
        font-size: 15px;
        border: none;
        color: white;
        border-radius: 4px;
        font-family: -apple-system, Roboto, Arial, sans-serif;
        padding: 6px 0;
    }
</style>


<!-- CARD: Informações Pessoais -->
<div class="card mb-4">
    <div class="card-header">
        Informações Pessoais
    </div>
    <div class="card-body">
        <div class="row mb-3">
            <div class="col-md-6">
                <label for="txtNome" class="form-label">Nome</label>
                <asp:TextBox ID="txtNome" CssClass="form-control" runat="server" />
            </div>
            <div class="col-md-6">
                <label for="txtSobrenome" class="form-label">Sobrenome</label>
                <asp:TextBox ID="txtSobrenome" CssClass="form-control" runat="server" />
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-md-6">
                <label for="txtCpf" class="form-label">CPF</label>
                <asp:TextBox ID="txtCpf" CssClass="form-control" runat="server" />
            </div>
            <div class="col-md-6">
                <label for="txtDataNascimento" class="form-label">Data de Nascimento</label>
                <asp:TextBox ID="txtDataNascimento" CssClass="form-control" TextMode="Date" runat="server" />
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-md-6">
                <label for="txtTelefone" class="form-label">Telefone</label>
                <asp:TextBox ID="txtTelefone" CssClass="form-control" runat="server" />
            </div>
            <div class="col-md-6">
                <label for="txtEmail" class="form-label">Email</label>
                <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" />
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-md-6">
                <label for="txtFpjj" class="form-label">Carteira FPJJ</label>
                <asp:TextBox ID="txtFpjj" CssClass="form-control" runat="server" />
            </div>
            <div class="col-md-6">
                <label for="txtCBJJ" class="form-label">Carteira CBJJ</label>
                <asp:TextBox ID="txtCBJJ" CssClass="form-control" runat="server" />
            </div>
        </div>
    </div>
</div>

<!-- CARD: Informações de Logradouro -->
<div class="card mb-4">
    <div class="card-header">
        Informações de Logradouro
    </div>
    <div class="card-body">
        <div class="row mb-3">
            <div class="col-md-6">
                <label for="txtCep" class="form-label">CEP</label>
                <div class="d-flex gap-2">
                    <asp:TextBox ID="txtCep" runat="server" CssClass="form-control" OnTextChanged="cepResponsavel_TextChanged" AutoPostBack="true" />
                    <asp:Button ID="buscarCepResponsavel" runat="server" OnClick="buscarCepResponsavel_Click" 
                        Text="Buscar" CssClass="btn btn-danger btn-custom2" UseSubmitBehavior="false" />
                </div>
            </div>
            <div class="col-md-6">
                <label for="txtCidade" class="form-label">Cidade</label>
                <asp:TextBox ID="txtCidade" CssClass="form-control" runat="server" />
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-md-6">
                <label for="txtBairro" class="form-label">Bairro</label>
                <asp:TextBox ID="txtBairro" CssClass="form-control" runat="server" />
            </div>
            <div class="col-md-6">
                <label for="txtRua" class="form-label">Rua</label>
                <asp:TextBox ID="txtRua" CssClass="form-control" runat="server" />
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-md-6">
                <label for="txtNumero" class="form-label">Número</label>
                <asp:TextBox ID="txtNumero" CssClass="form-control" runat="server" />
            </div>
            <div class="col-md-6">
                <label for="txtComplemento" class="form-label">Complemento</label>
                <asp:TextBox ID="txtComplemento" CssClass="form-control" runat="server" />
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-md-6">
                <label for="txtEstado" class="form-label">Estado</label>
                <asp:TextBox ID="txtEstado" CssClass="form-control" runat="server" />
            </div>
        </div>
    </div>
</div>

<!-- Botão Enviar -->
<div class="text-end">
    <asp:Button Text="Enviar" ID="btnEnviar" runat="server"
        CssClass="btn btn-danger btn-custom px-5" OnClick="btnEnviar_Click" />
</div>



 

         <!-- Incluir o jQuery e o jQuery Mask Plugin -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.16/jquery.mask.min.js"></script>

    <!-- Script para aplicar as máscaras -->
    <script type="text/javascript">
        $(document).ready(function () {
            // Máscara para o telefone
            $('#<%= txtTelefone.ClientID %>').mask('(00) 00000-0000');

            // Máscara para o CPF
            $('#<%= txtCpf.ClientID %>').mask('000.000.000-00');


            $('#<%= txtCep.ClientID %>').mask('00000-000')
        });

        function formatarCep(campo) {
            let cep = campo.value.replace(/\D/g, "");

            if (cep.length > 8) cep = cep.substring(0, 8);

            cep = cep.replace(/^(\d{5})(\d)/, "$1-$2");

            campo.value = cep;
        }
    </script>
</asp:Content>


