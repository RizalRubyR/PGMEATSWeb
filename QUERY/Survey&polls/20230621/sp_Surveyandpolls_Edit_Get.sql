
--/****** Object:  StoredProcedure [dbo].[sp_Surveyandpolls_Edit_Get]    Script Date: 6/21/2023 9:09:43 AM ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
---- =============================================
---- Author		: Pebri
---- Create date	: 2023-04-26
---- Description	: 
---- =============================================
--ALTER PROCEDURE [dbo].[sp_Surveyandpolls_Edit_Get]
DECLARE
	@SurveyID	varchar(10) = '1'
--AS
BEGIN
	SELECT
		 SurveyID		= A.SurveyID
		,SurveyTitle	= A.SurveyTitle
		,StartDate		= FORMAT(A.StartDate, 'dd MMM yyyy')
		,EndDate		= FORMAT(A.EndDate, 'dd MMM yyyy')
		,Department		= coalesce(B.department,'')
		,Designation	= coalesce(C.designation,'')
		,ViewChart		= A.ViewChart
		,Type			= A.Type
		,Finalized		= coalesce(A.Finalized,'0')
	FROM
		M_SurveyPolls_Header A
	LEFT 
		JOIN 
		(
			SELECT 
				distinct dept.SurveyID, department = 
				STUFF
			((
				SELECT	';' + rtrim(deptx.GroupDepartment) AS [text()]
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
				SELECT	';' + rtrim(x.SALPlan) AS [text()]
				FROM	M_SurveyPolls_designation x
				where	x.SurveyID = design.SurveyID
				FOR XML PATH('') 
			), 1, 1, '' )
			FROM  M_SurveyPolls_designation design
		) C ON C.SurveyID = A.SurveyID
	WHERE
		A.SurveyID = @SurveyID
	AND SurveyStatus = 0
END
