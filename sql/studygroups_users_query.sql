SELECT sg.*
FROM StudyGroups sg
JOIN StudyGroupUsers sgu ON sg.StudyGroupId = sgu.StudyGroupId
JOIN Users u ON sgu.UserId = u.UserId
WHERE u.Name LIKE 'M%'
ORDER BY sg.CreateDate;