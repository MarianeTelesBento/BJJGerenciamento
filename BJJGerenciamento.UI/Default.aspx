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
        
        <asp:GridView CssClass="table table-striped table-bordered table-hover" ID="GridView1" runat="server" AutoGenerateColumns="False" 
            OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="IdAlunos" HeaderText="ID" />
                <asp:BoundField DataField="NomeCompleto" HeaderText="Nome" />
                <asp:BoundField DataField="Cpf" HeaderText="CPF" />
                <asp:BoundField DataField="Telefone" HeaderText="Telefone"/>
<%--                <asp:BoundField DataField="EstadoMatricula" HeaderText="Estado Da Matricula" />--%>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hfIdTurma" runat="server" Value='<%# Eval("IdPlano") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hfEmail" runat="server" Value='<%# Eval("Email") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hfRg" runat="server" Value='<%# Eval("Rg") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hfDataNascimento" runat="server" Value='<%# Eval("DataNascimento") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hfCep" runat="server" Value='<%# Eval("Cep") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hfRua" runat="server" Value='<%# Eval("Rua") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hfBairro" runat="server" Value='<%# Eval("Bairro") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hfCidade" runat="server" Value='<%# Eval("Cidade") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hfEstado" runat="server" Value='<%# Eval("Estado") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField >
                    <ItemTemplate>
                        <asp:HiddenField ID="hfNumero" runat="server" Value='<%# Eval("NumeroCasa") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

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
            background: white; padding: 20px; border: 1px solid black;">
            <h3>Detalhes do Aluno</h3>

            <asp:Label>Nome:</asp:Label>
            <asp:TextBox ID="modalNome" runat="server" Text="modalNome"></asp:TextBox>

            <asp:Label>CPF:</asp:Label>
            <asp:TextBox ID="modalCpf" runat="server" Text="modalCpf"></asp:TextBox>

            <asp:Label>Email:</asp:Label>
            <asp:TextBox ID="modalEmail" runat="server" Text="modalEmail"></asp:TextBox>

            <asp:Label>Turma:</asp:Label>
            <asp:TextBox ID="modalTurma" runat="server" Text="modalTurma"></asp:TextBox>

            <asp:Label>Telefone:</asp:Label>
            <asp:TextBox ID="modalTelefone" runat="server"></asp:TextBox>

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

            <button onclick="fecharModal()">Fechar</button>
        </div>

        <script>
            function abrirModal() {
                //nome, cpf, email, idTurma, telefone, estadoMatricula, rg, dataNascimento,
                //    cep, rua, bairro, cidade, estado, numero

                //document.getElementById("modalNome").innerText = nome;
                //document.getElementById("modalCpf").innerText = cpf;
                //document.getElementById("modalEmail").innerText = email;
                //document.getElementById("modalTurma").innerText = idTurma;
                //document.getElementById("modalTelefone").innerText = telefone;
                //document.getElementById("modalEstadoMatricula").innerText = estadoMatricula;
                //document.getElementById("modalRg").innerText = rg;
                //document.getElementById("modalDataNascimento").innerText = dataNascimento;
                //document.getElementById("modalCep").innerText = cep;
                //document.getElementById("modalRua").innerText = rua;
                //document.getElementById("modalBairro").innerText = bairro;
                //document.getElementById("modalCidade").innerText = cidade;
                //document.getElementById("modalEstado").innerText = estado;
                //document.getElementById("modalNumero").innerText = numero;
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
