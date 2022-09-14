IF OBJECT_ID('SP_MEDICINE') IS NOT NULL
DROP PROCEDURE SP_MEDICINE
GO
CREATE PROCEDURE SP_MEDICINE
@Flag CHAR =NULL,
@Name NVARCHAR(100)=NULL,
@Description NVARCHAR(200)=NULL,
@ImagePath NVARCHAR(MAX)=NULL,
@ManuFactureDate DATETIME=NULL,
@ExpiryDate DATETIME=NULL,
@CreatedBy NVARCHAR(450)=NULL,
@UserId NVARCHAR(450)=NULL,
@QuantityAvailable INT=NULL,
@Quantity INT=NULL,
@Id INT =NULL,
@SalePrice Decimal(18,2) =NUll,
@BuyPrice Decimal(18,2) =NULL,
@Price DECIMAL(18,2)=NULL,
@CategoryId INT =NULL,
@ManufacturerName Nvarchar(500)=NULL,
@Unit Varchar(50)=Null,
@IsActive bit=Null,
@productId int=NULL,
@BillingAddress Nvarchar(500)=NULL,
@PhoneNumber nvarchar(20)=NULL,
@Remarks nvarchar(Max)=NULL,
@OTP NVARCHAR(8)=NULL,
@JsonFile NVARCHAR(MAX)=NULL,
@SubCategory NVARCHAR(500)=NULL,
@SearchName NVARCHAR(100)=NULL
AS
BEGIN
If @Flag='I'
 BEGIN
     INSERT INTO tblMedicine(Name,Description,ImagePath,ManufactureDate,ExpiryDate,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,StatusId,QuantityAvailable,BuyPrice,SalePrice,ManufacturerName,IsPrescriptionReq,Unit,CategorId,SubCategory)
	 VALUES (@Name,@Description,@ImagePath,@ManuFactureDate,@ExpiryDate,@CreatedBy,GETDATE(),@CreatedBy,GETDATE(),1,@QuantityAvailable,@BuyPrice,@SalePrice,@ManufacturerName,@IsActive,@Unit,@CategoryId,@SubCategory)
 END
 If @Flag='U'
 BEGIN
   BEGIN TRY
     Update tblMedicine Set 
	 Name=@Name,
	 Description=@Description,
	 ImagePath=@ImagePath,
	 ManufactureDate=@ManuFactureDate,
	 ExpiryDate=@ExpiryDate,
	 LastModifiedOn=GETDATE(),
	 QuantityAvailable=@QuantityAvailable,
	 BuyPrice=@BuyPrice,
	 SalePrice=@SalePrice,
	 ManufacturerName=@ManufacturerName,
	 Unit=@Unit,
	 IsPrescriptionReq=@IsActive,
	 CategorId=@CategoryId
	 WHERE Id=@Id
   END TRY
   BEGIN CATCH
    SELECT '0' STATUS,ERROR_MESSAGE() MSG,ERROR_LINE()
   END CATCH
 END
 If @Flag='G'
 BEGIN
  SELECT T.Id,T.Name,Description,ManufactureDate,ExpiryDate,QuantityAvailable,T.ImagePath,Unit,CategorId,SalePrice,BuyPrice,ManufacturerName as Manufacturer,IsPrescriptionReq as IsAstive,c.Name as CategoryName
  FROM tblMedicine T WITH(NOLOCK) 
  LEFT JOIN tblCategory c WITH(NOLOCK) ON T.CategorId=c.Id
  where StatusId=1
 END
  If @Flag='D'
 BEGIN
  Update tblMedicine set StatusId=2 where Id=@Id
 END
 If @Flag='E'
 BEGIN
  Select Id,Name,ImagePath from tblCategory WITH(NOLOCK)
 END
 IF @Flag='H'
 BEGIN
   select TOP 100 Id,Name,BuyPrice,IsPrescriptionReq,ImagePath,Description from TblMedicine WITH(NOLOCK) where CategorId=@Id and StatusId=1 and ExpiryDate>GETDATE()
 END
 IF @Flag='J'
 BEGIN
  SElect TOP 10 Name,ImagePath,Description,IsPrescriptionReq,SalePrice from tblMedicine WITH(NOLOCK)
  ORder By Id Desc
 END
 IF @Flag='K'
 BEGIN
    select Id,Name,SalePrice,IsPrescriptionReq as IsActive,ImagePath,Description,QuantityAvailable from TblMedicine WITH(NOLOCK) where Id=@Id
 END
 IF @Flag='L'
 BEGIN
   INSERT INTO TblUserOrder(ProductId,PrescriptionImage,UserId,PhoneNumber,BillingAddress,Remarks,IsApproved,IsDelivered,OrderDate)
   VALUES(@ProductId,@ImagePath,@CreatedBy,@PhoneNumber,@BillingAddress,@Remarks,0,0,GETDATE())
 END
 IF @Flag='M'
 BEGIN
   Insert INTO tblUserOTP(PhoneNumber,OTP,ISUsed,CreatedDate) 
   SELECT @PhoneNumber,@OTP,0,GETDATE()
 END
 IF @Flag='N'
 BEGIN 
   SELECT OTP,(datediff(SECOND,CreatedDate,GETDATE())) as OTPTIme FROM tblUserOTP Where PhoneNumber=@PhoneNumber and ISUSED=0
   ORDER BY Id DESC
 END
 If @Flag='O'
 BEGIN
   Update tblUserOTP set ISUSED=1 where PhoneNumber=@PhoneNumber
 END
 If @Flag='P'
 BEGIN
   select O.Id,PrescriptionImage as ImagePath,UserId,PhoneNumber,BillingAddress,Remarks,ProductId,M.Name as ProductName,IsApproved,IsDelivered from tblUserOrder O WITH(NOLOCK)
   LEFT JOIN  tblMedicine M WITH(NOLOCK) ON M.Id=O.ProductId
   ORDER BY O.ID DESC
 END
 IF @Flag='Q'
 BEGIN
  INSERT INTO tblCart(ProductId,Quantity,Price,USERID,PrescriptionPath,CreatedDate,Status)
  VALUES(@productId,@Quantity,@price,@UserId,@ImagePath,GETDATE(),1)
 END
 IF @Flag='R'
 BEGIN
   SELECT SUM(Quantity) as Count FROM tblCart where UserId=@UserId and Status=1
 END
 IF @flag='S'
 BEGIN 
   select UserId,C.ID,ProductId,Quantity,QuantityAvailable as AvailableQuantity,Price,CreatedDate,M.Name,M.Name as ProductName,C.PrescriptionPath as ImagePath from tblcart C WITH(NOLOCK)
   left join tblMedicine M WITH(NOLOCK) ON C.PRODUCTID=M.Id
   where UserId=@UserId and Status=1 and M.StatusId=1
 END
 IF @Flag='T'
 BEGIN
  Update tblCart set Status=2 where ID=@Id
 END
 IF @Flag='V'
 BEGIN
  BEGIN TRY
    BEGIN TRAN
      INSERT INTO TblUserOrder(ProductId,UserId,PhoneNumber,BillingAddress,Remarks,IsApproved,IsDelivered,OrderDate)
	  Values(@productId,@UserId,@PhoneNumber,@BillingAddress,@Remarks,0,0,GETDATE())

	  INSERT INTO tblMultipleRequest(ProductId,Prescription,Quantity)
	  select ProductId,PrescriptionImage,Quantity
	  From OPENJSON(@JsonFile)
      WITH(
      ProductId INT '$.ProductId',
      Quantity INT '$.Quantity',
      PrescriptionImage NVARCHAR(Max) '$.ImagePath'
      );
 COMMIT TRAN
 END TRY
 BEGIN CATCH
   ROLLBACK TRAN
   ;throw 
 END CATCH
 END
 IF @Flag='W'
 BEGIN
  UPDATE tblCart set Status=2 where UserId=@UserId
 END
 IF @Flag='X'
 BEGIN
   select UserId,C.ID,ProductId,Quantity,QuantityAvailable,Price,CreatedDate,M.Name,M.Name as ProductName,C.PrescriptionPath as ImagePath from tblcart C WITH(NOLOCK)
   left join tblMedicine M WITH(NOLOCK) ON C.PRODUCTID=M.Id
   where UserId=@UserId and Status=1 and M.StatusId=1
 END
 IF @Flag='Y'
 BEGIN
   update M set QuantityAvailable=(QuantityAvailable-Quantity) from tblMedicine M WITH(NOLOCK)
   JOIN tblCart C WITH(NOLOCK) ON C.PRODUCTID=M.Id
   where Status=1 and StatusId=1 and UserId=@UserId
 END
  IF @Flag='Z'
 BEGIN
   update M set QuantityAvailable=(QuantityAvailable+Quantity) from tblMedicine M WITH(NOLOCK)
   JOIN tblCart C WITH(NOLOCK) ON C.PRODUCTID=M.Id
   where Status=1 and StatusId=1 and UserId=@UserId
 END
 IF @Flag='1'
 BEGIN
    SELECT T.Id,T.Name,Description,ManufactureDate,ExpiryDate,QuantityAvailable,T.ImagePath,Unit,CategorId,SalePrice,BuyPrice,ManufacturerName as Manufacturer,IsPrescriptionReq as IsAstive
    FROM tblMedicine T WITH(NOLOCK) where Name like '%'+@SearchName+'%'
 END
END










   
