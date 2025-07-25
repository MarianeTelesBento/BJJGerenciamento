<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastrarAdesao.aspx.cs" Inherits="BJJGerenciamento.UI.CadastrarAdesao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<style>
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

    .form-control-small {
        width: 100px;
        display: inline-block;
    }
    .checkbox-list input[type="checkbox"] {
    display: none;
}

.checkbox-list label {
    display: inline-block;
    padding: 8px 14px;
    margin: 4px;
    border: 2px solid #198754;
    border-radius: 8px;
    background-color: white;
    color: #198754;
    cursor: pointer;
    transition: 0.3s;
    user-select: none;
}

.checkbox-list input[type="checkbox"]:checked + label {
    background-color: #198754;
    color: white;
}

</style>

<div class="container mt-4">
    <h2 class="mb-4">Cadastro de Adesão</h2>

    <div class="mb-4">
        <label class="form-label fw-bold">Nome da Adesão</label>
        <asp:TextBox ID="txtNomeAdesao" runat="server" CssClass="form-control" />
    </div>
<h4>Frequência (dias por semana) e Valor</h4>
<div>
 <asp:CheckBoxList ID="chkFrequencias" runat="server" RepeatDirection="Vertical" />
<% for (int i = 1; i <= 5; i++) { %>
    Valor para <%= i %>x: <input type="text" name="valor_<%= i %>" style="width: 100px;" /><br />
<% } %>

  
</div>



<!-- Turmas permitidas -->
<label class="form-label fw-bold mt-3">Turmas permitidas</label>
<div class="card-check-group">
    <asp:CheckBoxList ID="chkListTurmas" runat="server" CssClass="checkbox-btn" RepeatLayout="Flow" RepeatDirection="Horizontal">
       
    </asp:CheckBoxList>
</div>
  

    <asp:Button ID="BtnSalvar" runat="server" Text="Salvar" CssClass="btn btn-success mt-4 mb-3" OnClick="btnSalvar_Click" />
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
                    OnClientClick='<%# "abrirModalConfirmar(" + Eval("IdAdesao") + "); return false;" %>' />
   <asp:Button ID="btnEditar" runat="server"
    Text="Editar"
    CommandName="Editar"
    CommandArgument='<%# Container.DataItemIndex %>'
    CssClass="btn btn-warning btn-sm" />

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
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
        <asp:Button ID="btnConfirmarExclusao" runat="server" CssClass="btn btn-danger" Text="Excluir" OnClick="btnConfirmarExclusao_Click" />
      </div>
    </div>
  </div>
</div>
    <!-- Modal de edição -->
<div class="modal fade" id="modalEditarAdesao" tabindex="-1" role="dialog">
  <div class="modal-dialog" role="document">
    <div class="modal-content p-3">
      <div class="modal-header">
        <h5 class="modal-title">Editar Adesão</h5>
        <button type="button" class="close" data-dismiss="modal">&times;</button>
      </div>

      <div class="modal-body">
        <asp:HiddenField ID="hdnIdAdesaoEditar" runat="server" />
        
        <div class="form-group">
          <label>Nome da Adesão</label>
          <asp:TextBox ID="txtNomeAdesaoEditar" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
          <label>Frequências e Mensalidades:</label>
          <div class="row">
            <% for (int i = 1; i <= 6; i++) { %>
              <div class="col-md-6 mb-2">
                <label><%= i %>x por semana</label>
                <input type="text" class="form-control" id="valor_edit_<%= i %>" name="valor_edit_<%= i %>" placeholder="R$">
                <input type="hidden" id="id_freq_edit_<%= i %>" name="id_freq_edit_<%= i %>">
              </div>
            <% } %>
          </div>
        </div>

        <div class="form-group">
          <label>Turmas Permitidas</label>
          <asp:CheckBoxList ID="chkListTurmasEditar" runat="server" RepeatDirection="Vertical" CssClass="form-check" />
        </div>
      </div>

      <div class="modal-footer">
        <asp:Button ID="btnSalvarEdicao" runat="server" Text="Salvar" CssClass="btn btn-success" OnClick="btnSalvarEdicao_Click" />
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
      </div>
    </div>
  </div>
</div>

    <script type="text/javascript">
        function abrirModalConfirmar(id)
        {
            document.getElementById('<%= hdnIdAdesaoExcluir.ClientID %>').value = id;
            var myModal = new bootstrap.Modal(document.getElementById('modalConfirmarExclusao'));
            myModal.show();
        }
        function abrirModalEditar(id)
        {
            document.getElementById('<%= hdnIdAdesaoEditar.ClientID %>').value = id;
           // Chama o backend para carregar dados da adesão
            var myModal = new bootstrap.Modal(document.getElementById('modalEditarAdesao'));
            myModal.show();
        }


    </script>


</asp:Content>