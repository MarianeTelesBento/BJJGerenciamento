<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Guia.aspx.cs" Inherits="BJJGerenciamento.UI.Guia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <h1>Bem-Vindo ao Guia Do BJJ Gerenciamento</h1>
    <p>Este guia irá ajudá-lo a navegar e utilizar todas as funcionalidades do sistema de gerenciamento de academias de Jiu-Jitsu.</p>

<div class="accordion" id="accordionGuia">
     <div class="card-header" style="background-color: #f8f9fa; border-bottom: 1px solid #e3e6ed;">
     <summary style="font-size: 1.2em; font-weight: bold; cursor: pointer; color: #333;">
         1. Guia de Cadastro de Alunos (Clique para expandir)
     </summary>
 </div>


<div class="card mb-3"> <%-- Adicione 'mb-3' para uma margem inferior --%>
    <details>
       
        <div class="card-body">
            <p>Para cadastrar um novo aluno, vá até a seção "Alunos" no menu principal e clique em "Cadastrar Aluno". Preencha os dados necessários, como nome, sobrenome, CPF, Carteira FPJJ (Se o aluno não tiver o cadastro FPJJ, o link ao lado do campo, irá redirecionar para pagina ofical da federação)
            Preencha os dados de logradouro e prossiga.</p>
            <img src="/Images/cadastroAluno1.png" alt="Tela de cadastro de aluno" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px;" /> <%-- Use img-fluid para responsividade --%>
            <img src="/Images/cadsatroAluno2.png" alt="Tela de cadastro de aluno" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px;" />

            <p>Se o aluno for menor de idade, é obrigatóriamente cadastrar um responsavel, a página então, irá redirecionar para página "Cadastro de Responsável", Siga a mesma logica de cadastro, e prossiga, confirme os dados e envie o cadastro. </p>
            <img src="/Images/cadastroresponsavel1.png" alt="Tela de cadastro de aluno" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px;" />
            <img src="/Images/Confirmar.png" alt="Tela de cadastro de aluno" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px;" />
        </div>
    </details>
</div>
         <div class="card-header" style="background-color: #f8f9fa; border-bottom: 1px solid #e3e6ed;">
         <summary style="font-size: 1.2em; font-weight: bold; cursor: pointer; color: #333;">
             2. Guia de Cadastro de Planos (Clique para expandir)
         </summary>
     </div>
    <div class="card mb-3">
    <details>
   

        <div class="card-body">
            <p>
                Após realizar o cadastro de um aluno, é importante associar um plano de treinamento a ele. então a pagina será redirecionada para a página de cadastro de planos.
            </p>
            
            <img src="/Images/plano1.png" alt="Tela inicial de cadastro de planos" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />
            
            <p>
                Selecione uma adesão, em seguida uma turma na qual o aluno estará associado, defina os dias e horarios disponiveis (se o aluno escolher um plano vip, basta acionar a caixinha "VIP", Atenção, ao escolher uma adesão que não possui a opção vip, não será possivel ativar a caixinha,), calcule o valor do  plano, ou altere manualmente,
                escolha uma data de vencimento (o dia  esocolhido, será o dia que o sistema irá gerar a cobrança mensalmente), finalize o cadastro.
                
            </p>
            <img src="/Images/plano2.png" alt="Detalhe do formulário de planos" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />
             <img src="/Images/plano3.png" alt="Detalhe do formulário de planos" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />
             <img src="/Images/plano4.png" alt="Detalhe do formulário de planos" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />

        </div>
    </details>
       
</div>
      <div class="card-header" style="background-color: #f8f9fa; border-bottom: 1px solid #e3e6ed;">
      <summary style="font-size: 1.2em; font-weight: bold; cursor: pointer; color: #333;">
          3. Como Visualizar e Gerenciar a Lista de Alunos (Clique para expandir)
      </summary>
  </div>
    <div class="card mb-3">
    <details>
      

        <div class="card-body">
            <p>
                Para acessar a lista completa de alunos, navegue até o menu "Alunos" e clique em "Lista de Alunos". 
                Nesta tela, você pode visualizar todos os alunos cadastrados, pesquisar por um aluno específico ou usar os filtros para refinar sua busca.
            </p>
            
            <img src="/Images/lista1.png" alt="Tela da lista de alunos com filtros de busca" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />
            
            <p>
                Para acionar os filtros, clique no icone mostrado na imagem acima, e selecione os filtros desejados, como nome, status do plano ou turmas, e clique em "Pesquisar".
            </p>

            <img src="/Images/lista2.png" alt="Detalhe dos botões de ação na lista de alunos" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />

            <p>
                Cada aluno na lista possui botões de ação que permitem editar informações, visualizar detalhes do plano e Graduações, ou Desativar um cadastro. 
                Use esses botões conforme necessário para gerenciar os dados dos alunos.
            </p>
               <img src="/Images/lista3.png" alt="Tela da lista de alunos ediçao" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />
               <img src="/Images/listaPlano.png" alt="Tela da lista de planos" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />
               <img src="/Images/listaGrdu.png" alt="Tela da lista de alunos graduaçao class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />
             <img src="/Images/listaresponsavel.png" alt=" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />
            <p> <strong>Atenção:</strong> ao modificar um plano, as informações antigas não são salvas, ou seja, tudo será reinserido, então, modifique tendo plena ciência detsa informação!</p>

        </div>
    </details>
</div>
       <div class="card-header" style="background-color: #f8f9fa; border-bottom: 1px solid #e3e6ed;">
       <summary style="font-size: 1.2em; font-weight: bold; cursor: pointer; color: #333;">
           4. Como Realizar a Chamada (Clique para expandir)
       </summary>
   </div>

    <div class="card mb-3">
    <details>
     
        <div class="card-body">
            <p>
                Para realizar a chamada, acesse a seção "Chamada" no menu. acione o icone de filtros, que são divididos por turmas, horarios, e uma aba de pesquisa, selecione a turma e o horario, e clique em "pesquisar"( em casos de alunos "VIP's" que podem frequentar horarios e turmas diferentes de acordo com o nivel, pesquise pelo aluno na lista geral, e compute a presença a ele).
            </p>
            
            <img src="/Images/chamada1.png" alt="Tela de chamada com seleção de turma e data" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />
            
            <p>
                Após carregar a lista,  clique em "Nova Chamada", selecione uma sala onde a aula ocorreu, e quem foi o professor responsável.
            </p>

            <img src="/Images/chamda2.png" alt="Lista de alunos com checkboxes para marcar presença" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />
           
            <p>Salve a chamada no icone que esta na imagem</p>
        </div>
    </details>
</div>
           <div class="card-header" style="background-color: #f8f9fa; border-bottom: 1px solid #e3e6ed;">
       <summary style="font-size: 1.2em; font-weight: bold; cursor: pointer; color: #333;">
           5. Como Realizar a Graduação (Clique para expandir)
       </summary>
   </div>

    <div class="card mb-3">
    <details>
     
        <div class="card-body">

            <p>Para realizar a graduação selecione o icone de filtros, busque de forma personalizad, clique no botão "Graduação", que esta ao lado de cada aluno, e adicione a observação</p>
            
            <img src="/Images/graduacao1.png" alt="Tela de chamada com seleção de turma e data" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />
    
            <img src="/Images/graduacao2.png" alt="Lista de alunos com checkboxes para marcar presença" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />
           
           
        </div>
    </details>
</div>
    <div class="card-header" style="background-color: #f8f9fa; border-bottom: 1px solid #e3e6ed;">
    <summary style="font-size: 1.2em; font-weight: bold; cursor: pointer; color: #333;">
        5. Como Cadastrar Novas Turmas (Clique para expandir)
    </summary>
</div>
    <div class="card mb-3">
    <details>
        
        <div class="card-body">
            <p>
                Para criar uma nova turma, acesse o menu "Turmas" e selecione a opção "Cadastrar Turma".
                Preencha os campos necessários, como o nome da turma, e os dias que ela acontecerá.
            </p>
            <img src="/Images/cadastrarTurma.png" alt="Tela de cadastro de novas turmas" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />
        </div>
    </details>
</div>

  <div class="card-header" style="background-color: #f8f9fa; border-bottom: 1px solid #e3e6ed;">
      <summary style="font-size: 1.2em; font-weight: bold; cursor: pointer; color: #333;">
          6. Como Listar e Gerenciar Turmas (Clique para expandir)
      </summary>
  </div>
<div class="card mb-3">
    <details>
      
        <div class="card-body">
            <p>
                Acesse "Turmas" > "Listar Turmas" para ver todas as turmas cadastradas. Nesta tela, você pode clicar nos botões de ação para editar ou visualizar os detalhes de uma turma.
            </p>
            <img src="/Images/listarTurma.png" alt="Tela mostrando a lista de turmas cadastradas" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />
                <img src="/Images/listarTurma2.png" alt="Tela mostrando a lista de turmas cadastradas" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />
        </div>
    </details>
</div>

    <div class="card-header" style="background-color: #f8f9fa; border-bottom: 1px solid #e3e6ed;">
    <summary style="font-size: 1.2em; font-weight: bold; cursor: pointer; color: #333;">
        7. Como Cadastrar um Novo Professor (Clique para expandir)
    </summary>
</div>
<div class="card mb-3">
    <details>
        
        <div class="card-body">
            <p>
                Para adicionar um novo professor ao sistema, vá até o menu "Professor" e clique em "Cadastrar Professor". Preencha as informações pessoais e de contato do professor e as de logradouro.
            </p>
            <img src="/Images/cadastrarProfessor.png" alt="Formulário de cadastro de professor" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />
        </div>
    </details>
</div>

 <div class="card-header" style="background-color: #f8f9fa; border-bottom: 1px solid #e3e6ed;">
     <summary style="font-size: 1.2em; font-weight: bold; cursor: pointer; color: #333;">
         8. Como Listar e Gerenciar Professores (Clique para expandir)
     </summary>
 </div>
<div class="card mb-3">
    <details>
       
        <div class="card-body">
            <p>
                Em "Professores" > "Listar Professores", você encontrará a lista de todos os professores. Utilize as opções para editar suas informações ou ver seu histórico.
            </p>
            <img src="/Images/listaProfessor.png" alt="Tela da lista de professores" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />
        </div>
    </details>
</div>

  <div class="card-header" style="background-color: #f8f9fa; border-bottom: 1px solid #e3e6ed;">
      <summary style="font-size: 1.2em; font-weight: bold; cursor: pointer; color: #333;">
          9. Como Gerar Horários para as Turmas (Clique para expandir)
      </summary>
  </div>
<div class="card mb-3">
    <details>
      
        <div class="card-body">
            <p>
              Para gerar um horário, acesse "Horários" > "Gerar Horário". Selecione um horario de inicio e de término, A lista de horários, possibilita desativar ou ativar, e até mesmo editar.
            </p>
            <img src="/Images/gerarHorario1.png" alt="Tela de geração de horários" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />
              <img src="/Images/gerarHorario3.png" alt="Grade de horários preenchida" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />
            <p>
               Essa é uma grade de horários geral, você só associa a uma turma, no cadastro de turmas.
            </p>
            <img src="/Images/gerarHorario2.png" alt="Grade de horários preenchida" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />
        </div>
    </details>
</div>
     <div class="card-header" style="background-color: #f8f9fa; border-bottom: 1px solid #e3e6ed;">
     <summary style="font-size: 1.2em; font-weight: bold; cursor: pointer; color: #333;">
         10. Como Gerenciar Adesões de Planos (Clique para expandir)
     </summary>
 </div>
    <div class="card mb-3">
    <details>
       
        <div class="card-body">
            <p>
                A seção de "Adesões" permite que você crie adesões com frequencias e valores diferentes, e permite que você exclua ou edite adesões <strong>Atenção:</strong> Se uma adesão estiver associada a um aluno, ela não poderá ser excluida, apenas editada.
                Para criar uma adesão, preencha o nome, a frequência (quantidade de vezes que o aluno poderá frequentar a academia por semana) o valor para cada frequencia. Ex:2x na semana R$145,00, e as turmas que poderão se associar a essa adesão. <strong>Atenção:</strong> se a adesão for "VIP" ou seja, o aluno poderá frequentar qualquer turma de acordo com seu nível e horario, é obrigatóriamennte selecionar a caixinha "VIP", e asssociar um valor.
            </p>
            <img src="/Images/adesao1.png" alt="Tela de nova adesão de plano para um aluno" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />
             <img src="/Images/adesao3.png" alt="Tela de nova adesão de plano para um aluno" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />
            <p>
                Você também editar as adesões, o processo será o mesmo
            </p>
            <img src="/Images/adesao2.png" alt="Histórico de adesões de um aluno" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />
        </div>
    </details>
</div>

   <div class="card-header" style="background-color: #f8f9fa; border-bottom: 1px solid #e3e6ed;">
       <summary style="font-size: 1.2em; font-weight: bold; cursor: pointer; color: #333;">
           11. Como Cadastrar e Gerenciar Salas (Clique para expandir)
       </summary>
   </div>
<div class="card mb-3">
    <details>
     
        <div class="card-body">
            <p>
                Para organizar onde as aulas acontecem, você pode criar diferentes salas. Acesse > "Salas" e clique em "Gerar Nova Sala".
                
            </p>
            <img src="/Images/gerenciarSalas.png" alt="Formulário de cadastro de uma nova sala de aula" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />
        </div>
    </details>
</div>
    <div class="card mb-3">
    <details>
        <div class="card-header" style="background-color: #f8f9fa; border-bottom: 1px solid #e3e6ed;">
            <summary style="font-size: 1.2em; font-weight: bold; cursor: pointer; color: #333;">
                12. Como Cadastrar um Novo Usuário do Sistema (Clique para expandir)
            </summary>
        </div>
        <div class="card-body">
            <p>
                Para dar acesso ao sistema para um novo funcionário ou administrador, vá até "Configurações" > "Usuários" e clique em "Novo Usuário".
                Preencha o nome, e-mail, crie uma senha e defina o nível de permissão (ex: Administrador, Professor).
            </p>
            <img src="/Images/cadastrarUsuario.png" alt="Tela de cadastro de um novo usuário do sistema" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />
        </div>
    </details>
</div>

     <div class="card-header" style="background-color: #f8f9fa; border-bottom: 1px solid #e3e6ed;">
     <summary style="font-size: 1.2em; font-weight: bold; cursor: pointer; color: #333;">
         13. Como criar e Editar Usuários (Clique para expandir)
     </summary>
 </div>
<div class="card mb-3">
    <details>
       
        <div class="card-body">
            <p>
                Na seção de  "Usuários", vá até "Cadastrar Usuário", preencha todos os campos e salve
            </p>
              <img src="/Images/cadastrarUsuario.png" alt="Lista de usuários com opções para editar" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />
            <img src="/Images/editarUsuario.png" alt="Lista de usuários com opções para editar" class="img-fluid" style="border: 1px solid #ccc; margin-top: 10px; margin-bottom: 15px;" />
            <p>Para editar, clique em 'editar usuário" ou na mensagem "Olá, Usuário"</p>
        </div>
    </details>
</div>


</asp:Content>
