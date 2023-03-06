$(document).ready(function() {
    $(".user_profile .top").click(function(e) {

        $(".profile_info_iner").toggleClass("active");
        $(".ic").toggleClass("rotate");
    });

    /********************************************************/

    $(".notification").click(function() {

        $(".Menu_NOtification_Wrap").toggleClass("active");

    });

    /********************************************************/

    $(".hamburger-menu").click(function(e) {

        $(".sidebar").toggleClass("open");
        $(".hamburger-menu").toggleClass("open");


    });

    /*********************************************************/

    // remove all .active classes when clicked anywhere
    hide = true;
    $('body').on("click", function() {
        if (hide) $('.page_banner_contant_extra').removeClass('active');
        hide = true;
    });

    // add and remove .active
    $('body').on('click', '.page_banner_contant_extra', function() {

        var self = $(this);

        if (self.hasClass('active')) {
            $('.page_banner_contant_extra').removeClass('active');
            return false;
        }

        $('.page_banner_contant_extra').removeClass('active');

        self.toggleClass('active');
        hide = false;
    });

    /************************/

    $(document).on('change', '.file-upload input[type="file"]', function() {
        var filename = $(this).val();
        if (/^\s*$/.test(filename)) {
            $(this).parents(".file-upload").find(".file-select-name").text("No file chosen...");
        } else {
            $(this).parents(".file-upload").find(".file-select-name").text(filename.substring(filename.lastIndexOf("\\") + 1, filename.length));
        }

        var uploadFile = $(this);
        var files = !!this.files ? this.files : [];
        if (!files.length || !window.FileReader) return; // no file selected, or no FileReader support

        if (/^image/.test(files[0].type)) { // only image file
            var reader = new FileReader(); // instance of the FileReader
            reader.readAsDataURL(files[0]); // read the local file

            reader.onloadend = function() { // set image data as background of div
                // alert(uploadFile.closest(".file-upload").find('.imagePreview').length);
                uploadFile.closest(".file-upload").find('.imagePreview').css("background-image", "url(" + this.result + ")");
            }
        }
    });
    $(document).on('click', '.file-remove', function(e) {
        e.preventDefault();
        $(this).closest('.file-upload').remove();
        return false;
    });


});


/**************** */

// let del = document.querySelector(".edit .del");
// let popWin = document.querySelector(".delete-container");
// let cancelButton = document.querySelector(".cancel");

// del.addEventListener("click", popIt);

// function popIt() {
//     popWin.classList.remove("unpop");
//     popWin.classList.add("pop");
// }

// cancelButton.addEventListener("click", unPopIt);

// function unPopIt() {
//     popWin.classList.remove("pop");
//     popWin.classList.add("unpop");
// }

// popWin.addEventListener("click", outWin);

// function outWin(e) {
//     if (e.target.id === "delete") {
//         unPopIt();
//     }
// }

$('.del').on('click', function() {
    $('.delete-container').removeClass('pop');
    $('.delete-container').addClass('pop');
});
$('.confirm button.cancel').on('click', function() {
    $('.delete-container').removeClass('pop');
});

$(document).on("click", function(e) {
    if ($(e.target).is(".delete-container .window") === false) {
        $(".delete-container").removeClass("pop");
    }
});
/**********************************************/