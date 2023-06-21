
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
	 @StartDate			VARCHAR(11)
	,@EndDate			VARCHAR(11)
	,@Groupdepartment	VARCHAR(20)
	,@Designation		VARCHAR(20)
AS
BEGIN
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
			M_SurveyPolls_Header A
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
		WHERE
			A.StartDate >= @StartDate and A.EndDate <= @EndDate
		--AND dept.GroupDepartment = case when @Groupdepartment = 'ALL' then dept.GroupDepartment else @Groupdepartment end
		--AND design.SALPlan = case when @Designation = 'ALL' then design.SALPlan else @Designation end
	) A
	Order by
		A.UpdateDate Desc, A.SurveyStatus ASC
END
