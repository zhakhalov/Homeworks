<html>
    <?php
    // includes ----------------------------------------------------------------------------------------------------
    include_once('TodoList/TaskRepository.php');
    include_once('TodoList/Task.php');
    use TodoList\Task;
    use TodoList\TaskRepository;

    // global ------------------------------------------------------------------------------------------------------

    global $debug;

    // reqest vars -------------------------------------------------------------------------------------------------

    $issetId = isset($_GET['id']);
    $taskId = $_GET['id'];

    $issetDelete = isset($_POST['delete']);

    // vars --------------------------------------------------------------------------------------------------------
    $dbhost = 'localhost';
    $dbname = 'mytodolist';
    $dbuser = 'root';
    $dbpass = '';

    $debug = [];

    // -------------------------------------------------------------------------------------------------------------

    $taskRepository = new TaskRepository($dbhost, $dbname, $dbuser, $dbpass);

    if ($issetDelete){
        $issetId = false;
    }

    if ($issetId){
        $task = $taskRepository->getById($taskId);
    }

    $successDelete = $taskRepository->removeTask($taskId);
    ?>
    <head>
        <link href="styles/styles.css" rel="stylesheet" type="text/css">
        <link href="styles/bootstrap.css" rel="stylesheet" type="text/css">
    </head>
    <body>
        <header>
            <h3>Remove task</h3>
        </header>
        <div class="task-list">
            <?php if($issetId): ?>
                <div class="todo-add">
                    <div class="todo-task-content">You sure, you want to detete task "<?= $task->title ?>"?</div>
                    <form method="post">
                        <input type="submit" name="delete" class="btn btn-success" value="Delete task">
                    </form>
                </div>
            <?php endif; ?>
            <?php if ($issetDelete): ?>
                <?= ($successDelete)
                    ? '<div class="todo-task update-sucsess">Task was deleted successfully</div>'
                    : '<div class="todo-task update-error">Error occurred during task deleting!</div>'
                ?>
            <?php endif; ?>
        </div>
        <div class="task-list">
            <div class="todo-task">
                <a href="index.php" class="btn">Back</a>
            </div>
        </div>
    </body>
</html>