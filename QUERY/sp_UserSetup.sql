 

ALTER PROCEDURE [dbo].[sp_UserSetup]
	@Func int = '1', --1 = Get List Data User Setup; 2 = Get List Data by UserID User Setup; 3 = Insert Data User Setup; 4 = Update Data User Setup; 5 = Delete Data User Setup; 6 = Insert Data M_JSOXPasswordHistory
	@UserID varchar(max) = '',
	@UserName varchar(max) = '',
	@Password varchar(max) = '',
	@Department varchar(50) = '',
	@UserType char(1) = '',
	@CatererID varchar(50) = '',
	@AdminStatus varchar(max) = '',
	@LockStatus char(1) = '',
	@UserLogin varchar(max) = '',
	@Email varchar(100) = '',
	@ReceiveNotification char(1) = ''

AS
BEGIN

 -- ==========================================
 -- 
 -- ==========================================
	If @Func = 1
	Begin
		Select UserID		= UserID
			 , UserName		= UserName
			 , Password		= Password
			 , Department   = Department
			 , UserType		= Case When UserType = '2' Then 'Caterer' When UserType = '1' Then 'Admin' Else 'User' End
			 , AdminStatus	= case when AdminStatus = '1' then 'Yes' else 'No' end
			 , CatererID	= CatererID
			 , LastUpdate	= format(isnull(UpdateDate, CreateDate), 'yyyy-MM-dd HH:mm:ss') 
			 , LastUser		= isnull(UpdateUser, CreateUser) 
			  , Email = Email
			 , ReceiveNotification = ISNULL(ReceiveNotification,'0')
		From M_UserSetup
		Return
	End
  -- ==========================================

	If @Func = 2
	Begin
		Select UserID		= UserID
			 , UserName		= UserName
			 , Password		= Password
			 , Department   = isnull(Department,'')
			 , UserType		= Case When UserType = '2' Then 'Caterer' When UserType = '1' Then 'Admin' Else 'User' End
			 , AdminStatus	= case when AdminStatus = '1' then 'Yes' else 'No' end
			 , CatererID	= isnull(CatererID,'')
			 , LastUpdate	= format(isnull(UpdateDate, CreateDate), 'yyyy-MM-dd HH:mm:ss') 
			 , LastUser		= isnull(UpdateUser, CreateUser)
			 , LockStatus   = ISNULL(Locked,'0')
			  , Email = Email
			 , ReceiveNotification = ISNULL(ReceiveNotification,'0')
		From M_UserSetup Where UserID = @UserID
		Return
	End

	If @Func = 3
	Begin
		Insert Into M_UserSetup 
			( 
				UserID, UserName, Password
			  , Department, UserType
			  , Locked, AdminStatus, CatererID
			  , CreateDate, CreateUser
			  , UpdateDate, UpdateUser
			  , Email, ReceiveNotification
			)
		Values 
			(
				@UserID, @UserName, @Password
				, @Department, @UserType
				, @LockStatus, @AdminStatus, @CatererID
				, getdate(), @UserLogin
				, getdate(), @UserLogin
				, @Email, @ReceiveNotification
			)
		Return
	End

	If @Func = 4
	Begin
	 If @Password <> '' Begin
		 Update M_UserSetup 
		Set UserName			= @UserName
		  , Department			= @Department
		  , UserType			= @UserType
		  , AdminStatus			= @AdminStatus
		  , Password			= @Password
		  , CatererID			= @CatererID
		  , Locked				= @LockStatus
		  , UpdateDate			= getdate()
		  , UpdateUser			= @UserLogin 
		  , Email				= @Email
		  , ReceiveNotification = @ReceiveNotification
		Where UserID	= @UserID
		Return
	 End
	 Else Begin
		Update M_UserSetup 
		Set UserName			= @UserName
		  , Department			= @Department
		  , UserType			= @UserType
		  , AdminStatus			= @AdminStatus
		  , CatererID			= @CatererID
		  , Locked				= @LockStatus
		  , UpdateDate			= getdate()
		  , UpdateUser			= @UserLogin 
		  , Email				= @Email
		  , ReceiveNotification = @ReceiveNotification
		Where UserID	= @UserID
		Return
	 End
		
	End

	If @Func = 5
	Begin
		Delete From M_UserSetup where UserID = @UserID
		Return
	End

	If @Func = 6
	Begin
		Insert into M_JSOXPasswordHistory
			( 
				AppID, UserID, SeqNo
				, UpdateDate, Password
			) 
		values
		   (
		       'WEB', @UserID, (select isnull(max(SeqNo), 0) + 1 from M_JSOXPasswordHistory where UserID = @UserID)
		      , GetDate(), @Password
		   )
		 Return
	End
END

