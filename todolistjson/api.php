<?php

// includes ----------------------------------------------------------------------------------------------------
include_once('TodoList/TaskRepository.php');
include_once('TodoList/Task.php');
use TodoList\Task;
use TodoList\TaskRepository;

// global ------------------------------------------------------------------------------------------------------

// reqest vars -------------------------------------------------------------------------------------------------

$issetFilterOption = isset($_GET['options']);
$issetTasks = isset($_GET['tasks']);
if ($issetTasks){
    $tasks = $_GET['tasks'];
}


// vars --------------------------------------------------------------------------------------------------------
$dbhost = 'localhost';
$dbname = 'mytodolist';
$dbuser = 'root';
$dbpass = '';

// -------------------------------------------------------------------------------------------------------------

if ($issetFilterOption){
    echo(json_encode([TaskRepository::ALL, TaskRepository::DONE, TaskRepository::OPEN]));
} else if ($issetTasks){
    $taskRepository = new TaskRepository($dbhost, $dbname, $dbuser, $dbpass);

    if (TaskRepository::ALL == $tasks){
        $result = $taskRepository->getAll();
    } else {
        $result = $taskRepository->getAllByIsDone(TaskRepository::DONE == $tasks);
    }

    echo(json_encode($result));
}
