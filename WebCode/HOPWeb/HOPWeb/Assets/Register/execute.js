/* Download by http://www.codefans.net*/
// Begin jQuery
$(document).ready(function () {
    /* =Shadow Nav
        -------------------------------------------------------------------------- */
    // Append shadow image to each LI

    $("#nav-shadow li").append('<img class="shadow" src="/wp/wp-content/themes/shixiaobao/gfx/icon/icon_shadow.png" width="81" height="27" alt="" />');

    // Animate buttons, shrink and fade shadow

    $("#nav-shadow li").hover(function () {

        var e = this;

        $(e).find("a").stop().animate({ marginTop: "-8px" }, 200, function () {

            $(e).find("a").animate({ marginTop: "-4px" }, 200);

        });

        $(e).find("img.shadow").stop().animate({ width: "80%", height: "20px", marginLeft: "8px", opacity: 0.25 }, 250);

    }, function () {

        var e = this;

        $(e).find("a").stop().animate({ marginTop: "4px" }, 200, function () {

            $(e).find("a").animate({ marginTop: "0px" }, 200);

        });

        $(e).find("img.shadow").stop().animate({ width: "100%", height: "27px", marginLeft: "0", opacity: 1 }, 250);

    });

    // End jQuery

    $("#pudt-shadow li").append('<img class="shadow" src="/wp/wp-content/themes/shixiaobao/gfx/icon/icon_shadow.png" width="81" height="27" alt="" />');

    // Animate buttons, shrink and fade shadow

    $("#pudt-shadow li").hover(function () {

        var e = this;

        $(e).find("a").stop().animate({ marginTop: "-8px" }, 200, function () {

            $(e).find("a").animate({ marginTop: "-4px" }, 200);

        });

        $(e).find("img.shadow").stop().animate({ width: "80%", height: "20px", marginLeft: "8px", opacity: 0.25 }, 250);

    }, function () {

        var e = this;

        $(e).find("a").stop().animate({ marginTop: "4px" }, 200, function () {

            $(e).find("a").animate({ marginTop: "0px" }, 200);

        });

        $(e).find("img.shadow").stop().animate({ width: "100%", height: "27px", marginLeft: "0", opacity: 1 }, 250);

    });

});