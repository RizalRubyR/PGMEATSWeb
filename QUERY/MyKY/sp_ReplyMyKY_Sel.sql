 
-- =============================================
-- Author		:Riana
-- Create date	: 
-- Description	:	
-- =============================================
ALTER PROCEDURE [dbo].[sp_ReplyMyKY_Sel]
	@MyKYID varchar(50)
AS
BEGIN
	SELECT 
		  MyKYID				= KY.MyKYID
		, KYID_Eats				= ISNULL(KY.KYID_Eats,'')
		, MyKYLocationDesc		= ISNULL(KY.MyKYLocation,'')
		, MyKYSpecLocation		= ISNULL(KY.MyKYSpecLocation,'')		
		, EvidenceAfter		= ISNULL(KY.EvidenceAfter,'')		
		, MyKYDesc				= ISNULL(KY.MyKYDesc,'')
		, MyKYReply				= ISNULL(KY.MyKYReply,'')	
		, CreateUser			= KY.CreateUser	
		, CreateDate			= FORMAT(KY.CreateDate,'dd MMM yyyy')
		, Status				= KY.MyKYStatus
		
	FROM M_MyKY KY
	WHERE MyKYID = @MyKYID
END
