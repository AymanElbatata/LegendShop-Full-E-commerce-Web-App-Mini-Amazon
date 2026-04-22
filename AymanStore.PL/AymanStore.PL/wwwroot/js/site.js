// Simple cart functionality
//document.querySelectorAll('.product-card').forEach(card => {
//    card.querySelector('.product-image').addEventListener('click', function () {
//        alert('Product details would open here');
//    });
//});

// Search functionality
//document.querySelector('.header-search button').addEventListener('click', function () {
//    const searchTerm = document.querySelector('.header-search input').value;
//    if (searchTerm) {
//        alert(`Searching for: ${searchTerm}`);
//    }
//});

$(document).ready(function () {
    $(".category").hover(
        function () {
            $(this).find(".submenu").stop(true, true).slideDown(200);
        },
        function () {
            $(this).find(".submenu").stop(true, true).slideUp(200);
        }
    );
});

$(document).ready(function () {
    let params = new URLSearchParams(window.location.search);
    // Example: get parameter by name
    let CategorySearchId = params.get("CategorySearchId");
    let MainSearchWord = params.get("MainSearchWord");
    if (MainSearchWord != null) {
        document.getElementById("CategorySearchMenu").value = CategorySearchId;
        document.getElementById("MainSearchWord").value = MainSearchWord;
    }

    $("#CategorySearchMenuBTN").on("click", function () {
        var selectedValue = $("#CategorySearchMenu").val();
        $("#CategorySearchId").val(selectedValue);
    });

});

//window.onload = function () {
//    fetch("https://ipwho.is/")
//        .then(response => response.json())
//        .then(data => {
//            document.getElementById("userCountryName").textContent = data.country;
//        })
//        .catch(error => console.error("Error fetching country:", error));
//};



// <script>
//        // Example: get user country from ipapi.co
//    fetch("https://ipwho.is/")
//            .then(response => response.json())
//            .then(data => {
//        let countryName = data.country;
//    // Send to MVC action using AJAX
//    $.ajax({
//        url: '/Home/SaveCurrentUserCountry',   // your controller/action
//    type: 'POST',
//    data: {currentUserCountry: countryName },
//    success: function (response) {
//        console.log("Server Response:", response);
//                    },
//    error: function (xhr, status, error) {
//        console.error("Error:", error);
//                    }
//                });
//            });
//// document.getElementById("userCountryName").textContent = countryName;
//</script>

let userCountryNameGetElementById = document.getElementById('userCountryNameGetElementById');
if (userCountryNameGetElementById) {
    userCountryNameGetElementById.textContent = sessionStorage.getItem("CurrentUserCountry")
}
