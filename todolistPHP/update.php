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

        $issetUpdateTask = isset($_POST['update-task']);

        // vars --------------------------------------------------------------------------------------------------------
        $dbhost = 'localhost';
        $dbname = 'mytodolist';
        $dbuser = 'root';
        $dbpass = '';

        $debug = [];

        // -------------------------------------------------------------------------------------------------------------
        $taskRepository = new TaskRepository($dbhost, $dbname, $dbuser, $dbpass);

        if ($issetUpdateTask){

            $task = new Task();
            $task->id = $taskId;
            $task->title = $_POST['title'];
            $task->description = $_POST['description'];
            $task->dueDate = $_POST['due-date'];
            $task->isDone = (isset($_POST['is-done']) && ('Done' == $_POST['is-done'])) ? 1 : 0;

            unset($_GET['id']);
            $issetId = false;

            $successUpdate = $taskRepository->updateTask($task);
        }

        if ($issetId){

            $task = $taskRepository->getById($taskId);
        }


    ?>
    <head>
        <link href="styles/styles.css" rel="stylesheet" type="text/css">
        <link href="styles/bootstrap.css" rel="stylesheet" type="text/css">
    </head>

    <body>
        <header>
            <h3>Edit task</h3>
        </header>
        <div class="task-list">
            <?php if ($issetId || $issetUpdateTask): ?>
                <div class="todo-add">
                    <form method="post">
                        <input type="text" placeholder="Title" name="title" value="<?= $task->title ?>"/>
                        <textarea placeholder="Descrtipion" name="description"><?= $task->description ?></textarea>
                        <input type="text" placeholder="Due Date (dd/mm/yyyy)" name="due-date" value="<?= $task->dueDate ?>"/>
                        <input type="checkbox" name="is-done" value="Done" <?= ($task->isDone) ? 'checked="checked"' : ''?>>Done
                        <input type="submit" name="update-task" class="btn btn-success" value="Update Task" />
                    </form>
                </div>
            <?php endif; ?>
            <?php if (isset($successUpdate)): ?>
                <?= ($successUpdate)
                    ? '<div class="todo-task update-sucsess">Task was updated successfully</div>'
                    : '<div class="todo-task update-error">Error occurred during task updating!</div>'
                ?>
            <?php endif; ?>
            <div class="todo-add">
                <a href="index.php" class="btn">Back</a>
            </div>
        </div>
        <div id="debug">
            <?php foreach($debug as $d): ?>
                <p><?= print_r($d); ?>
            <?php endforeach; ?>
        </div>
    </body>
</html>