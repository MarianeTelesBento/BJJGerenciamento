<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Financeiro.aspx.cs" Inherits="BJJGerenciamento.UI.Financeiro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       <title>Financeiro</title>

    <style>
        .grid-style {
            margin: 30px auto;
            width: 90%;
            border: 1px solid #ccc;
            border-radius: 10px;
            box-shadow: 0 2px 6px rgba(0,0,0,0.2);
            background-color: #fff;
        }
        .grid-style th, .grid-style td {
            padding: 12px;
            text-align: center;
        }
        .custom-width {
            width: 200px !important;
            height: 35px !important;
        }
        .margin-bottom-15 {
            margin-bottom: 15px;
        }

    </style>

    <h1>Financeiro</h1>

<asp:DropDownList ID="ddlFiltro" runat="server" AutoPostBack="true" 
    OnSelectedIndexChanged="ddlFiltro_SelectedIndexChanged" 
    CssClass="form-select custom-width margin-bottom-15" >
    <asp:ListItem Value="Todos" Text="Todos" />
    <asp:ListItem Value="Vencidos" Text="Vencidos" />
    <asp:ListItem Value="Proximos" Text="Próximos" />
</asp:DropDownList>


<asp:GridView ID="gvFinanceiro"
              runat="server"
              AutoGenerateColumns="False"
              CssClass="table table-bordered table-striped table-hover"
              OnRowCommand="gvFinanceiro_RowCommand"
              OnSelectedIndexChanged="gvFinanceiro_SelectedIndexChanged">

    <Columns>
        <asp:BoundField DataField="NomeCompleto" HeaderText="Aluno" />
      <asp:BoundField DataField="DataVencimento" HeaderText="Vencimento" />
        <asp:BoundField DataField="Mensalidade" HeaderText="Mensalidade" DataFormatString="{0:C}" />
        <asp:BoundField DataField="Status" HeaderText="Status" />

        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="btnDetalhesPlano"
                            runat="server"
                            Text="Detalhes"
                            CssClass="btn btn-primary"
                            Style="height:35px"
                            CommandName="ExibirDetalhes"
                            CommandArgument='<%# Eval("idPlanoAluno") %>' OnClick="btnDetalhesPlano_Click" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>



<!-- Modal Bootstrap -->
   <div id="modalDetalhes" class="modal" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">Detalhes do Plano</h5>
                 <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="fecharModal()">
         <span aria-hidden="true">&times;</span>
     </button>
            </div>
            <div class="modal-body">
                <asp:Label ID="lblNomeAluno" runat="server" /><br />
                <asp:Label ID="lblPlano" runat="server" /><br />
                <asp:Label ID="lblVencimento" runat="server" /><br />
                <asp:Label ID="lblValor" runat="server" /><br />
                <asp:HiddenField ID="hiddenIdPlanoAluno" runat="server" />
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnPagamentoEfetuado" runat="server" Text="Pagamento Efetuado" CssClass="btn btn-success"  Style="height:35px" OnClick="btnPagamentoEfetuado_Click" />
            </div>
        </div>
    </div>
</div>
    <script>
           function abrirModal() {
               document.getElementById("modalDetalhes").style.display = "block";
           }

           function fecharModal() {
               document.getElementById("modalDetalhes").style.display = "none";
           }
    </script>


</asp:Content>
