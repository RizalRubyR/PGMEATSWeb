DROP PROCEDURE sp_SurveyAndPollS_Detail_Upd
GO 

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-04-27
-- Description	: 
-- =============================================
CREATE PROCEDURE sp_SurveyAndPollS_Detail_Upd
--DECLARE
	 @id					VARCHAR(100) = '1'
	,@QuestionID			VARCHAR(10)	 = '1'
	,@SurveyID				VARCHAR(10)	 = '5'
	,@QuestionSeqNo			VARCHAR(10)	 = '1'
	,@QuestionDesc			VARCHAR(250) = 'question 1'
	,@ParentQuestionID		VARCHAR(10)  = '0'
	,@ParentAnswerSeqNo		VARCHAR(10)  = '0'
	,@AnswerType			VARCHAR(1)	 = '0'
AS
BEGIN
	IF @ParentQuestionID = '0'
	BEGIN
		SET @ParentQuestionID = NULL
	END

	IF @ParentAnswerSeqNo = '0'
	BEGIN
		SET @ParentAnswerSeqNo = NULL
	END

	UPDATE
		M_SurveyPolls_Detail
	SET
		 QuestionDesc		= @QuestionDesc
		,ParentQuestionID	= @ParentQuestionID
		,ParentAnswerSeqNo	= @ParentAnswerSeqNo
		,AnswerType			= @AnswerType
	WHERE
		id = @id


END
GO
