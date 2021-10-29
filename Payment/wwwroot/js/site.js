// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(".custom-file-input").on("change", function () {
    //var fileName = $(this).val().split("\\").pop();
    //$(this).siblings(".custom-file-label").addClass("selected").html(fileName);
    var filesLabel = $(this).next(".custom-file-label");
    var files = $(this)[0].files;
    if (files.length > 1) {
        filesLabel.html(files.length + " files selected");
    }
    else if (files.length == 1) {
        filesLabel.html(files[0].name);
    }

});
