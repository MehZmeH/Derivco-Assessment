-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE pr_GetOrderSummary 
	-- Add the parameters for the stored procedure here
@StartDate DATE,
@EndDate DATE,
@EmployeeID INT = NULL,
@CustomerID NCHAR(5) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF @EmployeeID IS NOT NULL AND @CustomerID IS NOT NULL
		BEGIN
			SELECT e.TitleOfCourtesy + ' ' + e.FirstName + ' ' + e.LastName AS EmployeeFullName, o.OrderDate, c.CompanyName, s.CompanyName AS Shipper, SUM(o.Freight) AS TotalFreightCost, COUNT(od.OrderID) AS NumberOfDifferentProducts, SUM(od.UnitPrice * (1 - od.Discount) * od.Quantity) AS TotalCost
			FROM Employees AS e
			INNER JOIN Orders AS o ON e.EmployeeID = o.EmployeeID
			INNER JOIN Customers AS c ON o.CustomerID = c.CustomerID
			INNER JOIN Shippers AS s ON o.ShipVia = s.ShipperID
			INNER JOIN [Order Details] AS od ON o.OrderID = od.OrderID
			WHERE o.OrderDate >= @StartDate AND o.OrderDate <= @EndDate AND e.EmployeeID = @EmployeeID AND c.CustomerID = @CustomerID
			GROUP BY e.TitleOfCourtesy + ' ' + e.FirstName + ' ' + e.LastName, o.OrderDate, c.CompanyName, s.CompanyName;
		END
	ELSE IF @CustomerID IS NOT NULL AND @EmployeeID IS NULL
		BEGIN
			SELECT e.TitleOfCourtesy + ' ' + e.FirstName + ' ' + e.LastName AS EmployeeFullName, o.OrderDate, c.CompanyName, s.CompanyName AS Shipper, SUM(o.Freight) AS TotalFreightCost, COUNT(od.OrderID) AS NumberOfDifferentProducts, SUM(od.UnitPrice * (1 - od.Discount) * od.Quantity) AS TotalCost
			FROM Employees AS e
			INNER JOIN Orders AS o ON e.EmployeeID = o.EmployeeID
			INNER JOIN Customers AS c ON o.CustomerID = c.CustomerID
			INNER JOIN Shippers AS s ON o.ShipVia = s.ShipperID
			INNER JOIN [Order Details] AS od ON o.OrderID = od.OrderID
			WHERE o.OrderDate >= @StartDate AND o.OrderDate <= @EndDate AND c.CustomerID = @CustomerID
			GROUP BY e.TitleOfCourtesy + ' ' + e.FirstName + ' ' + e.LastName, o.OrderDate, c.CompanyName, s.CompanyName;
		END
	ELSE IF @CustomerID IS NULL AND @EmployeeID IS NOT NULL
		BEGIN
			SELECT e.TitleOfCourtesy + ' ' + e.FirstName + ' ' + e.LastName AS EmployeeFullName, o.OrderDate, c.CompanyName, s.CompanyName AS Shipper, SUM(o.Freight) AS TotalFreightCost, COUNT(od.OrderID) AS NumberOfDifferentProducts, SUM(od.UnitPrice * (1 - od.Discount) * od.Quantity) AS TotalCost
			FROM Employees AS e
			INNER JOIN Orders AS o ON e.EmployeeID = o.EmployeeID
			INNER JOIN Customers AS c ON o.CustomerID = c.CustomerID
			INNER JOIN Shippers AS s ON o.ShipVia = s.ShipperID
			INNER JOIN [Order Details] AS od ON o.OrderID = od.OrderID
			WHERE o.OrderDate >= @StartDate AND o.OrderDate <= @EndDate AND e.EmployeeID = @EmployeeID
			GROUP BY e.TitleOfCourtesy + ' ' + e.FirstName + ' ' + e.LastName, o.OrderDate, c.CompanyName, s.CompanyName;
		END
	ELSE
		BEGIN
			SELECT e.TitleOfCourtesy + ' ' + e.FirstName + ' ' + e.LastName AS EmployeeFullName, o.OrderDate, c.CompanyName, s.CompanyName AS Shipper, SUM(o.Freight) AS TotalFreightCost, COUNT(od.OrderID) AS NumberOfDifferentProducts, SUM(od.UnitPrice * (1 - od.Discount) * od.Quantity) AS TotalCost
			FROM Employees AS e
			INNER JOIN Orders AS o ON e.EmployeeID = o.EmployeeID
			INNER JOIN Customers AS c ON o.CustomerID = c.CustomerID
			INNER JOIN Shippers AS s ON o.ShipVia = s.ShipperID
			INNER JOIN [Order Details] AS od ON o.OrderID = od.OrderID
			WHERE o.OrderDate >= @StartDate AND o.OrderDate <= @EndDate
			GROUP BY e.TitleOfCourtesy + ' ' + e.FirstName + ' ' + e.LastName, o.OrderDate, c.CompanyName, s.CompanyName;
		END
END
GO
