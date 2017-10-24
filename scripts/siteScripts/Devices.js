$().ready(function () {
    console.log("Hhhhh");
    var urlToGetPaginatorValues = '/Device/GetPaginatorValues/)';


    $.ajax({
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        url: urlToGetPaginatorValues,
        success: function (response) {
            console.log(response);
            //$("#previousDevices").prop("disabled", false);
            //$("#nextDevices").prop("disabled", false);

            $("#previousDevices").show ();
            $("#nextDevices").show();

            $("#howManyDevicesPerPage option").filter(function () {
                //may want to use $.trim in here
                return $(this).text() == response.numberOfDevicesPerPage;
            }).prop('selected', true);



            $("#totalNumberOfRegisteredDevices").html('<h2>' + response.totalNumberOfDevicesRegisteredByUser + ' devices registered by you</h2>');
            $("#pageNumbers").show();
            $("#pageNumbers").text('Page ' + (response.currentPage + 1) + ' out of ' + response.maxPages + '.');


            if (response.maxPages != 1)
            {
                console.log("cool");
                if (response.currentPage == 0)
                {
                    $("#previousDevices").prop("disabled", true);
                }

                if (response.maxPages - 1 == response.currentPage || response.maxPages == 0)
                {
                    $("#nextDevices").prop("disabled", true);
                }
            }
            else
            {
                //  if only one page, then no need to show the buttons
                $("#previousDevices").hide();
                $("#nextDevices").hide();
            }
        },
        error: function (response) {
            console.log(response);
        }
    });

    //  change number of displayed items
    $("#howManyDevicesPerPage").change(function () {
        console.log($("#howManyDevicesPerPage").val());

        var urlToChangeNumberOfDevices = '/Device/ChangeNumberOfItemsPerPage/';
        var jsonedNumberOfDevices = { newNumberOfDevicesPerPage: $("#howManyDevicesPerPage").val() };
        $.ajax({
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            url: urlToChangeNumberOfDevices,
            dataType: 'json',
            data: JSON.stringify(jsonedNumberOfDevices),
            success: function (response) {
                console.log(response);
                location.reload();
            },
            error: function (response) {
                console.log(response);
            }
        });
    });

    //  get button's id for pagination

    $("#previousDevices, #nextDevices").click(function () {
        console.log($(this).attr("id"));

        //  depending on who it is in the url - admin or registrar

        var urlToGetToNextPage = '/Device/PaginatedRegisteredDevices/';
        var jsonedButtonName = { callCafeFrom_str: $(this).attr("id") };
        $.ajax({
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            url: urlToGetToNextPage,
            dataType: 'json',
            data: JSON.stringify(jsonedButtonName),
            success: function (response) {
                console.log(response);
                location.reload();
            },
            error: function (response) {
                console.log(response);
            }
        });
    });
});