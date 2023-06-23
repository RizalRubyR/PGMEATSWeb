SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-06-16
-- Description	: 
-- =============================================
CREATE or ALTER PROCEDURE sp_M_SurveyAndPolls_Designation_Load
AS
BEGIN
	SELECT DISTINCT
		Designation = SALPlan
	FROM 
		M_Designation
END
GO
