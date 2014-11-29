<?php

// includes ----------------------------------------------------------------------------------------------------
include_once('TodoList/TaskRepository.php');
include_once('TodoList/Task.php');
use TodoList\Task;
use TodoList\TaskRepository;

// global ------------------------------------------------------------------------------------------------------

// reqest vars -------------------------------------------------------------------------------------------------

$request = $_POST;

// vars --------------------------------------------------------------------------------------------------------

$dbhost = 'localhost';
$dbname = 'mytodolist';
$dbuser = 'root';
$dbpass = '';

// -------------------------------------------------------------------------------------------------------------

$handler = new RequestHandler();

$handler->dbhost = 'localhost';
$handler->dbname = 'mytodolist';
$handler->dbuser = 'root';
$handler->dbpass = '';

echo($handler->handle($request));


class RequestHandler {

    public $dbhost = 'localhost';
    public $dbname = 'mytodolist';
    public $dbuser = 'root';
    public $dbpass = '';


    /**
     * @param $request  request to handle
     * $request['method'] - function, witch handle current request
     * $request['params'] - array of request params
     * @return string JSON results of a $request
     */
    public function handle($request){
        return $this->$request['method']($request['params']);
    }

    /**
     * @param $params   filter value
     * $params['tasks'] - filter value
     * @return string   JSON Array of tasks
     */
    function getTasks($params){
        $taskRepository = new TaskRepository($this->dbhost, $this->dbname, $this->dbuser, $this->dbpass);
        if (TaskRepository::ALL == $params['tasks']){
            $result = $taskRepository->getAll();
        } else {
            $result = $taskRepository->getAllByIsDone(TaskRepository::DONE == $params['tasks']);
        }

        return json_encode($result);
    }

    /**
     * @param $params   empty
     * @return string   JSON Array of filter options
     */
    function getOptions($params){
        return json_encode([TaskRepository::ALL, TaskRepository::DONE, TaskRepository::OPEN]);
    }

    /**
     * @param $params   task to add to repository
     * @return string   {id:}   added task id
     */
    function addTask($params){
        $taskRepository = new TaskRepository($this->dbhost, $this->dbname, $this->dbuser, $this->dbpass);

        $task = new Task();
        $task->title = $params['task']['title'];
        $task->description = $params['task']['description'];
        $task->dueDate = $params['task']['dueDate'];
        $task->isDone = 0;

        return json_encode(['id' => $taskRepository->addTask($task)]);
    }

    /**
     * @param $params   deleted task id
     * $params['id']    deleted task id
     * @return string   {id:} deleted task id
     */
    function deleteTask($params){
        $taskRepository = new TaskRepository($this->dbhost, $this->dbname, $this->dbuser, $this->dbpass);
        return json_encode(['success' => $taskRepository->removeTask($params['id']), 'id' => $params['id']]);
    }

    /**
     * @param $params   task to update
     * $params['task']  task to update
     * @return string   JSON updated task
     */
    function updateTask($params){
        $taskRepository = new TaskRepository($this->dbhost, $this->dbname, $this->dbuser, $this->dbpass);

        $task = new Task();
        $task->id = $params['task']['id'];
        $task->title = $params['task']['title'];
        $task->description = $params['task']['description'];
        $task->dueDate = $params['task']['dueDate'];
        $task->isDone = $params['task']['isDone'];

        $taskRepository->updateTask($task);

        return json_encode($task);
    }
}