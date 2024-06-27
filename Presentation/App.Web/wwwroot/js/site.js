// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function showNotificationMessage(type, message) {

    var icon = "info";

    switch (type) {
        case "Success":
            icon = "success";
            break;
        case "Error":
            icon = "error";
            break;
        case "Warning":
            icon = "warning";
            break;
        default:
            icon = "info";
            break;
    }

    $.toast({
        heading: type,
        text: message,
        showHideTransition: 'slide',
        icon: icon,
        position: 'top-right',
        loader: true,  
        loaderBg: '#9EC600'
    })
}
