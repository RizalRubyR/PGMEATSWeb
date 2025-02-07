DROP PROCEDURE sp_QuestionID_Get
GO 

/****** Object:  StoredProcedure [dbo].[sp_SurveyAndPollsCreate_SurveyID]    Script Date: 4/10/2023 4:42:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-02-16
-- Description	: 
-- =============================================
CREATE PROCEDURE sp_QuestionID_Get
	@SurveyID	varchar(10)
AS
BEGIN
	SELECT Coalesce(MAX(QuestionID),0) + 1 QuestionID FROM M_SurveyPolls_Detail where SurveyID = @SurveyID
END
