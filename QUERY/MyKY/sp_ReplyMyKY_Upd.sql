 
-- =============================================
-- Author		:Riana
-- Create date	: 
-- Description	:	
-- =============================================
ALTER PROCEDURE [dbo].[sp_ReplyMyKY_Upd]
	@MyKYID		As Varchar(15) = '',
	@MyKYReply	As Varchar(MAX) = '',
	@Status		As char(1) = '0',
	@MyKYLoc	As varchar(Max) = '',
	@EvidenceAfter	As varchar(Max) = '',
	@CreateUser As Varchar(15) = ''
AS
BEGIN

	/*======================================
	Declaration
	========================================*/
	Declare @UpdateDate As Datetime = GetDate()


	UPDATE	M_MyKY
	SET		MyKYReply	= @MyKYReply
			, MyKYStatus	= @Status
			, MyKYLocation	= @MyKYLoc
			, UpdateUser	= @CreateUser
			, UpdateDate	= @UpdateDate
			, EvidenceAfter = @EvidenceAfter
	WHERE	MyKYID	= @MyKYID
END
