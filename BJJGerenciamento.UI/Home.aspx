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
<h4 class="mt-4">Mensalidades Vencidas</h4>
<asp:GridView ID="gvVencidas" runat="server"
    AutoGenerateColumns="False"
    CssClass="table table-bordered table-striped"
    DataKeyNames="idPlanoAluno">
  <Columns>
    <asp:BoundField DataField="NomeCompleto" HeaderText="Aluno" />
    
    <asp:TemplateField HeaderText="Vencimento">
  <ItemTemplate>
    <%# ((DateTime)Eval("DataReal")).ToString("dd/MM/yyyy") %>
  </ItemTemplate>
</asp:TemplateField>

    <asp:BoundField DataField="Mensalidade" HeaderText="Mensalidade" DataFormatString="{0:C}" />
    
    <asp:TemplateField HeaderText="Ação">
      <ItemTemplate>
        <a href='Financeiro.aspx?idPlanoAluno=<%# Eval("idPlanoAluno") %>' 
           class='btn btn-link btn-sm'>Visualizar</a>
      </ItemTemplate>
    </asp:TemplateField>
  </Columns>
  <EmptyDataTemplate>
    <div class="text-center text-muted py-3">Nenhuma mensalidade vencida</div>
  </EmptyDataTemplate>
</asp:GridView>


<h4 class="mt-5">Mensalidades Próximas a Vencer</h4>
<asp:GridView ID="gvProximas" runat="server"
    AutoGenerateColumns="False"
    CssClass="table table-bordered table-striped"
    DataKeyNames="idPlanoAluno">
  <Columns>
    <asp:BoundField DataField="NomeCompleto" HeaderText="Aluno" />
  <asp:TemplateField HeaderText="Vencimento">
  <ItemTemplate>
    <%# ((DateTime)Eval("DataReal")).ToString("dd/MM/yyyy") %>
  </ItemTemplate>
</asp:TemplateField>



    <asp:BoundField DataField="Mensalidade" HeaderText="Mensalidade" DataFormatString="{0:C}" />

    <asp:TemplateField HeaderText="Ação">
      <ItemTemplate>
        <a href='Financeiro.aspx?idPlanoAluno=<%# Eval("idPlanoAluno") %>' 
           class='btn btn-link btn-sm'>Visualizar</a>
      </ItemTemplate>
    </asp:TemplateField>
  </Columns>
  <EmptyDataTemplate>
    <div class="text-center text-muted py-3">Nenhuma mensalidade próxima</div>
  </EmptyDataTemplate>
</asp:GridView>






</asp:Content>
