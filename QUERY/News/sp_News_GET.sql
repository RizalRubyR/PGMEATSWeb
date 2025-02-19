USE [PGMEATS_WEB_UAT_20230614]
GO
/****** Object:  StoredProcedure [dbo].[sp_News_GET]    Script Date: 06/14/2023 08:00:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Fikri
-- Create date	: 
-- Description	: Get for List News (Mobile)
-- =============================================
CREATE PROCEDURE [dbo].[sp_News_GET]
	  @Data			As varchar(5)
	 ,@Department	As varchar(50)
AS
BEGIN
	Declare @sql	as nvarchar(max)
	Declare @params as nvarchar(max)
	Declare @Top	as varchar(max) = ''

	IF @Data <> 'ALL'
	Begin
		Set @Top = 'Top ' + @Data + ''
	End

	SET @sql = 
	'
	Declare	@GroupDept	as varchar(max) = ISNULL((Select Top 1 GroupDepartment	from M_CostCenter where Department	= '''+ @Department + '''), '''')

	select ' + @Top + '
		NewsID,
		NewsTitle		= ISNULL(A.NewsTitle,''''),
		NewsDescCode	= A.NewsDescriptionCode,
		NewsDescText	= A.NewsDescriptionText,
		UserInfo		= ISNULL(A.UpdateUser,''Administrator IT''),
		AttachmentFile	= ISNULL(A.FileName,''''),
		Date			= FORMAT(A.UpdateDate,''dd MMM yyy''),
		HotNews			= 
					Case
						When Datediff(Day,Cast(A.UpdateDate As Date), GETDATE()) <= 3 Then 1
						Else 0
					End
	From
		M_News A
	Where
		Cast(GETDATE() as date) between Cast(A.StartDate as date) and Cast(A.EndDate as date) 
	And	(
			A.TargetParticipant = ''ALL''
			or
			A.TargetParticipant = @GroupDept
		)
	Order by
		UpdateDate Desc
	'

	EXEC sp_executesql @sql, @params
END
GO
