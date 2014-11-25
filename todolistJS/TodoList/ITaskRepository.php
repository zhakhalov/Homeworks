<?php

namespace TodoList;


interface ITaskRepository{
    function getAll();
    function getAllByIsDone($isDone);
    function getById($id);
    function getRange($startId, $endId);

    function addTask($task);
    function updateTask($task);
    function removeTask($id);
}