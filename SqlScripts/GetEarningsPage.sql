IF OBJECTPROPERTY(object_id('GetEarningsPage'), N'IsProcedure') = 1
	DROP PROCEDURE [dbo].[GetEarningsPage]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetPendingMail] */
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetEarningsPage]
(
	@PageNum int,
	@RowsPerPage int,
	@SortField varchar(20)
)
AS
BEGIN
	DECLARE @startRow int = ((@PageNum - 1) * @RowsPerPage) + 1
	DECLARE @endRow int = @PageNum * @RowsPerPage;

	WITH EarningsTable AS
	(
		SELECT EarnedDate,
			LastName + ', ' + FirstName AS Name,
			Points,
			ROW_NUMBER() OVER (ORDER BY LastName + ', ' + FirstName) AS RowNumber
		FROM dbo.Earnings
		INNER JOIN dbo.Earners ON dbo.Earnings.EarnerId = dbo.Earners.EarnerId
	)
	SELECT
		EarnedDate,
		Name,
		Points
	FROM
		EarningsTable
	WHERE
		RowNumber BETWEEN @startRow AND @endRow

END
GO

/*
exec GetEarningsPage 1, 10, 'EarnedDate'
*/