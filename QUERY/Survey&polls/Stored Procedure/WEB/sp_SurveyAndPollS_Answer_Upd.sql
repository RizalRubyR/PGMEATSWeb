DROP PROCEDURE sp_SurveyAndPollS_Answer_Upd
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
CREATE PROCEDURE sp_SurveyAndPollS_Answer_Upd
--DECLARE
	 @SurveyID		VARCHAR(10)
	,@QuestionID	VARCHAR(10)
	,@AnswerSeqNo	VARCHAR(10)
	,@AnswerDesc	VARCHAR(250)
AS
BEGIN
	INSERT INTO M_SurveyPolls_Answer
	(
		 QuestionID
		,SurveyID
		,AnswerSeqNo
		,AnswerDesc
	)
	VALUES
	(
		 @QuestionID
		,@SurveyID
		,@AnswerSeqNo
		,@AnswerDesc
	)
END
GO
