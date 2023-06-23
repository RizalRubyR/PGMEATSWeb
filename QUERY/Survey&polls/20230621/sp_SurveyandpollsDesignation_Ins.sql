
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-06-16
-- Description	: 
-- =============================================
CREATE or ALTER PROCEDURE sp_SurveyandpollsDesignation_Ins
	 @SurveyID			VARCHAR(10)
	,@Designation		VARCHAR(50) 
AS
BEGIN
	insert 
		into M_SurveyPolls_Designation
			(SurveyID, SALPlan)
		values
			(@SurveyID, @Designation)
END
GO
