
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE or ALTER PROCEDURE sp_SurveyandpollsHeader_Finalized
	 @SurveyID		varchar(10)
	,@Finalized		varchar(1)
AS
BEGIN
	UPDATE M_SurveyPolls_Header 
		SET
			Finalized = @Finalized
		WHERE SurveyID = @SurveyID
END
GO
