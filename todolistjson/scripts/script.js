var _tasklist = tasklist("api.php");

$(function(){

    _tasklist.getFilterOptions(function(options){
        options.forEach(function(entry){
            $("#filter").append($("<option></option>")
                .attr("value", entry)
                .text(entry)).on('change', function(){
                $(this).val();
            });
        });

        _tasklist.getTasks(_tasklist.defaultOption, function(tasks){
            tasks.forEach(function(entry) {
                _tasklist.getElement(function(element){
                    $(".task-header", element).html(entry.title);
                    $(".task-date", element).html(entry.dueDate);
                    $(".task-description", element).html(entry.description);

                    if (entry.isDone == "1"){
                        $(".task-header", element).addClass("todo-done");
                        $(".task-date", element).addClass("todo-done");
                        $(".task-description", element).addClass("todo-done");
                    }

                    $("#tasklist").append(element);
                })
            });
        });
    });
});
