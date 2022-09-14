IF OBJECT_ID('SP_BILL_ENTRY') IS NOT NULL
DROP PROCEDURE SP_BILL_ENTRY
GO
CREATE PROCEDURE SP_BILL_ENTRY
@Id INT =NULL,
@Flag CHAR=NULL,
@BillNumber nvarchar(100)=NULL,
@UserID NVARCHAR(450)=NULL,
@DoctorId INT=NULL
AS
BEGIN
If @Flag='I'
 BEGIN
  INSERT INTO tblBillEntry(BillNumber,UserId,DoctorId,CreatedDate,StatusId)
  VALUES(@BillNumber,@UserID,@DoctorId,GETDATE(),1)
 END
If @Flag='U'
 BEGIN
  Update tblBillEntry SET
  BillNumber=@BillNumber,
  UserId=@UserID,
  DoctorId=@DoctorId
  WHERE Id=@Id 
 END
 IF @Flag='D'
 BEGIN
  UPDATE tblBillEntry set StatusId=2 where Id=@Id
 END
 IF @Flag='G'
 BEGIN
  Select B.Id,UserId,DoctorId,d.Name,CONCAT(U.FirstName,U.LastName) as UserName from tblBillEntry B
  JOIN [Identity].Users U WITH(NOLOCK) ON B.UserId=U.Id 
  JOIN tblDoctor D WITH(NOLOCK) ON B.DoctorId=D.ID
 END
 If @Flag='Z'
 BEGIN
   select Id,CONCAT(FirstName,' ',LastName) as Name from [Identity].Users
 END
END