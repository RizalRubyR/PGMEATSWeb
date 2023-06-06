DROP PROCEDURE sp_detailandAnswer_del
GO 

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-04-20
-- Description	: 
-- =============================================
CREATE PROCEDURE sp_detailandAnswer_del
--DECLARE
	@id	integer	= 10033
AS
BEGIN
	DECLARE 
		 @SurveyID		VARCHAR(10)
		,@QuestionID	VARCHAR(10)

	SELECT 
		 @SurveyID	 = SurveyID
		,@QuestionID = QuestionID
	FROM 
		M_SurveyPolls_Detail
	WHERE 
		id = @id

	DELETE FROM M_SurveyPolls_Answer WHERE SurveyID = @SurveyID and QuestionID = @QuestionID
	delete from M_SurveyPolls_Detail where id=@id

		
END
GO
