<?php

namespace TodoList;
include_once('ITaskRepository.php');
include_once('SqlHelper.php');
include_once('Task.php');

class TaskRepository implements ITaskRepository{
    // constants -------------------------------------------------------------------------------------------------------

    const ALL  = 'All';
    const DONE = 'Done';
    const OPEN = 'Open';

    // private variables -----------------------------------------------------------------------------------------------
    private $dbhost;
    private $dbuser;
    private $dbpass;
    private $dbname;

    private $connection;

    // public variables ------------------------------------------------------------------------------------------------

    public $lastInsert;
    public $errorCode;

    // constructor -----------------------------------------------------------------------------------------------------

    function __construct($dbhost, $dbname, $dbuser, $dbpass){
        $this->dbhost = $dbhost;
        $this->dbname = $dbname;
        $this->dbuser = $dbuser;
        $this->dbpass = $dbpass;
    }

    // public functions ------------------------------------------------------------------------------------------------

    function getAll(){

        return $this->getTasks(SqlHelper::getSelectAllSql());
    }

    function getAllByIsDone($isDone){
        return $this->getTasks(SqlHelper::getSelectAllByIsDoneSql($isDone));
    }

    function getRange($startId, $endId){
        return $this->getTasks(SqlHelper::getSelectIdRangeSql($startId, $endId));
    }

    function getById($id){

        $this->createConnection();

        $sql = SqlHelper::getSelectIdSql($id);

        $result = mysql_query($sql);

        $tasks = [];

        while ($row = mysql_fetch_array($result, MYSQL_ASSOC)) {

            $task = new Task();
            $task->id = $row['Id'];
            $task->title = $row['Title'];
            $task->description = $row['Description'];
            $task->dueDate = $row['DueDate'];
            $task->isDone = $row['IsDone'];

            $tasks[] = $task;
        }

        mysql_free_result($result);
        $this->killConnection();

        return $tasks[0];

    }

    function addTask($task){
        $this->createConnection();
        mysql_query(SqlHelper::getInsertSql($task));
        $this->lastInsert = mysql_insert_id();
        $this->errorCode = mysql_errno($this->connection);
        $this->killConnection();

        return $this->lastInsert;
    }

    function updateTask($task){
        $this->createConnection();
        mysql_query(SqlHelper::getUpdateSql($task));
        $this->errorCode = mysql_errno($this->connection);
        $this->killConnection();

        return $this->errorCode;
    }

    function removeTask($id){
        $this->createConnection();

        $sql = SqlHelper::getDeleteSql($id);

        mysql_query($sql);

        $sqlError = mysql_errno($this->connection);

        $this->killConnection();

        return 0 == $sqlError;
    }

    // private functions -----------------------------------------------------------------------------------------------

    private function createConnection(){
        $this->connection = mysql_connect($this->dbhost, $this->dbuser, $this->dbpass);
        mysql_select_db($this->dbname);

        if (!$this->connection){
            die('Could not connect... '. mysql_error());
        }
    }

    private function killConnection(){
        mysql_close($this->connection);
    }

    private function getTasks($sql){
        $this->createConnection();

        $result = mysql_query($sql);

        $tasks = [];

        while ($row = mysql_fetch_array($result, MYSQL_ASSOC)) {

            $task = new Task();
            $task->id = $row['Id'];
            $task->title = $row['Title'];
            $task->description = $row['Description'];
            $task->dueDate = $row['DueDate'];
            $task->isDone = $row['IsDone'];

            $tasks[] = $task;
        }

        mysql_free_result($result);
        $this->killConnection();

        return $tasks;
    }
}