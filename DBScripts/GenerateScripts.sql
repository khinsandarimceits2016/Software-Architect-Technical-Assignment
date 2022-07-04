USE [TechnicalAssignment]
GO
/****** Object:  Table [dbo].[payment_transaction]    Script Date: 7/3/2022 12:14:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[payment_transaction](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[transaction_id] [varchar](50) NULL,
	[amount] [decimal](18, 2) NULL,
	[currency_code] [char](3) NULL,
	[transaction_date] [datetime] NULL,
	[transaction_status] [varchar](10) NULL,
	[created_datetime] [datetime] NULL,
 CONSTRAINT [PK_payment_transaction] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[uspGetTransByCurrency]    Script Date: 7/3/2022 12:14:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[uspGetTransByCurrency] 
	-- Add the parameters for the stored procedure here
	@CurrencyType char(3)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF @CurrencyType <> '' 
	BEGIN
	SELECT transaction_id,CONCAT(amount,' ',currency_code)[payment],transaction_date,
	case 
	when UPPER(transaction_status)='APPROVED' THEN 'A'
	when UPPER(transaction_status)='FAILED' Or UPPER(transaction_status)='REJECTED' THEN 'R'
	when UPPER(transaction_status)='FINISHED' Or UPPER(transaction_status)='DONE' THEN 'D' 
	End as status_code
	FROM payment_transaction
	WHERE currency_code=@CurrencyType
	ORDER BY transaction_date
	END
	ELSE
	BEGIN
	SELECT transaction_id,CONCAT(amount,' ',currency_code)[payment],transaction_date,
	case 
	when UPPER(transaction_status)='APPROVED' THEN 'A'
	when UPPER(transaction_status)='FAILED' Or UPPER(transaction_status)='REJECTED' THEN 'R'
	when UPPER(transaction_status)='FINISHED' Or UPPER(transaction_status)='DONE' THEN 'D' 
	End as status_code
	FROM payment_transaction
	ORDER BY transaction_date
	END
END

GO
/****** Object:  StoredProcedure [dbo].[uspGetTransByDateRange]    Script Date: 7/3/2022 12:14:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[uspGetTransByDateRange]
	-- Add the parameters for the stored procedure here
	@FromDate varchar(11),
	@ToDate varchar(11)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF @FromDate<> '' AND @ToDate <> ''
	BEGIN
	SELECT transaction_id,CONCAT(amount,' ',currency_code)[payment],transaction_date,
	case 
	when UPPER(transaction_status)='APPROVED' THEN 'A'
	when UPPER(transaction_status)='FAILED' Or UPPER(transaction_status)='REJECTED' THEN 'R'
	when UPPER(transaction_status)='FINISHED' Or UPPER(transaction_status)='DONE' THEN 'D' 
	End as status_code
	FROM payment_transaction
	WHERE CONVERT(DATE,transaction_date) BETWEEN @FromDate AND @ToDate
	ORDER BY transaction_date
	END
	ELSE 
	BEGIN
	SELECT transaction_id,CONCAT(amount,' ',currency_code)[payment],transaction_date,
	case 
	when UPPER(transaction_status)='APPROVED' THEN 'A'
	when UPPER(transaction_status)='FAILED' Or UPPER(transaction_status)='REJECTED' THEN 'R'
	when UPPER(transaction_status)='FINISHED' Or UPPER(transaction_status)='DONE' THEN 'D' 
	End as status_code
	FROM payment_transaction
	ORDER BY transaction_date
	END
END

GO
/****** Object:  StoredProcedure [dbo].[uspGetTransByStatus]    Script Date: 7/3/2022 12:14:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[uspGetTransByStatus]
	-- Add the parameters for the stored procedure here
	@StatusCode varchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF @StatusCode = 'A' 
	BEGIN
	SELECT transaction_id,CONCAT(amount,' ',currency_code)[payment],transaction_date,
	case 
	when UPPER(transaction_status)='APPROVED' THEN 'A'
	when UPPER(transaction_status)='FAILED' Or UPPER(transaction_status)='REJECTED' THEN 'R'
	when UPPER(transaction_status)='FINISHED' Or UPPER(transaction_status)='DONE' THEN 'D' 
	End as status_code
	FROM payment_transaction
	WHERE UPPER(transaction_status)='APPROVED'
	ORDER BY transaction_date
	END
	ELSE IF @StatusCode = 'R' 
	BEGIN
	SELECT transaction_id,CONCAT(amount,' ',currency_code)[payment],transaction_date,
	case 
	when UPPER(transaction_status)='APPROVED' THEN 'A'
	when UPPER(transaction_status)='FAILED' Or UPPER(transaction_status)='REJECTED' THEN 'R'
	when UPPER(transaction_status)='FINISHED' Or UPPER(transaction_status)='DONE' THEN 'D' 
	End as status_code
	FROM payment_transaction
	WHERE UPPER(transaction_status) IN ('FAILED','REJECTED')
	ORDER BY transaction_date
	END
	ELSE IF @StatusCode='D'
	BEGIN
	SELECT transaction_id,CONCAT(amount,' ',currency_code)[payment],transaction_date,
	case 
	when UPPER(transaction_status)='APPROVED' THEN 'A'
	when UPPER(transaction_status)='FAILED' Or UPPER(transaction_status)='REJECTED' THEN 'R'
	when UPPER(transaction_status)='FINISHED' Or UPPER(transaction_status)='DONE' THEN 'D' 
	End as status_code
	FROM payment_transaction
	WHERE UPPER(transaction_status) IN ('FINISHED','DONE')
	ORDER BY transaction_date
	END
	ELSE IF @StatusCode=''
	SELECT transaction_id,CONCAT(amount,' ',currency_code)[payment],transaction_date,
	case 
	when UPPER(transaction_status)='APPROVED' THEN 'A'
	when UPPER(transaction_status)='FAILED' Or UPPER(transaction_status)='REJECTED' THEN 'R'
	when UPPER(transaction_status)='FINISHED' Or UPPER(transaction_status)='DONE' THEN 'D' 
	End as status_code
	FROM payment_transaction
	ORDER BY transaction_date
END


GO
