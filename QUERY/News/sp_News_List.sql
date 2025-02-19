GO
/****** Object:  StoredProcedure [dbo].[sp_News_List]    Script Date: 08/17/2023 08:21:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Fikri
-- Create date	: 
-- Description	: Get for List News (Web)
-- =============================================
ALTER PROCEDURE [dbo].[sp_News_List]
	@NewsID		As Int = 0, --0 untuk List di Datatable || selain itu untuk Fill Detail (For Mobile & Web) atau Mobile view Detail
	@DateFrom	As Varchar(10)  = 'ALL', --2023-01-01
	@DateTo		As Varchar(10)  = 'ALL', --2023-01-01
	@GroupDept	As Varchar(10)  = 'ALL',
	@SALPlan	As Varchar(10)  = 'ALL',
	@Status		As Varchar(MAX) = '',
	@User		As Varchar(Max) = 'Mobile view Detail'
AS
BEGIN
	Declare @Department	as varchar(max) = ISNULL((Select Top 1 Department		from M_UserSetup  where UserID		= @User), '' )
	Declare @Admin		as varchar(max) = ISNULL((Select Top 1 AdminStatus		from M_UserSetup  where UserID		= @User), '0')
	Declare @UserType	as varchar(max) = ISNULL((Select Top 1 UserType			from M_UserSetup  where UserID		= @User), '0')
	Declare	@GroupDeptUser	as varchar(max) = ISNULL((Select Top 1 GroupDepartment	from M_CostCenter where Department	= @Department), '')

	select 
		NewsID				= A.NewsID,
		NewsTitle			= A.NewsTitle,
		NewsDescCode		= A.NewsDescriptionCode,
		NewsDescText		= A.NewsDescriptionText,
		Attachment			= ISNULL(A.FileName,''),
		DateFrom			= FORMAT(A.StartDate,'dd MMM yyy'),
		DateTo				= FORMAT(A.EndDate,	 'dd MMM yyy'),
		TargetPart			= ISNULL(B.department, ''),
		TargetDesignation	= ISNULL(B2.designation, ''),
		CreatedDate			= FORMAT(A.UpdateDate,'dd MMM yyy')
	From
		M_News A Left Join
		(
			SELECT distinct dept.NewsID, department = LTRIM(
			STUFF
				((
					SELECT	'; ' + rtrim(deptx.GroupDepartment) AS [text()]
					FROM	M_News_Department deptx
					where	deptx.NewsID = dept.NewsID
					FOR XML PATH('') 
				), 1, 1, '' )) + ';'
				FROM  M_News_Department dept
				Where 1 =
						Case
							When @NewsID =  0 And (@Admin = '1' or  @UserType = '1' or @User = 'Mobile view Detail') Then 1 --Jika Super User (Admin) || Jika User Type nya 1 tapi bukan Super User
							When @NewsID =  0 And  @Admin = '0' And @UserType = '0' And dept.GroupDepartment in (@GroupDeptUser) Then 1
							When @NewsID <> 0 Then 1
							Else 0
						End 
		) B On A.NewsID = B.NewsID Left Join
		(
			SELECT distinct dest.NewsID, designation = LTRIM(
			STUFF
				((
					SELECT	'; ' + rtrim(destx.SALPlan) AS [text()]
					FROM	M_News_Designation destx
					where	destx.NewsID = dest.NewsID
					FOR XML PATH('') 
				), 1, 1, '' )) + ';'
				FROM  M_News_Designation dest
		) B2 On A.NewsID = B2.NewsID Left Join
		(Select NewsID, SALPlan			From M_News_Designation	Where SALPlan in (@SALPlan)) C On A.NewsID = C.NewsID Left Join
		(Select NewsID, GroupDepartment From M_News_Department	Where GroupDepartment in (@GroupDept)) D On A.NewsID = D.NewsID 
	Where
		1 = 
			Case 
				When @DateFrom = 'ALL'  or @DateTo = 'ALL' or @User = 'Mobile view Detail' Then 1 
				--When (@DateFrom <> 'ALL' or @DateTo <> 'ALL') And Cast(A.StartDate as Date) >= @DateFrom and Cast(A.EndDate as Date) <= @DateTo Then 1
				When (@DateFrom <> 'ALL' or @DateTo <> 'ALL') And Cast(CreateDate as date) between Cast(@DateFrom as date) and Cast(@DateTo as date) Then 1
				Else 0
			End
	And 1 = 
			Case 
				When @User = 'Mobile view Detail' Then 1 
				--When (@DateFrom <> 'ALL' or @DateTo <> 'ALL') And Cast(A.StartDate as Date) >= @DateFrom and Cast(A.EndDate as Date) <= @DateTo Then 1
				When @Status = 'Active'	 and Cast(GETDATE() As Date) Between Cast(A.StartDate as Date) and Cast(A.EndDate as Date) Then 1
				When @Status = 'Expired' and Cast(GETDATE() As Date) Not Between Cast(A.StartDate as Date) and Cast(A.EndDate as Date) Then 1
				Else 0
			End
	And	A.NewsID = 
			Case
				When @NewsID = 0 Then A.NewsID
				Else @NewsID
			End 
	And	1 =
			Case
				When @NewsID =  0 And (@GroupDept = 'ALL' or @User = 'Mobile view Detail')Then 1
				When @NewsID =  0 And d.GroupDepartment Is Not NULL Then 1
				When @NewsID <> 0 Then 1
				Else 0
			End 
	And	1 =
			Case
				When @NewsID =  0 And (@SALPlan = 'ALL' or @User = 'Mobile view Detail') Then 1
				When @NewsID =  0 And c.SALPlan Is Not NULL Then 1
				When @NewsID <> 0 Then 1
				Else 0
			End 
	And	1 =
			Case
				When @NewsID =  0 And (@Admin = '1' or  @UserType = '1' or @User = 'Mobile view Detail') Then 1 --Jika Super User (Admin) || Jika User Type nya 1 tapi bukan Super User
				When @NewsID =  0 And  @Admin = '0' And @UserType = '0' And B.department Is Not NULL Then 1
				When @NewsID <> 0 Then 1
				Else 0
			End 
	Order by
		UpdateDate Desc
END
