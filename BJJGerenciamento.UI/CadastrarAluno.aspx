<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastrarAluno.aspx.cs" Inherits="BJJGerenciamento.UI.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"> Cadastro </h2>

        <label for="cpf">Cpf:</label>
        <asp:TextBox ID="cpf" runat="server" OnTextChanged="cpf_TextChanged" AutoPostBack="true" oninput="formatarCpf(this)"/>
        <br />
            
        <label for="nome">Nome:</label>
        <asp:TextBox ID="txtNome" runat="server" OnTextChanged="txtNome_TextChanged" />
        <br />

        <label for="sobrenome">Sobrenome:</label>
        <asp:TextBox ID="sobrenome" runat="server" OnTextChanged="sobrenome_TextChanged" />
        <br />

        <label for="telefone">Telefone:</label>
        <asp:TextBox ID="telefone" runat="server" OnTextChanged="telefone_TextChanged" />
        <br />

        <label for="email">Email:</label>
        <asp:TextBox ID="email" runat="server" OnTextChanged="email_TextChanged" />
        <br />

        <label for="rg">Rg:</label>
        <asp:TextBox ID="rg" runat="server" OnTextChanged="rg_TextChanged" />
        <br />

        <label for="dataNascimento">Data de Nascimento:</label>
        <asp:TextBox ID="dataNascimento" runat="server" OnTextChanged="dataNascimento_TextChanged" />
        <br />

        <label for="cep">Cep:</label>
        <asp:TextBox ID="cep" runat="server" OnTextChanged="cep_TextChanged" />

        <label for="naoCep">Não sei o Cep:</label>
        <asp:Checkbox ID="naoCep" runat="server" OnCheckedChanged="naoCep_CheckedChanged" AutoPostBack="true"/>
        <asp:Button ID="BuscarCep" runat="server" OnClick="BuscarCep_Click" Text="Buscar" />
        <br />

        <label for="rua">Rua:</label>
        <asp:TextBox ID="rua" runat="server" OnTextChanged="rua_TextChanged" />
        <br />

        <label for="bairro">Bairro:</label>
        <asp:TextBox ID="bairro" runat="server" OnTextChanged="bairro_TextChanged" />
        <br />

        <label for="cidade">Cidade:</label>
        <asp:TextBox ID="cidade" runat="server" OnTextChanged="cidade_TextChanged" />
        <br />

        <label for="estado">Estado:</label>
        <asp:TextBox ID="estado" runat="server" OnTextChanged="estado_TextChanged" />
        <br />

        <label for="numeroCasa">NumeroCasa:</label>
        <asp:TextBox ID="numeroCasa" runat="server" OnTextChanged="numeroCasa_TextChanged" />
        <br />

        <asp:Button ID="btnEnviar" Text="Enviar" OnClick="btnEnviar_Click" runat="server" />
    </main>

    <script>
        function formatarCpf(campo) {
            let cpf = campo.value.replace(/\D/g, "");

            if (cpf.length > 11) cpf = cpf.substring(0, 11); 

            cpf = cpf.replace(/^(\d{3})(\d)/, "$1.$2");
            cpf = cpf.replace(/^(\d{3})\.(\d{3})(\d)/, "$1.$2.$3");
            cpf = cpf.replace(/\.(\d{3})(\d)/, ".$1-$2");

            campo.value = cpf;
        }
    </script>

</asp:Content>






