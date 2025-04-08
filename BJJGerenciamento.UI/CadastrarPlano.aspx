<%@ Page Title="Cadastrar Plano" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastrarPlano.aspx.cs" Inherits="BJJGerenciamento.UI.CadastrarPlano" %>
 

<asp:content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <ContentTemplate>

            <div class="container mt-4">
                <h2 class="text-center">Cadastrar Plano do Aluno</h2>

                <div class="row">
                    <!-- Seleção da Turma -->
                    <div class="col-md-6 mb-3">
                        <label for="ddPlanos" class="form-label">Selecione uma turma:</label>
                        <asp:DropDownList ID="ddPlanos" style="border-radius: 2px;" runat="server" AutoPostBack="true" 
                            OnSelectedIndexChanged="ddPlanos_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>

                    <!-- Dias disponíveis -->
                    <div class="col-md-6 mb-3">
                        <label for="cbDias">Dias:</label>
                        <asp:CheckBoxList ID="cbDias" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="cbDias_SelectedIndexChanged">
                        </asp:CheckBoxList><br />
                    </div>
                </div>


                <!-- Horários para cada dia -->
                <div class="row">
                    <asp:Panel ID="pnlSegunda" runat="server" Visible="false">
                        <div class="col-12 mb-3">
                            <label for="cbHorariosSegunda" class="form-label">Selecione os horários de SEGUNDA</label>
                            <asp:CheckBoxList ID="cbHorariosSegunda" CssClass="form-check" runat="server"/>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlTerca" runat="server" Visible="false">
                        <div class="col-12 mb-3">
                            <label for="cbHorariosTerca" class="form-label">Selecione os horários de TERÇA</label>
                            <asp:CheckBoxList ID="cbHorariosTerca" CssClass="form-check" runat="server"/>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlQuarta" runat="server" Visible="false">
                        <div class="col-12 mb-3">
                            <label for="cbHorariosQuarta" class="form-label">Selecione os horários de QUARTA</label>
                            <asp:CheckBoxList ID="cbHorariosQuarta" CssClass="form-check" runat="server"/>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlQuinta" runat="server" Visible="false">
                        <div class="col-12 mb-3">
                            <label for="cbHorariosQuinta" class="form-label">Selecione os horários de QUINTA</label>
                            <asp:CheckBoxList ID="cbHorariosQuinta" CssClass="form-check" runat="server"/>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlSexta" runat="server" Visible="false">
                        <div class="col-12 mb-3">
                            <label for="cbHorariosSexta" class="form-label">Selecione os horários de SEXTA</label>
                            <asp:CheckBoxList ID="cbHorariosSexta" CssClass="form-check" runat="server"/>
                        </div>
                    </asp:Panel>
                </div>

                <!-- Valor do plano -->
                <div class="row">
                    <div class="col-12 mb-3">
                        <asp:Label ID="ValorPagoPlano" runat="server" CssClass="fw-bold fs-5 text-success" Text="Valor: R$ 0,00" />
                    </div>
                </div>

                <!-- Botão de Enviar -->
                <div class="col-12 text-center mt-3">
                    <asp:Button ID="EnviarInformacoes" runat="server" style="background-color:dodgerblue; font-size: 14px; border: none; color: aliceblue; font-family: -apple-system, Roboto, Arial, sans-serif;
            ; border-radius: 3px;" Text="Enviar" OnClick="btnEnviarInformacoes_Click" />
                </div>
            </div>

        </ContentTemplate>
</asp:content>

