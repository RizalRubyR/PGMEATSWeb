SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-04-11
-- Description	: 
-- =============================================
ALTER PROCEDURE [dbo].[sp_AnswerType_GET]
	
AS
BEGIN
	Select  Code		= '0'
		   ,Description = 'Multiple Choice'
	Union ALL
	Select  Code		= '1'
		   ,Description = 'Free Text'
	Union ALL
	Select  Code		= '2'
		   ,Description = 'Multiple Answer'
END
