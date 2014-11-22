
function tasklist(url) {
    return {
        // fields ------------------------------------------------------------------------------------------------------
        url : url,
        defaultOption : "All",

        // functions ---------------------------------------------------------------------------------------------------
        getElement : function(callback){
            if ('undefined' !== typeof this.$element){
                callback(this.$element.clone())
            }
            $.get( "blank/task.html", function( data ) {
                this.$element = $(data);
                callback(this.$element.clone());
            })
        },

        getTasks : function(filter, callback){
            $.get(this.url, {tasks: filter}, function(data){
                    this.tasks = data;
                    callback(this.tasks);
                },"json");
        },

        getFilterOptions : function(callback){
            $.get(this.url, { options : "a" }, function(data){
                    callback(data);
                },"json"
            );
        }
    }
}
