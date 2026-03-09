SELECT P.Description, SUB.TotalQuantity FROM (
SELECT
  ProductId,
  SUM(COALESCE(Quantity, 0)) AS TotalQuantity
FROM RDC.OrderProducts OP 
GROUP BY OP.ProductId) SUB
JOIN RDC.Products P ON P.Id = SUB.ProductId
ORDER BY TotalQuantity DESC

