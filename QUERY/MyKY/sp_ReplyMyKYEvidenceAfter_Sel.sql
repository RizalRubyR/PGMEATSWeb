 
CREATE PROCEDURE [dbo].[sp_ReplyMyKYEvidenceAfter_Sel]
	@MyKYID varchar(50)
AS
BEGIN
	SELECT 
		  MyKYID				= KY.MyKYID
		 ,EvidenceAfter		= ISNULL(KY.EvidenceAfter,'')		
		 
	FROM M_MyKY KY
	WHERE MyKYID = @MyKYID
END
