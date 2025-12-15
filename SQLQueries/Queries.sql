USE MyStore

SELECT Id, Name, Description, Price FROM Products WHERE Price > 300 ORDER BY price

CREATE Procedure [SP_GetSuitableProducts]
    @fromPrice decimal,
    @toPrice decimal
AS
SELECT * FROM Products
WHERE Price BETWEEN @fromPrice AND @toPrice 
ORDER BY Price

EXEC [SP_GetSuitableProducts] 10, 10000

CREATE PROCEDURE [dbo].[PricesUp] 
   @procent smallint, @higherPrice decimal OUTPUT, @lowerPrice decimal OUTPUT
AS
BEGIN
 UPDATE [Products]
 SET Price = Price + (Price / 100 * @procent);

 SET @higherPrice = (SELECT MAX(Price) FROM Products);
 SET @lowerPrice = (SELECT MIN(Price) FROM Products);
END


DECLARE @result decimal
DECLARE @result2 decimal

EXECUTE [dbo].[PricesUp] 10, @result OUTPUT,@result2 OUTPUT

PRINT @result
PRINT @result2

CREATE FUNCTION GetTheMostPopularProduct(@datefrom date, @dateto date)
RETURNS TABLE 
AS
RETURN
(SELECT * FROM Products 
WHERE Code =
(SELECT ProductId FROM (SELECT ProductId, SUM(od.Quantity) sumProducts FROM Products p
JOIN OrderDetails od
ON (p.Code = od.ProductId)
GROUP BY ProductId) d
WHERE sumProducts =
(SELECT MAX(sumProducts) FROM
(SELECT ProductId, SUM(od.Quantity) sumProducts FROM Products p
JOIN OrderDetails od
ON (p.COde = od.ProductId)
JOIN Orders o 
ON (o.Id = od.OrderId)
WHERE o.Date BETWEEN @datefrom AND @dateto
GROUP BY ProductId) g)))

SELECT * FROM GetTheMostPopularProduct('2024-01-01', '2026-01-01')


CREATE VIEW V_EXPENSIVE_PRODUCTS
AS
SELECT NAME, PRICE, SellPRICE FROM PRODUCTS
WHERE Price > 1000


SELECT * FROM V_EXPENSIVE_PRODUCTS

UPDATE V_EXPENSIVE_PRODUCTS
SET PRICE = PRICE + 10 