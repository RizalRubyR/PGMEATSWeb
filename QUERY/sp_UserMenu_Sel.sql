
/****** Object:  StoredProcedure [dbo].[sp_UserMenu_Sel]    Script Date: 4/17/2023 11:02:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Royyan>
-- Create date: <12 December 2022>
-- Description:	<API Get Menu>
-- =============================================
ALTER PROCEDURE [dbo].[sp_UserMenu_Sel] 
	@UserID varchar(max) = null,
	@AdminStatus varchar(max) = null
AS
BEGIN
	if @AdminStatus = 'Yes'
	begin
		Select * From M_UserMenu
		Where ActiveStatus = '1'
		Order By MenuIndex 
	end
	else
	begin
		Select * From M_UserMenu UM
		JOIN M_UserPrivilege UP ON UM.MenuID = UP.MenuID
		Where ActiveStatus = '1' AND AllowAccess = '1' AND UserID =  @UserID
		Order By MenuIndex 
	end

END
