$(function () {
    $(".mydatepicker").datepicker({
        dateFormat: "mm/dd/yy",
        changeMonth: true,
        changeYear: true,
        yearRange: "1950:2024",
        timeFormat: '',
        showTimepicker: false
    });
});