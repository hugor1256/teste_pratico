(function ($) {
    "use strict";

    if (!$ || !$.fn.mask) {
        return;
    }

    $(function () {
        $(".mask-cpf").mask("000.000.000-00");
        $(".mask-date").mask("00/00/0000");
        $(".mask-phone").mask("(00) 00000-0000");
    });
})(window.jQuery);
