DROP PROCEDURE sp_surveypolls_finalized
GO 

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-04-19
-- Description	: 
-- =============================================
CREATE PROCEDURE sp_surveypolls_finalized 
	@SurveyID	VARCHAR(10)
AS
BEGIN
	
	update 
		M_SurveyPolls_Header
	set
		SurveyStatus = 1
	where 
		SurveyID = @SurveyID


END
GO
