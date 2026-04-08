<%@ Page Title="EISOL Tarefa 1" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tarefa1.aspx.cs" Inherits="EISOL_TestePraticoWebForms.Tarefa1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        &nbsp;
    </div>
    <div class="row">
        <div class="panel panel-success">
            <div class="panel-heading">
                <h3 class="panel-title">Server Side</h3>
            </div>
            <div class="panel-body">
                <p>
                    O WebForms é uma tecnologia que facilita muito no desenvolvimento. No entanto ele depende dos componentes que são executados no lado do servidor (Server Side).
                    É fundamental saber utilizar os componentes do servidor e seus eventos para executar as tarefas triviais do WebForms.
                </p>
                <p>
                    Identifique as peças que faltam e coloque-as em seus devidos lugares para esse formulário poder funcionar. 
                </p>
                <p>
                    Entre no código fonte pelo Visual Studio e resolva os códigos místicos do Server Side.
                </p>
                <p class="text-warning">
                    Atenção nos campos dos formulário para que eles não excedam o tamanho das tabelas do banco!
                </p>
            </div>
        </div>
    </div>

    <%--    
        Ah sim é um formulário muito bonito que utiliza o Bootstrap!
        Mas parece que falta alguma coisa faltando nele para você conseguir iniciar.
        Observe atentamente os controles!
        Esses controles também são conhecidos por Server Controls... fazem parte do Server Side (Luke, come to the dark side... =S).
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
                <div>
                    <asp:Label runat="server" CssClass="form-control" ID="msgErro" Visible="false">Erro, verifique o Campo: </asp:Label>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-md-6 col-sm-12 col-xs-12">
                            <label>
                                Nome <span class="text-danger">*</span>
                            </label>
                            <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
                            <asp:Label ID="valNome" runat="server" CssClass="text-danger" Visible="false">Campo Obrigatório</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12">
                            <label>
                                CPF <span class="text-danger">*</span>
                            </label>
                            <asp:TextBox ID="txtCpf" runat="server" CssClass="form-control mask-cpf" MaxLength="14"></asp:TextBox>
                            <asp:Label ID="valCpf" runat="server" CssClass="text-danger" Visible="false">Campo Obrigatório</asp:Label>
                            <asp:Label ID="valCpfInvalido" runat="server" CssClass="text-danger" Visible="false">CPF inválido</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12">
                            <label>
                                RG <span class="text-danger">*</span>
                            </label>
                            <asp:TextBox ID="txtRg" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                            <asp:Label ID="valRg" runat="server" CssClass="text-danger" Visible="false">Campo Obrigatório</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12">
                            <label>
                                Telefone
                            </label>
                            <asp:TextBox ID="txtTelefone" runat="server" CssClass="form-control mask-phone" MaxLength="15"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 col-sm-12 col-xs-12">
                            <label>
                                Email
                            </label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
                            <asp:Label ID="valEmail" runat="server" CssClass="text-danger" Visible="false">Email inválido</asp:Label>
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
                            <asp:Label ID="valSexo" runat="server" CssClass="text-danger" Visible="false">Campo Obrigatório</asp:Label>
                        </div>
                        <div class="col-md-3 col-sm-12 col-xs-12">
                            <label>
                                Data de nascimento <span class="text-danger">*</span>
                            </label>
                            <asp:TextBox ID="txtDataNascimento" runat="server" CssClass="form-control mask-date" placeholder="DD/MM/YYYY" MaxLength="10"></asp:TextBox>
                            <asp:Label ID="valDataNascimento" runat="server" CssClass="text-danger" Visible="false">Campo Obrigatório</asp:Label>
                            <asp:Label ID="valDataNascimentoInvalida" runat="server" CssClass="text-danger" Visible="false">Data inválida</asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <asp:Button ID="btnGravar" runat="server" Text="Gravar" CssClass="btn btn-default" OnClick="btnGravar_Click" OnClientClick="return PageLoading.show();" />
            <a class="btn btn-primary" href="Default.aspx">Voltar</a>
        </div>
    </div>
    <div runat="server" visible="false" id="divAlerta">
        <div class="row">
            &nbsp;
        </div>
        <div class="alert alert-success" role="alert">
            <strong>Muito Bom!</strong> Você conseguiu salvar os dados no banco de dados... será? Vou verificar isso depois :p.
        </div>
    </div>

    <div id="pageLoading" class="page-loading">
        <div class="page-loading-spinner" role="status" aria-hidden="true"></div>
        <div class="page-loading-text">Carregando...</div>
    </div>

</asp:Content>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="ScriptContent" runat="server">
    <link href="Content/page-loading.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.16/jquery.mask.min.js" type="text/javascript"></script>
    <script src="Scripts/mask-init.js" type="text/javascript"></script>
    <script src="Scripts/page-loading.js" type="text/javascript"></script>
</asp:Content>
