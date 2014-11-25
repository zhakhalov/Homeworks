<?php
namespace TodoList;


class SqlHelper {

    public static function getSelectAllSql(){
        return  "SELECT `Id`, `Title`, `Description`, `DueDate`, `IsDone` FROM `Todolist`";
    }

    public static function getSelectAllByIsDoneSql($isDone){
        $GLOBALS['debug'][] = "SELECT `Id`, `Title`, `Description`, `DueDate`, `IsDone` FROM `Todolist` WHERE `IsDone`=" . (($isDone) ? "true" : "false");
        return "SELECT `Id`, `Title`, `Description`, `DueDate`, `IsDone` FROM `Todolist` WHERE `IsDone`=" . (($isDone) ? "true" : "false");
    }

    public static function getSelectIdSql($id){
        return  "SELECT `Id`, `Title`, `Description`, `DueDate`, `IsDone` FROM `Todolist` WHERE `id` = {$id}";
    }

    public static function getSelectIdRangeSql($from, $to){
        return  "SELECT `Id`,`Title`,`Description`,`DueDate`,`IsDone` FROM `Todolist` WHERE `id` IN ({$from}..{$to})";
    }

    public static function getInsertSql($task){
        return
        "INSERT INTO `Todolist`(`Title`,`Description`,`DueDate`,`IsDone`)".
        " VALUES ('{$task->title}','{$task->description}','{$task->dueDate}',{$task->isDone})";
    }

    public static function getUpdateSql($task){
        return
        "UPDATE `Todolist`".
        " SET `Title`='{$task->title}',`Description`='{$task->description}',`DueDate`='{$task->dueDate}',`IsDone`={$task->isDone}".
        " WHERE `Id`={$task->id}";
    }

    public static function getDeleteSql($id){
        return "DELETE FROM `Todolist` WHERE `Id`={$id}";
    }
} 