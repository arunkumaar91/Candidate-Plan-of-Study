DECLARE @ipvDept VARCHAR(20)
SET @ipvDept='CINFDept'

SELECT '' AS Grade,courseRubric,courseNumber,className,credits,'' AS TermEnrolled from @ipvDept cd WHERE mandatory='true'
UNION
 SELECT * INTO #TEMP FROM(SELECT ec.grade,cc.courseRubric,cc.courseNumber,cc.className,cc.credits,ec.termEnrolled+' '+Convert(varchar(10),ec.yearEnrolled) AS Term FROM [C:\USERS\ALIRA\DOCUMENTS\VISUAL STUDIO 2013\PROJECTS\CPSFINAL\CPSFINAL\APP_DATA\ESERVICES.MDF].[dbo].[EnrolledClasses] ec JOIN [C:\USERS\ALIRA\DOCUMENTS\VISUAL STUDIO 2013\PROJECTS\CPSFINAL\CPSFINAL\APP_DATA\COURSECATALOG.MDF].[dbo].CINFDept cc ON  cc.courseNumber=ec.courseNumber WHERE ec.studentId=1456068 AND cc.mandatory=1) 

SELECT *FROM sys.servers




SELECT ec.grade,cc.courseRubric,cc.courseNumber,cc.className,cc.credits,ec.termEnrolled+' '+Convert(varchar(10),ec.yearEnrolled) AS Term FROM [C:\USERS\ALIRA\DOCUMENTS\VISUAL STUDIO 2013\PROJECTS\CPSFINAL\CPSFINAL\APP_DATA\ESERVICES.MDF].[dbo].[EnrolledClasses] ec JOIN [C:\USERS\ALIRA\DOCUMENTS\VISUAL STUDIO 2013\PROJECTS\CPSFINAL\CPSFINAL\APP_DATA\COURSECATALOG.MDF].[dbo].CINFDept cc ON  cc.courseNumber=ec.courseNumber WHERE ec.studentId=1456068 AND cc.mandatory=1
UNION
SELECT '' AS Grade,courseRubric,courseNumber,cd.className,cd.credits,'' AS TermEnrolled from CINFDept cd WHERE courseNumber NOT IN (SELECT ec.courseNumber FROM [C:\USERS\ALIRA\DOCUMENTS\VISUAL STUDIO 2013\PROJECTS\CPSFINAL\CPSFINAL\APP_DATA\ESERVICES.MDF].dbo.EnrolledClasses ec WHERE ec.studentId=1456068) AND cd.mandatory=1 

SELECT ec.courseNumber FROM [C:\USERS\ALIRA\DOCUMENTS\VISUAL STUDIO 2013\PROJECTS\CPSFINAL\CPSFINAL\APP_DATA\ESERVICES.MDF].dbo.EnrolledClasses ec WHERE ec.studentId=1456068

SELECT '' AS Grade,cd.courseRubric,cd.courseNumber,cd.className,cd.credits,'' AS TermEnrolled from CINFDept cd,[C:\USERS\ALIRA\DOCUMENTS\VISUAL STUDIO 2013\PROJECTS\CPSFINAL\CPSFINAL\APP_DATA\ESERVICES.MDF].[dbo].EnrolledClasses ec WHERE cd.courseNumber =ec.courseNumber
SELECT 


SELECT * FROM(SELECT DISTINCT '' AS Grade,courseRubric,courseNumber,className,credits,'' AS TermEnrolled from CINFDept cd WHERE mandatory='true' UNION
 SELECT DISTINCT ec.grade,cc.courseRubric,cc.courseNumber,cc.className,cc.credits,ec.termEnrolled+' '+Convert(varchar(10),ec.yearEnrolled) AS Term FROM [C:\USERS\ALIRA\DOCUMENTS\VISUAL STUDIO 2013\PROJECTS\CPSFINAL\CPSFINAL\APP_DATA\ESERVICES.MDF].[dbo].[EnrolledClasses] ec JOIN [C:\USERS\ALIRA\DOCUMENTS\VISUAL STUDIO 2013\PROJECTS\CPSFINAL\CPSFINAL\APP_DATA\COURSECATALOG.MDF].[dbo].CINFDept cc ON  cc.courseNumber=ec.courseNumber WHERE ec.studentId=1456068 AND cc.mandatory=1)a




