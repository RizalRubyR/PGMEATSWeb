
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-06-14
-- Description	: 
-- =============================================
CREATE or ALTER PROCEDURE sp_SurveyandpollsDepartmentBySurveyID_Del
	@SurveyID			VARCHAR(10)
AS
BEGIN
	delete from M_SurveyPolls_Department where SurveyID = @SurveyID
END
GO
