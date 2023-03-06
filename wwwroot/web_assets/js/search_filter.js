$(document).ready(function () {
    $(".my-search").on('keyup', function () {
        var isotope = $('.grid').isotope({
            itemSelector: '.grid-item',
        });
        var value = $(this).val().toLowerCase();
        $(".storeslist ul li").each(function () {
            if ($(this).text().toLowerCase().search(value) > -1) {
                $(this).show();
            } else {
                $(this).hide();
            }
            isotope.imagesLoaded().progress(function () {
                isotope.isotope('layout');
            });

        });
        exit();
    });
});