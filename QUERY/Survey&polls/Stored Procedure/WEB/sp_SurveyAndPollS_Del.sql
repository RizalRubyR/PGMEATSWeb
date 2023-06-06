DROP PROCEDURE sp_SurveyAndPollS_Del
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-04-26
-- Description	: 
-- =============================================
CREATE PROCEDURE sp_SurveyAndPollS_Del 
	@SurveyID	VARCHAR(10)
AS
BEGIN
	Delete FROM M_SurveyPolls_Header WHERE SurveyID = @SurveyID
	DELETE FROM M_SurveyPolls_Detail WHERE SurveyID = @SurveyID
	DELETE FROM M_SurveyPolls_Answer WHERE SurveyID = @SurveyID
END
GO
