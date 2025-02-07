
/****** Object:  StoredProcedure [dbo].[sp_UserSetup]    Script Date: 6/14/2023 8:07:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author			: Royyan
-- Create date		: 
-- Update date/By	: Riana / 2023-06-14
-- Description		:	
-- =============================================

ALTER PROCEDURE [dbo].[sp_UserSetup]
	@Func int = '1', --1 = Get List Data User Setup; 2 = Get List Data by UserID User Setup; 3 = Insert Data User Setup; 4 = Update Data User Setup; 5 = Delete Data User Setup; 6 = Insert Data M_JSOXPasswordHistory
	@UserID varchar(max) = '',
	@UserName varchar(max) = '',
	@Password varchar(max) = '',
	@Department varchar(50) = '',
	@UserType char(1) = '',
	@CatererID varchar(50) = '',
	@AdminStatus varchar(max) = '',
	@UserLogin varchar(max) = ''
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
			 , UserType		= Case When UserType = 1 Then 'Caterer' When UserType = '2' Then 'Admin' Else 'User' End
			 , AdminStatus	= case when AdminStatus = '1' then 'Yes' else 'No' end
			 , CatererID	= CatererID
			 , LastUpdate	= format(isnull(UpdateDate, CreateDate), 'yyyy-MM-dd HH:mm:ss') 
			 , LastUser		= isnull(UpdateUser, CreateUser) 
		From M_UserSetup
		Return
	End
  -- ==========================================

	If @Func = 2
	Begin
		Select UserID		= UserID
			 , UserName		= UserName
			 , Password		= Password
			 , Department   = Department
			 , UserType		= Case When UserType = 1 Then 'Caterer' When UserType = '2' Then 'Admin' Else 'User' End
			 , AdminStatus	= case when AdminStatus = '1' then 'Yes' else 'No' end
			 , CatererID	= CatererID
			 , LastUpdate	= format(isnull(UpdateDate, CreateDate), 'yyyy-MM-dd HH:mm:ss') 
			 , LastUser		= isnull(UpdateUser, CreateUser) 
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
			)
		Values 
			(
				@UserID, @UserName, @Password
				, @Department, @UserType
				, 0, @AdminStatus, @CatererID
				, getdate(), @UserLogin
				, getdate(), @UserLogin
			)
		Return
	End

	If @Func = 4
	Begin
		Update M_UserSetup 
		Set UserName	= @UserName
		  , Department	= @Department
		  , UserType	= @UserType
		  , AdminStatus	= @AdminStatus
		  , CatererID	= @CatererID
		  , UpdateDate	= getdate()
		  , UpdateUser	= @UserLogin 
		Where UserID	= @UserID
		Return
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



/****** Object:  StoredProcedure [dbo].[sp_web_fillcombo]    Script Date: 6/14/2023 8:07:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: TOS - RIANA
-- Create date	: 2023-06-14
-- Description	:
-- =============================================
CREATE PROCEDURE [dbo].[sp_web_fillcombo]
	@Action	varchar(10) = '',
	@Param varchar(max) = ''
AS
BEGIN
 -- ====================================
 -- Get Department
 -- ====================================
	IF @Action = '01' BEGIN 
		SELECT Code = TRIM(Department), CodeDesc = TRIM(Department) FROM M_CostCenter
		RETURN
	END
 -- ====================================

 -- ====================================
 -- Get Admin Status
 -- ====================================
	IF @Action = '02' BEGIN 
		SELECT Code = '1', CodeDesc = 'Yes'
		UNION ALL
		SELECT Code = '0', CodeDesc = 'No'
		RETURN
	END
 -- ====================================

 -- ====================================
 -- Get User Type
 -- ====================================
	IF @Action = '03' BEGIN 
		SELECT Code = '0', CodeDesc = 'User'
		UNION ALL
		SELECT Code = '1', CodeDesc = 'Caterer'
		UNION ALL
		SELECT Code = '2', CodeDesc = 'Admin'
		RETURN
	END
 -- ====================================


 -- ====================================
 -- Get CatererID
 -- ====================================
	IF @Action = '04' BEGIN 
		SELECT Distinct Code = TRIM(CatererID), CodeDesc = TRIM(CatererID) FROM M_MealOrder
		RETURN
	END
 -- ====================================
END

