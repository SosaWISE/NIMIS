USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_SeasonGetCurrentOrVisibleToRecruit')
	BEGIN
		PRINT 'Dropping Procedure custRU_SeasonGetCurrentOrVisibleToRecruit'
		DROP  Procedure  dbo.custRU_SeasonGetCurrentOrVisibleToRecruit
	END
GO

PRINT 'Creating Procedure custRU_SeasonGetCurrentOrVisibleToRecruit'
GO
/******************************************************************************
**		File: custRU_SeasonGetCurrentOrVisibleToRecruit.sql
**		Name: custRU_SeasonGetCurrentOrVisibleToRecruit
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
CREATE Procedure dbo.custRU_SeasonGetCurrentOrVisibleToRecruit
/*
(
)
*/
AS
BEGIN

	IF (EXISTS(SELECT TOP 1
		*
	FROM RU_Season AS RS WITH(NOLOCK)
	WHERE
		RS.IsVisibleToRecruits = 1))
	BEGIN
		SELECT TOP 1
			*
		FROM RU_Season AS RS WITH(NOLOCK)
		WHERE
			RS.IsVisibleToRecruits = 1
		ORDER BY
			RS.IsCurrent DESC	/* Want one that is current if it exists */
			, RS.StartDate DESC	/* If there isn't a current one, use the latest start date*/
	END
	ELSE
	BEGIN
		SELECT TOP 1
			*
		FROM
			dbo.RU_Season AS RS WITH(NOLOCK)
		WHERE
			(GETDATE() BETWEEN RS.StartDate AND RS.EndDate)
		ORDER BY
			RS.IsCurrent DESC	/* Want one that is current if it exists */
			, RS.StartDate DESC	/* If there isn't a current one, use the latest start date*/
	END

END
GO

GRANT EXEC ON dbo.custRU_SeasonGetCurrentOrVisibleToRecruit TO PUBLIC
GO