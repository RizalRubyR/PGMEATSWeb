DROP PROCEDURE sp_parentAnswer_Get
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
CREATE PROCEDURE sp_parentAnswer_Get
	 @SurveyID	  varchar(10)
	,@ParentQuestionID  VARCHAR(10)
AS
BEGIN
	SELECT 
		 AnswerSeqNo = '0'
		,AnswerDesc = ''
	UNION ALL
	SELECT 
		 AnswerSeqNo = CONVERT(varchar,AnswerSeqNo)
		,AnswerDesc
	FROM
		M_SurveyPolls_Answer
	WHERE 
		SurveyID = @SurveyID
	AND QuestionID = @ParentQuestionID
	
END
GO
