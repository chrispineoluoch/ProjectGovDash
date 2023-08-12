$(document).ready(function () {
    var profile_data = $('#CapturedProfileData').val();

    if (profile_data == "toggle-show") {
        $('.toast').toast('show');
        $('.homepage').hide();
    } else {
        $('.toast').toast('hide');
        $('.homepage').show();
    }
})
