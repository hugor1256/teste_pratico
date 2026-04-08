/// <reference path="../../Scripts/jquery-1.10.2.js" />
/// <reference path="../../Scripts/bootstrap.js" />

// Legal né, essas linhas mágicas aí de cima permitem o Visual Studio a montar o Intellisense do jQuery aqui como ele já faz com o C#.
// Não apague elas, elas são do bem.

"use strict"

var TAREFA2 = TAREFA2 || {
    Carregar: () => {
        TAREFA2.BindValidacao();
        $("[id$='_btnEstranho']").on('click', () => {
            return TAREFA2.Autodestruir();
        });
    },
    BindValidacao: () => {
        var $btnGravar = $("[id$='_btnGravar']");
        $btnGravar.off("click.tarefa2").on("click.tarefa2", function (e) {
            if (!TAREFA2.Validar()) {
                e.preventDefault();
                return false;
            }
            if (window.PageLoading && window.PageLoading.show) {
                window.PageLoading.show();
            }
            return true;
        });
    },
    Autodestruir: () => {
        window.alert('Este computador se autodestruirá em 20 segundos...\r\nTodos os seus códigos serão descartados e não poderão ser recuperados.');
        window.setTimeout(() => {
            window.alert('A autodestruição era brincadeira tá!')
        }, 3000);
        return false;
    },
    Validar: ()=> {
        var $nome = $("[id$='_txtNome']");
        var $cpf = $("[id$='_txtCpf']");
        var $rg = $("[id$='_txtRg']");
        var $telefone = $("[id$='_txtTelefone']");
        var $email = $("[id$='_txtEmail']");
        var $sexo = $("[id$='_ddlSexo']");
        var $data = $("[id$='_txtDataNascimento']");

        TAREFA2.OcultarErros();

        var valido = true;

        if (!$.trim($nome.val())) {
            TAREFA2.MostrarErro("valNome");
            valido = false;
        }

        var cpfValor = $.trim($cpf.val());
        if (!cpfValor) {
            TAREFA2.MostrarErro("valCpf");
            valido = false;
        } else if (!TAREFA2.CpfValido(cpfValor)) {
            TAREFA2.MostrarErro("valCpfInvalido");
            valido = false;
        }

        if (!$.trim($rg.val())) {
            TAREFA2.MostrarErro("valRg");
            valido = false;
        }

        if (!$.trim($sexo.val())) {
            TAREFA2.MostrarErro("valSexo");
            valido = false;
        }

        var emailValor = $.trim($email.val());
        if (emailValor && !TAREFA2.EmailValido(emailValor)) {
            TAREFA2.MostrarErro("valEmail");
            valido = false;
        }

        var dataValor = $.trim($data.val());
        if (!dataValor) {
            TAREFA2.MostrarErro("valDataNascimento");
            valido = false;
        } else if (!TAREFA2.DataValida(dataValor)) {
            TAREFA2.MostrarErro("valDataNascimentoInvalida");
            valido = false;
        }

        return valido;
    },
    MostrarErro: (suffixId) => {
        $("[id$='_" + suffixId + "']").show();
    },
    OcultarErros: () => {
        $(".field-error").hide();
    },
    SomenteDigitos: (value) => {
        return (value || "").replace(/\D/g, "");
    },
    CpfValido: (value) => {
        var digits = TAREFA2.SomenteDigitos(value);
        if (digits.length !== 11) {
            return false;
        }
        if (/^(\d)\1{10}$/.test(digits)) {
            return false;
        }

        var sum1 = 0;
        for (var i = 0; i < 9; i++) {
            sum1 += parseInt(digits.charAt(i), 10) * (10 - i);
        }
        var mod1 = (sum1 * 10) % 11;
        if (mod1 === 10) {
            mod1 = 0;
        }
        if (parseInt(digits.charAt(9), 10) !== mod1) {
            return false;
        }

        var sum2 = 0;
        for (var j = 0; j < 10; j++) {
            sum2 += parseInt(digits.charAt(j), 10) * (11 - j);
        }
        var mod2 = (sum2 * 10) % 11;
        if (mod2 === 10) {
            mod2 = 0;
        }

        return parseInt(digits.charAt(10), 10) === mod2;
    },
    EmailValido: (value) => {
        var regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return regex.test(value);
    },
    DataValida: (value) => {
        var parts = value.split("/");
        if (parts.length !== 3) {
            return false;
        }

        var day = parseInt(parts[0], 10);
        var month = parseInt(parts[1], 10);
        var year = parseInt(parts[2], 10);

        if (!day || !month || !year) {
            return false;
        }

        var date = new Date(year, month - 1, day);
        if (date.getFullYear() !== year || date.getMonth() !== (month - 1) || date.getDate() !== day) {
            return false;
        }

        var today = new Date();
        today.setHours(0, 0, 0, 0);
        return date <= today;
    }
}

// Isso aqui são coisas que usamos pra fazer os scripts funcionarem bem com o WebForms
// Pode ser que você tenha um código muito melhor!
// Use este modelo se preferir.

var postBackPage = postBackPage || Sys.WebForms.PageRequestManager.getInstance();

$(document).ready(function () {
    TAREFA2.Carregar();
});

postBackPage.add_endRequest(function () {
    TAREFA2.Carregar();
});
