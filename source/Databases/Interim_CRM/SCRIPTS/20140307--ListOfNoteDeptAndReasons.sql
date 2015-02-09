USE [Platinum_Protection_InterimCRM]
GO

SELECT --TOP 100
	DEPT.NotesDepartmentID AS [Dept ID]
	, DEPT.DepartmentName AS [Dept Name]
	, PRIR.PrimaryReasonID
	, PRIR.Reason AS [1st Reason]
	, SECR.SecondaryReasonID
	, SECR.Reason AS [2nd Reason]
	, ISNULL(SECR.DefaultNote, '') AS [Default Note]
FROM
	[dbo].[MC_AccountNotesDepartment] AS DEPT WITH (NOLOCK)
	INNER JOIN [dbo].[MC_AccountNotesPrimaryReason] AS PRIR WITH (NOLOCK)
	ON
		(PRIR.NotesDepartmentID = DEPT.NotesDepartmentID)
	INNER JOIN [dbo].[MC_AccountNotesSecondaryReason] AS SECR WITH (NOLOCK)
	ON
		(SECR.PrimaryReasonID = PRIR.PrimaryReasonID)