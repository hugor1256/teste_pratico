(function ($) {
    "use strict";

    if (!$ || $.fn.mask) {
        return;
    }

    function applyMask(value, pattern) {
        var digits = (value || "").replace(/\D/g, "");
        var result = "";
        var di = 0;

        for (var i = 0; i < pattern.length; i++) {
            var p = pattern.charAt(i);
            if (p === "0") {
                if (di < digits.length) {
                    result += digits.charAt(di);
                    di++;
                } else {
                    break;
                }
            } else {
                if (di > 0 && di <= digits.length) {
                    result += p;
                }
            }
        }

        return result;
    }

    $.fn.mask = function (pattern) {
        return this.each(function () {
            var $input = $(this);
            if (!$input.is("input, textarea")) {
                return;
            }

            $input.on("input", function () {
                this.value = applyMask(this.value, pattern);
            });

            if ($input.val()) {
                $input.val(applyMask($input.val(), pattern));
            }
        });
    };
})(window.jQuery);
