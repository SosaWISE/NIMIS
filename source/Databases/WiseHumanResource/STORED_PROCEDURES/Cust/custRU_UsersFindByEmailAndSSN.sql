USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UsersFindByEmailAndSSN')
	BEGIN
		PRINT 'Dropping Procedure custRU_UsersFindByEmailAndSSN'
		DROP  Procedure  dbo.custRU_UsersFindByEmailAndSSN
	END
GO

PRINT 'Creating Procedure custRU_UsersFindByEmailAndSSN'
GO
/******************************************************************************
**		File: custRU_UsersFindByEmailAndSSN.sql
**		Name: custRU_UsersFindByEmailAndSSN
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: 
**		Date: 12/05/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:				Description:
**	-----------	-------------		-------------------------------------------
**	12/05/2013	Carly Christiansen	Created by
*******************************************************************************/
CREATE Procedure dbo.custRU_UsersFindByEmailAndSSN
(
	@Email NVARCHAR(100)
	, @SSN NVARCHAR(50)
	, @SSNEncrypted NVARCHAR(50)
)
AS
BEGIN


	DECLARE @IsDeleted BIT
	SET @IsDeleted = 0


	DECLARE @CompanyID NVARCHAR(25)
	SELECT
		@CompanyID = GPEmployeeID
	FROM RU_Users AS RU WITH(NOLOCK)
	WHERE
		RU.Email = @Email


	SELECT TOP 1
		RU.*
	FROM RU_Users AS RU WITH(NOLOCK)
	INNER JOIN
	(
		-------------------------------------------
		/*Users*/
		SELECT
			GPEmployeeID AS CompanyID
			, NULL AS SSN
			, SSN AS SSNEncrypted
		FROM RU_Users AS RU WITH(NOLOCK)
		WHERE
			RU.GPEmployeeID = @CompanyID
		-------------------------------------------
		UNION ALL
		-------------------------------------------
		/*Sales Reps*/
		SELECT
			VENDORID AS CompanyID
			, REPLACE(TXIDNMBR, '-', '') AS SSN
			, NULL AS SSNEncrypted
		FROM dbo.PM00200 WITH(NOLOCK)
		WHERE
			(VENDORID = @CompanyID)
		-------------------------------------------
		UNION ALL
		-------------------------------------------
		/*Techs*/
		SELECT
			EMPLOYID AS CompanyID
			, REPLACE(SOCSCNUM, '-', '') AS SSN
			, NULL AS SSNEncrypted
		FROM dbo.UPR00100
		WHERE
			(EMPLOYID = @CompanyID)
		-------------------------------------------
	) AS AllUsers
	ON
		RU.GPEmployeeID = AllUsers.CompanyID
	WHERE
		(RU.GPEmployeeID = @CompanyID)
		AND
		(
			/*Encrypted or not encrypted ssn needs to match 1 of the 3*/
			AllUsers.SSN = @SSN
			OR AllUsers.SSNEncrypted = @SSNEncrypted
		)
		AND RU.IsDeleted = @IsDeleted


END
GO

GRANT EXEC ON dbo.custRU_UsersFindByEmailAndSSN TO PUBLIC
GO