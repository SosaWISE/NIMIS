USE [WISE_CRM]
GO
/**********************************************************************************************************************
* DESCRIPTION: This block of code is to be able to synconize Dealers and their RU_Users account in Human Resources
*	database.
* DATE: 09/28/2012
* AUTHOR: Andres Sosa
***********************************************************************************************************************/

	/** Get first name and last name */
	BEGIN TRANSACTION

	DECLARE @DealerID INT;
	SET @DealerID = 5014;  -- Vilmar
	DECLARE @FirstName NVARCHAR(50);
	DECLARE @LastName NVARCHAR(50);
	DECLARE @SalesRepID NVARCHAR(20);
	DECLARE @Username NVARCHAR(500);
	
	SELECT @FirstName = ContactFirstName, @LastName = ContactLastName, @Username = Username FROM [WISE_CRM].[dbo].AE_Dealers WHERE DealerID = @DealerID;
	SET @SalesRepID = [WISE_HumanResource].dbo.fxRU_UserGenerateGPEmployeeID(@FirstName, @LastName);
	
	PRINT 'SalesRepID: ' + @SalesRepID;
	
	/** Update all Leads */
	UPDATE dbo.QL_Leads SET SalesRepId = @SalesRepID WHERE SalesRepId = @Username;
	UPDATE dbo.QL_LeadProductOffers SET SalesRepId = @SalesRepID WHERE SalesRepId = @Username;
	UPDATE [WISE_HumanResource].[dbo].RU_Users SET GPEmployeeID = @SalesRepID WHERE GPEmployeeID = @Username;
	SELECT 
		DLR.DealerID
		, DLR.Username AS DlrUserName
		, DLR.[Password] AS DlrPassword
		, DUR.DealerUserID
		, DUR.Username AS DurUsername
		, DUR.[Password] AS DurPassword
		, ACU.UserID AS AcuUserID
		, ACU.Username AS AcuUsername
		, ACU.[Password] AS AcuPassword
		, HRU.UserID AS HruUserID
		, HRU.UserName AS HruUsername
		, HRU.[Password] AS HruPassword
		, HRU.GPEmployeeID
	FROM
		[WISE_CRM].[dbo].AE_Dealers AS DLR WITH (NOLOCK)
		INNER JOIN [WISE_CRM].[dbo].MC_DealerUsers AS DUR WITH (NOLOCK)
		ON
			(DLR.DealerID = DUR.DealerId)
		LEFT OUTER JOIN [WISE_AuthenticationControl].[dbo].AC_Users AS ACU WITH (NOLOCK)
		ON
			(DUR.AuthUserId = ACU.UserID)
		LEFT OUTER JOIN [WISE_HumanResource].[dbo].RU_Users AS HRU WITH (NOLOCK)
		ON
			(ACU.HRUserId = HRU.UserID)
	ORDER BY 
		DLR.DealerID
	
	ROLLBACK TRANSACTION