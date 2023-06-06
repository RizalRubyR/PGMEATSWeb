DROP PROCEDURE sp_parentQuestion_Get
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
CREATE PROCEDURE sp_parentQuestion_Get
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
END
GO
