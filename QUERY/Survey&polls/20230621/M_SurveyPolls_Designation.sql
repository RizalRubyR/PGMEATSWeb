/****** Object:  Table [dbo].[M_SurveyPolls_Designation]    Script Date: 6/16/2023 1:50:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[M_SurveyPolls_Designation](
	[SurveyID] [int] NOT NULL,
	[SALPlan] [varchar](50) NOT NULL,
 CONSTRAINT [PK_M_SurveyPolls_Designation] PRIMARY KEY CLUSTERED 
(
	[SurveyID] ASC,
	[SALPlan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


