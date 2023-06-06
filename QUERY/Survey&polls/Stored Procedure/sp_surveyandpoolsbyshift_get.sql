drop procedure sp_surveyandpoolsbyshift_get
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		: Pebri
-- Create date	: 2023-04-07
-- Description	: 
-- =============================================
CREATE PROCEDURE sp_surveyandpoolsbyshift_get
--DECLARE
	@SurveyID		VARCHAR(10) = 2
AS
BEGIN
	SELECT 
		*
	FROM
	(
		SELECT TOP 100 PERCENT
			 Shift
			,SurveyID
			,AnserDesc
			,SUM(Total) Total
			,ROW_NUMBER() Over(Order by Shift) LastCol
		FROM 
			(
				select 
					 Shift = 'Shift ' + B.Shift
					,A.SurveyID
					,AnserDesc = (select AnswerDesc FROM M_SurveyPolls_Answer WHERE QuestionID = A.QuestionID and AnswerSeqNo = A.AnswerSeqNo)
					,COUNT(A.AnswerSeqNo) Total
				from M_SurveyPolls_Answer_Staff A
				INNER JOIN M_UserStaff B ON B.StaffID = A.StaffID
				WHERE SurveyID = @surveyID
				GROUP BY 
					 B.Shift
					,A.SurveyID
					,A.QuestionID
					,A.AnswerSeqNo
			) A
		Group BY Shift,SurveyID,AnserDesc
	) A
	ORDER BY Shift, AnserDesc
--DECLARE
--	@cols AS NVARCHAR(MAX),
--	@query AS NVARCHAR(MAX)

--SET @Cols = STUFF
--(
--	(
--		SELECT  ',' + QUOTENAME(A.AnserDesc)
--		FROM (
--			SELECT distinct
--				 AnserDesc
--			FROM 
--				(
--					select
--						 B.Shift
--						,A.SurveyID
--						,A.QuestionID
--						,A.AnswerSeqNo
--						,AnserDesc = (select AnswerDesc FROM M_SurveyPolls_Answer WHERE QuestionID = A.QuestionID and AnswerSeqNo = A.AnswerSeqNo)
--						,COUNT(A.AnswerSeqNo) Total
--					from M_SurveyPolls_Answer_Staff A
--					INNER JOIN M_UserStaff B ON B.StaffID = A.StaffID
--					WHERE SurveyID = @surveyID
--					GROUP BY 
--						 B.Shift
--						,A.SurveyID
--						,A.QuestionID
--						,A.AnswerSeqNo
--				) A
--		) A
--		FOR XML  PATH(''), TYPE
--	).value('.', 'NVARCHAR(MAX)'),1,1,''
--)


--SET @query = '

--	DECLARE 
--		@surveyID	Integer = ' + @surveyID + '
--	SELECT 
--		 Shift
--		,SurveyID
--		,' + @cols + '
--	FROM 
--		(
--		SELECT TOP 100 PERCENT
--			* 
--		FROM 
--			(
--				select 
--					 B.Shift
--					,A.SurveyID
--					,AnserDesc = (select AnswerDesc FROM M_SurveyPolls_Answer WHERE QuestionID = A.QuestionID and AnswerSeqNo = A.AnswerSeqNo)
--					,COUNT(A.AnswerSeqNo) Total
--				from M_SurveyPolls_Answer_Staff A
--				INNER JOIN M_UserStaff B ON B.StaffID = A.StaffID
--				WHERE SurveyID = @surveyID
--				GROUP BY 
--					 B.Shift
--					,A.SurveyID
--					,A.QuestionID
--					,A.AnswerSeqNo
--			) A
--		ORDER BY Shift,AnserDesc
--	) A
--	pivot(
--		SUM(Total)
--		FOR AnserDesc IN 
--		(
--			' + @cols + '
--		)
--	) AS pivot_table order by Shift
--'

--EXECUTE(@query)
END
GO
