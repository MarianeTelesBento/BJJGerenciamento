<%@ Page Title="Chamada" Language="C#" AutoEventWireup="true" CodeBehind="Chamada.aspx.cs" Inherits="BJJGerenciamento.UI.Chamada" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <contentTemplate>
            <main>
                <h1 id="aspnetTitle">Chamada</h1>    

                <asp:TextBox ID="TxtTermoPesquisa" runat="server"></asp:TextBox>
                <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" OnClick="btnPesquisar_Click" />
                <asp:ImageButton ID="btnFiltro" runat="server" ImageUrl="Images/filtro.png" OnClick="btnFiltro_Click" AlternateText="Filtrar" />
                <asp:DropDownList ID="ddPlanos" runat="server"
                    AutoPostBack="true" OnSelectedIndexChanged="ddPlanos_SelectedIndexChanged"  Visible="false"/> 

                <asp:Button ID="btnChamada" runat="server" Text="Chamada" OnClick="btnChamada_Click" />

                <asp:DropDownList ID="ddSalas" runat="server"
                    AutoPostBack="true" OnSelectedIndexChanged="ddPlanos_SelectedIndexChanged"  Visible="false"/> 
                <asp:DropDownList ID="ddProfessores" runat="server"
                    AutoPostBack="true" OnSelectedIndexChanged="ddPlanos_SelectedIndexChanged"  Visible="false"/> 
                <asp:ImageButton ID="btnSalvarChamada" runat="server" ImageUrl="Images/save.png" OnClick="btnFiltro_Click" AlternateText="Filtrar" Visible="false"/>

                <asp:GridView CssClass="table table-striped table-bordered table-hover" ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="Email,DataNascimento, DataMatricula,IdAlunos"
                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                    <Columns>

                        <asp:BoundField DataField="IdMatricula" HeaderText="Matrícula" />
                        <asp:BoundField DataField="Nome" HeaderText="Nome" HtmlEncode="false"/>
                        <asp:BoundField DataField="Sobrenome" HeaderText="Sobrenome" HtmlEncode="false"/>
                        <asp:BoundField DataField="Cpf" HeaderText="CPF" />
                        <asp:BoundField DataField="Telefone" HeaderText="Telefone"/>
                                
                        <asp:TemplateField HeaderText="Status da Matrícula">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkStatusMatricula" runat="server" Enabled="false"
                                    Checked='<%# Convert.ToBoolean(Eval("StatusMatricula")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Presente?" >
                            <ItemTemplate>
                                <asp:CheckBox ID="chkPresente" runat="server"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </main>
        </contentTemplate>
</asp:Content>


