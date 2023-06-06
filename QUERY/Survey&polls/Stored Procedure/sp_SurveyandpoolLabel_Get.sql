DROP PROCEDURE sp_SurveyandpoolLabel_Get
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-04-09
-- Description	: 
-- =============================================
CREATE PROCEDURE sp_SurveyandpoolLabel_Get
	@SurveyID		VARCHAR(10) = 2
AS
BEGIN
	SELECT Distinct
		 SurveyID
		,AnswerDesc
		,LastCol = (
			SELECT 
				MAX(a.lastCol)
			FROM 
			(
				SELECT Distinct
					 SurveyID
					,AnswerDesc
					,ROW_NUMBER() Over(Order by (select 0)) lastCol
				FROM
					M_SurveyPolls_Answer
				WHERE SurveyID = @surveyID
				GROUP BY SurveyID,AnswerDesc
			) A
			group by 
			 SurveyID
		)
	FROM
		M_SurveyPolls_Answer
	WHERE SurveyID = @surveyID
	order by SurveyID, AnswerDesc
END
GO
