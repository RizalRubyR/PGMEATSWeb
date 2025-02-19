SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-04-03
-- Description	: 
-- =============================================
ALTER PROCEDURE [dbo].[sp_M_SurveyAndPollsNextQuestion_Ins]
	 @questionID	integer
	,@surveyID		integer
	,@staffID		varchar(50)
	,@answerSeqNo	integer
	,@Type			integer = 0
AS
BEGIN
	IF @Type = 0
		BEGIN
			IF EXISTS(SELECT * FROM M_SurveyPolls_Answer_Staff A WHERE A.StaffID = @staffID AND A.SurveyID = @surveyID AND QuestionID = @questionID)
				BEGIN
					UPDATE
						M_SurveyPolls_Answer_Staff
					SET
						 AnswerSeqNo	= @answerSeqNo
						,Answered_Date	= GETDATE()
					WHERE StaffID = @staffID AND SurveyID = @surveyID AND QuestionID = @questionID
				END
			ELSE
				BEGIN
					INSERT 
						INTO M_SurveyPolls_Answer_Staff
							(QuestionID, SurveyID, StaffID, AnswerSeqNo, Answered_Date) --Register_User, Register_Date, Update_User, Update_Date) 
						VALUES
							(@questionID, @surveyID, @staffID, @answerSeqNo, GETDATE()) --,@staffID,GETDATE(), @staffID, GETDATE())
				END
		END
	ELSE IF @Type = 1
		BEGIN
			INSERT 
				INTO M_SurveyPolls_Answer_Staff
					(QuestionID, SurveyID, StaffID, AnswerSeqNo, Answered_Date) 
				VALUES
					(@questionID, @surveyID, @staffID, @answerSeqNo, GETDATE())
		END
END
