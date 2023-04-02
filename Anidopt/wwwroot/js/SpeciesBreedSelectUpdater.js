$(function () {
    $("#species-select").change(function () {
        $.ajax({
            type: "Get",
            url: "/Breeds/ForSpecies?id=" + $(this).val(),
            success: function (data) {
                $("#breed-select").empty();
                $.each(data, function (i, item) {
                    $("#breed-select").append(new Option(item["name"], item["id"]));
                });
            },
            error: function (response) {
                console.log(response.responseText);
            }
        });
    });
});