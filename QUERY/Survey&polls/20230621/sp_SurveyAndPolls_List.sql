
/****** Object:  StoredProcedure [dbo].[sp_SurveyAndPolls_List]    Script Date: 6/21/2023 1:11:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-02-05
-- Description	: 
-- =============================================
CREATE OR ALTER  PROCEDURE [dbo].[sp_SurveyAndPolls_List]
	 @StartDate			VARCHAR(10)
	,@EndDate			VARCHAR(10)
	,@Groupdepartment	VARCHAR(20)
	,@Designation		VARCHAR(20)
	,@User				VARCHAR(20)
AS
BEGIN
	
	Declare @Department	as varchar(max) = ISNULL((Select Top 1 Department		from M_UserSetup  where UserID		= @User), '' )
	Declare @Admin		as varchar(max) = ISNULL((Select Top 1 AdminStatus		from M_UserSetup  where UserID		= @User), '0')
	Declare @UserType	as varchar(max) = ISNULL((Select Top 1 UserType			from M_UserSetup  where UserID		= @User), '0')
	Declare	@GroupDeptUser	as varchar(max) = ISNULL((Select Top 1 GroupDepartment	from M_CostCenter where Department	= @Department), '')


	select 
		 A.SurveyID
		,A.SurveyTitle
		,A.Department
		,A.Designation
		,A.SurveyStatus
		,A.StartDate
		,A.EndDate
	from 
	(
		select distinct
			SurveyID	 = A.SurveyID,
			SurveyTitle	 = A.SurveyTitle,
			Department	 = coalesce(B.department,''),
			Designation  = coalesce(C.designation,''),
			SurveyStatus = case when A.SurveyStatus = 0 then 'New' when A.surveyStatus = 1 then 'In Progress' else 'Done' end,
			StartDate	 = FORMAT(A.StartDate,'dd MMM yyy'),
			EndDate		 = FORMAT(A.EndDate,'dd MMM yyy'),
			UpdateDate	 = A.UpdateDate
		From
			(
				select
					 A.SurveyID
					,A.SurveyTitle
					,A.SurveyStatus
					,A.StartDate
					,A.EndDate
					,A.ViewChart
					,A.CreateUser
					,A.UpdateDate
					,A.Type
					,A.Finalized
					,B.GroupDepartment
				from M_SurveyPolls_Header A
				inner 
					join
					(
						select
 							 A.UserID
							,UserType = A.UserType
							,A.AdminStatus
							,A.Department
							,B.GroupDepartment
						from M_UserSetup A
						inner join M_CostCenter B on B.Department = A.Department 
					) B ON B.UserID = A.CreateUser
				where
					cast(A.StartDate as date) >= @StartDate and cast(A.EndDate as date) <= @EndDate
				and B.GroupDepartment = case when @Admin = 1 or @UserType = 1 then B.GroupDepartment else @GroupDeptUser end	
			) A
		LEFT 
			JOIN 
			(
				SELECT 
					distinct dept.SurveyID, department = 
					STUFF
				((
					SELECT	', ' + rtrim(deptx.GroupDepartment) AS [text()]
					FROM	M_SurveyPolls_Department deptx
					where	deptx.SurveyID = dept.SurveyID
					FOR XML PATH('') 
				), 1, 1, '' )
				FROM  M_SurveyPolls_Department dept
			) B ON B.SurveyID = A.SurveyID
		LEFT 
			JOIN 
			(
				SELECT 
					distinct design.SurveyID, designation = 
					STUFF
				((
					SELECT	', ' + rtrim(x.SALPlan) AS [text()]
					FROM	M_SurveyPolls_designation x
					where	x.SurveyID = design.SurveyID
					FOR XML PATH('') 
				), 1, 1, '' )
				FROM  M_SurveyPolls_designation design
			) C ON C.SurveyID = A.SurveyID
		LEFT 
			JOIN 
			(
				select distinct	
					SurveyID
				from M_SurveyPolls_Department
				where GroupDepartment = case when @Groupdepartment = 'ALL' then GroupDepartment else @Groupdepartment end
					
			) dept ON dept.SurveyID = A.SurveyID
		LEFT 
			JOIN 
			(
				select distinct
					SurveyID
				from
				M_SurveyPolls_Designation
				where SALPlan = case when @Designation = 'ALL' then SALPlan else @Designation end
			) design ON design.SurveyID = A.SurveyID
	) A
	Order by
		A.UpdateDate Desc, A.SurveyStatus ASC
END
