<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastrarAluno.aspx.cs" Inherits="BJJGerenciamento.UI.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" />

            <asp:Panel ID="pnlInformacoesPessoaisAluno" runat="server">
                <main aria-labelledby="title">
                    <h2 id="title"> Informações Pessoais </h2>

                    <label for="cpf">Cpf:</label>
                    <asp:TextBox ID="cpf" runat="server" OnTextChanged="cpf_TextChanged" AutoPostBack="true" oninput="formatarCpf(this)"/>
                    <br />

                    <label for="nome">Nome:</label>
                    <asp:TextBox ID="txtNome" runat="server" OnTextChanged="txtNome_TextChanged" />
                    <br />

                    <label for="sobrenome">Sobrenome:</label>
                    <asp:TextBox ID="sobrenome" runat="server" OnTextChanged="sobrenome_TextChanged" />
                    <br />

                    <label for="telefone">Telefone:</label>
                    <asp:TextBox ID="telefone" runat="server" OnTextChanged="telefone_TextChanged" TextMode="Phone" AutoPostBack="true" oninput="formatarTelefone(this)"/>
                    <br />

                    <label for="email">Email:</label>
                    <asp:TextBox ID="email" runat="server" OnTextChanged="email_TextChanged" textMode="Email"/>
                    <br />

                    <label for="rg">Rg:</label>
                    <asp:TextBox ID="rg" runat="server" OnTextChanged="rg_TextChanged" AutoPostBack="true" oninput="formatarRg(this)"/>
                    <br />

                    <label for="dataNascimento">Data de Nascimento:</label>
                    <asp:TextBox ID="dataNascimento" runat="server" OnTextChanged="dataNascimento_TextChanged" textMode="Date"/>
                    <br />

                    <label for="cep">Cep:</label>
                    <asp:TextBox ID="cep" runat="server" OnTextChanged="cep_TextChanged" AutoPostBack="true" oninput="formatarCep(this)"/>

                    <asp:Button ID="BuscarCep" runat="server" OnClick="BuscarCep_Click" Text="Buscar" /> <br />

                    <label for="rua">Rua:</label>
                    <asp:TextBox ID="rua" runat="server" OnTextChanged="rua_TextChanged"/>
                    <br />

                    <label for="bairro">Bairro:</label>
                    <asp:TextBox ID="bairro" runat="server" OnTextChanged="bairro_TextChanged" />
                    <br />

                    <label for="cidade">Cidade:</label>
                    <asp:TextBox ID="cidade" runat="server" OnTextChanged="cidade_TextChanged" />
                    <br />

                    <label for="estado">Estado:</label>
                    <asp:TextBox ID="estado" runat="server" OnTextChanged="estado_TextChanged" />
                    <br />

                    <label for="numeroCasa">Numero Casa:</label>
                    <asp:TextBox ID="numeroCasa" runat="server" OnTextChanged="numeroCasa_TextChanged" AutoPostBack="true" oninput="formatarNumero(this)"/>
                    <br /> 

                    <label for="carteiraFPJJ">Carteira FPJJ</label>
                    <asp:TextBox ID="carteiraFPJJ" runat="server" OnTextChanged="carteiraFPJJ_TextChanged" textMode="Number"/>
                    <br />

                    <asp:Button ID="btnProximoResponsavel" Text="Próximo" OnClick="btnProximoResponsavel_Click" runat="server" />
                </main>
            </asp:Panel>     
            <asp:Panel ID="pnlInformacoesResponsavelAluno" runat="server" Visible="false">
                <main aria-labelledby="title">
                    <h2 id="title2"> Responsável </h2>


                    <label for="nome">Nome:</label>
                    <asp:TextBox ID="TextBox2" runat="server" OnTextChanged="txtNome_TextChanged" />
                    <br />

                    <label for="sobrenome">Sobrenome:</label>
                    <asp:TextBox ID="TextBox3" runat="server" OnTextChanged="sobrenome_TextChanged" />
                    <br />

                    <label for="cpf">Cpf:</label>
                    <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="cpf_TextChanged" AutoPostBack="true" oninput="formatarCpf(this)"/>
                    <br />

                    <label for="cpf">Rg:</label>
                    <asp:TextBox ID="TextBox6" runat="server" OnTextChanged="cpf_TextChanged" AutoPostBack="true" oninput="formatarCpf(this)"/>
                    <br />

                    <label for="telefone">Telefone:</label>
                    <asp:TextBox ID="TextBox4" runat="server" OnTextChanged="telefone_TextChanged" TextMode="Phone" AutoPostBack="true" oninput="formatarTelefone(this)"/>
                    <br />

                    <label for="email">Email:</label>
                    <asp:TextBox ID="TextBox5" runat="server" OnTextChanged="email_TextChanged" textMode="Email"/>
                    <br />

                    <label for="Cep">Cep:</label>
                    <asp:TextBox ID="TextBox7" runat="server" OnTextChanged="email_TextChanged" textMode="Email"/>
                    <br />

                    <label for="rua">Rua:</label>
                    <asp:TextBox ID="TextBox8" runat="server" OnTextChanged="rua_TextChanged"/>
                    <br />

                    <label for="bairro">Bairro:</label>
                    <asp:TextBox ID="TextBox9" runat="server" OnTextChanged="bairro_TextChanged" />
                    <br />

                    <label for="cidade">Cidade:</label>
                    <asp:TextBox ID="TextBox10" runat="server" OnTextChanged="cidade_TextChanged" />
                    <br />

                    <label for="estado">Estado:</label>
                    <asp:TextBox ID="TextBox11" runat="server" OnTextChanged="estado_TextChanged" />
                    <br />

                    <label for="numeroCasa">Numero Casa:</label>
                    <asp:TextBox ID="TextBox12" runat="server" OnTextChanged="numeroCasa_TextChanged" AutoPostBack="true" oninput="formatarNumero(this)"/>

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
    </asp:UpdatePanel>



    
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






