
/****** Object:  Table [dbo].[M_SurveyPolls_Department]    Script Date: 6/15/2023 10:33:29 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[M_SurveyPolls_Department](
	[SurveyID] [int] NOT NULL,
	[GroupDepartment] [varchar](50) NOT NULL,
 CONSTRAINT [PK_M_SurveyPolls_Department] PRIMARY KEY CLUSTERED 
(
	[SurveyID] ASC,
	[GroupDepartment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


