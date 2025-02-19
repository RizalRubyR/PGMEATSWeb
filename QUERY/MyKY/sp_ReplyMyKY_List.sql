 
-- =============================================
-- Author		:Riana
-- Create date	: 
-- Description	:	
-- =============================================
ALTER PROCEDURE [dbo].[sp_ReplyMyKY_List]
 (	@FromDate		varchar(10) = '',
	@ToDate			varchar(10) = '',
	@MyKYStatus		varchar(15) = '',
	@MyKYLocation	varchar(15) = '',
	@UserID			varchar(50) = ''
 )
AS
BEGIN
	 -- ==============================================================
 -- Get User Privilege
 -- ==============================================================
	Declare @AdminStatus char(1)= null
		  , @UserType char(1)= null
		  , @CatererID varchar(50)= null

	Select @AdminStatus = Isnull(AdminStatus,'0')
		, @UserType = Isnull(UserType,'0')
		, @CatererID = isnull(CatererID,'') 
		From M_UserSetup Where UserID = @UserID

	--Select @AdminStatus , @UserType , @CatererID 

	Drop Table If Exists #TempUserSetup

	Select A.GroupDepartment Into #TempUserSetup
	From M_CostCenter A
	Left Join M_UserSetup B ON A.GroupDepartment = B.Department And B.UserID = @UserID
	Where 1 = case when @MyKYLocation = 'ALL' 
			  then 
				case when @AdminStatus = '1' Then 1 
				else
				   case when @UserType = '1' or  @UserType = '2' Then 1 
				   when @UserType = '0' and UserType = @UserType Then 1
				   else 0 End 								
			     end
			  when  @MyKYLocation <> 'ALL' and A.GroupDepartment = @MyKYLocation Then 1
			  else 0 end

	--Select * FRom #TempUserSetup 
 -- ==============================================================
	SELECT * 
	FROM (
		SELECT DISTINCT
			  MyKYID				= KY.MyKYID
			, KYID_Eats				= ISNULL(KY.KYID_Eats,'')
			, MyKYDate				= ISNULL(Ky.MyKYDate,'')
			, MyKYLocation			= ISNULL(KY.MyKYLocation,'')
			, MyKYLocationDesc		= ISNULL(CC.UserDepartment,'')
			, MyKYSpecLocation		= ISNULL(KY.MyKYSpecLocation,'')		
			, MyKYDesc				= ISNULL(KY.MyKYDesc,'')
			, MyKYReply				= ISNULL(KY.MyKYReply,'')
			, MyKYStatus			= ISNULL(KY.MyKYStatus,'')
			
			, MyKYStatusDesc		= CASE WHEN KY.MyKYStatus = '0' THEN 'Submitted' 
									  WHEN KY.MyKYStatus = '1' THEN 'Verification'
									  WHEN KY.MyKYStatus = '2' THEN 'In Progress'
									  WHEN KY.MyKYStatus = '3' THEN 'Closed'
									  WHEN KY.MyKYStatus = '4' THEN 'Rejected'END
			, Evidence				= KY.Evidence
			, EvidenceAfter			= KY.EvidenceAfter
			, Section			    = ISNULL(KY.Section,'')
			, Shift			        = ISNULL(KY.Shift,'')
			, CreateUser			= KY.CreateUser	
			, CreateDate			= FORMAT(KY.CreateDate,'dd MMM yyyy')
			, LastUpdate			= ISNULL(FORMAT(KY.UpdateDate,'dd MMM yyyy, HH:mm'),'1999-01-01')
			, LastUser				= ISNULL(KY.UpdateUser,'')
		FROM M_MyKY KY
		LEFT JOIN M_CostCenter CC ON KY.MyKYLocation = CC.GroupDepartment
		WHERE (CAST( ky.CreateDate AS DATE) BETWEEN CAST(@FromDate AS DATE) AND CAST(@ToDate AS DATE))
			AND 1 = CASE WHEN @MyKYStatus = 'ALL' THEN 1
				WHEN @MyKYStatus <> 'ALL' AND ky.MyKYStatus = @MyKYStatus THEN 1
				ELSE 0 END
			AND KY.MyKYLocation In (select GroupDepartment from #TempUserSetup)
			AND 1 = CASE WHEN @MyKYLocation = 'ALL' THEN 1
				WHEN @MyKYLocation <> 'ALL' AND Ky.MyKYLocation = @MyKYLocation THEN 1
				ELSE 0 END
			--AND 1 = case when @Department = 'ALL' then 1 
			--	     when @Department <> 'ALL' and US.Department = @Department then 1
			--		 Else 0 End
	) A
	ORDER BY A.CreateDate Desc
END
