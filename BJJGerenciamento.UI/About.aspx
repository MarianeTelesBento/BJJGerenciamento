<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="BJJGerenciamento.UI.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"> Cadastro </h2>

        <label for="matricula">Matricula</label>
        <asp:TextBox ID="matricula" runat="server" enable="false"/>
        <br />
            
        <label for="nome">Nome:</label>
        <asp:TextBox ID="txtNome" runat="server" />
        <br />

        <label for="sobrenome">Sobrenome:</label>
        <asp:TextBox ID="sobrenome" runat="server" />
        <br />

        <label for="telefone">Telefone:</label>
        <asp:TextBox ID="telefone" runat="server" />
        <br />

        <label for="email">Email:</label>
        <asp:TextBox ID="email" runat="server" />
        <br />

        <label for="rg">Rg:</label>
        <asp:TextBox ID="rg" runat="server" />
        <br />

        <label for="cpf">Cpf:</label>
        <asp:TextBox ID="cpf" runat="server" />
        <br />

        <label for="dataNascimento">Data de Nascimento:</label>
        <asp:TextBox ID="dataNascimento" runat="server" />
        <br />

        <label for="cep">Cep:</label>
        <asp:TextBox ID="cep" runat="server" />
        <label for="naoCep">Não sei o Cep:</label>
        <asp:Checkbox ID="naoCep" runat="server" />
        <br />

        <label for="endereco">Endereço:</label>
        <asp:TextBox ID="endereco" runat="server" />
        <br />

        <label for="bairro">Bairro:</label>
        <asp:TextBox ID="bairro" runat="server" />
        <br />

        <label for="numero">Numero:</label>
        <asp:TextBox ID="numero" runat="server" />
        <br />

        <asp:Button ID="btnEnviar" Text="Enviar" OnClick="btnEnviar_Click" runat="server" />
    </main>
</asp:Content>
