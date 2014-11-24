$(function () {
    var cache = {
        options: {},
        $task: undefined,
        $delete: undefined,
        $edit: undefined
    };
    $.ajax({
        url: "api.php",
        type: "POST",
        dataType: "json",
        data: {options: ""},
        success: function (options) {
            cache.options = options;
            cache.options.forEach(function (entry) {
                $("#dl-task-type")
                    .append($("<option></option>")
                        .attr("value", entry)
                        .text(entry));
            });
            $("#dl-task-type").on("change", function () {
                callTasks();
            });
            var callTasks = function () {
                $.ajax({
                    url: "blank/task.html",
                    type: "POST",
                    success: function (element) {
                        cache.$task = $(element);
                        $.ajax({
                            url: "api.php",
                            type: "POST",
                            dataType: "json",
                            data: {tasks: $("#dl-task-type").val()},
                            success: function (tasks) {
                                $("#tasklist").empty();
                                tasks.forEach(function (entry) {
                                    var $task = cache.$task.clone();
                                    $(".task-header", $task)
                                        .text(entry.title)
                                        .addClass((entry.isDone == "1") ? "todo-done" : "");
                                    $(".task-date", $task)
                                        .text(entry.dueDate)
                                        .addClass((entry.isDone == "1") ? "todo-done" : "");
                                    $(".task-description", $task)
                                        .text(entry.description)
                                        .addClass((entry.isDone == "1") ? "todo-done" : "");
                                    $("#tasklist").append($task);
                                    $(".btn-danger", $task).on("click", function () {
                                        cache.$delete.show();
                                        $("#delete-task-name").text(entry.title);
                                        $(".accept", cache.$delete).on("click", function () {
                                            $task.remove();
                                            console.log("Remove task", entry.id);
                                            cache.$delete.hide();
                                            $(this).off("click");
                                        });
                                    });
                                });
                            }
                        });
                    }
                });
            };
            callTasks();
        }
    });

    $.ajax({
        url: "blank/delete.html",
        type: "POST",
        success: function (element) {
            cache.$delete = $(element).hide();
            $(".decline", cache.$delete).on("click", function () {
                cache.$delete.hide();
            })
            $("body").append(cache.$delete);
        }
    });
});
