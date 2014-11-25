<?php

// includes ----------------------------------------------------------------------------------------------------
include_once('TodoList/TaskRepository.php');
include_once('TodoList/Task.php');
use TodoList\Task;
use TodoList\TaskRepository;

// global ------------------------------------------------------------------------------------------------------

// reqest vars -------------------------------------------------------------------------------------------------

$issetFilterOption = isset($_POST['options']);
$issetTasks = isset($_POST['tasks']);
$issetAdd = isset($_POST['add']);
$issetDelete = isset($_POST['delete']);
$issetUpdate = isset($_POST['update']);

if ($issetTasks){
    $tasks = $_POST['tasks'];
}
if ($issetAdd){
    $add = $_POST['add'];
}
if ($issetDelete){
    $deleteId = $_POST['delete'];
}
if ($issetUpdate){
    $update = $_POST['update'];
}

// vars --------------------------------------------------------------------------------------------------------

$dbhost = 'localhost';
$dbname = 'mytodolist';
$dbuser = 'root';
$dbpass = '';

// -------------------------------------------------------------------------------------------------------------

$taskRepository = new TaskRepository($dbhost, $dbname, $dbuser, $dbpass);

if ($issetFilterOption) {

    echo(json_encode([TaskRepository::ALL, TaskRepository::DONE, TaskRepository::OPEN]));

} else if ($issetTasks) {

    if (TaskRepository::ALL == $tasks){

        $result = $taskRepository->getAll();

    } else {

        $result = $taskRepository->getAllByIsDone(TaskRepository::DONE == $tasks);

    }
    echo(json_encode($result));

} else if ($issetAdd) {

    $task = new Task();
    $task->title = $add['title'];
    $task->description = $add['description'];
    $task->dueDate = $add['dueDate'];
    $task->isDone = 0;

    echo(json_encode(['id' => $taskRepository->addTask($task)]));

} else if ($issetUpdate){
    $task = new Task();
    $task->id = $update['id'];
    $task->title = $update['title'];
    $task->description = $update['description'];
    $task->dueDate = $update['dueDate'];
    $task->isDone = $update['isDone'];

    $taskRepository->updateTask($task);

    echo(json_encode($task));
} else if($issetDelete) {

    echo(json_encode(['success' => $taskRepository->removeTask($deleteId), 'id' => $deleteId]));

}
