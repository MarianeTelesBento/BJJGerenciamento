<%@ Page Title="..." Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaProfessores.aspx.cs" Inherits="BJJGerenciamento.UI.ListaProfessores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<main>

    <h1 id="aspnetTitle">Lista de Professores</h1>

    <div class="grid-responsive-container">
            <asp:GridView CssClass="table table-striped table-bordered table-hover" ID="GridView1" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="IdProfessor,Email,DataNasc,CEP,Rua,Bairro,Cidade,Numero,Complemento,CarteiraFPJJ,CarteiraCBJJ,Estado"
                OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="IdProfessor" HeaderText="ID" />
                    <asp:BoundField DataField="Nome" HeaderText="Nome" HtmlEncode="false"/>
                    <asp:BoundField DataField="Sobrenome" HeaderText="Sobrenome" HtmlEncode="false"/>
                    <asp:BoundField DataField="Cpf" HeaderText="CPF" />
                    <asp:BoundField DataField="Telefone" HeaderText="Telefone"/>
                    <asp:TemplateField HeaderText="Status da Matrícula">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkAtivo" runat="server" Enabled="false"
                                Checked='<%# Convert.ToBoolean(Eval("Ativo")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ação">
                        <ItemTemplate>
                            <asp:Button ID="btnDetalhes" runat="server" Text="Mais" 
                                CommandName="Detalhes" 
                                OnClick="btnDetalhes_Click" CssClass="asp-button" Style="height: 35px" /> 
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
    </div>

    <!-- Modal Detalhes -->
    <div id="modalDetalhes" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Detalhes do Professor</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="fecharModal()">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">

                    <div class="form-group">
                        <asp:Label for="modalIdProfessor">ID:</asp:Label>
                        <asp:TextBox ID="modalIdProfessor" runat="server" ReadOnly CssClass="form-control"></asp:TextBox>
                    </div>
                     <div class="form-group d-flex align-items-center gap-2">
                        <asp:Label ID="lblmodalAtivo" runat="server" CssClass="mb-0">Status da matrícula:</asp:Label>
                        <asp:CheckBox ID="modalAtivo" runat="server" />
                    </div>
                    <div class="form-group">
                        <asp:Label for="modalNome">Nome:</asp:Label>
                        <asp:TextBox ID="modalNome" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label for="modalSobrenome">Sobrenome:</asp:Label>
                        <asp:TextBox ID="modalSobrenome" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label for="modalCpf">CPF:</asp:Label>
                        <asp:TextBox ID="modalCpf" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label for="modalDataNasc">Data de Nascimento:</asp:Label>
                        <asp:TextBox ID="modalDataNasc" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label for="modalEmail">Email:</asp:Label>
                        <asp:TextBox ID="modalEmail" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label for="modalTelefone">Telefone:</asp:Label>
                        <asp:TextBox ID="modalTelefone" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label for="modalCep">CEP:</asp:Label>
                        <asp:TextBox ID="modalCep" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label for="modalRua">Rua:</asp:Label>
                        <asp:TextBox ID="modalRua" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label for="modalBairro">Bairro:</asp:Label>
                        <asp:TextBox ID="modalBairro" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label for="modalCidade">Cidade:</asp:Label>
                        <asp:TextBox ID="modalCidade" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label for="modalEstado">Estado:</asp:Label>
                        <asp:TextBox ID="modalEstado" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label for="modalNumero">Número:</asp:Label>
                        <asp:TextBox ID="modalNumero" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label for="modalComplemento">Complemento:</asp:Label>
                        <asp:TextBox ID="modalComplemento" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label for="modalCarteiraFPJJ">Carteira FPJJ:</asp:Label>
                        <asp:TextBox ID="modalCarteiraFPJJ" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label for="modalCarteiraCBJJ">Carteira CBJJ:</asp:Label>
                        <asp:TextBox ID="modalCarteiraCBJJ" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                                      <div class="modal-footer text-center">
                                      <asp:Button ID="SalvarProfessor" OnClick="SalvarProfessor_Click" runat="server" CssClass="asp-button btn btn-primary" Text="Salvar" />
                                      </div>

  



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

    <style>
        #GridView1 {
            text-align: center;
        }
      .asp-button {
      display: inline-block;
      padding: 5px 12px;
      font-size: 14px;
      color: #fff;
      background-color: #0d6efd;
      border: 1px solid #0d6efd;
      border-radius: 5px;
      cursor: pointer;
      text-decoration: none;
      transition: background-color 0.2s ease-in-out;
      height:35px
  }

.asp-button:hover {
    background-color: #0056b3;
    transform: translateY(-2px);
}

.asp-button:active {
    background-color: #004494;
    transform: translateY(0);
}
.form-group {
    margin-bottom: 16px;
}




    </style>

</main>
</asp:Content>
