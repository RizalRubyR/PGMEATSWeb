SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-06-14
-- Description	: 
-- =============================================
CREATE or ALTER PROCEDURE sp_SurveyandpollsDepartment_Ins 
	 @SurveyID			VARCHAR(10)
	,@GroupDepartment	VARCHAR(50) 
AS
BEGIN
	insert 
		into M_SurveyPolls_Department
			(SurveyID, GroupDepartment)
		values
			(@SurveyID, @GroupDepartment)
END
GO
