<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tarefa3.aspx.cs" Inherits="EISOL_TestePraticoWebForms.Tarefa3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        &nbsp;
    </div>
    <div class="row">
        <div class="panel panel-success">
            <div class="panel-heading">
                <h3 class="panel-title">Server Controls Events</h3>
            </div>
            <div class="panel-body">
                <p>
                    Os controles WebForms possuem a característica dos enventos. Os controles abaixo já são devidamente povoados, mas ainda não funcionam em cascata.
                </p>
                <p>
                    Faça com que ao selecionar a UF, apenas as cidades correspondente àquela UF sejam apresentadas!                
                </p>
                <p>
                    Entre no código fonte pelo Visual Studio e invoque as forças do Leviatã para realizar a cascata perfeita.
                </p>
            </div>
        </div>
    </div>

    <%--    
        Identifique o que faltam nesses controles aqui no HTML e depois verifique o code behind.
    --%>

    <div class="row">
        <div class="col-md-12">
            <h2>Cascatas de Leviatã</h2>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>Faça cair...</h5>
                </div>
                <div class="ibox-content">
                    <asp:UpdatePanel ID="upCascata" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-6 col-sm-12 col-xs-12">
                                    <label>
                                        UF
                                    </label>
                                    <%--DICA: Coloque o evento OnSelectedIndexChanged pra esse cara aqui!--%>
                                    <asp:DropDownList ID="ddlUf" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlUf_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6 col-sm-12 col-xs-12">
                                    <label>
                                        Cidade
                                    </label>
                                    <asp:DropDownList ID="ddlCidades" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlUf" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <a class="btn btn-primary" href="Default.aspx">Voltar</a>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
