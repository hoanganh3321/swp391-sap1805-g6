create database banhang_3

CREATE TABLE Category (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(50) NOT NULL,
    Description NVARCHAR(255)
);
INSERT INTO Category (CategoryName, Description)
VALUES 
    ('Rings', 'Various rings made of gold, silver, diamonds, and gemstones.'),
    ('Necklaces', 'Beautiful and luxurious necklaces for every occasion.'),
    ('Bracelets', 'Bracelets made of silver, gold, and various gemstones.'),
    ('Earrings', 'Earrings made of gold, silver, and diamonds.'),
    ('Pendants', 'Pendants with diverse and exquisite designs.'),
    ('Anklets', 'Anklets made of silver, gold, and various gemstones.'),
    ('Jewelry Sets', 'Complete jewelry sets for special occasions.'),
    ('Brooches', 'Elegant and stylish brooches for all occasions.'),
    ('Cufflinks', 'Stylish cufflinks for formal wear.'),
    ('Hair Accessories', 'Beautiful hair accessories made with precious materials.');




INSERT INTO Category (CategoryName, Description)
VALUES 
    ('Rings', 'A collection of beautiful rings for all occasions.'),
    ('Necklaces', 'Elegant necklaces crafted with the finest materials.'),
    ('Earrings', 'Stylish earrings to complement any look.'),
    ('Bracelets', 'Trendy bracelets for everyday wear.'),
    ('Watches', 'Luxurious watches for both men and women.'),
    ('Chains', 'Classic chains suitable for various pendants.'),
    ('Brooches', 'Exquisite brooches to add a touch of glamour.'),
    ('Pendants', 'Charming pendants in various designs.'),
    ('Anklets', 'Dainty anklets perfect for summer vibes.'),
    ('Charms', 'Adorable charms to personalize your jewelry collection.');
	delete from Category

CREATE TABLE Store (
    StoreID INT PRIMARY KEY IDENTITY(1,1),
    StoreName NVARCHAR(100) NOT NULL,
    [Location] NVARCHAR(255),
	Revenue DECIMAL(18, 2) DEFAULT 0
);
INSERT INTO Store (StoreName, Location, Revenue)
VALUES
    ('Cửa hàng Đá quý & Trang sức Trung Tâm', '123 Đường ABC, Quận 1, Thành phố Hồ Chí Minh', 1000000.00),
    ('Cửa hàng Vàng Bạc Ngọc Thạch', '456 Đường DEF, Quận 3, Thành phố Hồ Chí Minh', 500000.00),
    ('Cửa hàng Trang sức Kim Cương', '789 Đường GHI, Quận 5, Thành phố Hồ Chí Minh', 750000.00),
    ('Cửa hàng Đá quý Hoàng Gia', '101 Đường JKL, Quận 10, Thành phố Hồ Chí Minh', 200000.00),
    ('Cửa hàng Trang sức Phượng Hoàng', '202 Đường MNO, Quận 7, Thành phố Hồ Chí Minh', 1200000.00);

	drop table Product

CREATE TABLE Product (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(100) NOT NULL,
    Barcode NVARCHAR(50),
    [Weight] DECIMAL(10, 2),
    Price DECIMAL(10, 2),
    Manufacturing_Cost DECIMAL(10, 2),
    Stone_Cost DECIMAL(10, 2),
    Warranty NVARCHAR(255),
	Quantity Int,
    Is_Buyback BIT DEFAULT 0,  
    CategoryID INT,
	StoreID INT,
	[Image] NVARCHAR(255), 
    FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID),
	FOREIGN KEY (StoreID) REFERENCES Store(StoreID)
);
-- Insert sample data into Product table

INSERT INTO Product (ProductName, Barcode, [Weight], Price, Manufacturing_Cost, Stone_Cost, Warranty, Quantity, Is_Buyback, CategoryID, StoreID,[Image])
VALUES 
	('Gold Ring', '123456789012', 10.50, 19536.375, 200, 150, '2 years warranty', 20, 0, 17, 1,
	'https://www.tanishq.co.in/on/demandware.static/-/Sites-Tanishq-product-catalog/default/dw97a38a7c/images/hi-res/503419FIPAA09_1.jpg'),
	('Silver Necklace', '123456789013', 5.25, 9835.375, 100, 50, '1 year warranty', 15, 0, 18, 1,
	'https://m.media-amazon.com/images/I/71F1VISOs4L._AC_UY1000_.jpg'),
    ('Diamond Bracelet', '123456789014', 7.75, 17645, 300, 250, '3 years warranty', 10, 0, 19, 1,
	'https://image.brilliantearth.com/media/product_images/00/BE5D100TB_white_top.jpg'),
    ('Platinum Earrings', '123456789015', 2.50, 5138.125, 400, 100, '2 years warranty', 25, 0, 20, 1,
	'https://cdn.caratlane.com/media/catalog/product/J/E/JE00948-PTP900_1_lar.jpg'),
    ('Gold Pendant', '123456789016', 4.00, 7878.4, 150, 200, '5 years warranty', 30, 0, 21, 1,
	'https://img3.junaroad.com/uiproducts/18536064/pri_175_p-1663947657.jpg'),
    ('Gold Anklet', '123456789017', 3.75, 7261.3125, 180, 120, '2 years warranty', 22, 0, 22, 2,
	'https://i.etsystatic.com/11516823/r/il/da2dad/3084415211/il_fullxfull.3084415211_i3sp.jpg'),
    ('Silver Jewelry Set', '123456789018', 15.00, 28132.5, 250, 300, '3 years warranty', 12, 0, 23, 2,
	'https://www.maharaniwomen.com/wp-content/uploads/2023/01/162290.jpg'),
    ('Gold Brooch', '123456789019', 6.25, 12287.5, 220, 180, '1 year warranty', 18, 0, 24, 2,
	'https://i.ebayimg.com/images/g/A80AAOSwU7Bi4vRg/s-l1200.webp'),
    ('Silver Cufflinks', '123456789020', 1.50, 2852.875, 90, 60, '6 months warranty', 35, 0, 25, 2,
	'https://assets.aspinaloflondon.com/cdn-cgi/image/fit=pad,format=auto,quality=85,width=999,height=999/images/original/177876-047-1619-104100000.jpg'),
    ('Gold Hairpin', '123456789021', 2.25, 4512.225, 140, 70, '1 year warranty', 40, 0, 26, 2,
	'https://www.roots.gov.sg/CollectionImages/1147462.jpg'),
    ('Diamond Ring', '123456789022', 3.00, 6192.25, 320, 400, '4 years warranty', 8, 0, 17, 3,
	'https://www.nektanewyork.com/cdn/shop/products/10-carat-princess-cut-diamond-engagement-ring-28999330988206_2000x.jpg?v=1643652837'),
    ('Platinum Necklace', '123456789023', 8.50, 16703.25, 350, 280, '2 years warranty', 7, 0, 18, 3,
	'https://platinumborn.com/cdn/shop/files/PTN2013-CosmicTapestryNecklaceLarge-Necklace-Prestige.jpg?v=1694528282'),
    ('Silver Bracelet', '123456789024', 6.75, 13657.5, 210, 130, '1 year warranty', 9, 0, 19, 3,
	'https://m.media-amazon.com/images/I/71Tyj8-ZaQL._SL1500_.jpg'),
    ('Gold Earrings', '123456789025', 2.00, 3970.625, 180, 90, '3 years warranty', 15, 0, 20, 3,
	'https://www.charleskeith.vn/dw/image/v2/BCWJ_PRD/on/demandware.static/-/Sites-vn-products/default/dw37643973/images/hi-res/2023-L6-CK5-42120375-27-1.jpg?sw=756&sh=1008'),
    ('Platinum Pendant', '123456789026', 5.50, 10886.55, 230, 170, '5 years warranty', 10, 0, 21, 3,
	'https://www.tanishq.co.in/on/demandware.static/-/Sites-Tanishq-product-catalog/default/dw520d675e/images/hi-res/741188PJUAAA04.jpg'),
    ('Gold Anklet', '123456789027', 4.75, 9004.8125, 160, 110, '2 years warranty', 13, 0, 22, 3,
	'https://images.cltstatic.com/media/product/350/AI00070-SS000P-link-of-love--stone-anklets-in-gold-plated--silver-prd-1-model.jpg'),
    ('Diamond Jewelry Set', '123456789028', 12.50, 23818.75, 400, 450, '4 years warranty', 11, 0, 23, 4,
	'https://jahan.ch/wp-content/uploads/2020/07/Diamond-bridal-set-1.png'),
    ('Platinum Brooch', '123456789029', 7.00, 15220, 240, 200, '2 years warranty', 6, 0, 24, 4,
	'https://moirafinejewellery.com/cdn-cgi/image/format=webp/22057-large_default/belle-epoque-diamond-and-platinum-brooch-circa-1910.jpg'),
    ('Gold Cufflinks', '123456789030', 1.75, 3343.6875, 100, 80, '6 months warranty', 28, 0, 25, 4,
	'https://www.ninesparis.com/35170-thickbox_default/gold-cufflinks-barcelona.jpg'),
    ('Diamond Hairpin', '123456789031', 2.50, 5228.275, 170, 120, '3 years warranty', 19, 0, 26, 4,
	'https://suzannekalan.com/cdn/shop/products/HA110-WG-2_web.jpg?v=1666651106&width=1638');


delete from Product

CREATE TABLE [Role] (
    RoleID INT PRIMARY KEY,
    RoleName NVARCHAR(50) NOT NULL,
    [Description] NVARCHAR(255),
	CONSTRAINT CK_Role_RoleID CHECK (RoleID IN (1, 2)),
	CONSTRAINT CK_Role_RoleName CHECK (RoleName IN ('ad', 'staff'))
);


CREATE TABLE [Admin] (
    AdminID INT PRIMARY KEY IDENTITY(1,1),
    AdminName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    [Password] NVARCHAR(255) NOT NULL,
    RoleID INT DEFAULT 1,
	Phone NVARCHAR(20),
    FOREIGN KEY (RoleID) REFERENCES Role(RoleID)
);
CREATE TABLE Staff (
    StaffID INT PRIMARY KEY IDENTITY(1,1),
    StaffName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    [Password] NVARCHAR(255) NOT NULL,
    RoleID INT DEFAULT 2,
    StoreID INT,
	Phone NVARCHAR(20),
	Hire_Date DATE,
    FOREIGN KEY (RoleID) REFERENCES Role(RoleID),
    FOREIGN KEY (StoreID) REFERENCES Store(StoreID)
);

INSERT INTO Staff (StaffName, Email, [Password], RoleID, StoreID, Phone, Hire_Date)
VALUES 
('Alice Nguyen', 'alice.nguyen@example.com', 'hashedPassword1', 2, 1, '0901234567', '2022-01-15'),
('Bob Tran', 'bob.tran@example.com', 'hashedPassword2', 2, 2, '0902345678', '2023-03-20'),
('Charlie Le', 'charlie.le@example.com', 'hashedPassword3', 2, 3, '0903456789', '2021-07-10');

CREATE TABLE Promotion (
  PromotionID INT PRIMARY KEY IDENTITY(1,1),
  [Name] VARCHAR(255),
  [Start_Date] DATE,
  End_Date DATE,
  Discount DECIMAL(5, 2),
  Approved BIT DEFAULT 0,  
  Approved_By int
  CONSTRAINT FK_Promotion_Approved_By FOREIGN KEY (Approved_By) REFERENCES [Admin](AdminID)
);
drop table Promotion
CREATE TABLE ReturnPolicy (
  PolicyID INT PRIMARY KEY IDENTITY(1,1),
  [Description] VARCHAR(255),
);
CREATE TABLE Customer (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100),
	[Password] NVARCHAR(20),
    PhoneNumber NVARCHAR(20),
    [Address] NVARCHAR(255),	
);
CREATE TABLE [Order] (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT,
    OrderDate DATETIME,
    TotalAmount DECIMAL(18, 2),--tong tien don hang
    CONSTRAINT FK_Order_Customer FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);
delete from [Order]
drop table [Order]
CREATE TABLE OrderDetail (
    OrderDetailID INT PRIMARY KEY IDENTITY(1,1),
    OrderID INT,
    ProductID INT,
    Quantity INT,
    UnitPrice DECIMAL(18, 2),--gia moi mat hang
    CONSTRAINT FK_OrderDetail_Order FOREIGN KEY (OrderID) REFERENCES [Order](OrderID),
  
);
-- CONSTRAINT FK_OrderDetail_Product FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
delete from OrderDetail
drop table OrderDetail
CREATE TABLE GoldPriceDisplay (
    DisplayID INT PRIMARY KEY IDENTITY(1,1),
    DeviceID INT NOT NULL,
    Location NVARCHAR(100),
    GoldPrice DECIMAL(10, 2) NOT NULL,
    LastUpdated DATETIME NOT NULL
);
drop table GoldPriceDisplay
-- Tạo bảng Invoice
CREATE TABLE Invoice (
    InvoiceID INT PRIMARY KEY IDENTITY(1,1), -- Khóa chính, tự tăng
    OrderID INT, -- Khóa ngoại liên kết với bảng Order
    PromotionID INT, -- Khóa ngoại liên kết với bảng Promotion
    PromotionName NVARCHAR(255), -- Tên của khuyến mãi
    TotalPrice DECIMAL(18, 2), -- Tổng giá của hóa đơn
	StaffID INT,
    CONSTRAINT FK_Invoice_Order FOREIGN KEY (OrderID) REFERENCES [Order](OrderID), -- Ràng buộc khóa ngoại đến bảng Order
    CONSTRAINT FK_Invoice_Promotion FOREIGN KEY (PromotionID) REFERENCES Promotion(PromotionID), -- Ràng buộc khóa ngoại đến bảng Promotion
	CONSTRAINT FK_Invoice_Staff FOREIGN KEY (StaffID) REFERENCES Staff(StaffID)
);
ALTER TABLE Invoice
ADD CONSTRAINT UQ_Invoice_OrderID UNIQUE (OrderID);
drop table Invoice


INSERT INTO GoldPriceDisplay (DeviceID, Location, GoldPrice, LastUpdated)
VALUES 
    (101, 'vietnam', 1850.75, '2024-05-30 14:23:45');
CREATE TABLE LoyaltyPoints (
    ID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT NOT NULL,
    Points INT NOT NULL DEFAULT 0,
    LastUpdated DATETIME NOT NULL,
    CONSTRAINT FK_LoyaltyPoints_Customer FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);
delete from LoyaltyPoints
CREATE TABLE DashboardStatistics (
    StatisticID INT PRIMARY KEY IDENTITY(1,1),
    StatisticName NVARCHAR(100) NOT NULL,
    StatisticValue DECIMAL(18, 2) NOT NULL,
    StatisticDate DATE NOT NULL
);
delete from Promotion
CREATE TABLE ProductReturns (
    ProductID INT PRIMARY KEY,
    CustomerID INT NOT NULL,    
    ReturnDate DATETIME NOT NULL,
    ReturnReason NVARCHAR(255),
    CONSTRAINT FK_ProductReturns_Customer FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    CONSTRAINT FK_ProductReturns_Product FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);
DROP TABLE ProductReturns

