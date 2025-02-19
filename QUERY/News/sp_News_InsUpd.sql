GO
/****** Object:  StoredProcedure [dbo].[sp_News_InsUpd]    Script Date: 06/19/2023 11:40:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Fikri
-- Create date	: 
-- Description	: Insert Update for List News (Web)
-- =============================================
CREATE PROCEDURE [dbo].[sp_News_InsUpd]
	 @NewsID			As Int
	,@NewsTitle			As varchar(100)
	,@NewsDescCode		As varchar(MAX)
	,@NewsDescText		As varchar(MAX)
	,@NewsDescImg		As varchar(MAX)
	,@DateFrom			As varchar(10)
	,@DateTo			As varchar(10)
	,@GroupDepartment	As varchar(MAX)
	,@SALPLan			As varchar(MAX)
	,@User				As varchar(50)
AS
BEGIN
	Declare @Msg As Varchar(100)
	Declare @Attach As Varchar(MAX) = ''

	IF Exists(Select Top 1 NewsID From M_News Where NewsID = @NewsID)
		Begin
			Update	M_News
			SET		NewsTitle			= @NewsTitle
				   ,NewsDescriptionCode	= @NewsDescCode
				   ,NewsDescriptionText	= @NewsDescText
				   ,NewsDescriptionImage= @NewsDescImg
				   ,StartDate			= @DateFrom
				   ,EndDate				= @DateTo
				   ,UpdateDate			= GETDATE()
				   ,UpdateUser			= @User
			Where	NewsID = @NewsID

			SET @Msg = 'Successfully Updated data!'
			SET @Attach = (Select top 1 FileName from M_News Where NewsID = @NewsID)
		End
	Else
		Begin
			Insert Into M_News
			([NewsTitle], [NewsDescriptionCode], [NewsDescriptionText], [NewsDescriptionImage], [StartDate], [EndDate], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate])
			Values
			(@NewsTitle, @NewsDescCode, @NewsDescText, @NewsDescImg, @DateFrom, @DateTo, @User, GETDATE(), @User, GETDATE())

			SET @NewsID = (Select Top 1 NewsID From M_News Order By NewsID Desc)
			SET @Msg	= 'Successfully Saved data!'
		End

	--Delete Data di News Department lalu Insert Kembali
	Delete M_News_Department Where NewsID = @NewsID

	Insert Into M_News_Department
	SELECT @NewsID,value
	FROM STRING_SPLIT(@GroupDepartment, ';')
	Where value <> ''

	--Delete Data di News Designation lalu Insert Kembali
	Delete M_News_Designation Where NewsID = @NewsID

	Insert Into M_News_Designation
	SELECT @NewsID,value
	FROM STRING_SPLIT(@SALPLan, ';')
	Where value <> ''

	Select Message = @Msg, ID = @NewsID, OldFile = @Attach
END