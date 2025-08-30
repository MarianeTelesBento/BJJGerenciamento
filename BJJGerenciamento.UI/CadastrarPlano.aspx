<%@ Page Title="Cadastrar Plano" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastrarPlano.aspx.cs" Inherits="BJJGerenciamento.UI.CadastrarPlano" %>
 

<asp:content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <ContentTemplate>
            <style>
                .btn-custom {
                    background-color: #e53935; 
                    font-size: 16px;
                    border: none;
                    color: white;
                    font-family: -apple-system, Roboto, Arial, sans-serif;
                    border-radius: 6px;
                    padding: 10px 20px;
                    text-align: center;
                    height: 42px; 
                    line-height: 22px; 
                    display: inline-flex;
                    align-items: center;
                    justify-content: center;
                }
                .form-container {
                    background-color: white;
                    padding: 30px;
                    border-radius: 12px;
                    box-shadow: 0 0 10px rgba(0,0,0,0.08);
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

               .btn-custom {
                background-color: #e53935; 
                font-size: 14px;
                border: none;
                color: white;
                font-family: -apple-system, Roboto, Arial, sans-serif;
                border-radius: 6px;
                padding: 10px 20px;
                text-align: center;


                 }

               .btn-pequeno {
                background-color: #e53935;
                width: 120px;
                height: 35px;
                line-height: 18px;
     
               }

                .form-select {
                    padding: 0.375rem 0.75rem;
                    font-size: 14px;
                    line-height: 1.5;
                    height: 40px; 
                    border-radius: 4px;
                    border: 1px solid #ccc;
                    font-family: -apple-system, Roboto, Arial, sans-serif;
                    appearance: none;
                    background-color: #fff;
                    background-image: url("data:image/svg+xml;charset=US-ASCII,%3Csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 4 5'%3E%3Cpath fill='%23000' d='M2 0L0 2h4zm0 5L0 3h4z'/%3E%3C/svg%3E");
                    background-repeat: no-repeat;
                    background-position: right 0.75rem center;
                    background-size: 8px 10px;
                }

                .form-label {
                    font-weight: 600;
                    color: #333;
                    margin-bottom: 8px;
                    display: block;
                }
                             .campo-vencimento {
                    display: flex;
                    flex-direction: column;
                    margin-bottom: 1rem;
                    font-family: 'Segoe UI', sans-serif;
                }

                .campo-vencimento label {
                    margin-bottom: 0.4rem;
                    font-weight: 600;
                    color: #333;
                }

                .campo-vencimento input[type="date"] {
                    padding: 10px;
                    border: 1px solid #ccc;
                    border-radius: 8px;
                    font-size: 14px;
                    transition: border-color 0.3s ease;
                }

                .campo-vencimento input[type="date"]:focus {
                    border-color: #007bff;
                    outline: none;
                    box-shadow: 0 0 0 2px rgba(0, 123, 255, 0.2);
                }
            </style>
            <div class="container mt-4">
                <h2>Cadastrar Plano do Aluno</h2>


            <div class="container mt-5">
           <div class="form-container">
        <div class="row">
      <div class="col-md-6 mb-4">
     <label for="ddPlanos" class="form-label">Selecione uma adesão:</label>
    <asp:DropDownList ID="ddlAdesao" runat="server" AutoPostBack="true" CssClass="form-select" OnSelectedIndexChanged="ddlAdesao_SelectedIndexChanged">        
    </asp:DropDownList>     <asp:CheckBox ID="chkUsarVip" runat="server" OnCheckedChanged="chkUsarVip_CheckedChanged" AutoPostBack="true"  CssClass="form-check-input" /> <label for="chkUsarVip" class="form-check-label">VIP</label>  
  
    
      </div>
  
                        <!-- Seleção da Turma -->
                        <div class="col-md-6 mb-4">
                            <label for="ddPlanos" class="form-label">Selecione uma turma:</label>
                            <asp:DropDownList ID="ddPlanos" runat="server" CssClass="form-select"
                                AutoPostBack="true" OnSelectedIndexChanged="ddPlanos_SelectedIndexChanged" />
                       
                        </div>

                        <!-- Dias disponíveis -->
                        <div class="col-md-6 mb-4">
                            <label class="form-label">Selecione os dias disponíveis:</label>
                            <asp:CheckBoxList ID="cbDias" runat="server" CssClass="form-check"
                                AutoPostBack="true" OnSelectedIndexChanged="cbDias_SelectedIndexChanged"
                                RepeatLayout="Flow" RepeatDirection="Horizontal" />
                        </div>
                    </div>

                    <div class="separador"></div>

                    <!-- Painéis de horários por dia -->
                    <div class="row">
                        <asp:Panel ID="pnlSegunda" runat="server" Visible="false">
                            <div class="col-12 mb-4">
                                <label class="form-label">Horários de SEGUNDA</label>
                                <asp:CheckBoxList ID="cbHorariosSegunda" CssClass="form-check" runat="server"
                                    RepeatLayout="Flow" RepeatDirection="Horizontal" />
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlTerca" runat="server" Visible="false">
                            <div class="col-12 mb-4">
                                <label class="form-label">Horários de TERÇA</label>
                                <asp:CheckBoxList ID="cbHorariosTerca" CssClass="form-check" runat="server"
                                    RepeatLayout="Flow" RepeatDirection="Horizontal" />
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlQuarta" runat="server" Visible="false">
                            <div class="col-12 mb-4">
                                <label class="form-label">Horários de QUARTA</label>
                                <asp:CheckBoxList ID="cbHorariosQuarta" CssClass="form-check" runat="server"
                                    RepeatLayout="Flow" RepeatDirection="Horizontal" />
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlQuinta" runat="server" Visible="false">
                            <div class="col-12 mb-4">
                                <label class="form-label">Horários de QUINTA</label>
                                <asp:CheckBoxList ID="cbHorariosQuinta" CssClass="form-check" runat="server"
                                    RepeatLayout="Flow" RepeatDirection="Horizontal" />
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlSexta" runat="server" Visible="false">
                            <div class="col-12 mb-4">
                                <label class="form-label">Horários de SEXTA</label>
                                <asp:CheckBoxList ID="cbHorariosSexta" CssClass="form-check" runat="server"
                                    RepeatLayout="Flow" RepeatDirection="Horizontal" />
                            </div>
                        </asp:Panel>
                    </div>

                    <div class="separador"></div>

                    <!-- Valor do plano -->
                   <div class="d-flex align-items-center gap-2">
                        <asp:TextBox ID="ValorPagoPlano" runat="server" CssClass="input-text" />
                        <asp:Button ID="btnValorPlano" runat="server" Text="Calcular"
                            OnClick="btnValorPlano_Click" CssClass="btn btn-danger btn-pequeno" />
                    </div>
                       <div class="separador"></div>

                    <div class="campo-vencimento d-flex gap-2">
                        <asp:Label ID="lblDataEscolhida" runat="server" Text="Data de Vencimento:"></asp:Label>
                        <asp:TextBox ID="txtDataVencimento" runat="server" TextMode="Date" CssClass="input-date" Style="height:35px" />
                    </div>
                

                    <!-- Botão de Enviar -->
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="EnviarInformacoes" runat="server" Text="Enviar"
                                OnClick="btnEnviarInformacoes_Click" Visible="false" CssClass="btn btn-danger btn-custom w-100" />
                        </div>
                    </div>
                </div>
            </div>

    </ContentTemplate>
        </div>
</asp:content>


