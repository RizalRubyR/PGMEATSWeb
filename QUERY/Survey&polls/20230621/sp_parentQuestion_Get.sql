
/****** Object:  StoredProcedure [dbo].[sp_parentQuestion_Get]    Script Date: 6/15/2023 7:14:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-04-11
-- Description	: 
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[sp_parentQuestion_Get]
--DECLARE
	@SurveyID	varchar(10) = 5
AS
BEGIN
	SELECT
		 QuestionID = '0'
		,QuestionDesc = ''
	UNION ALL
	SELECT
		 QuestionID = CONVERT(varchar,QuestionID)
		,QuestionDesc
	FROM 
		M_SurveyPolls_Detail
	WHERE
		SurveyID = @SurveyID
	AND AnswerType = 0
END
