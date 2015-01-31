$(document).ready(function () {
    // Input Delete Script's
    $(document).on('click', '.remove-entity', function () {
        var entityID = $(this).attr('data-entity-id');
        var entityModel = $(this).attr('data-model');
        var removeConfirm = $(this).attr('data-confirm');
        if (removeConfirm && removeConfirm !== 'false') {
            var conf = confirm("Remove " + entityModel + " " + entityID + "!");
        } else {
            var conf = true;
        }
        
        if (conf == true) {
            $.ajax({
                method: 'POST',
                url: rootControllerUrl + entityModel + "/remove",
                data: {
                    ContactId: entityID
                },
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).
           success(function (responseData, status, headers, config) {
               console.log("success Ajax delete")
               console.log(responseData)
           }).
           error(function (responseData, status, headers, config) {
               console.log("error Ajax delete")
               console.log(responseData)
           });
        }
    });
    // END
});