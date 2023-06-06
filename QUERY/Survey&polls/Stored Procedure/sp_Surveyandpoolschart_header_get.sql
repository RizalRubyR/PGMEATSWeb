drop procedure sp_Surveyandpoolschart_header_get
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-04-07
-- Description	: 
-- =============================================
CREATE PROCEDURE sp_Surveyandpoolschart_header_get 
--DECLARE
	@surveyID	integer = 2
AS
BEGIN
	SELECT DISTINCT
		 A.SurveyID
		,A.QuestionID
		,A.QuestionDesc
		,B.ViewChart
	FROM 
		M_SurveyPolls_Detail A
	INNER  JOIN M_SurveyPolls_Header B ON B.SurveyID = A.SurveyID
	where 
		A.SurveyID = @SurveyID
	AND A.AnswerType = 0
END
GO
