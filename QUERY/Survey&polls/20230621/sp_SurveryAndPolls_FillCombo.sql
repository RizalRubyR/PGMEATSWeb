
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-06-21
-- Description	: 
-- =============================================
CREATE or ALTER PROCEDURE sp_SurveryAndPolls_FillCombo
	@Type		As varchar(1)
AS
BEGIN
--Filter Group Department / Target Participant
	IF @Type = '0'
	Begin
		Select Code, Description
		from
		(
			Select 1 No, 'ALL' Code, 'ALL' Description
			Union  ALL
			select distinct 2 No, GroupDepartment, GroupDepartment from M_CostCenter
		) A
		order by A.No, A.Description
	End

--Filter Designation
	IF @Type = '1'
	Begin
		Select Code, Description
		from
		(
			Select 1 No, 'ALL' Code, 'ALL' Description
			Union  ALL
			select distinct 2 No, SALPlan, SALPlan from M_Designation
		) A
		order by A.No, A.Description
	End

--FillCombo Group Department / Target Participant
	IF @Type = '2'
	Begin
		select distinct GroupDepartment Code, GroupDepartment Description 
		from	M_CostCenter
		order by Description
	End

--FillCombo Designation
	IF @Type = '3'
	Begin
		select distinct SALPlan Code, SALPlan Description 
		from	M_Designation
		order by Description
	End
END
GO
