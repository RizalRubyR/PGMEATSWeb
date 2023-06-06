DROP PROCEDURE sp_Surveyandpolls_Edit_Get
GO 

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-04-26
-- Description	: 
-- =============================================
CREATE PROCEDURE sp_Surveyandpolls_Edit_Get
--DECLARE
	@SurveyID	varchar(10) = '9'
AS
BEGIN
	SELECT
		 SurveyID		= A.SurveyID
		,SurveyTitle	= A.SurveyTitle
		,StartDate		= FORMAT(A.StartDate, 'dd MMM yyyy')
		,EndDate		= FORMAT(A.EndDate, 'dd MMM yyyy')
		,ViewChart		= A.ViewChart
	FROM
		M_SurveyPolls_Header A
	WHERE
		SurveyID = @SurveyID
	AND SurveyStatus = 0
END
GO
