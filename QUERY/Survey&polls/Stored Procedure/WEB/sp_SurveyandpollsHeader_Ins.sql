DROP PROCEDURE sp_SurveyandpollsHeader_Ins
GO 

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-04-11
-- Description	: 
-- =============================================
CREATE PROCEDURE sp_SurveyandpollsHeader_Ins
	 @SurveyID		varchar(10)
	,@SurveyTitle	varchar(100)
	,@SurveyStatus	char(1)
	,@StartDate		date
	,@EndDate		date
	,@ViewChart		char(1)
	,@CreateUser	varchar(50)
AS
BEGIN
	INSERT INTO M_SurveyPolls_Header
	(
		 SurveyID
		,SurveyTitle
		,SurveyStatus
		,StartDate
		,EndDate
		,ViewChart
		,CreateUser
		,CreateDate
	) VALUES 
	(
		 @SurveyID
		,@SurveyTitle
		,@SurveyStatus
		,@StartDate
		,@EndDate
		,@ViewChart
		,@CreateUser
		,GETDATE()
	)
END
GO
