 
-- =============================================
-- Author		:Riana
-- Create date	: 
-- Description	:	
-- =============================================
ALTER PROCEDURE [dbo].[sp_MyKY_Ins]
(
     @MyKYLocation		varchar(15),
     @MyKYSpecLocation	varchar(max),
     @MyKYDesc			varchar(max),
     @Section			varchar(50),
     @Shift				varchar(1),
     @CreateUser		varchar(50)
)
AS
BEGIN
	Declare @MyKYID		int,
			@MyKYStatus	char(1) = '0', --(0 inprogress, 1 replayed),
			@CreateDate	datetime = GETDATE(),
			@MyKYDate	varchar(25) = FORMAT(GETDATE(),'yyyy-MM-dd'),
			@KYID_Eats  varchar(50) = ''

	--Generate MyKYID
	IF EXISTS(SELECT * FROM M_MyKY)
	BEGIN
		SELECT @MyKYID = MAX(CAST(MyKYID As Int))  FROM M_MyKY
		SET @MyKYID = @MyKYID + 1

		SET @KYID_Eats = 'KY' + FORMAT(GETDATE(),'yyMM') + format(@MyKYID,'0000')
	END
	ELSE
	BEGIN
		SET @MyKYID = 1
		SET @KYID_Eats = 'KY' + FORMAT(GETDATE(),'yyMM') + format(@MyKYID,'0000')
	END

	--Insert data to MY KY
	INSERT INTO [dbo].[M_MyKY]
           ([MyKYID]
		   ,[KYID_Eats]
           ,[MyKYDate]
           ,[MyKYLocation]
           ,[MyKYSpecLocation]
           ,[MyKYDesc]
           ,[MyKYReply]
           ,[MyKYStatus]
           ,[Evidence]
           ,[CreateUser]
           ,[CreateDate]
           ,[UpdateUser]
           ,[UpdateDate]
		   ,[Section]
		   ,[Shift])
     VALUES
           (@MyKYID
		   ,@KYID_Eats
           ,@MyKYDate
           ,@MyKYLocation 
           ,@MyKYSpecLocation
           ,@MyKYDesc
           ,NULL
           ,@MyKYStatus
           ,NULL
           ,@CreateUser
           ,@CreateDate
           ,@CreateUser
           ,@CreateDate
		   ,@Section
		   ,@Shift)

	--complaintid guna menyimpan Img complaint meggunakan @ComplaintID
	SELECT MyKYID = @MyKYID
END
