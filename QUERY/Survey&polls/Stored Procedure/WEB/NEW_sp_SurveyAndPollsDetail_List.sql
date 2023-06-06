DROP PROCEDURE NEW_sp_SurveyAndPollsDetail_List
GO

/****** Object:  StoredProcedure [dbo].[sp_SurveyAndPollsDetail_List]    Script Date: 4/10/2023 3:18:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-04-10
-- Description	: 
-- =============================================
CREATE PROCEDURE [dbo].[NEW_sp_SurveyAndPollsDetail_List]
	@surveyID	varchar(10)
AS
BEGIN
	SELECT [id]
      ,CAST(QuestionID AS varchar(15)) [QuestionID]
      ,CAST(SurveyID AS varchar(15)) [SurveyID]
      ,CAST(QuestionSeqNo AS varchar(15)) [QuestionSeqNo]
      ,[QuestionDesc]
      ,COALESCE(CAST(ParentQuestionID AS varchar(15)),'') [ParentQuestionID]
      ,COALESCE(CAST(ParentAnswerSeqNo AS varchar(15)),'') [ParentAnswerSeqNo]
  FROM [dbo].[M_SurveyPolls_Detail]
  WHERE SurveyID = @surveyID
  ORDER BY [id] ASC
	--SELECT [id]
 --     ,[QuestionID]
 --     ,[SurveyID]
 --     ,[QuestionSeqNo]
 --     ,[QuestionDesc]
 --     ,[ParentQuestionID]
 --     ,[ParentAnswerSeqNo]
	--FROM [dbo].[M_SurveyPolls_Detail]
	--ORDER BY [id] ASC
END
