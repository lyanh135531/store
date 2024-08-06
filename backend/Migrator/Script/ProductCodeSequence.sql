IF NOT EXISTS (SELECT * FROM sys.sequences WHERE name = 'ProductCodeSequence')
BEGIN
EXEC sp_executesql N'
        CREATE SEQUENCE ProductCodeSequence
        AS INT
        START WITH 1
        INCREMENT BY 1;
    ';
END