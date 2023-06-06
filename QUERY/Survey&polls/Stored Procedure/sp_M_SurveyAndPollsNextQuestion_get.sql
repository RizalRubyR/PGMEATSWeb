drop procedure sp_M_SurveyAndPollsNextQuestion_get
GO 

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2022-04-03
-- Description	: 
-- =============================================
CREATE PROCEDURE sp_M_SurveyAndPollsNextQuestion_get 
--DECLARE
	 @surveyid	integer		= 1
	,@parentsq	integer		= 2
	,@answersq	integer		= 1
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
		,A.ParentQuestionID
		,C.ViewChart
	from 
		M_SurveyPolls_Detail A
	inner JOIN M_SurveyPolls_Answer B ON B.SurveyID = A.SurveyID And B.QuestionID = A.QuestionID 
	INNER JOIN M_SurveyPolls_Header C ON C.SurveyID = A.SurveyID
	where 
		A.SurveyID	 = @surveyid
	AND A.QuestionSeqNo = @parentsq
	--AND A.ParentAnswerSeqNo = @answersq
END
GO
