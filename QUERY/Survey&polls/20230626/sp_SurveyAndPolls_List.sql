
/****** Object:  StoredProcedure [dbo].[sp_SurveyAndPolls_List]    Script Date: 6/23/2023 3:00:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-02-05
-- Description	: 
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[sp_SurveyAndPolls_List]
--DECLARE
	 @StartDate			VARCHAR(10)	= '2023-06-01'
	,@EndDate			VARCHAR(10) = '2023-07-31'
	,@Groupdepartment	VARCHAR(20) = 'ALL'
	,@Designation		VARCHAR(20) = 'ALL'
	,@User				VARCHAR(20) = 'PGMTesting1'
AS
BEGIN
	
	Declare @Department	as varchar(max) = ISNULL((Select Top 1 Department		from M_UserSetup  where UserID		= @User), '' )
	Declare @Admin		as varchar(max) = ISNULL((Select Top 1 AdminStatus		from M_UserSetup  where UserID		= @User), '0')
	Declare @UserType	as varchar(max) = ISNULL((Select Top 1 UserType			from M_UserSetup  where UserID		= @User), '0')
	Declare	@GroupDeptUser	as varchar(max) = ISNULL((Select Top 1 GroupDepartment	from M_CostCenter where Department	= @Department), '')

DECLARE
	@Query	varchar(MAX)


	IF @Groupdepartment = 'ALL'
		BEGIN 
			SET @Groupdepartment = ''
		END

	IF @Designation = 'ALL'
		BEGIN 
			SET @Designation = ''
		END
	SET @Query = 
	'
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
				Department	 = coalesce(B.department,''''),
				Designation  = coalesce(C.designation,''''),
				SurveyStatus = case when A.SurveyStatus = 0 then ''New'' when A.surveyStatus = 1 then ''In Progress'' else ''Done'' end,
				StartDate	 = FORMAT(A.StartDate,''dd MMM yyy''),
				EndDate		 = FORMAT(A.EndDate,''dd MMM yyy''),
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
						cast(A.CreateDate as date) >= ''' + @StartDate + ''' and cast(A.CreateDate as date) <= ''' + @EndDate + '''
					and 
					B.GroupDepartment = case when ''' + @Admin + ''' = 1 or ''' + @UserType + ''' = 1 then B.GroupDepartment else ''' + @GroupDeptUser + ''' end	
				) A
			LEFT 
				JOIN 
				(
					SELECT 
						distinct dept.SurveyID, department = 
						STUFF
					((
						SELECT	'', '' + rtrim(deptx.GroupDepartment) AS [text()]
						FROM	M_SurveyPolls_Department deptx
						where	deptx.SurveyID = dept.SurveyID
						FOR XML PATH('''') 
					), 1, 1, '''' )
					FROM  M_SurveyPolls_Department dept
				) B ON B.SurveyID = A.SurveyID
			LEFT 
				JOIN 
				(
					SELECT 
						distinct design.SurveyID, designation = 
						STUFF
					((
						SELECT	'', '' + rtrim(x.SALPlan) AS [text()]
						FROM	M_SurveyPolls_designation x
						where	x.SurveyID = design.SurveyID
						FOR XML PATH('''') 
					), 1, 1, '''' )
					FROM  M_SurveyPolls_designation design
				) C ON C.SurveyID = A.SurveyID
			LEFT 
				JOIN 
				(
					select distinct	
						SurveyID
					from M_SurveyPolls_Department
					where GroupDepartment = case when ''' + @Groupdepartment + ''' = ''ALL'' then GroupDepartment else ''' + @Groupdepartment + ''' end
					
				) dept ON dept.SurveyID = A.SurveyID
			LEFT 
				JOIN 
				(
					select distinct
						SurveyID
					from
					M_SurveyPolls_Designation
					where SALPlan = case when ''' + @Designation + ''' = ''ALL'' then SALPlan else ''' + @Designation + ''' end
				) design ON design.SurveyID = A.SurveyID
		) A
		WHERE 
			A.Department Like ''%' + @Groupdepartment + '%''
		AND A.Designation Like ''%' + @Designation + '%''
		Order by
			A.UpdateDate Desc, A.SurveyStatus ASC
	'


	EXECUTE(@query)
END
