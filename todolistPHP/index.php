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

        $issetAddTask = isset($_POST['add-task']);
        $issetFilter = isset($_POST['apply-filter']);

        // vars --------------------------------------------------------------------------------------------------------
        $dbhost = 'localhost';
        $dbname = 'mytodolist';
        $dbuser = 'root';
        $dbpass = '';

        $filterValues = ['All','Done','Open'];
        $filter = $issetFilter ? $_POST['filter'] : 'All';

        $debug = [];

        // -------------------------------------------------------------------------------------------------------------
        $taskRepository = new TaskRepository($dbhost, $dbname, $dbuser, $dbpass);

        if ($issetAddTask)
        {
            $task = new Task();
            $task->title = $_POST['title'];
            $task->description = $_POST['description'];
            $task->dueDate = $_POST['due-date'];
            $task->isDone = 0;

            $taskRepository->addTask($task);
        }

        if($issetFilter && ('All' != $filter)) {
            $result = $taskRepository->getAllByIsDone($filter == 'Done');
            $debug[] = $result;
        }
        else{
            $result = $taskRepository->getAll();
        }

    ?>
    <head>
        <link href="styles/styles.css" rel="stylesheet" type="text/css">
        <link href="styles/bootstrap.css" rel="stylesheet" type="text/css">
    </head>
    <body>
        <header>
            <h3>TODO-List</h3>
        </header>
        <div class="task-list">
            <form class="todo-task" method="post">
                Filter tasks:
                <select name="filter">
                    <?php foreach($filterValues as $value): ?>
                        <option value="<?= $value?>" <?= ($filter == $value) ? 'selected="selected"' : '' ?>>
                            <?= $value?>
                        </option>
                    <?php endforeach; ?>
                </select>
                <input type="submit" class="btn btn-success" name="apply-filter" value="Apply Filter"/>
            </form>
        </div>
        <div class="task-list">
            <?php foreach($result as $row):?>
                <div class="todo-task">
                    <div class="todo-task-content">
                        <div class="task-header <?= ($row->isDone) ? 'todo-done' : ''?>">
                            <?= $row->title ?>
                        </div>
                        <div class="task-date <?= ($row->isDone) ? 'todo-done' : ''?>">
                            <?= $row->dueDate ?>
                        </div>
                        <div class="task-description <?= ($row->isDone) ? 'todo-done' : ''?>">
                            <?= $row->description ?>
                        </div>
                    </div>
                    <div class="todo-task-content">
                        <a href="update.php?id=<?= $row->id ?>" class="btn btn-success">Edit</a>
                        <a href="delete.php?id=<?= $row->id ?>" class="btn btn-danger">Delete</a>
                    </div>
                </div>
            <?php endforeach;?>
        </div>
        <div class="task-list">
            <h4>Add new task.</h4>
            <div class="todo-add">
                <form method="post">
                    <input type="text" placeholder="Title" name="title"/>
                    <textarea placeholder="Descrtipion" name="description"></textarea>
                    <input type="text" placeholder="Due Date (dd/mm/yyyy)" name="due-date"/>
                    <input type="submit" name="add-task" class="btn btn-success" value="Add Task" />
                </form>
            </div>
        </div>
    <footer>

    </footer>
    <div id="debug">
        <?php foreach($debug as $d){
            print_r($d);
        }?>
    </div>
    </body>
</html>
