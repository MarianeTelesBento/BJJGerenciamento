<%@ Page Title="Lista de Alunos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaAlunos.aspx.cs" Inherits="BJJGerenciamento.UI._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>

        <h1 id="aspnetTitle">Lista de Alunos</h1>

        <%--<asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="Black" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellSpacing="2">
                        <FooterStyle BackColor="#CCCCCC" />
                        <HeaderStyle BackColor="Pink" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                        <RowStyle BackColor="White" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#808080" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#383838" />  
        </asp:GridView>--%>

       <%-- <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="IdAlunos" HeaderText="ID" />
                <asp:BoundField DataField="Nome" HeaderText="Nome" />
                <asp:BoundField DataField="Cpf" HeaderText="CPF" />
            </Columns>
 
            

            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Pink" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
            <RowStyle BackColor="White" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>--%>
        
        <asp:GridView CssClass="table table-striped table-bordered table-hover" ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="Email,Rg,DataNascimento,Cep,Rua,Bairro,Cidade,Estado,NumeroCasa,Complemento,CarteiraFPJJ"
            OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="IdAlunos" HeaderText="ID" ReadOnly/>
                <asp:BoundField DataField="Nome" HeaderText="Nome" />
                <asp:BoundField DataField="Sobrenome" HeaderText="Sobrenome" />
                <asp:BoundField DataField="Cpf" HeaderText="CPF" />
                <asp:BoundField DataField="Telefone" HeaderText="Telefone"/>
<%--                <asp:BoundField DataField="EstadoMatricula" HeaderText="Estado Da Matricula" />--%>

                <asp:TemplateField HeaderText="Ação">
                    <ItemTemplate>
                        <asp:Button ID="btnDetalhes" runat="server" Text="Mais" 
                            CommandName="Detalhes" 
                            OnClick="btnDetalhes_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <!-- Modal (Mini Tela) -->
        <div id="modalDetalhes" style="display: none; position: fixed; top: 10%; left: 40%; 
            background: white; padding: 20px; border: 1px solid black; z-index: 99; ">
            <h3>Detalhes do Aluno</h3>

            <asp:Label>ID:</asp:Label>
            <asp:TextBox ID="modalIdAluno" runat="server" Text="modalNome" ReadOnly></asp:TextBox>

            <asp:Label>Nome:</asp:Label>
            <asp:TextBox ID="modalNome" runat="server" Text="modalNome"></asp:TextBox>

            <asp:Label>Sobrenome:</asp:Label>
            <asp:TextBox ID="modalSobrenome" runat="server" Text="modalSobrenome"></asp:TextBox>

            <asp:Label>CPF:</asp:Label>
            <asp:TextBox ID="modalCpf" runat="server" Text="modalCpf"></asp:TextBox>
            
            <asp:Label>Telefone:</asp:Label>
            <asp:TextBox ID="modalTelefone" runat="server"></asp:TextBox>

            <asp:Label>Email:</asp:Label>
            <asp:TextBox ID="modalEmail" runat="server" Text="modalEmail"></asp:TextBox>

            <asp:Label>Estado da Matrícula:</asp:Label>
            <asp:TextBox ID="modalEstadoMatricula" runat="server"></asp:TextBox>

            <asp:Label>RG:</asp:Label>
            <asp:TextBox ID="modalRg" runat="server"></asp:TextBox>

            <asp:Label>Data de Nascimento:</asp:Label>
            <asp:TextBox ID="modalDataNascimento" runat="server"></asp:TextBox>

            <asp:Label>CEP:</asp:Label>
            <asp:TextBox ID="modalCep" runat="server"></asp:TextBox>

            <asp:Label>Rua:</asp:Label>
            <asp:TextBox ID="modalRua" runat="server"></asp:TextBox>

            <asp:Label>Bairro:</asp:Label>
            <asp:TextBox ID="modalBairro" runat="server"></asp:TextBox>

            <asp:Label>Cidade:</asp:Label>
            <asp:TextBox ID="modalCidade" runat="server"></asp:TextBox>

            <asp:Label>Estado:</asp:Label>
            <asp:TextBox ID="modalEstado" runat="server"></asp:TextBox>

            <asp:Label>Número:</asp:Label>
            <asp:TextBox ID="modalNumero" runat="server"></asp:TextBox>

            <asp:Label>Complemento:</asp:Label>
            <asp:TextBox ID="modalComplemento" runat="server"></asp:TextBox>

            <asp:Label>CarteiraFpjj:</asp:Label>
            <asp:TextBox ID="modalCarteiraFpjj" runat="server"></asp:TextBox>

            <div class="modal-footer">
                <asp:Button ID="SalvarAluno" OnClick="SalvarAluno_Click" runat="server" CssClass="asp-button btn btn-primary" Text="Salvar" />
                <button type="button" class="btn btn-secondary" onclick="fecharModal()">Fechar</button>
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
                width: auto;
                padding: 8px 16px;
                font-size: 16px;
                font-weight: 400;
                line-height: 1.5;
                color: #fff;
                background-color: #0d6efd;
                border: 1px solid #0d6efd;
                border-radius: 5px;
                cursor: pointer;
                text-align: center;
                text-decoration: none;
                transition: background-color 0.2s ease-in-out;
                height: 38px;
            }

            .asp-button:hover {
                background-color: #0b5ed7;
                border-color: #0a58ca;
            }

        </style>
    </main>

</asp:Content>
