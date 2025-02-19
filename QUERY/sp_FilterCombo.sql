GO
/****** Object:  StoredProcedure [dbo].[sp_FilterCombo]    Script Date: 06/14/2023 08:01:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author		:Riana
-- Create date	: 
-- Description	:	
-- =============================================
CREATE PROCEDURE [dbo].[sp_FilterCombo]
	@Type	varchar(30)
AS
BEGIN
--List status complaint
	if @Type = 'StatusComplaint'
	Begin
		Select Code, Description
		from(
			Select 1 No, 'ALL' Code, 'ALL' Description
			union ALL
			Select 2 No, '0' Code, 'In Progress' Description
			Union  ALL
			Select 3 no, '1' Code, 'Replied' Description
			) A
		order by A.No
	End

--List master issue type
	else if @Type = 'IssueType'
	Begin
		SELECT Code = IssueTypeID, Description = IssueTypeDescription
		FROM M_IssueType
		WHERE ActiveStatus = '1'
		ORDER BY Description
	End
	
--List master session
	else if @Type = 'Session'
	Begin
		SELECT Code = SessionID, Description = SessionName
		FROM M_Session
		WHERE ActiveStatus = '1'
		ORDER BY Description
	End

--List master location
	else if @Type = 'Location'
	Begin
		SELECT Code = LocationID, Description = LocationName
		FROM M_Location
		WHERE ActiveStatus = '1'
		ORDER BY Code
	End

--List master CostCenter
	else if @Type = 'CostCenter'
	Begin
		Select Code, Description
		from(
			Select 1 No, 'ALL' Code, 'ALL' Description
			union ALL
			Select 2 No, Code = Department, Description = Department
			FROM M_CostCenter
			) A
		order by A.No, A.Code
	End

	--List master CostCenter
	else if @Type = 'CostCenter_ins'
	Begin
		Select Code = Department, Description = Department
		From M_CostCenter
		order by Code
	End

--List status MyKY
	else if @Type = 'MyKYStatus'
	Begin
		Select Code, Description
		from(
			Select 1 No, 'ALL' Code, 'ALL' Description
			union ALL
			Select 2 No, '0' Code, 'In Progress' Description
			Union  ALL
			Select 3 no, '1' Code, 'Replied' Description
			) A
		order by A.No
	End

--List status ActiveStatus
	else if @Type = 'ActiveStatus'
	Begin
		Select Code, Description
		from(
			Select 2 No, '0' Code, 'NO' Description
			Union  ALL
			Select 1 No, '1' Code, 'YES' Description
			) A
		order by A.No
	End

--List status Department
	else if @Type = 'Department'
	Begin
		Select Code, Description
		from(
			Select 1 No, 'ALL' Code, 'ALL' Description
			Union  ALL
			select 2 No, Department Code, Department Description from (
				Select Department from M_UserStaff where Department is not null group by Department
				) A1
			) A
		order by A.No, A.Description
	End

	
--List master issue type
	else if @Type = 'IssueType_Web'
	Begin
		Select Code, Description
		from(
			Select 1 No, 'ALL' Code, 'ALL' Description
			Union  ALL
			Select 2 No, IssueTypeID Code, IssueTypeDescription Description From M_IssueType
			Where ActiveStatus = '1'
			) A
		order by A.No, A.Description
	End

--List Group Department
	else if @Type = 'Group Department'
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
END
GO
