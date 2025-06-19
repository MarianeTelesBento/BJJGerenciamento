<%@ Page Title="Página inicial" Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="BJJGerenciamento.UI.Home" MasterPageFile="~/Site.Master"%>
 

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .header {
            background: rgba(0, 0, 0, 0.6);
            backdrop-filter: blur(10px);
            padding: 20px;
            border-radius: 12px;
            text-align: center;
            margin: 20px auto;
            width: 80%;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.2);
            color: white;
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
        body {
         background-image: url('Images/ImagemDesfocada.png');
         background-size: cover;
         background-repeat: no-repeat;
         background-position: center;
         background-attachment: fixed;
        }
        .card-mensalidade {
            background-color: #f0f0f0;
            padding: 20px;
            border-radius: 10px;
            margin-top: 30px; /* espaçamento entre os blocos */
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
            cursor: pointer;
        }
        .card-mensalidade p {
            margin-bottom: 5px;
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
  <!-- CARD GERAL: Mensalidades Próximas a Vencer -->
<div class="card-mensalidade">
    <h3>Mensalidades Próximas a Vencer</h3>
    <asp:Repeater ID="rptMensalidadesProximas" runat="server">
        <ItemTemplate>
            <div onclick="location.href='Financeiro.aspx?idAluno=<%# Eval("idAlunos") %>'">
                <p><strong><%# Eval("Nome") %> <%# Eval("Sobrenome") %></strong></p>
                <p>Vencimento: dia <%# Eval("DiaVencimento") %> – Mensalidade: <%# Eval("Mensalidade", "{0:C}") %></p>
                <hr />
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>

<div class="card-mensalidade">
    <h3>Mensalidades Vencidas</h3>
    <asp:Repeater ID="rptMensalidadesVencidas" runat="server">
        <ItemTemplate>
            <div onclick="location.href='Financeiro.aspx?idAluno=<%# Eval("idAlunos") %>'">
                <p><strong><%# Eval("Nome") %> <%# Eval("Sobrenome") %></strong></p>
                <p>Vencimento: dia <%# Eval("DiaVencimento") %> – Mensalidade: <%# Eval("Mensalidade", "{0:C}") %></p>
                <hr />
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>




</asp:Content>
