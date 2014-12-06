"Pure" PHP Todolist demo project.
Generating pages via PHP
storing data in MySQL database

db name: mytodolist

table:
    Name : Todolist
 - Id, int, notnull, PK, AI
 - Title, varchar, 64, notnull,
 - Description, varchar, 128, notnull
 - DueDate, varchar, 32, notnull (Why? I don't know. )
 - IsDone, Boolean, notnull
