<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastrarTurmas.aspx.cs" Inherits="BJJGerenciamento.UI.CadastrarTurmas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .btn-custom {
            background-color: #e53935;
            font-size: 15px;
            border: none;
            color: white;
            font-family: -apple-system, Roboto, Arial, sans-serif;
            border-radius: 6px;
            padding: 10px 20px;
            text-align: center;
            width: 100%;
            height: 35px;
        }

        .btn-pequeno {
            background-color: #e53935;
            width: 120px;
            height: 35px;
            line-height: 18px;
        }

        .form-container {
            background-color: white;
            padding: 30px;
            border-radius: 12px;
            box-shadow: 0 0 10px rgba(0,0,0,0.08);
            margin-top: 30px;
        }

        .form-label {
            font-weight: 600;
            color: #333;
            margin-bottom: 8px;
            display: block;
        }

        .form-check {
            display: flex;
            flex-wrap: wrap;
            gap: 12px;
            padding-left: 0;
        }

        .form-check input[type="checkbox"] {
            margin-right: 6px;
        }

        .form-check label {
            display: flex;
            align-items: center;
            gap: 4px;
            background-color: #f8f9fa;
            border: 1px solid #ced4da;
            border-radius: 4px;
            padding: 6px 10px;
            cursor: pointer;
            font-size: 14px;
        }

        .form-check label:hover {
            background-color: #e2e6ea;
        }

        .input-text {
            border-radius: 4px;
            border: 1px solid #ccc;
            padding: 15px;
            width: 100%;
            font-size: 14px;
        }

        .mb-4 {
            margin-bottom: 1.5rem !important;
        }

        .separador {
            height: 2px;
            background-color: #dee2e6;
            margin: 20px 0;
        }
     
    .horarios-container {
        display: flex;
        flex-wrap: wrap;
        gap: 10px;
        margin-top: 10px;
    }

    .horario-item {
        background-color: #f8f9fa;
        border: 1px solid #ccc;
        padding: 8px 12px;
        border-radius: 4px;
        font-size: 14px;
        display: flex;
        align-items: center;
    }

    .horario-item input {
        margin-right: 6px;
    }

    .dia-horario-titulo {
        font-weight: 600;
        margin-top: 15px;
        margin-bottom: 8px;
    }
</style>



    <div class="container">
        <div class="form-container">
            <h2>Cadastrar Turma</h2>

            <!-- Nome do novo plano -->
            <div class="mb-4">
                <label for="txtNomeNovoPlano" class="form-label">Nome da Turma:</label>
                <asp:TextBox ID="txtNomeNovoPlano" runat="server" CssClass="input-text" Placeholder="Digite o nome da nova turma" />
            </div>

            <!-- Dias -->
            <div class="mb-4">
                <label class="form-label">Escolha os Dias:</label>
                <asp:CheckBoxList ID="cblDias" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cblDias_SelectedIndexChanged"
                    CssClass="form-check" RepeatLayout="Flow" RepeatDirection="Horizontal" />
            </div>

            <!-- Horários -->
            <div class="mb-4">
                <label class="form-label">Escolha os Horários:</label>
                <asp:PlaceHolder ID="phHorarios" runat="server" />
            </div>

            <div class="separador"></div>

            <!-- Mensalidade -->
            <div class="d-flex align-items-center gap-2 mb-4">
                <asp:TextBox ID="txtMensalidade" runat="server" CssClass="input-text" />
                <asp:Button ID="btnCalcularMensalidade" runat="server" Text="Calcular"
                    OnClick="btnCalcularMensalidade_Click" CssClass="btn btn-danger btn-pequeno" />
            </div>

            <!-- Botão Salvar -->
            <div class="text-center">
                <asp:Button ID="btnSalvar" runat="server" Text="Salvar"
                    OnClick="btnSalvar_Click" CssClass="btn btn-danger btn-custom" />
            </div>
        </div>
    </div>
</asp:Content>
