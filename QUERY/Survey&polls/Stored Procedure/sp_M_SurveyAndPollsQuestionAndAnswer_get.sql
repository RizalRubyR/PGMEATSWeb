DROP PROCEDURE sp_M_SurveyAndPollsQuestionAndAnswer_get
go

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2022=02-28
-- Description	: 
-- =============================================
CREATE PROCEDURE sp_M_SurveyAndPollsQuestionAndAnswer_get 
--DECLARE
	 @surveyid	 integer = 1
	,@QuestionID integer = 1
AS
BEGIN
	select
		 C.SurveyID
		,C.SurveyTitle
		,A.QuestionID
		,A.QuestionSeqNo
		,A.QuestionDesc
		,B.AnswerSeqNo
		,B.AnswerDesc
		,A.AnswerType
		,C.ViewChart
	from 
		M_SurveyPolls_Detail A
	INNER JOIN M_SurveyPolls_Answer B ON B.SurveyID = A.SurveyID And B.QuestionID = A.QuestionID
	INNER JOIN M_SurveyPolls_Header C ON C.SurveyID = A.SurveyID
	where 
		A.SurveyID	 = @surveyid
	AND A.QuestionSeqNo = @QuestionID 
END
GO
