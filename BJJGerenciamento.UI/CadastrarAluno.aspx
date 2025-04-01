<%@ Page Title="Cadastrar aluno" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastrarAluno.aspx.cs" Inherits="BJJGerenciamento.UI.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <ContentTemplate>

            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" style="background-color:dodgerblue; font-size: 14px; border: none; color: aliceblue; font-family: -apple-system, Roboto, Arial, sans-serif;
 ; border-radius: 3px;" Visible="false"/>

            <asp:Panel ID="pnlInformacoesPessoaisAluno" runat="server" CssClass="container mt-4">
                <main aria-labelledby="title">
                    <h2 id="titleAluno" class="mb-4 text-center">Informações Pessoais</h2>

                    <div class="row g-3">
                        <div class="col-md-6">
                            <label for="cpfAluno" class="form-label">CPF:</label>
                            <asp:TextBox ID="cpfAluno" runat="server" CssClass="form-control" OnTextChanged="cpfAluno_TextChanged" AutoPostBack="true" oninput="formatarCpf(this)"/>
                        </div>

                        <div class="col-md-6">
                            <label for="nomeAluno" class="form-label">Nome:</label>
                            <asp:TextBox ID="nomeAluno" runat="server" CssClass="form-control" OnTextChanged="nomeAluno_TextChanged" />
                        </div>

                        <div class="col-md-6">
                            <label for="sobrenomeAluno" class="form-label">Sobrenome:</label>
                            <asp:TextBox ID="sobrenomeAluno" runat="server" CssClass="form-control" OnTextChanged="sobrenomeAluno_TextChanged" />
                        </div>

                        <div class="col-md-6">
                            <label for="telefoneAluno" class="form-label">Telefone:</label>
                            <asp:TextBox ID="telefoneAluno" runat="server" CssClass="form-control" OnTextChanged="telefoneAluno_TextChanged" TextMode="Phone" AutoPostBack="true" oninput="formatarTelefone(this)"/>
                        </div>

                        <div class="col-md-6">
                            <label for="emailAluno" class="form-label">Email:</label>
                            <asp:TextBox ID="emailAluno" runat="server" CssClass="form-control" OnTextChanged="emailAluno_TextChanged" TextMode="Email"/>
                        </div>

                        <div class="col-md-6">
                            <label for="rgAluno" class="form-label">RG:</label>
                            <asp:TextBox ID="rgAluno" runat="server" CssClass="form-control" OnTextChanged="rgAluno_TextChanged" AutoPostBack="true" oninput="formatarRg(this)"/>
                        </div>

                        <div class="col-md-6">
                            <label for="dataNascimentoAluno" class="form-label">Data de Nascimento:</label>
                            <asp:TextBox ID="dataNascimentoAluno" runat="server" CssClass="form-control" OnTextChanged="dataNascimentoAluno_TextChanged" TextMode="Date"/>
                        </div>

                        <div class="col-md-6">
                            <label for="cepAluno" class="form-label">CEP:</label>
                            <div class="input-group">
                                <asp:TextBox ID="cepAluno" runat="server" CssClass="form-control" OnTextChanged="cepAluno_TextChanged" AutoPostBack="true" oninput="formatarCep(this)"/> 
                                <br />
                                <br />
                                <asp:Button ID="buscarCepAluno" runat="server"  OnClick="buscarCepAluno_Click" Text="Buscar" style="background-color:dodgerblue; font-size: 14px; border: none; color: aliceblue; font-family: -apple-system, Roboto, Arial, sans-serif;
             ; border-radius: 3px;" />
                            </div>
                        </div>

                        <div class="col-md-6">
                            <label for="ruaAluno" class="form-label">Rua:</label>
                            <asp:TextBox ID="ruaAluno" runat="server" CssClass="form-control" OnTextChanged="ruaAluno_TextChanged"/>
                        </div>

                        <div class="col-md-6">
                            <label for="bairroAluno" class="form-label">Bairro:</label>
                            <asp:TextBox ID="bairroAluno" runat="server" CssClass="form-control" OnTextChanged="bairroAluno_TextChanged" />
                        </div>

                        <div class="col-md-6">
                            <label for="cidadeAluno" class="form-label">Cidade:</label>
                            <asp:TextBox ID="cidadeAluno" runat="server" CssClass="form-control" OnTextChanged="cidadeAluno_TextChanged" />
                        </div>

                        <div class="col-md-6">
                            <label for="estadoAluno" class="form-label">Estado:</label>
                            <asp:TextBox ID="estadoAluno" runat="server" CssClass="form-control" OnTextChanged="estadoAluno_TextChanged" />
                        </div>

                        <div class="col-md-6">
                            <label for="complementoAluno" class="form-label">Complemento:</label>
                            <asp:TextBox ID="complementoAluno" runat="server" CssClass="form-control" OnTextChanged="complementoAluno_TextChanged"/>
                        </div>

                        <div class="col-md-6">
                            <label for="numeroCasaAluno" class="form-label">Número:</label>
                            <asp:TextBox ID="numeroCasaAluno" runat="server" CssClass="form-control" OnTextChanged="numeroCasaAluno_TextChanged" AutoPostBack="true" oninput="formatarNumero(this)"/>
                        </div>

                        <div class="col-md-6">
                            <label for="carteiraFPJJAluno" class="form-label">Carteira FPJJ:</label>
                            <asp:TextBox ID="carteiraFPJJAluno" runat="server" CssClass="form-control" OnTextChanged="carteiraFPJJAluno_TextChanged" TextMode="Number"/>
                        </div>

                        <div class="col-12 text-center mt-4">
                           <asp:Button ID="btnProximoResponsavel" Text="Próximo" OnClick="btnProximoResponsavel_Click" runat="server" style="background-color:dodgerblue; font-size: 14px; border: none; color: aliceblue; font-family: -apple-system, Roboto, Arial, sans-serif;
             ; border-radius: 3px;"/>
                        </div>
                    </div>
                </main>
            </asp:Panel>

            <asp:Panel ID="pnlInformacoesResponsavelAluno" runat="server" Visible="false">
                <div class="container mt-4">
                    <h2 class="text-center">Responsável</h2>
                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label for="cpfResponsavel" class="form-label">CPF:</label>
                            <asp:TextBox ID="cpfResponsavel" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="cpfResponsavel_TextChanged" oninput="formatarCpf(this)"/>
                        </div>

                        <div class="col-md-6 mb-3">
                            <label for="nomeResponsavel" class="form-label">Nome:</label>
                            <asp:TextBox ID="nomeResponsavel" CssClass="form-control" runat="server" OnTextChanged="nomeResponsavel_TextChanged" />
                        </div>

                        <div class="col-md-6 mb-3">
                            <label for="sobrenomeResponsavel" class="form-label">Sobrenome:</label>
                            <asp:TextBox ID="sobrenomeResponsavel" CssClass="form-control" runat="server" OnTextChanged="sobrenomeResponsavel_TextChanged" />
                        </div>

                        <div class="col-md-6 mb-3">
                            <label for="rgResponsavel" class="form-label">RG:</label>
                            <asp:TextBox ID="rgResponsavel" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="rgResponsavel_TextChanged" oninput="formatarRg(this)"/>
                        </div>

                        <div class="col-md-6 mb-3">
                            <label for="dataNascimentoResponsavel" class="form-label">Data de Nascimento:</label>
                            <asp:TextBox ID="dataNascimentoResponsavel" CssClass="form-control" runat="server" textMode="Date" OnTextChanged="dataNascimentoResponsavel_TextChanged"/>
                        </div>

                        <div class="col-md-6 mb-3">
                            <label for="telefoneResponsavel" class="form-label">Telefone:</label>
                            <asp:TextBox ID="telefoneResponsavel" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="telefoneResponsavel_TextChanged" textMode="Phone" oninput="formatarTelefone(this)"/>
                        </div>

                        <div class="col-md-6 mb-3">
                            <label for="emailResponsavel" class="form-label">Email:</label>
                            <asp:TextBox ID="emailResponsavel" CssClass="form-control" runat="server" textMode="Email" OnTextChanged="emailResponsavel_TextChanged"/>
                        </div>

                        <div class="col-md-6 mb-3">
                            <label for="cepResponsavel" class="form-label">CEP:</label>
                            <asp:TextBox ID="cepResponsavel" CssClass="form-control" runat="server" OnTextChanged="cepResponsavel_TextChanged"/>
                             <asp:Button ID="buscarCepResponsavel" runat="server" OnClick="buscarCepResponsavel_Click" Text="Buscar" style="background-color:dodgerblue; font-size: 14px; border: none; color: aliceblue; font-family: -apple-system, Roboto, Arial, sans-serif;
             ; border-radius: 3px;"/>
                        </div>

                        <div class="col-md-6 mb-3 d-flex align-items-end">
               
                        </div>

                        <div class="col-md-6 mb-3">
                            <label for="ruaResponsavel" class="form-label">Rua:</label>
                            <asp:TextBox ID="ruaResponsavel" CssClass="form-control" runat="server" OnTextChanged="ruaResponsavel_TextChanged"/>
                        </div>

                        <div class="col-md-6 mb-3">
                            <label for="bairroResponsavel" class="form-label">Bairro:</label>
                            <asp:TextBox ID="bairroResponsavel" CssClass="form-control" runat="server" OnTextChanged="bairroResponsavel_TextChanged" />
                        </div>

                        <div class="col-md-6 mb-3">
                            <label for="cidadeResponsavel" class="form-label">Cidade:</label>
                            <asp:TextBox ID="cidadeResponsavel" CssClass="form-control" runat="server" OnTextChanged="cidadeResponsavel_TextChanged" />
                        </div>

                        <div class="col-md-6 mb-3">
                            <label for="estadoResponsavel" class="form-label">Estado:</label>
                            <asp:TextBox ID="estadoResponsavel" CssClass="form-control" runat="server" OnTextChanged="estadoResponsavel_TextChanged" />
                        </div>

                        <div class="col-md-6 mb-3">
                            <label for="complementoResponsavel" class="form-label">Complemento:</label>
                            <asp:TextBox ID="complementoResponsavel" CssClass="form-control" runat="server" OnTextChanged="complementoResponsavel_TextChanged" />
                        </div>

                        <div class="col-md-6 mb-3">
                            <label for="numeroCasaResponsavel" class="form-label">Número:</label>
                            <asp:TextBox ID="numeroCasaResponsavel" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="numeroCasaResponsavel_TextChanged" oninput="formatarNumero(this)"/>
                        </div>

                        <div class="col-12 text-center mt-4">
                            <asp:Button ID="proximoPlano" Text="Próximo" OnClick="btnProximoPlano_Click" style="background-color:dodgerblue; font-size: 14px; border: none; color: aliceblue; font-family: -apple-system, Roboto, Arial, sans-serif;
             ; border-radius: 3px;" runat="server" />
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlPlanoAluno" runat="server" Visible="false">
                <div class="container mt-4">
                    <h2 class="text-center">Plano</h2>

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


                    <!-- Painel de horários para cada dia -->
                    <div class="row">
                        <asp:Panel ID="pnlHorarios" runat="server" Visible="false">
                            <div class="col-12 mb-3">
                                <label for="cbHorarios" class="form-label">Selecione os horários:</label>
                                <asp:CheckBoxList ID="cbHorarios" CssClass="form-check" runat="server"/>
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
            </asp:Panel>

        </ContentTemplate>

    
    <script>
        function formatarCpf(campo) {
            let cpf = campo.value.replace(/\D/g, "");

            if (cpf.length > 11) cpf = cpf.substring(0, 11); 

            cpf = cpf.replace(/^(\d{3})(\d)/, "$1.$2");
            cpf = cpf.replace(/^(\d{3})\.(\d{3})(\d)/, "$1.$2.$3");
            cpf = cpf.replace(/\.(\d{3})(\d)/, ".$1-$2");

            campo.value = cpf;
        }
        function formatarRg(campo) {
            let rg = campo.value.replace(/\D/g, "");

            if (rg.length > 9) rg = rg.substring(0, 9);

            rg = rg.replace(/^(\d{2})(\d)/, "$1.$2");
            rg = rg.replace(/^(\d{2})\.(\d{3})(\d)/, "$1.$2.$3");
            rg = rg.replace(/\.(\d{3})(\d)/, ".$1-$2");

            campo.value = rg;
        }
        function formatarCep(campo) {
            let cep = campo.value.replace(/\D/g, "");

            if (cep.length > 8) cep = cep.substring(0, 8);

            cep = cep.replace(/^(\d{5})(\d)/, "$1-$2");

            campo.value = cep;
        }
        function formatarTelefone(campo) {
            let telefone = campo.value.replace(/\D/g, "");

            if (telefone.length > 11) telefone = telefone.substring(0, 11);

            telefone = telefone.replace(/^(\d{2})(\d)/, "($1) $2");
            telefone = telefone.replace(/(\d{5})(\d{4})$/, "$1-$2");

            campo.value = telefone;
        }
        function formatarNumero(campo) {
            let telefone = campo.value.replace(/\D/g, "");

            if (telefone.length > 10) telefone = telefone.substring(0, 10);

            campo.value = telefone;
        }
    </script>

</asp:Content>






