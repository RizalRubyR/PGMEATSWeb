drop procedure sp_detailandanswer_get
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-04-20
-- Description	: 
-- =============================================
CREATE PROCEDURE sp_detailandanswer_get
--DECLARE
	@id	integer = 7
AS
BEGIN
	SELECT
		 QuestionID			= A.QuestionID
		,QuestionDesc		= A.QuestionDesc
		,AnswerType			= A.AnswerType
		,ParentQuestionID	= COALESCE(CONVERT(VARCHAR, A.ParentQuestionID),'')
		,ParentAnswerSeqNo	= COALESCE(CONVERT(VARCHAR, A.ParentAnswerSeqNo),'')
		,AnswerSeqNo		= B.AnswerSeqNo
		,AnswerDesc			= B.AnswerDesc
	FROM
		M_SurveyPolls_Detail A
	INNER 
		JOIN M_SurveyPolls_Answer B ON B.SurveyID = A.SurveyID AND B.QuestionID = A.QuestionID
	WHERE 
		id = @id
END
GO
