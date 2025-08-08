<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastrarAdesao.aspx.cs" Inherits="BJJGerenciamento.UI.CadastrarAdesao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<style>
    /* ... seus estilos CSS ... */
    .frequencia-card {
         border: 1px solid #ddd;
         border-radius: 12px;
         padding: 16px;
         margin-bottom: 12px;
         box-shadow: 0 2px 6px rgba(0,0,0,0.1);
         background: #fff;
     }

     .checkbox-btn {
         position: relative;
         display: inline-block;
     }

     .checkbox-btn input[type="checkbox"] {
         position: absolute;
         opacity: 0;
         pointer-events: none;
     }

     .checkbox-btn label {
         display: inline-block;
         padding: 8px 14px;
         border: 2px solid #198754;
         border-radius: 8px;
         background-color: white;
         color: #198754;
         cursor: pointer;
         transition: 0.3s;
         margin: 4px;
         user-select: none;
     }

     .checkbox-btn input[type="checkbox"]:checked + label {
         background-color: #198754;
         color: white;
     }

     .card-check-group {
         display: flex;
         flex-wrap: wrap;
         gap: 8px;
     }

     .input-valor-cadastro {
         width: 120px;
         display: inline-block;
     }
    
    .btn-custom,
    .btn-custom:hover {
        border-radius: 10px !important;
        height: 35px;
        line-height: 20px;
        padding-top: 5px;
    }
    
    .checkbox-dia-semana label {
        display: inline-block;
        padding: 8px 16px;
        border: 1px solid #ced4da;
        border-radius: 6px;
        margin: 4px;
        cursor: pointer;
        background-color: #f8f9fa;
        color: #495057;
        transition: all 0.3s ease;
        user-select: none;
    }

    .checkbox-dia-semana input[type="checkbox"]:checked + label {
        background-color: #0d6efd;
        border-color: #0d6efd;
        color: white;
    }

    .checkbox-turma label {
        display: inline-block;
        padding: 8px 16px;
        border: 1px solid #ced4da;
        border-radius: 6px;
        margin: 4px;
        cursor: pointer;
        background-color: #f8f9fa;
        color: #495057;
        transition: all 0.3s ease;
        user-select: none;
    }

    .checkbox-turma input[type="checkbox"]:checked + label {
        background-color: #ffc107;
        border-color: #ffc107;
        color: black;
    }
    /* Este estilo garante que o alinhamento de texto seja forçado para a esquerda
   apenas dentro do seu modal de edição, sobrescrevendo qualquer estilo
   que esteja sendo herdado. */
#modalEditarAdesao .modal-body label {
    text-align: left !important;
    display: block; /* Garante que o label ocupe toda a largura */
}

/* Opcional: Para alinhar o input também, embora o Bootstrap já faça isso */
#modalEditarAdesao .modal-body .form-control {
    text-align: left !important;
}
</style>

<div class="container mt-4">
    <h2 class="mb-4">Cadastro de Adesão</h2>

    <div class="mb-4">
        <label class="form-label fw-bold">Nome da Adesão</label>
        <asp:TextBox ID="txtNomeAdesao" runat="server" CssClass="form-control" />
    </div>

<h4>Dias da Semana:</h4>
<div class="d-flex gap-3 flex-wrap">
    <asp:CheckBoxList ID="chkFrequencias" runat="server" CssClass="checkbox-dia-semana" RepeatLayout="Flow" RepeatDirection="Horizontal" />
</div>

<h4 class="mt-4">Frequência (dias por semana) e Valor</h4>
<div>
<% for (int i = 1; i <= 7; i++) { %>
    Valor para <%= i %>x: <input type="text" name="valor_<%= i %>" id="valor_<%= i %>" class="form-control input-valor-cadastro" /><br />
<% } %>

</div>

<label class="form-label fw-bold mt-3">Turmas permitidas</label>
<div class="card-check-group d-flex gap-2 flex-wrap">
    <asp:CheckBoxList ID="chkListTurmas" runat="server" CssClass="checkbox-turma" RepeatLayout="Flow" RepeatDirection="Horizontal" />
</div>
  
    <asp:Button ID="BtnSalvar" runat="server" Text="Salvar" CssClass="btn btn-success mt-4 mb-3 btn-custom" OnClick="btnSalvar_Click" />
    <asp:Label ID="lblMensagem" runat="server" CssClass="text-success d-block mb-4" />

    <hr />

    <h4 class="mb-3">Adesões Cadastradas</h4>
    <asp:GridView ID="gridAdesoes" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered"
        DataKeyNames="IdAdesao" OnRowCommand="gridAdesoes_RowCommand">
        <Columns>
            <asp:BoundField DataField="NomeAdesao" HeaderText="Adesão" />
            <asp:BoundField DataField="FrequenciasTexto" HeaderText="Frequências e Valores" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnExcluir" runat="server" 
                        CommandName="Excluir" 
                        CommandArgument='<%# Eval("IdAdesao") %>' 
                        Text="Excluir" 
                        OnClientClick='<%# "abrirModalConfirmar(" + Eval("IdAdesao") + "); return false;" %>' 
                        CssClass="btn btn-danger btn-custom" />
                    <asp:Button ID="btnEditar" runat="server"
                        Text="Editar"
                        CommandName="Editar"
                        CommandArgument='<%# Eval("IdAdesao") %>'
                        CssClass="btn btn-warning btn-custom" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>

<div class="modal fade" id="modalConfirmarExclusao" tabindex="-1" aria-labelledby="modalLabel" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">
      <div class="modal-header bg-danger text-white">
        <h5 class="modal-title" id="modalLabel">Confirmar Exclusão</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Fechar"></button>
      </div>
      <div class="modal-body">
        Tem certeza que deseja excluir esta adesão?
      </div>
      <div class="modal-footer">
        <asp:HiddenField ID="hdnIdAdesaoExcluir" runat="server" />
        <button type="button" class="btn btn-secondary btn-custom" data-bs-dismiss="modal">Cancelar</button>
        <asp:Button ID="btnConfirmarExclusao" runat="server" CssClass="btn btn-danger btn-custom" Text="Excluir" OnClick="btnConfirmarExclusao_Click" />
      </div>
    </div>
  </div>
</div>

<div class="modal fade" id="modalEditarAdesao" tabindex="-1" role="dialog">
  <div class="modal-dialog modal-l" role="document">
    <div class="modal-content p-3">
      <div class="modal-header">
        <h5 class="modal-title">Editar Adesão</h5>
        <button type="button" class="close" data-dismiss="modal">&times;</button>
      </div>

      <div class="modal-body">
        <asp:HiddenField ID="hdnIdAdesaoEditar" runat="server" />
        
        <div class="form-group text-left">
          <label>Nome da Adesão</label>
          <asp:TextBox ID="txtNomeAdesaoEditar" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
          <label>Frequências e Mensalidades:</label>
          
                        <% for (int i = 1; i <= 7; i++) { %>
              <div class="mb-2">
                <label><%= i %>x por semana</label>
                <input type="text" class="form-control" id="valor_edit_<%= i %>" name="valor_edit_<%= i %>" placeholder="R$">
                <input type="hidden" id="id_freq_edit_<%= i %>" name="id_freq_edit_<%= i %>">
              </div>
            <% } %>
        </div>

        <div class="form-group">
          <label>Turmas Permitidas</label>
          <div class="d-flex gap-2 flex-wrap">
            <asp:CheckBoxList ID="chkListTurmasEditar" runat="server" CssClass="checkbox-turma" RepeatLayout="Flow" RepeatDirection="Horizontal" />
          </div>
        </div>
      </div>

      <div class="modal-footer">
        <asp:Button ID="btnSalvarEdicao" runat="server" Text="Salvar" CssClass="btn btn-success btn-custom" OnClick="btnSalvarEdicao_Click" />
        <button type="button" class="btn btn-secondary btn-custom" data-dismiss="modal">Cancelar</button>
      </div>
    </div>
  </div>
</div>
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.16/jquery.mask.min.js"></script>

<script type="text/javascript">
    // Funções JavaScript
    function abrirModalConfirmar(id) {
        document.getElementById('<%= hdnIdAdesaoExcluir.ClientID %>').value = id;
        var myModal = new bootstrap.Modal(document.getElementById('modalConfirmarExclusao'));
        myModal.show();
    }

    function abrirModalEditar(idAdesao) {
        document.getElementById('<%= hdnIdAdesaoEditar.ClientID %>').value = idAdesao;
        __doPostBack('buscarAdesao', idAdesao);
    }

    function aplicarMascaras() {
        // Máscara para o modal de cadastro
        for (let i = 1; i <= 7; i++) {
            $('input[name="valor_' + i + '"]').mask('000.000.000,00', {reverse: true});
        }
        // Máscara para o modal de edição
        for (let i = 1; i <= 7; i++) {
            $('#valor_edit_' + i).mask('000.000.000,00', {reverse: true});
        }
    }

    function limparMascarasAntesDeSalvar() {
        for (let i = 1; i <= 7; i++) {
            let campo = document.getElementById('valor_edit_' + i);
            if (campo && campo.value) {
                let valorLimpo = campo.value.replace(/\./g, '').replace(',', '.');
                campo.value = valorLimpo;
            }
        }
    }
    
    document.getElementById('<%= btnSalvarEdicao.ClientID %>').onclick = function () {
        limparMascarasAntesDeSalvar();
    };

    $(document).ready(function () {
        aplicarMascaras();
    });

    $('#modalEditarAdesao').on('shown.bs.modal', function () {
        aplicarMascaras();
    });
</script>

</asp:Content>