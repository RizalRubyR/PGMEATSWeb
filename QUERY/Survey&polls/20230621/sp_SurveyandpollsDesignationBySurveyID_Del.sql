SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-06-16
-- Description	: 
-- =============================================
CREATE or ALTER PROCEDURE sp_SurveyandpollsDesignationBySurveyID_Del
	@SurveyID			VARCHAR(10)
AS
BEGIN
	delete from M_SurveyPolls_Designation where SurveyID = @SurveyID
END
GO
