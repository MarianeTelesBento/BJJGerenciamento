<%@ Page Title="Chamada" Language="C#" AutoEventWireup="true" CodeBehind="Chamada.aspx.cs" Inherits="BJJGerenciamento.UI.Chamada" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .btn-custom {
        padding: 0.5rem 1rem;
        font-size: 15px;
       /* background-color: #3366ff;*/
        color: #fff;
        border: none;
        border-radius: 4px;
        height: 38px;
        line-height: 1.5;
        display: flex;
        align-items: center;
        justify-content: center;
        gap: 0.5rem;
        cursor: pointer;
        min-width: 120px;
    }

    .btn-danger-custom {
        background-color: #ff0000;
        color: #fff;
        padding: 0.5rem 1rem;
        font-size: 15px;
        border: none;
        border-radius: 4px;
        height: 38px;
        line-height: 1.5;
        display: flex;
        align-items: center;
        justify-content: center;
        gap: 0.5rem;
        cursor: pointer;
        min-width: 120px;
    }

    .form-select-custom,
    .form-control {
        height: 38px;
        font-size: 15px;
        padding: 0.45rem 1rem;
        color: #000 !important;
        background-color: #fff !important;
        border: 1px solid #ccc;
        border-radius: 4px;
        min-width: 180px;
    }

    .flex-container {
        display: flex;
        flex-wrap: wrap;
        align-items: center;
        gap: 0.5rem;
        margin-bottom: 1rem;
    }

    #aspnetTitle {
        font-size: 2rem;
        font-weight: bold;
        color: #3366ff;
        margin-bottom: 1rem;
    }

    main {
        padding: 20px;
    }

    .icon-btn {
        width: 38px;
        height: 38px;
        padding: 0;
        display: flex;
        align-items: center;
        justify-content: center;
        background-color: transparent;
        border: none;
    }

    .icon-btn img {
        max-width: 70%;
        max-height: 70%;
    }
</style>

<contentTemplate>
    <main>
        <h1>Chamada</h1>

        <!-- Filtros -->
        <div class="flex-container">
            <asp:ImageButton ID="btnFiltro" runat="server" ImageUrl="~/Images/filtro.png" OnClick="btnFiltro_Click" AlternateText="Filtrar" CssClass="btn btn-light icon-btn" />

            <asp:DropDownList ID="ddPlanos" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddPlanos_SelectedIndexChanged" Visible="false" CssClass="form-select-custom" />
            <asp:DropDownList ID="ddHorarios" runat="server" AutoPostBack="false" CssClass="form-select-custom" OnSelectedIndexChanged="ddHorarios_SelectedIndexChanged" />


<%--            <asp:DropDownList ID="ddHorarios" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddHorarios_SelectedIndexChanged" Visible="false" CssClass="form-select-custom" />--%>

            <asp:TextBox ID="TxtTermoPesquisa" runat="server" Visible="false" CssClass="form-control" placeholder="Pesquisar..." />

            <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" OnClick="btnPesquisar_Click" Visible="false" CssClass="btn btn-primary btn-custom" style="background-color:blue" />

            <asp:Button ID="btnLimpar" runat="server" Text="Limpar filtros" OnClick="btnLimpar_Click" Visible="false" CssClass="btn btn-danger btn-custom" />
        </div>

        <!-- Chamada -->
        <div class="flex-container">
            <asp:Button ID="btnChamada" runat="server" Text="Nova Chamada" OnClick="btnChamada_Click" CssClass="btn  btn-custom" style="background-color:green"/>

            <asp:DropDownList ID="ddSalas" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddPlanos_SelectedIndexChanged" Visible="false" CssClass="form-select-custom" />
            <asp:DropDownList ID="ddProfessores" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddPlanos_SelectedIndexChanged" Visible="false" CssClass="form-select-custom" />

            <asp:ImageButton ID="btnSalvarChamada" runat="server" ImageUrl="~/Images/save.png" OnClick="btnSalvarChamada_Click" AlternateText="Salvar" Visible="false" CssClass="btn btn-light icon-btn" />
        </div>

        <!-- Tabela -->
        <asp:GridView CssClass="table table-striped table-bordered table-hover" ID="GridView1" runat="server" AutoGenerateColumns="False"
            DataKeyNames="Email,DataNascimento, DataMatricula,IdAlunos"
            OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="IdMatricula" HeaderText="Matrícula" />
                <asp:BoundField DataField="Nome" HeaderText="Nome" HtmlEncode="false" />
                <asp:BoundField DataField="Sobrenome" HeaderText="Sobrenome" HtmlEncode="false" />
                <asp:BoundField DataField="Cpf" HeaderText="CPF" />
                <asp:BoundField DataField="Telefone" HeaderText="Telefone" />

                <asp:TemplateField HeaderText="Status da Matrícula">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkStatusMatricula" runat="server" Enabled="false"
                            Checked='<%# Convert.ToBoolean(Eval("StatusMatricula")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Presente" HeaderStyle-CssClass="center" ItemStyle-CssClass="center" Visible="false">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkPresente" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </main>
</contentTemplate>
</asp:Content>
