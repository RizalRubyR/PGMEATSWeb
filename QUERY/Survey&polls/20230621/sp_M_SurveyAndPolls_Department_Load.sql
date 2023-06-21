
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-06-14
-- Description	: 
-- =============================================
CREATE or ALTER PROCEDURE sp_M_SurveyAndPolls_Department_Load

AS
BEGIN
	--select GroupDepartment = ''
	--union all
	select distinct
		 GroupDepartment
		--,Description = Department 
	from M_CostCenter
END
GO
