

update [RDC].[Products] set StockQuantity = 0;

delete from RDC.OrderProducts;
delete from RDC.OrderAdditionalFees;
delete from RDC.Orders;

delete from RDC.RestockingAdditionalFees;
delete from RDC.RestockingProducts;
delete from RDC.Restockings;

delete from RDC.Products;

delete from RDC.PrintControls;