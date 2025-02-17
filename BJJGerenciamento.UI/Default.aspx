<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BJJGerenciamento.UI._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>

        <h1 id="aspnetTitle">Chamada</h1>

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
        
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="IdAlunos" HeaderText="ID" />
                <asp:BoundField DataField="NomeCompleto" HeaderText="Nome" />
                <asp:BoundField DataField="Cpf" HeaderText="CPF" />
                <asp:BoundField DataField="EstadoMatricula" HeaderText="Estado Da Matricula" />

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
        <div id="modalDetalhes" style="display: none; position: fixed; top: 30%; left: 40%; 
            background: white; padding: 20px; border: 1px solid black;">
            <h3>Detalhes do Aluno</h3>
            <p><strong>Nome:</strong> <span id="modalNome"></span></p>
            <p><strong>CPF:</strong> <span id="modalCpf"></span></p>
            <p><strong>Email:</strong> <span id="modalEmail"></span></p>
            <button onclick="fecharModal()">Fechar</button>
        </div>

        <script>
            function abrirModal(nome, cpf, email) {
                document.getElementById("modalNome").innerText = nome;
                document.getElementById("modalCpf").innerText = cpf;
                document.getElementById("modalEmail").innerText = email;
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
        </style>
    </main>

</asp:Content>
