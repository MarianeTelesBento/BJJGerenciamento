<%@ Page Title="Página inicial" Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="BJJGerenciamento.UI.Home" MasterPageFile="~/Site.Master"%>
 

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .header {
            background-color: black;
            color: white;
            padding: 30px;
            text-align: center;
            border-radius: 10px;
            margin-bottom: 30px;
        }

        .cards {
            display: flex;
            gap: 20px;
            flex-wrap: wrap;
        }

        .card {
            background-color: #f0f0f0;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
            flex: 1;
            min-width: 200px;
        }

        .card h3 {
            margin-top: 0;
        }

        .list {
            margin: 0;
            padding-left: 20px;
        }
    </style>

    <div class="header">
        <h1>BJJ HOUSE</h1>
        <p>Bem-vindo ao sistema da sua escola de Jiu-Jitsu</p>
    </div>

    <div class="cards">
        <div class="card">
            <h3>Alunos Ativos</h3>
            <asp:Label ID="lblAtivos" runat="server" Font-Size="X-Large" />
        </div>

        <div class="card">
            <h3>Alunos Inativos</h3>
            <asp:Label ID="lblInativos" runat="server" Font-Size="X-Large" />
        </div>

        <div class="card">
            <h3>Aniversariantes do Mês</h3>
            <asp:Repeater ID="rptAniversariantes" runat="server">
                <ItemTemplate>
                    <li><%# Eval("Nome") %> <%# Eval("Sobrenome") %> - <%# Eval("DataNascimento") %></li>
                </ItemTemplate>
                <HeaderTemplate><ul class="list"></HeaderTemplate>
                <FooterTemplate></ul></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
