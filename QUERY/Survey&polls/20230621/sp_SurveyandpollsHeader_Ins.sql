
/****** Object:  StoredProcedure [dbo].[sp_SurveyandpollsHeader_Ins]    Script Date: 6/14/2023 3:01:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-04-11
-- Description	: 
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[sp_SurveyandpollsHeader_Ins]
	 @SurveyID		varchar(10)
	,@SurveyTitle	varchar(100)
	,@SurveyStatus	char(1)
	,@StartDate		date
	,@EndDate		date
	,@ViewChart		char(1)
	,@Type			char(1)
	,@Finalized		char(1)
	,@CreateUser	varchar(50)
AS
BEGIN
if not exists(select * from M_SurveyPolls_Header where SurveyID = @SurveyID)
	begin
		INSERT INTO M_SurveyPolls_Header
		(
			 SurveyID
			,SurveyTitle
			,SurveyStatus
			,StartDate
			,EndDate
			,ViewChart
			,Type
			,Finalized
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
			,@Type
			,@Finalized
			,@CreateUser
			,GETDATE()
		)
	end
else
	begin
		UPDATE M_SurveyPolls_Header
			SET
				 SurveyTitle	 = @SurveyTitle
				,StartDate		 = @StartDate
				,EndDate		 = @EndDate
				,ViewChart		 = @ViewChart
				,Type			 = @Type
				,UpdateUser		 = @CreateUser
				,UpdateDate		 = GETDATE()
			WHERE SurveyID = @SurveyID
	end
END
