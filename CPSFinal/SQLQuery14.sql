USE [C:\USERS\ALIRA\DOCUMENTS\VISUAL STUDIO 2013\PROJECTS\CPSFINAL\CPSFINAL\APP_DATA\ESERVICES.MDF]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[GetAllMandatoryCourses]
		@ipvDeptName = N'cinfdept',
		@ipvStudentId = 1456068

SELECT	@return_value as 'Return Value'

GO
