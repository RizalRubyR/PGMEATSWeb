/****** Object:  StoredProcedure [dbo].[sp_ReplyMyKY_Sel]    Script Date: 5/24/2023 3:32:16 PM ******/
DROP PROCEDURE [dbo].[sp_ReplyMyKY_Sel]
GO
/****** Object:  StoredProcedure [dbo].[sp_ReplyComplaint_Sel]    Script Date: 5/24/2023 3:32:16 PM ******/
DROP PROCEDURE [dbo].[sp_ReplyComplaint_Sel]
GO
/****** Object:  StoredProcedure [dbo].[sp_ReplyComplaint_Sel]    Script Date: 5/24/2023 3:32:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		:Riana
-- Create date	: 
-- Description	:	
-- =============================================
CREATE PROCEDURE [dbo].[sp_ReplyComplaint_Sel]
	@ComplaintID varchar(15)
AS
BEGIN
	SELECT 
		  ComplaintID			= MC.ComplaintID		  
		, CreatedDate			= FORMAT(MC.CreateDate,'dd MMM yyyy')
		, Department			= US.Department
		, IssueTypeDesc			= ISNULL(MIT.IssueTypeDescription,'')
		, ComplaintDesc			= ISNULL(MC.ComplaintDescription,'')		
		, CreatedUser			= ISNULL(MC.CreateUser,'')	
		, ComplaintReply		= ISNULL(MC.ComplaintReply,'')

	FROM M_Complaint MC
		JOIN M_IssueType MIT ON MC.IssueTypeID = MIT.IssueTypeID
		JOIN M_UserStaff US ON MC.CreateUser = US.StaffID
	WHERE MC.ComplaintID = @ComplaintID
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ReplyMyKY_Sel]    Script Date: 5/24/2023 3:32:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		:Riana
-- Create date	: 
-- Description	:	
-- =============================================
CREATE PROCEDURE [dbo].[sp_ReplyMyKY_Sel]
	@MyKYID varchar(50)
AS
BEGIN
	SELECT 
		  MyKYID				= KY.MyKYID
		, MyKYLocationDesc		= ISNULL(KY.MyKYLocation,'')
		, MyKYSpecLocation		= ISNULL(KY.MyKYSpecLocation,'')		
		, MyKYDesc				= ISNULL(KY.MyKYDesc,'')
		, MyKYReply				= ISNULL(KY.MyKYReply,'')	
		, CreateUser			= KY.CreateUser	
		, CreateDate			= FORMAT(KY.CreateDate,'dd MMM yyyy')
		
	FROM M_MyKY KY
		JOIN M_CostCenter CC on KY.MyKYLocation = CC.Department
	WHERE MyKYID = @MyKYID
END
GO
