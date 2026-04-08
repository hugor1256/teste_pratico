(function (window, document) {
    "use strict";

    function show() {
        var el = document.getElementById("pageLoading");
        if (el) {
            el.className = "page-loading is-visible";
        }
    }

    function hide() {
        var el = document.getElementById("pageLoading");
        if (el) {
            el.className = "page-loading";
        }
    }

    window.PageLoading = {
        show: show,
        hide: hide
    };
})(window, document);
