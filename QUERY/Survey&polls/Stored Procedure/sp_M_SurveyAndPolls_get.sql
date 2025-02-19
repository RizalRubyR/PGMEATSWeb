
/****** Object:  StoredProcedure [dbo].[sp_M_SurveyAndPolls_get]    Script Date: 4/3/2023 1:06:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-02-23
-- Description	: 
-- =============================================
ALTER PROCEDURE [dbo].[sp_M_SurveyAndPolls_get] 
--DECLARE
	@date	date
AS
BEGIN
	Select 
		 SurveyID	  = A.SurveyID
		,SurveyTitle  = A.SurveyTitle
		,SurveyStatus = case when A.SurveyStatus = 0 then
								  'New'
							 when A.SurveyStatus = 1 then
								  'In Progress'
							 else 
								  'Done'
						end
		,CountQ		  = B.CountQ
		,QSeqNo		  = C.QuestionSeqNo
		,QuestionID	  = C.QuestionID
	from M_SurveyPolls_Header A
	INNER JOIN (
		select TOP 100 PERCENT
			surveyID,
			CountQ = MAX(QuestionSeqNo) 
		from M_SurveyPolls_Detail 
		Group by SurveyID
	) B ON B.SurveyID = A.SurveyID
	INNER JOIN (SELECT * from M_SurveyPolls_Detail where QuestionSeqNo = 1) C ON C.SurveyID = A.SurveyID
	WHERE
		@date between A.StartDate and A.EndDate
	ORDER BY SurveyStatus asc
END
