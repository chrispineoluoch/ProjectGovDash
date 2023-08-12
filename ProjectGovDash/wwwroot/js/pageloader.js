$(window).on("load", function () {
    var loader = document.getElementById("loader");
    var wrapper = document.getElementById("wrapper");
    if (loader && wrapper) {
        function hideLoader() {
            loader.style.display = "none";
            wrapper.style.display = "block";
        }
        setTimeout(hideLoader, 3000);
    } else {
        console.error("loader or wrapper element not found");
    }
});
    