USE [WISE_HumanResource]
GO
DROP PROCEDURE [dbo].[ppCustGetEmployeeInfo];
GO

/****** Object:  StoredProcedure [dbo].[ppCustGetEmployeeInfo]    Script Date: 10/29/2015 7:20:16 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: ppCustGetEmployeeInfo.sql
**		Name: ppCustGetEmployeeInfo
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
**		Auth: Brett Kotter
**		Date: 06/22/2010
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	06/22/2010	Brett Kotter	Created
*******************************************************************************/
CREATE PROCEDURE [dbo].[ppCustGetEmployeeInfo]
(
	@InActive BIT = NULL,
	@FirstName NVARCHAR(50) = NULL,
	@LastName NVARCHAR(50) = NULL,
	@Department CHAR(7) = NULL
)
AS
BEGIN

	SELECT
		'NOTSET' AS [GP ID]
		, @FirstName AS [FirstName]

	--SELECT emp.EMPLOYID AS [GP ID]
	--		, emp.FRSTNAME AS [FirstName]
	--		, emp.MIDLNAME AS [MI]
	--		, emp.LASTNAME AS [LastName]
	--		, emp.INACTIVE
	--		, emp.DEMPINAC AS [InactiveDate]
	--		, CASE emp.MARITALSTATUS 
	--			WHEN 1 THEN 'Married'
	--			WHEN 0 THEN 'Single'
	--		  END AS [Marital Status]
	--		, emp.SOCSCNUM AS [SSN]
	--		, CASE emp.GENDER 
	--			WHEN 1 THEN 'Male'
	--			WHEN 2 THEN 'Female'
	--			WHEN 3 THEN 'N/A'
	--		  END AS [Gender]
	--		, dept.DSCRIPTN AS [Department]
	--		, emp.BRTHDATE AS [BirthDate]
	--		, sup.SUPERVISOR AS [SupervisorName]
	--		, emp.STRTDATE AS [StartDate] 
	--		, CASE emp.EMPLOYMENTTYPE 
	--			WHEN 1 THEN 'FT '
	--			WHEN 2 THEN 'FT Temp'
	--			WHEN 3 THEN 'PT' 
	--			WHEN 4 THEN 'PT Temp'
	--			WHEN 5 THEN 'Intern'
	--			ELSE 'OTHER'
	--		  END AS [Employment Type]
	--		, LTRIM(RTRIM(addr.ADDRESS1)) + ' ' +  LTRIM(RTRIM(addr.Address2)) + ' ' +  LTRIM(RTRIM(addr.Address3)) AS [StreetAddress]
	--		, addr.CITY AS [City]
	--		, addr.STATE AS [State]
	--		, addr.ZIPCODE AS [Zip]
	--		, addr.PHONE1 AS [Phone]
	--		, emp.EMPLCLAS
	--FROM [DYSNEYDAD].[NEX].[dbo].UPR00100 emp WITH(NOLOCK)
	--	LEFT JOIN [DYSNEYDAD].[NEX].[dbo].UPR40301 dept WITH(NOLOCK)
	--	ON 
	--		(dept.JOBTITLE = emp.JOBTITLE)
	--	LEFT JOIN [DYSNEYDAD].[NEX].[dbo].UPR41700 sup WITH(NOLOCK)
	--	ON 
	--		(sup.SUPERVISORCODE_I = emp.SUPERVISORCODE_I)
	--	LEFT JOIN [DYSNEYDAD].[NEX].[dbo].UPR00102 addr WITH(NOLOCK)
	--	ON 
	--		(addr.ADRSCODE = emp.ADRSCODE )
	--		AND (addr.EMPLOYID = emp.EMPLOYID)
	--WHERE -- emp.EMPLCLAS = 'CORP'
	--	(@FirstName IS NULL OR emp.FRSTNAME LIKE '%' + @FirstName + '%')
	--	AND (@LastName  IS NULL OR emp.LASTNAME LIKE '%' + @LastName + '%')
	--	AND (@InActive IS NULL OR emp.INACTIVE = @InActive)
	--	AND (@Department IS NULL OR emp.JOBTITLE = @Department)
END

GO


