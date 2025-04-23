<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastrarProfessor.aspx.cs" Inherits="BJJGerenciamento.UI.CadastrarProfessor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Cadastrar Professor</h2>

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

    <!-- Validation Summary -->
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger mb-3" HeaderText="Por favor, corrija os campos obrigatórios:" ShowSummary="true" ShowMessageBox="false" />

    <!-- Informações Pessoais -->
    <div class="card mb-4">
        <div class="card-header">Informações Pessoais</div>
        <div class="card-body">
            <div class="row mb-3">
                <div class="col-md-6">
                    <label class="form-label">Nome</label>
                    <asp:TextBox ID="txtNome" CssClass="form-control" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvNome" ControlToValidate="txtNome" runat="server" ErrorMessage="Informe o nome." CssClass="text-danger" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="revNome" ControlToValidate="txtNome" runat="server" ValidationExpression="^[a-zA-Zà-úÀ-Ú\s]+$" ErrorMessage="O nome só pode conter letras." CssClass="text-danger" Display="Dynamic" />
                </div>
                <div class="col-md-6">
                    <label class="form-label">Sobrenome</label>
                    <asp:TextBox ID="txtSobrenome" CssClass="form-control" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvSobrenome" ControlToValidate="txtSobrenome" runat="server" ErrorMessage="Informe o sobrenome." CssClass="text-danger" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="revSobrenome" ControlToValidate="txtSobrenome" runat="server" ValidationExpression="^[a-zA-Zà-úÀ-Ú\s]+$" ErrorMessage="O sobrenome só pode conter letras." CssClass="text-danger" Display="Dynamic" />
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <label class="form-label">CPF</label>
                    <asp:TextBox ID="txtCpf" CssClass="form-control" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvCpf" ControlToValidate="txtCpf" runat="server" ErrorMessage="Informe o CPF." CssClass="text-danger" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="revCpf" ControlToValidate="txtCpf" runat="server" ValidationExpression="^\d{3}\.\d{3}\.\d{3}-\d{2}$" ErrorMessage="CPF inválido." CssClass="text-danger" Display="Dynamic" />
                </div>
                <div class="col-md-6">
                    <label class="form-label">Data de Nascimento</label>
                    <asp:TextBox ID="txtDataNascimento" CssClass="form-control" TextMode="Date" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvDataNascimento" ControlToValidate="txtDataNascimento" runat="server" ErrorMessage="Informe a data de nascimento." CssClass="text-danger" Display="Dynamic" />
                    <asp:CustomValidator ID="cvIdade" ControlToValidate="txtDataNascimento" runat="server" OnServerValidate="cvIdade_ServerValidate" ErrorMessage="O professor precisa ser maior de idade." CssClass="text-danger" Display="Dynamic" />
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <label class="form-label">Telefone</label>
                    <asp:TextBox ID="txtTelefone" CssClass="form-control" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvTelefone" ControlToValidate="txtTelefone" runat="server" ErrorMessage="Informe o telefone." CssClass="text-danger" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="revTelefone" ControlToValidate="txtTelefone" runat="server" ValidationExpression="^\(\d{2}\) \d{5}-\d{4}$" ErrorMessage="Telefone inválido." CssClass="text-danger" Display="Dynamic" />
                </div>
                <div class="col-md-6">
                    <label class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvEmail" ControlToValidate="txtEmail" runat="server" ErrorMessage="Informe o email." CssClass="text-danger" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="revEmail" ControlToValidate="txtEmail" runat="server" ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" ErrorMessage="Email inválido." CssClass="text-danger" Display="Dynamic" />
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <label class="form-label">Carteira FPJJ</label>
                    <asp:TextBox ID="txtFpjj" CssClass="form-control" runat="server" />
                </div>
                <div class="col-md-6">
                    <label class="form-label">Carteira CBJJ</label>
                    <asp:TextBox ID="txtCBJJ" CssClass="form-control" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <!-- Informações de Logradouro -->
    <div class="card mb-4">
        <div class="card-header">Informações de Logradouro</div>
        <div class="card-body">
            <div class="row mb-3">
                <div class="col-md-6">
                    <label class="form-label">CEP</label>
                    <div class="d-flex gap-2">
                        <asp:TextBox ID="txtCep" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="cepResponsavel_TextChanged" />
                        <asp:Button ID="buscarCepResponsavel" runat="server" Text="Buscar" CssClass="btn btn-danger btn-custom2" OnClick="buscarCepResponsavel_Click" UseSubmitBehavior="false" />
                    </div>
                    <asp:RequiredFieldValidator ID="rfvCep" ControlToValidate="txtCep" runat="server" ErrorMessage="Informe o CEP." CssClass="text-danger" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="revCep" ControlToValidate="txtCep" runat="server" ValidationExpression="^\d{5}-\d{3}$" ErrorMessage="CEP inválido." CssClass="text-danger" Display="Dynamic" />
                </div>
                <div class="col-md-6">
                    <label class="form-label">Cidade</label>
                    <asp:TextBox ID="txtCidade" CssClass="form-control" runat="server" />
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <label class="form-label">Bairro</label>
                    <asp:TextBox ID="txtBairro" CssClass="form-control" runat="server" />
                </div>
                <div class="col-md-6">
                    <label class="form-label">Rua</label>
                    <asp:TextBox ID="txtRua" CssClass="form-control" runat="server" />
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <label class="form-label">Número</label>
                    <asp:TextBox ID="txtNumero" CssClass="form-control" runat="server" />
                    <asp:RangeValidator ID="rvNumero" ControlToValidate="txtNumero" runat="server" MinimumValue="1" MaximumValue="9999" Type="Integer" ErrorMessage="Número inválido." CssClass="text-danger" Display="Dynamic" />
                </div>
                <div class="col-md-6">
                    <label class="form-label">Complemento</label>
                    <asp:TextBox ID="txtComplemento" CssClass="form-control" runat="server" />
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <label class="form-label">Estado</label>
                    <asp:TextBox ID="txtEstado" CssClass="form-control" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <!-- Botão Enviar -->
    <div class="text-end">
        <asp:Button ID="btnEnviar" Text="Enviar" runat="server" CssClass="btn btn-danger btn-custom px-5" OnClick="btnEnviar_Click" />
    </div>

    <!-- Scripts de Máscara -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.16/jquery.mask.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#<%= txtTelefone.ClientID %>').mask('(00) 00000-0000');
            $('#<%= txtCpf.ClientID %>').mask('000.000.000-00');
            $('#<%= txtCep.ClientID %>').mask('00000-000');
        });
    </script>
</asp:Content>
