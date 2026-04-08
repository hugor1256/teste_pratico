<%@ Page Title="EISOL Tarefa 2" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tarefa2.aspx.cs" Inherits="EISOL_TestePraticoWebForms.Tarefa2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        &nbsp;
    </div>
    <div class="row">
        <div class="panel panel-success">
            <div class="panel-heading">
                <h3 class="panel-title">Client Side</h3>
            </div>
            <div class="panel-body">
                <p>
                    Os sistemas baseados na web, usam uma tríade de tecnologia imprescindíveis que são utilizadas junto qualquer linguagem de servidor como o C#. 
                    Essa tríade é constrituída pelos deuses mitológicos HTML, CSS e JAVASCRIPT.
                </p>
                <p>
                    O Client Side conhecemos por lado do cliente. É aquele lado íntimo que todos os usuários de um sistema web usam e abusam sem saber. 
                    Mas você nobre programador(a), sabe o poder do Client Side! Como muitos já devem conhcer, existe o framework JavaScript chamado jQuery que facilita muito o trabalho na manipulação de elementos DOM como validações, máscaras, AJAX, canvas e bla bla bla bla bla.
                    Use esse poder para nos mostrar as suas habilidades com o Client Side!                    
                </p>
                <p>
                    Aliás, por se tratar de um WebForms, você deve saber que a utilização do jQuery não é tão obvia assim. Bem, aqui nós temos somente um &lt;form&gt;.
                    Um controle chamado de MasterPage e suas ContentPages.
                    Isso implica em usar de forma adequada os seletores CSS no jQuery.
                    Agora se você é um cara roots e quer usar o JavaScript puro, fique a vontade para mostrar as suas habilidades mas aviso, pode ser que você tenha de digitar muito mais linhas de código.
                    Apesar do HTML oferecer um certo suporte com atributos de validação, ainda preferimos que você nos mostre o poder do Client Side com scripts.
                </p>
                <p>
                    Entre no código fonte pelo Visual Studio e exerça o seu poder criacional de scripts simples e poderosos.
                </p>
                <p class="text-warning">
                    Atenção nos campos dos formulário para que eles não excedam o tamanho das tabelas do banco!
                </p>
            </div>
        </div>
    </div>

    <%--    
        E se o usuário não preencher os campos obrigatórios desse formulário?
        Com certeza o banco de dados reclamaria e então seremos brindados com uma bela tela... de erro.
        Use o script para não permitir que isso aconteça.
        Ah! Tente não usar scripts inline ou atributos de eventos dos elementos. 
        Tente fazer a coisa realmente bacana que é um script externo!
        Ele está no rodapé desse formulário.
    --%>

    <div class="row">
        <div class="col-md-12">
            <h2>Cadastro de pessoas</h2>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>Seus dados</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-md-6 col-sm-12 col-xs-12">
                            <label>
                                Nome <span class="text-danger">*</span>
                            </label>
                            <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
                            <asp:Label ID="valNome" runat="server" CssClass="text-danger field-error" Style="display:none;">Campo Obrigatório</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12">
                            <label>
                                CPF <span class="text-danger">*</span>
                            </label>
                             <%--Colocar máscara de CPF aqui será um bônus--%>
                            <asp:TextBox ID="txtCpf" runat="server" CssClass="form-control mask-cpf" MaxLength="14"></asp:TextBox>
                            <asp:Label ID="valCpf" runat="server" CssClass="text-danger field-error" Style="display:none;">Campo Obrigatório</asp:Label>
                            <asp:Label ID="valCpfInvalido" runat="server" CssClass="text-danger field-error" Style="display:none;">CPF inválido</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12">
                            <label>
                                RG <span class="text-danger">*</span>
                            </label>
                            <asp:TextBox ID="txtRg" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                            <asp:Label ID="valRg" runat="server" CssClass="text-danger field-error" Style="display:none;">Campo Obrigatório</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12">
                            <label>
                                Telefone
                            </label>
                            <%--Colocar máscara de telefone aqui será um bônus--%>
                            <asp:TextBox ID="txtTelefone" runat="server" CssClass="form-control mask-phone" MaxLength="15"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 col-sm-12 col-xs-12">
                            <label>
                                Email
                            </label>
                            <%--Colocar expressão regular pra validar email aqui será um bônus plus 2.0 Ultimate Edition Deluxe Ultra Master Blaster --%>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
                            <asp:Label ID="valEmail" runat="server" CssClass="text-danger field-error" Style="display:none;">Email inválido</asp:Label>
                        </div>
                        <div class="col-md-3 col-sm-12 col-xs-12">
                            <label>
                                Sexo <span class="text-danger">*</span>
                            </label>
                            <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-control">
                                <asp:ListItem Value="">[Selecione]</asp:ListItem>
                                <asp:ListItem Value="M">Masculino</asp:ListItem>
                                <asp:ListItem Value="F">Feminino</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="valSexo" runat="server" CssClass="text-danger field-error" Style="display:none;">Campo Obrigatório</asp:Label>
                        </div>
                        <div class="col-md-3 col-sm-12 col-xs-12">
                            <label>
                                Data de nascimento <span class="text-danger">*</span>
                            </label>
                            <%--Colocar máscara de data aqui será um bônus--%>
                            <asp:TextBox ID="txtDataNascimento" runat="server" CssClass="form-control mask-date" placeholder="DD/MM/YYYY" MaxLength="10"></asp:TextBox>
                            <asp:Label ID="valDataNascimento" runat="server" CssClass="text-danger field-error" Style="display:none;">Campo Obrigatório</asp:Label>
                            <asp:Label ID="valDataNascimentoInvalida" runat="server" CssClass="text-danger field-error" Style="display:none;">Data inválida</asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <asp:Button ID="btnGravar" runat="server" Text="Gravar" CssClass="btn btn-default" OnClick="btnGravar_Click" />
            <asp:Button ID="btnEstranho" runat="server" Text="Não clique aqui!" CssClass="btn btn-danger" />
            <a class="btn btn-primary" href="Default.aspx">Voltar</a>
        </div>
    </div>
    <div runat="server" visible="false" id="divAlerta">
        <div class="row">
            &nbsp;
        </div>
        <div class="alert alert-success" role="alert">
            <strong>Muito Bom!</strong> Você conseguiu salvar os dados no banco de dados... será? Vou verificar isso depois.
        </div>
    </div>
    <div id="pageLoading" class="page-loading">
        <div class="page-loading-spinner" role="status" aria-hidden="true"></div>
        <div class="page-loading-text">Carregando...</div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.16/jquery.mask.min.js"></script>
    <script src="Scripts/mask-init.js"></script>
    <script src="Scripts/page-loading.js"></script>
    <script src="Scripts/ClientSide/tarefa2.js"></script>
</asp:Content>
