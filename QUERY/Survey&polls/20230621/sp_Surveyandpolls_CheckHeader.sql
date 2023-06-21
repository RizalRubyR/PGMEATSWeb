SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-06-16
-- Description	: 
-- =============================================
CREATE or ALTER PROCEDURE sp_Surveyandpolls_CheckHeader 
	@SurveyID	VARCHAR(10)
AS
BEGIN
	SELECT * FROM M_SurveyPolls_Header WHERE SurveyID = @SurveyID
END
GO
