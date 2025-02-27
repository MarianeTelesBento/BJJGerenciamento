<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastrarAluno.aspx.cs" Inherits="BJJGerenciamento.UI.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

        <ContentTemplate>

            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" />

            <asp:Panel ID="pnlInformacoesPessoaisAluno" runat="server">
                <main aria-labelledby="title">
                    <h2 id="titleAluno"> Informações Pessoais </h2>

                    <label for="cpfAluno">Cpf:</label>
                    <asp:TextBox ID="cpfAluno" runat="server" OnTextChanged="cpfAluno_TextChanged" AutoPostBack="true" oninput="formatarCpf(this)"/>
                    <br />

                    <label for="nomeAluno">Nome:</label>
                    <asp:TextBox ID="nomeAluno" runat="server" OnTextChanged="nomeAluno_TextChanged" />
                    <br />

                    <label for="sobrenomeAluno">Sobrenome:</label>
                    <asp:TextBox ID="sobrenomeAluno" runat="server" OnTextChanged="sobrenomeAluno_TextChanged" />
                    <br />

                    <label for="telefoneAluno">Telefone:</label>
                    <asp:TextBox ID="telefoneAluno" runat="server" OnTextChanged="telefoneAluno_TextChanged" TextMode="Phone" AutoPostBack="true" oninput="formatarTelefone(this)"/>
                    <br />

                    <label for="emailAluno">Email:</label>
                    <asp:TextBox ID="emailAluno" runat="server" OnTextChanged="emailAluno_TextChanged" textMode="Email"/>
                    <br />

                    <label for="rgAluno">Rg:</label>
                    <asp:TextBox ID="rgAluno" runat="server" OnTextChanged="rgAluno_TextChanged" AutoPostBack="true" oninput="formatarRg(this)"/>
                    <br />

                    <label for="dataNascimentoAluno">Data de Nascimento:</label>
                    <asp:TextBox ID="dataNascimentoAluno" runat="server" OnTextChanged="dataNascimentoAluno_TextChanged" textMode="Date"/>
                    <br />

                    <label for="cepAluno">Cep:</label>
                    <asp:TextBox ID="cepAluno" runat="server" OnTextChanged="cepAluno_TextChanged" AutoPostBack="true" oninput="formatarCep(this)"/>

                    <asp:Button ID="buscarCepAluno" runat="server" OnClick="buscarCepAluno_Click" Text="Buscar" /> <br />

                    <label for="ruaAluno">Rua:</label>
                    <asp:TextBox ID="ruaAluno" runat="server" OnTextChanged="ruaAluno_TextChanged"/>
                    <br />

                    <label for="bairroAluno">Bairro:</label>
                    <asp:TextBox ID="bairroAluno" runat="server" OnTextChanged="bairroAluno_TextChanged" />
                    <br />

                    <label for="cidadeAluno">Cidade:</label>
                    <asp:TextBox ID="cidadeAluno" runat="server" OnTextChanged="cidadeAluno_TextChanged" />
                    <br />

                    <label for="estadoAluno">Estado:</label>
                    <asp:TextBox ID="estadoAluno" runat="server" OnTextChanged="estadoAluno_TextChanged" />
                    <br />

                    <label for="complementoAluno">Complemento</label>
                    <asp:TextBox ID="complementoAluno" runat="server" OnTextChanged="complementoAluno_TextChanged"/>
                    <br />

                    <label for="numeroCasaAluno">Numero:</label>
                    <asp:TextBox ID="numeroCasaAluno" runat="server" OnTextChanged="numeroCasaAluno_TextChanged" AutoPostBack="true" oninput="formatarNumero(this)"/>
                    <br /> 

                    <label for="carteiraFPJJAluno">Carteira FPJJ</label>
                    <asp:TextBox ID="carteiraFPJJAluno" runat="server" OnTextChanged="carteiraFPJJAluno_TextChanged" textMode="Number"/>
                    <br />

                    

                    <asp:Button ID="btnProximoResponsavel" Text="Próximo" OnClick="btnProximoResponsavel_Click" runat="server" />
                </main>
            </asp:Panel>     
            <asp:Panel ID="pnlInformacoesResponsavelAluno" runat="server" Visible="false">
                <main aria-labelledby="title2">
                    <h2 id="title2"> Responsável </h2>

                    <label for="cpfResponsavel">Cpf:</label>
                    <asp:TextBox ID="cpfResponsavel" runat="server" OnTextChanged="cpfResponsavel_TextChanged" AutoPostBack="true" oninput="formatarCpf(this)"/>
                    <br />

                    <label for="nomeResponsavel">Nome:</label>
                    <asp:TextBox ID="nomeResponsavel" runat="server" OnTextChanged="nomeResponsavel_TextChanged" />
                    <br />

                    <label for="sobrenomeResponsavel">Sobrenome:</label>
                    <asp:TextBox ID="sobrenomeResponsavel" runat="server" OnTextChanged="sobrenomeResponsavel_TextChanged" />
                    <br />

                    <label for="rgResponsavel">Rg:</label>
                    <asp:TextBox ID="rgResponsavel" runat="server" OnTextChanged="rgResponsavel_TextChanged" AutoPostBack="true" oninput="formatarRg(this)"/>
                    <br />

                    <label for="dataNascimentoResponsavel">Data de Nascimento:</label>
                    <asp:TextBox ID="dataNascimentoResponsavel" runat="server" OnTextChanged="dataNascimentoResponsavel_TextChanged" textMode="Date"/>
                    <br />

                    <label for="telefoneResponsavel">Telefone:</label>
                    <asp:TextBox ID="telefoneResponsavel" runat="server" OnTextChanged="telefoneResponsavel_TextChanged" TextMode="Phone" AutoPostBack="true" oninput="formatarTelefone(this)"/>
                    <br />

                    <label for="emailResponsavel">Email:</label>
                    <asp:TextBox ID="emailResponsavel" runat="server" OnTextChanged="emailResponsavel_TextChanged" textMode="Email"/>
                    <br />

                    <label for="cepResponsavel">Cep:</label>
                    <asp:TextBox ID="cepResponsavel" runat="server" OnTextChanged="cepResponsavel_TextChanged"/>
                    <br />

                    <asp:Button ID="buscarCepResponsavel" runat="server" OnClick="buscarCepResponsavel_Click" Text="Buscar" /> <br />

                    <label for="ruaResponsavel">Rua:</label>
                    <asp:TextBox ID="ruaResponsavel" runat="server" OnTextChanged="ruaResponsavel_TextChanged"/>
                    <br />

                    <label for="bairroResponsavel">Bairro:</label>
                    <asp:TextBox ID="bairroResponsavel" runat="server" OnTextChanged="bairroResponsavel_TextChanged" />
                    <br />

                    <label for="cidadeResponsavel">Cidade:</label>
                    <asp:TextBox ID="cidadeResponsavel" runat="server" OnTextChanged="cidadeResponsavel_TextChanged" />
                    <br />

                    <label for="estadoResponsavel">Estado:</label>
                    <asp:TextBox ID="estadoResponsavel" runat="server" OnTextChanged="estadoResponsavel_TextChanged" />
                    <br />

                    <label for="complementoResponsavel">Complemento</label>
                    <asp:TextBox ID="complementoResponsavel" runat="server" OnTextChanged="complementoResponsavel_TextChanged" />
                    <br />

                    <label for="numeroCasaResponsavel">Numero:</label>
                    <asp:TextBox ID="numeroCasaResponsavel" runat="server" OnTextChanged="numeroCasaResponsavel_TextChanged" AutoPostBack="true" oninput="formatarNumero(this)"/>

                    <br />
                    <asp:Button ID="proximoPlano" Text="Próximo" OnClick="btnProximoPlano_Click" runat="server" />
                    
                </main>
            </asp:Panel>
            <asp:Panel ID="pnlPlanoAluno" runat="server" Visible="false">
                <main aria-labelledby="title">
                    <h2 id="title3"> Plano </h2>

                    <label for="ddTurmas">Selecione uma turma:</label>
                    <asp:DropDownList ID="ddTurmas" runat="server" AutoPostBack="true" 
                        OnSelectedIndexChanged="ddTurmas_SelectedIndexChanged">
                    </asp:DropDownList>

                    <label for="cbDias">Dias:</label>
                    <asp:CheckBoxList ID="cbDias" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="cbDias_SelectedIndexChanged">
                    </asp:CheckBoxList>

                    <asp:Panel ID="pnlHorarios" runat="server" Visible="false">
                        <label for="ddHorarios">Selecione um horário:</label>
                        <asp:DropDownList ID="ddHorarios" runat="server"></asp:DropDownList>
                    </asp:Panel>

                    <br />
                </main>
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






