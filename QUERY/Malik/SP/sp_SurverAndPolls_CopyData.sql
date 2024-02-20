
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Malik Ilman
-- Create date: 20 Februari 2024
-- Description:	Copy Data Surver & Polls
-- =============================================
CREATE PROCEDURE [sp_SurverAndPolls_CopyData] 
	-- Add the parameters for the stored procedure here
	@SurveyIDFrom int,
	@SurveyIDTo int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[M_SurveyPolls_Detail]
           ([SurveyID]
		   ,[QuestionID]
           ,[QuestionSeqNo]
           ,[QuestionDesc]
           ,[ParentQuestionID]
           ,[ParentAnswerSeqNo]
           ,[AnswerType])

     SELECT @SurveyIDTo, QuestionID, QuestionSeqNo, QuestionDesc, ParentQuestionID, ParentAnswerSeqNo, AnswerType FROM M_SurveyPolls_Detail where SurveyID = @SurveyIDFrom

	 INSERT INTO [dbo].[M_SurveyPolls_Answer]
           ([SurveyID]
           ,[QuestionID]
           ,[AnswerSeqNo]
           ,[AnswerDesc])
	select @SurveyIDTo, QuestionID, AnswerSeqNo, AnswerDesc from M_SurveyPolls_Answer where SurveyID = @SurveyIDFrom

END
GO
