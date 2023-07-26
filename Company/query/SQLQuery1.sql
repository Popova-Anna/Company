﻿--WITH dept_hierarchy AS (
--    SELECT ID, Name, ParentDepartmentID, 0 AS level
--    FROM TestDB.dbo.Department
--    WHERE ParentDepartmentID IS NULL -- выбираем корневые отделы
--    UNION ALL
--    SELECT d.ID, d.Name, d.ParentDepartmentID, dh.level + 1
--    FROM TestDB.dbo.Department d
--    INNER JOIN dept_hierarchy dh ON d.ParentDepartmentID = dh.id
--)
--SELECT id, name, level
--FROM dept_hierarchy
--ORDER BY level, id;
WITH dept_hierarchy AS (
    SELECT ID, Name, ParentDepartmentID, CONVERT(NVARCHAR(MAX), name) AS path
    FROM TestDB.dbo.Department
    WHERE ParentDepartmentID IS NULL -- выбираем корневые отделы
    UNION ALL
    SELECT d.ID, d.Name, d.ParentDepartmentID, CONCAT(dh.path, ' > ', d.name)
    FROM TestDB.dbo.Department d
    INNER JOIN dept_hierarchy dh ON d.ParentDepartmentID = dh.ID
)
SELECT id, name, path
FROM dept_hierarchy
ORDER BY path;