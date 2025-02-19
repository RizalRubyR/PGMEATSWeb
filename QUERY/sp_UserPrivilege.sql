
/****** Object:  StoredProcedure [dbo].[sp_UserPrivilege]    Script Date: 4/17/2023 11:02:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Royyan
-- modify date	: 2023-04-12 by Riana
-- Description	: 
-- =============================================

ALTER PROCEDURE [dbo].[sp_UserPrivilege]
	@Func int, --1 = Get List Data User Privilege; 2 = Insert or Update User Privilege
	@UserID varchar(max) = null,
	@AdminStatus varchar(max) = null,
	@MenuID varchar(max) = null,
	@AllowAccess varchar(max) = null,
	@AllowUpdate varchar(max) = null,
	@UserLogin varchar(max) = null
AS
BEGIN
	if @Func = 1
	begin
		select UserID		= @UserID
			 , GroupID		= A.GroupID
			 , MenuID		= A.MenuID
			 , MenuDesc		= A.MenuDesc
			 , AllowAccess	= ISNULL(B.AllowAccess, 0)
			 , AllowUpdate	= ISNULL(B.AllowUpdate, 0)
		from M_UserMenu A
		left join M_UserPrivilege B on A.MenuID = B.MenuID and B.UserID = @UserID
	end
	else if @Func = 2
	begin
		if not exists (select * from M_UserPrivilege where trim(UserID) = @UserID and trim(MenuID) = @MenuID)
		begin
			insert into M_UserPrivilege 
				(  AppID
				 , UserID
				 , MenuID
				 , AllowAccess
				 , AllowUpdate
				 , CreateUser
				 , CreateDate
				)
			values
				( 'EAT'
				 , @UserID
				 , @MenuID
				 , @AllowAccess
				 , @AllowUpdate 
				 , @UserLogin
				 , getdate() 
				) 
		end
		else
		begin
			update M_UserPrivilege 
			set AllowAccess = @AllowAccess, AllowUpdate = @AllowUpdate 
			where trim(UserID) = @UserID and trim(MenuID) = @MenuID
		end
	end
	else if @Func = 3
	begin
		select UserID		= @UserID
			 , GroupID		= A.GroupID
			 , MenuID		= A.MenuID
			 , MenuDesc		= A.MenuDesc
			 , AllowAccess	= case when @AdminStatus = 'Yes' then '1' else ISNULL(B.AllowAccess, 0) end
			 , AllowUpdate	= ISNULL(B.AllowUpdate, 0)
		from M_UserMenu A
		left join M_UserPrivilege B on A.MenuID = B.MenuID 
		Where B.UserID = @UserID and B.MenuID = @MenuID
	end
END

