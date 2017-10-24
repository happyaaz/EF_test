//  if a user wants to remove a device
function deleteDevice(id) {
    var r = confirm("Are you sure you want to delete this record?");
    if (r == true) {
        window.location.assign("/Device/DeleteDevice/" + id);
    }
}

function deleteClient(id) {
    var r = confirm("Are you sure you want to delete this record?");
    if (r == true) {
        window.location.assign("/Admin/DeleteClient/" + id);
    }
}

function deleteCategory(id) {
    var r = confirm("Are you sure you want to delete this record?");
    if (r == true) {
        window.location.assign("/Categories/DeleteCategory/" + id);
    }
}

function addCategory() {
    console.log("Add new category: " + $("#lastCategoryId").html());
    $('#addNewCategory').hide();
    var newCategoryId = $("#lastCategoryId").html();

    var newCategoryLabel = '<label id="label_category_' + newCategoryId + '" style="display: none;"></label>';
    var newCategoryInputField = '<input autofocus id="input_category_' + newCategoryId + '" type="text" value="" />';
    var td_categoryName = '<tr><td style="text-align:center;">' + newCategoryLabel + newCategoryInputField + '</td>';

    var td_categoryEdit = '<td style="text-align:center;"><a class="pointer-arrow" onclick="editCategory(' +
        newCategoryId + ')">Edit</a></td>';
    var td_categoryDelete = '<td style="text-align:center;"><a class="pointer-arrow" onclick="deleteCategory('
    + newCategoryId + ')">Delete</a></td></tr>';
    //  append new row
    $('#categoriesTable tr:last').after(td_categoryName);
    
    var enterWasPressed = false;
    $("#input_category_" + newCategoryId).keypress(function (e) {
        var key = e.which;
        if (key == 13)  // the enter key code
        {
            console.log("1");
            enterWasPressed = true;
            ifNeedToAddCategory(newCategoryId);
        }
    });
    //  do something with new stuff
    $(document).on('blur', '#input_category_' + newCategoryId, function () {
        if (enterWasPressed == false)
        {
            if (!$(this).data("run-once")) {

                ifNeedToAddCategory(newCategoryId);
                $(this).data("run-once", true);
            }
        }
    });
}

function ifNeedToAddCategory(newCategoryId) {
    //  get new category name
    var newCategoryName = $("#input_category_" + newCategoryId).val();
    $("#input_category_" + newCategoryId).hide();
    $("#label_category_" + newCategoryId).show();
    categoryBeingEdited = false;
    console.log('to add');
    $("#label_category_" + newCategoryId).text(newCategoryName);

    var urlToAddCategory = '/Categories/AddNewCategory/';
    var jsonedCategory = { id: newCategoryId, categoryName: newCategoryName };

    $.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        url: urlToAddCategory,
        dataType: 'json',
        data: JSON.stringify(jsonedCategory),
        success: function (response) {
            console.log(response);
            location.reload();
        },
        error: function (response) {
            console.log(response);
        }
    });
    $('#addNewCategory').show();
}


var editedCategoryId;
var categoryBeingEdited = false;

function editCategory(id) {

    editedCategoryId = id;
    //console.log("editedCategoryId = " + editedCategoryId);
    var oldCategoryName = $("#label_category_" + editedCategoryId).html();
    
    $("#label_category_" + editedCategoryId).hide();
    $("#input_category_" + editedCategoryId).show();
    categoryBeingEdited = true;

    if (categoryBeingEdited == true)
    {
        $("#input_category_" + editedCategoryId).focus();
        var enterWasPressed = false;
        $("#input_category_" + editedCategoryId).keypress(function (e) {
            var key = e.which;
            if (key == 13)  // the enter key code
            {
                console.log("1");
                enterWasPressed = true;
                ifNeedToEditCategory(oldCategoryName);
            }
        });
        $("#input_category_" + editedCategoryId).focusout(function () {
            if (enterWasPressed == false)
            {
                console.log("2");
                ifNeedToEditCategory(oldCategoryName);
            }
            stopBinding("#input_category_" + editedCategoryId);
        });
    }
}


function ifNeedToEditCategory(oldCategoryName)
{
    console.log('to edit');
    categoryBeingEdited = false;
    //  show new stuff
    $("#input_category_" + editedCategoryId).hide();
    var newCategoryName = $("#input_category_" + editedCategoryId).val();
    $("#label_category_" + editedCategoryId).show();

    if (newCategoryName != oldCategoryName) {
        console.log("newCategoryName = " + newCategoryName + ", oldName = " + oldCategoryName);
        $("#label_category_" + editedCategoryId).text(newCategoryName);

        //  update with ajax
        var urlToUpdateCategory = '/Categories/EditCategory/';
        var jsonedCategory = { id: editedCategoryId, newCategoryName: newCategoryName };

        $.ajax({
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            url: urlToUpdateCategory,
            dataType: 'json',
            data: JSON.stringify(jsonedCategory),
            success: function (response) {
                console.log(response);
            },
            error: function (response) {
                console.log(response);
            }
        });
    }
}


function stopBinding(id) {
    $(id).off();
}

$().ready(function () {
    console.log("Aoaoaoa");
    //  for making cascading dropdowns for categories and subcategories
    /*
    var urlToGetSubcategories = '/Device/GetSubcategories/)';
    $("#deviceCategory").change(function () {
        console.log("HUUUUT" +urlToGetSubcategories);
        console.log($("#deviceCategory").val());
        var dataData = {
            "categoryId": $("#deviceCategory").val()
        };
        $.ajax({
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            url: urlToGetSubcategories,
            dataType: 'json',
            data: JSON.stringify(dataData),
            success: function (response) {
                console.log(response.subcategories);
                var items = '<option>Select a subcategory</option>';
                $.each(response.subcategories, function (i, subcategory) {
                    items += "<option value='" + subcategory + "'>" + subcategory + "</option>";
                    // state.Value cannot contain ' character. We are OK because state.Value = cnt++;
                });
                $('#DeviceSubcategory').html(items);
            },
            error: function (response) {
                console.log(response);
            }
        });
    });
    */

    //  hide or display the carrier
    $("#deviceCategory").change(function () {
        console.log($("#deviceCategory").val());

        var chosenCategory = $("#deviceCategory").val();
        if (chosenCategory == "iPhone" || chosenCategory == "Samsung smartphones" || chosenCategory == "Other phones")
        {
            $("#phoneCarrier").show();
        }
        else
        {
            $("#phoneCarrier").hide();
        }
    });

    //  change number of displayed items
    $("#howManyDevicesPerPage").change(function () {
        console.log($("#howManyDevicesPerPage").val());
    });

    //  get button's id for pagination

    $("#previousDevices, #nextDevices").click(function () {
        console.log($(this).attr("id"));

        //  depending on who it is in the url - admin or registrar

        var urlToGetToNextPage = '/Admin/PaginatedRegisteredDevices/';
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