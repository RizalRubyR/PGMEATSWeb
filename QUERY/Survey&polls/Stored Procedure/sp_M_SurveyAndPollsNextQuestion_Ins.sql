DROP PROCEDURE sp_M_SurveyAndPollsNextQuestion_Ins
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-04-03
-- Description	: 
-- =============================================
CREATE PROCEDURE sp_M_SurveyAndPollsNextQuestion_Ins
	 @questionID	integer
	,@surveyID		integer
	,@staffID		varchar(50)
	,@answerSeqNo	integer
AS
BEGIN
	INSERT 
		INTO M_SurveyPolls_Answer_Staff
			(QuestionID, SurveyID, StaffID, AnswerSeqNo) 
		VALUES
			(@questionID, @surveyID, @staffID, @answerSeqNo)
END
GO
