DROP PROCEDURE sp_QuestionSeqNo_Get
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-04-17
-- Description	: 
-- =============================================
CREATE PROCEDURE sp_QuestionSeqNo_Get
--DECLARE
	 @SurveyID	 varchar(10) = '5'
	,@ParentQID	 varchar(10) = '0'
AS
BEGIN
	select 
		SeqNo = 
			case 
				when (COALESCE(@ParentQID,0) = '0') then 
					COALESCE(MAX(QuestionSeqNo),0) + 1 
				else 
					case 
						when exists(select * from M_SurveyPolls_Detail where SurveyID = @SurveyID and COALESCE(ParentQuestionID,'0') = @ParentQID ) then
							COALESCE(MAX(QuestionSeqNo),0)
						else
							COALESCE(MAX(QuestionSeqNo),0) + 1 
					end
			end
	from 
		M_SurveyPolls_Detail 
	where 
		SurveyID = @SurveyID
END
GO
