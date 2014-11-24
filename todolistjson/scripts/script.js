$(function () {

    var cache = {
        options: {},
        $task: undefined,
        $delete: undefined,
        $update: undefined
    };

    configAddTask();
    callOptions();
    callDeleteElement();
    callUpdateElement();

    function callOptions(){
        $.ajax({
            url: "api.php",
            type: "POST",
            dataType: "json",
            data: {options: ""},
            success: onOptionsResponse
        });
    }

    function callDeleteElement(){
        $.ajax({
            url: "blank/delete.html",
            type: "POST",
            success: onDeleteElementResponce
        });
    }

    function callUpdateElement(){
        $.ajax({
            url: "blank/update.html",
            type: "POST",
            success: onUpdateElementResponce
        });
    }

    function callTaskElement(){
        $.ajax({
            url: "blank/task.html",
            type: "POST",
            success: onTaskElementResponse
        });
    }

    function callTasks(){
        $.ajax({
            url: "api.php",
            type: "POST",
            dataType: "json",
            data: { tasks: $("#dl-task-type").val() },
            success: onTasksResponse
        });
    }

    function configAddTask(){
        var $add = $("#add-task");
        var task = {
            id: 0,
            title: "",
            dueDate: "",
            description: "",
            isDone: false
        };

        $(".txt-title", $add).on("input", function(){                   // on $add-title change
            $(this).css("background", "white");
        });

        $(".txt-date", $add).on("input", function(){                    // on $add-date change
            $(this).css("background", "white");
        });

        $(".txt-date", $add).datepicker({ dateFormat: 'dd/mm/yy' });

        $(".txt-desc", $add).on("input", function(){                    // on $add-desc change
            $(this).css("background", "white");
        });

        $(".btn-add", $add).on("click", function(){                     // on $add-button click
            task.title = $(".txt-title", $add).val();
            task.description = $(".txt-desc", $add).val();
            task.dueDate = $(".txt-date", $add).val();

            var isValid = true;

            if(0 === task.title.length){
                $(".txt-title", $add).css("background", "#FFAAAA");
                isValid = false;
            }
            if(0 === task.dueDate.length){
                $(".txt-date", $add).css("background", "#FFAAAA");
                isValid = false;
            }
            if(0 === task.description.length){
                $(".txt-desc", $add).css("background", "#FFAAAA");
                isValid = false;
            }

            if (!isValid){
                return;
            }

            $.ajax({
                url: "api.php",
                type: "POST",
                dataType: "json",
                data: {add: task},
                success: function(response){
                    task.id = response.id;
                    appendTask(task);
                }
            });
        });
    }

    function configUpdateTask($task, task){
        $(".txt-title", cache.$update).val(task.title);
        $(".txt-desc", cache.$update).val(task.description);
        $(".txt-date", cache.$update).val(task.dueDate);
        $(".chb-done", cache.$update).prop("checked", "1" == task.isDone);

        $(".txt-title", cache.$update).on("input", function(){                  // on $add-title change
            $(this).css("background", "white");
        });

        $(".txt-date", cache.$update).on("input", function(){                   // on $add-date change
            $(this).css("background", "white");
        });

        $(".txt-date", cache.$update).datepicker({ dateFormat: 'dd/mm/yy' });

        $(".txt-desc", cache.$update).on("input", function(){                   // on $add-desc change
            $(this).css("background", "white");
        });

        cache.$update.fadeIn();
        $(".accept", cache.$update).on("click", function () {           // on $update accept click

            var isValid = true;

            if(0 === $(".txt-title", cache.$update).val().length){
                $(".txt-title", cache.$update).css("background", "#FFAAAA");
                isValid = false;
            }
            if(0 === $(".txt-date", cache.$update).val().length){
                $(".txt-date", cache.$update).css("background", "#FFAAAA");
                isValid = false;
            }
            if(0 === $(".txt-desc", cache.$update).val().length){
                $(".txt-desc", cache.$update).css("background", "#FFAAAA");
                isValid = false;
            }

            if (!isValid){
                return;
            }

            task.title = $(".txt-title", cache.$update).val();
            task.description = $(".txt-desc", cache.$update).val();
            task.dueDate = $(".txt-date", cache.$update).val();
            task.isDone = $(".chb-done", cache.$update).prop("checked") ? "1" : "0";    // PHP problems with boolean :((

            $.ajax({
                url: "api.php",
                type: "POST",
                dataType: "json",
                data: {update: task},
                success: function(response){                            // on update response
                    setTaskElement($task, task);
                    $(".accept", cache.$update).off();
                    cache.$update.fadeOut();
                }
            });
        });
    }

    function onOptionsResponse(response){
        cache.options = response;
        cache.options.forEach(function (entry) {                            // add options to select
            $("#dl-task-type")
                .append($("<option></option>")
                    .attr("value", entry)
                    .text(entry));
        });
        $("#dl-task-type").on("change", function () {                       // on $task filter change
            callTasks();
        });
        callTaskElement();
    }

    function onDeleteElementResponce(response){
        cache.$delete = $(response).hide();
        $(".decline", cache.$delete).on("click", function () {              // on $delete decline click
            cache.$delete.fadeOut();
            $(".accept", cache.$delete).off();
        });
        $("body").append(cache.$delete);
    }

    function onUpdateElementResponce(response){
        cache.$update = $(response).hide();
        $(".decline", cache.$update).on("click", function () {              // on $update decline click
            cache.$update.fadeOut();
            $(".accept", cache.$update).off();
        });
        $("body").append(cache.$update);
    }

    function onTaskElementResponse(response){
        cache.$task = $(response);
        callTasks();
    }

    function onTasksResponse(response){
        $("#tasklist").empty();
        response.forEach(appendTask);
    }

    function appendTask(task){
        var $task = cache.$task.clone();
        setTaskElement($task, task);
        $("#tasklist").append($task);
        $task.hide();
        $task.slideDown();
        $(".btn-success", $task).on("click", function(){                    // on $task edit click
            configUpdateTask($task, task);
        });
        $(".btn-danger", $task).on("click", function () {                   // on $task delete click
            cache.$delete.fadeIn();
            $("#delete-task-name").text(task.title);
            $(".accept", cache.$delete).on("click", function () {           // on $delete accept click
                $(".accept", cache.$delete).off();
                $.ajax({
                    url: "api.php",
                    type: "POST",
                    dataType: "json",
                    data: {delete: task.id},
                    success: function(response){                            // on delete response
                        $task.slideUp(400,function(){                       // on $task slideUp
                            $task.remove();
                            console.log("Remove task", response.id);
                            cache.$delete.fadeOut();
                        });
                    }
                });
            });
        });
    }

    function setTaskElement($task, task){
        $(".task-header", $task)
            .text(task.title)
            .removeClass("todo-done")
            .addClass((task.isDone == "1") ? "todo-done" : "");
        $(".task-date", $task)
            .text(task.dueDate)
            .removeClass("todo-done")
            .addClass((task.isDone == "1") ? "todo-done" : "");
        $(".task-description", $task)
            .text(task.description)
            .removeClass("todo-done")
            .addClass((task.isDone == "1") ? "todo-done" : "");
    }
});
