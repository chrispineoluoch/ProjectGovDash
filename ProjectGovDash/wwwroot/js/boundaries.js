
$(document).ready(function () {

    $.ajax({
        type: "POST",
        url: "/BoundariesDatas/GetRegionBoundariesList",
        contentType: "html",
        success: function (response) {
            $("#Region").empty();
            $("#Region").append(response);
        }
    })
})

$(document).ready(function () {
    $("#Region").change(function () {
        var regionname = $("#Region option:selected").text();

        $.ajax({
            type: "POST",
            url: "/BoundariesDatas/GetCountyBoundariesList?Region=" + regionname,
            contentType: "html",
            success: function (response) {
                console.log(response);
                $("#County").empty();
                $("#County").append(response);
            }
        })
    })
})

$(document).ready(function () {
    $("#County").change(function () {
        var countyname = $("#County option:selected").text();

        $.ajax({
            type: "POST",
            url: "/BoundariesDatas/GetSubCountyBoundariesList?County=" + countyname,
            contentType: "html",
            success: function (response) {
                console.log(response);
                $("#SubCounty").empty();
                $("#SubCounty").append(response);
            }
        })
    })
})

$(document).ready(function () {
    $("#County").change(function () {
        var countyname = $("#County option:selected").text();

        $.ajax({
            type: "POST",
            url: "/BoundariesDatas/GetConstituencyBoundariesList?County=" + countyname,
            contentType: "html",
            success: function (response) {
                console.log(response);
                $("#Constituency").empty();
                $("#Constituency").append(response);
            }
        })
    })
})

$(document).ready(function () {
    $("#Constituency").change(function () {
        var constituencyname = $("#Constituency option:selected").text();

        $.ajax({
            type: "POST",
            url: "/BoundariesDatas/GetWardBoundariesList?Constituency=" + constituencyname,
            contentType: "html",
            success: function (response) {
                console.log(response);
                $("#Ward").empty();
                $("#Ward").append(response);
            }
        })
    })
})