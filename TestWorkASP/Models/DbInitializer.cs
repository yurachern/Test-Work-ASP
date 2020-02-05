using System.Data.Entity;

namespace TestWorkASP.Models
{
    public class DbInitializer: CreateDatabaseIfNotExists<DataBaseOrdersContext>
    {
        protected override void Seed(DataBaseOrdersContext context)
        {
            context.Database.ExecuteSqlCommand("CREATE PROCEDURE [dbo].[sp_fillingCustomers] AS BEGIN DECLARE @number INT SET @number = 1 WHILE @number < 100 BEGIN INSERT INTO Customers VALUES ('Customer' + CONVERT(varchar, @number), 'Adress' + CONVERT(varchar, @number), 'Normal') SET @number += 1; END; END;");
            context.Database.ExecuteSqlCommand("CREATE PROCEDURE [dbo].[sp_fillingProducts] AS BEGIN DECLARE @number INT SET @number = 1 WHILE @number < 5000 BEGIN INSERT INTO Products VALUES ('Product' + CONVERT(varchar, @number)) SET @number += 1; END; END;");
            context.Database.ExecuteSqlCommand("CREATE PROCEDURE [dbo].[sp_fillingOrders] AS BEGIN DECLARE @customerId INT DECLARE customerCursor CURSOR FOR SELECT Customers.Id FROM Customers OPEN customerCursor FETCH NEXT FROM customerCursor INTO @customerId WHILE @@FETCH_STATUS = 0 BEGIN DECLARE @ordersCount INT, @currentOrder INT SET @ordersCount = RAND() * (45) + 5; SET @currentOrder = 1; WHILE @currentOrder < @ordersCount BEGIN INSERT INTO Orders VALUES (CONVERT(datetime2, GETDATE()), @customerId) set @currentOrder += 1; END; FETCH NEXT FROM customerCursor INTO @customerId END CLOSE customerCursor DEALLOCATE customerCursor end;");
            context.Database.ExecuteSqlCommand("CREATE PROCEDURE [dbo].[sp_fillingOrderLines] AS BEGIN DECLARE @orderId INT, @productId INT DECLARE orderCursor CURSOR FOR SELECT Orders.Id FROM Orders OPEN orderCursor FETCH NEXT FROM orderCursor INTO @orderId WHILE @@FETCH_STATUS = 0 BEGIN DECLARE productCursor CURSOR FOR SELECT Products.Id FROM Products OPEN productCursor FETCH NEXT FROM productCursor INTO @productId DECLARE @productCount INT, @currentProduct INT SET @productCount = RAND() * (99) + 1; SET @currentProduct = 1; WHILE @currentProduct < @productCount BEGIN INSERT INTO OrderLines VALUES (100, 10, 1000, @orderId, @productId) SET @currentProduct += 1; FETCH NEXT FROM productCursor INTO @productId END; DEALLOCATE productCursor FETCH NEXT FROM orderCursor INTO @orderId END; CLOSE orderCursor DEALLOCATE orderCursor END;");
            context.Database.ExecuteSqlCommand("CREATE PROCEDURE [dbo].[sp_updateCategory] @customerId INT AS BEGIN DECLARE @countOrders INT SELECT @countOrders = COUNT(*) FROM Orders WHERE Orders.Customer_Id = @customerId IF @countOrders <= 5  UPDATE Customers SET Customers.Category = 'Normal' WHERE Customers.Id = @customerId ELSE IF @countOrders < 30 UPDATE Customers SET Customers.Category = 'Medium' WHERE Customers.Id = @customerId ELSE IF @countOrders < 40 UPDATE Customers SET Customers.Category = 'Top' WHERE Customers.Id = @customerId ELSE UPDATE Customers SET Customers.Category = 'VIP' WHERE Customers.Id = @customerId END;");
            context.Database.ExecuteSqlCommand("CREATE TRIGGER Order__Insert ON Orders AFTER INSERT AS BEGIN DECLARE @customerId INT SELECT @customerId = inserted.Customer_Id FROM inserted EXEC sp_updateCategory @customerId END;");
            context.Database.ExecuteSqlCommand("CREATE TRIGGER Order__Delete ON Orders AFTER INSERT AS BEGIN DECLARE @customerId INT SELECT @customerId = deleted.Customer_Id FROM deleted EXEC sp_updateCategory @customerId END;");
            context.Database.ExecuteSqlCommand("exec [dbo].[sp_fillingCustomers]");
            context.Database.ExecuteSqlCommand("exec [dbo].[sp_fillingProducts]");
            context.Database.ExecuteSqlCommand("exec [dbo].[sp_fillingOrders]");
            context.Database.ExecuteSqlCommand("exec [dbo].[sp_fillingOrderLines]");           
            base.Seed(context);
        }
    }
}