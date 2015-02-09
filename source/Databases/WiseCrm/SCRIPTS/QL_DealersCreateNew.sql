USE [WISE_CRM]
GO

/** Local Declareations */
DECLARE @MessageStagesStr NVARCHAR(4000);

/** INITIALIZE */
DECLARE @FirstName NVARCHAR(50);
DECLARE @LastName NVARCHAR(50);
DECLARE @Address NVARCHAR(50);
DECLARE @City NVARCHAR(50);
DECLARE @State CHAR(2);
DECLARE @PostalCode NVARCHAR(10);
DECLARE @Country NVARCHAR(50);
DECLARE @Phone VARCHAR(20);
DECLARE @Email NVARCHAR(250);
DECLARE @Username NVARCHAR(50);
DECLARE @Password NVARCHAR(50);
DECLARE @Sex BIT;
DECLARE @DealerName NVARCHAR(50);
DECLARE @DealerUserID INT;

SET @MessageStagesStr = 'START' + CHAR(13);

BEGIN TRY

	BEGIN TRANSACTION

	SET @FirstName = 'Mike';
	SET @LastName = 'Swint';
	SET @Address = '2917 W Saint Conrad St';
	SET @City = 'Tampa';
	SET @State = 'FL';
	SET @PostalCode = '33607';
	SET @Country = 'USA';
	SET @Phone = '8134044737';
	SET @Email = 'Mike.Swint@freedomsos.com';
	SET @Username = 'MSwint';
	SET @Password = '20Swint!13';
	SET @Sex = 0;
	SET @DealerName = @FirstName + ' ' + @LastName;
	
	SET @MessageStagesStr = @MessageStagesStr + 'CHECK USERNAME' + CHAR(13);
	/** Check that the username is not used */
	IF (EXISTS(SELECT * FROM [WISE_AuthenticationControl].[dbo].AC_Users WHERE Username = @Username))
	BEGIN
		DECLARE @MessageStr VARCHAR(200);
		SET @MessageStr = 'Username ''' + @Username + ''' is already in use.';
		RAISERROR (
			N'ERROR_NUMBER:%d|ERROR_SEVERITY:%d|ERROR_STATE:%d|ERROR_PROCEDURE:%s|ERROR_LINE:%d|ERROR_MESSAGE:%s'
			, 18 -- Severity
			, 1  -- State
			, 2000
			, 1
			, 1
			, 'Create a New Dealer'
			, 38
			, @MessageStr
		);
	END

	SET @MessageStagesStr = @MessageStagesStr + 'CREATE RU_USER' + CHAR(13);
	/** Create an HR User. */
	DECLARE @GPEmployeeID NVARCHAR(20);
	SET @GPEmployeeID = [WISE_HumanResource].dbo.fxRU_UserGenerateGPEmployeeID(@FirstName, @LastName);
	PRINT '@GPEmployeeID = ' + @GPEmployeeID;
	INSERT INTO [WISE_HumanResource].[dbo].[RU_Users] (
		[RecruitedByID]
		,[GPEmployeeID]
		,[UserEmployeeTypeId]
		,[PermanentAddressID]
		,[SSN]
		,[FirstName]
		,[MiddleName]
		,[LastName]
		,[PreferredName]
		,[CompanyName]
		,[MaritalStatus]
		,[SpouseName]
		,[UserName]
		,[Password]
		,[BirthDate]
		,[HomeTown]
		,[Sex]
		,[PhoneHome]
		,[PhoneCell]
		,[Email]
		,[CorporateEmail]
		,[HasVerifiedAddress]
		,[IsLocked]
		,[IsActive]
		,[IsDeleted]
		,[RecruitedDate]
		,[CreatedByID]
		,[CreatedDate]
		,[ModifiedByID]
		,[ModifiedDate]
	) VALUES (
		10 -- Andres Sosa
		, @GPEmployeeID --[WISE_HumanResource].dbo.fxRU_UserGenerateGPEmployeeID(@FirstName, @LastName)
		, 'CORP'
		, NULL
		, 'UG6HPI0/25IGOeRskfS5qw=='
		, @FirstName
		, NULL
		, @LastName
		, @FirstName
		, @FirstName + ' ' + @LastName
		, 0 -- False
		, NULL --<SpouseName, nvarchar(50),>
		, @Username --<UserName, nvarchar(50),>
		, @Password --<Password, nvarchar(50),>
		, NULL --<BirthDate, datetime,>
		, @City --<HomeTown, nvarchar(50),>
		, @Sex --<Sex, tinyint,>
		, @Phone -- <PhoneHome, nvarchar(25),>
		, @Phone -- <PhoneCell, nvarchar(50),>
		, @Email --<Email, nvarchar(100),>
		, @Email --<CorporateEmail, nvarchar(100),>
		, 0 --<HasVerifiedAddress, bit,>
		, 0 --<IsLocked, bit,>
		, 1 --<IsActive, bit,>
		, 0 --<IsDeleted, bit,>
		, GETDATE() --<RecruitedDate, datetime,>
		, 'SYSTEM' --<CreatedByID, nvarchar(50),>
		, GETDATE() --<CreatedDate, datetime,>
		, 'SYSTEM' --<ModifiedByID, nvarchar(50),>
		, GETDATE() --<ModifiedDate, datetime,>
	);
	DECLARE @UserID INT;
	SET @UserID = SCOPE_IDENTITY();
	PRINT '@UserID = ' + CAST(@UserID AS VARCHAR);

	SET @MessageStagesStr = @MessageStagesStr + 'CREATE AC_USER' + CHAR(13);
	INSERT INTO [WISE_AuthenticationControl].[dbo].[AC_Users] (
		[HRUserId]
		,[Username]
		,[Password]
		,[IsActive]
		,[IsDeleted]
		,[CreatedBy]
		,[CreatedOn]
		,[ModifiedBy]
		,[ModifiedOn]
	) VALUES (
		@UserID
		, @Username --<Username, nvarchar(50),>
		, @Password --<Password, nvarchar(50),>
		, 1 --<IsActive, bit,>
		, 0 --<IsDeleted, bit,>
		, 0 --<CreatedByID, nvarchar(50),>
		, GETDATE() --<CreatedDate, datetime,>
		, 0 --<ModifiedByID, nvarchar(50),>
		, GETDATE() --<ModifiedDate, datetime,>
	)
	DECLARE @AcUserID INT
	SET @AcUserID = SCOPE_IDENTITY();

	SET @MessageStagesStr = @MessageStagesStr + 'CREATE AE_Dealers' + CHAR(13);
	/** FOR DEBUGGING 
		PRINT '@DealerName=' + @DealerName + '|Length:' + CAST (LEN(@DealerName) AS VARCHAR);
		PRINT '@FirstName=' + @FirstName + '|Length:' + CAST (LEN(@FirstName) AS VARCHAR);
		PRINT '@LastName=' + @LastName + '|Length:' + CAST (LEN(@LastName) AS VARCHAR);
		PRINT '@Email=' + @Email + '|Length:' + CAST (LEN(@Email) AS VARCHAR);
		PRINT '@Phone=' + @Phone + '|Length:' + CAST (LEN(@Phone) AS VARCHAR);
		PRINT '@Phone=' + @Phone + '|Length:' + CAST (LEN(@Phone) AS VARCHAR);
		PRINT '@Address=' + @Address + '|Length:' + CAST (LEN(@Address) AS VARCHAR);
		PRINT '@City=' + @City + '|Length:' + CAST (LEN(@City) AS VARCHAR);
		PRINT '@State=' + @State + '|Length:' + CAST (LEN(@State) AS VARCHAR);
		PRINT '@PostalCode=' + @PostalCode + '|Length:' + CAST (LEN(@PostalCode) AS VARCHAR);
		PRINT '@Username=' + @Username + '|Length:' + CAST (LEN(@Username) AS VARCHAR);
		PRINT '@Password=' + @Password + '|Length:' + CAST (LEN(@Password) AS VARCHAR);
	*/
	INSERT INTO [WISE_CRM].[dbo].[AE_Dealers] (
		[DealerName]
		,[ContactFirstName]
		,[ContactLastName]
		,[ContactEmail]
		,[PhoneWork]
		,[PhoneMobile]
		,[Address]
		,[City]
		,[StateAB]
		,[PostalCode]
		,[Username]
		,[Password]
	) VALUES (
		@DealerName -- <DealerName, nvarchar(150),>
		, @FirstName --<ContactFirstName, nvarchar(50),>
		, @LastName -- <ContactLastName, nvarchar(50),>
		, @Email -- <ContactEmail, nvarchar(500),>
		, @Phone -- <PhoneWork, char(20),>
		, @Phone -- <PhoneMobile, char(20),>
		, @Address -- <Address, nvarchar(50),>
		, @City -- <City, nvarchar(50),>
		, @State -- <StateAB, char(2),>
		, @PostalCode -- <PostalCode, char(5),>
		, @Username --<Username, nvarchar(500),>
		, @Password -- <Password, nvarchar(20),>
	)
	DECLARE @DealerID INT 
	SET @DealerID = SCOPE_IDENTITY()

	SET @MessageStagesStr = @MessageStagesStr + 'CREATE MC_DealerUsers' + CHAR(13);
	INSERT INTO [WISE_CRM].[dbo].[MC_DealerUsers] (
		[DealerUserTypeId]
		,[DealerId]
		,[AuthUserId]
		,[Firstname]
		,[Lastname]
		,[FullName]
		,[Email]
		,[PhoneWork]
		,[PhoneCell]
		,[ADUsername]
		,[Username]
		,[Password]
	) VALUES (
		4 -- Dealer <DealerUserTypeId, tinyint,>
		, @DealerID -- <DealerId, int,>
		, @AcUserID -- <AuthUserId, int,>
		, @FirstName -- <Firstname, nvarchar(50),>
		, @LastName -- <Lastname, nvarchar(50),>
		, @FirstName + ' ' + @LastName -- <FullName, nvarchar(100),>
		, @Email -- <Email, nvarchar(500),>
		, @Phone --<PhoneWork, varchar(30),>
		, @Phone --<PhoneCell, varchar(20),>
		, SUBSTRING(@Username, 1, 1) + LOWER(@LastName) -- <ADUsername, nvarchar(200),>
		, @Username --<Username, nvarchar(500),>
		, @Password -- <Password, nvarchar(20),>
	)
	SET @DealerUserID = SCOPE_IDENTITY();
	
	PRINT 'SUCCESSFULLY CREATED A NEW DEALER.';
	
	PRINT @MessageStagesStr;

	/** Show the works of thine hands. */
	SELECT * FROM [WISE_HumanResource].[dbo].[RU_Users] WHERE UserID = @UserID;
	SELECT * FROM [WISE_AuthenticationControl].[dbo].[AC_Users] WHERE UserID = @AcUserID;
	SELECT * FROM [WISE_CRM].[dbo].[AE_Dealers] WHERE DealerID = @DealerID;
	SELECT * FROM [WISE_CRM].[dbo].[MC_DealerUsers] WHERE DealerUserID = @DealerUserID;

	COMMIT TRANSACTION

END TRY
BEGIN CATCH

	PRINT @MessageStagesStr;
	ROLLBACK TRANSACTION
	EXEC dbo.wiseSP_ExceptionsThrown;
	
END CATCH