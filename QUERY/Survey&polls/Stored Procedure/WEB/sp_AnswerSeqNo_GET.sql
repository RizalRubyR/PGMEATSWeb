DROP PROCEDURE sp_AnswerSeqNo_GET
GO

-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-04-17
-- Description	: 
-- =============================================
CREATE PROCEDURE sp_AnswerSeqNo_GET 
	 @SurveyID	 varchar(10)
	,@QuestionID varchar(10)
AS
BEGIN
	SELECT 
		SeqNo = MAX(AnswerSeqNo) + 1
	FROM 
		M_SurveyPolls_Answer 
	WHERE
		SurveyID   = @SurveyID
	AND QuestionID = @QuestionID
END
GO
