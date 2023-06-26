SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-06-23
-- Description	: 
-- =============================================
CREATE OR Alter PROCEDURE sp_SurveyAndPollS_Detail_Ins_Validate
	 @SurveyID		Varchar(10)
	,@QuestionID	varchar(10)
	,@ParentQID		varchar(10)
	,@ParentAns		varchar(10)
AS
BEGIN
	SELECT * FROM M_SurveyPolls_Detail
	where 
		SurveyID = @SurveyID 
	AND QuestionID = @QuestionID
	AND ParentQuestionID = @ParentQID
	and ParentAnswerSeqNo = @ParentAns
END
GO
