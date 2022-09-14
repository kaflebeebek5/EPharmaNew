IF OBJECT_ID('SP_USER_MEDICINE') IS NOT NULL
DROP PROCEDURE SP_USER_MEDICINE
GO
CREATE PROCEDURE SP_USER_MEDICINE
@Flag CHAR=NULL,
@UserMedicineJson NVARCHAR(MAX)=NULL,
@BillId INT =NULL,
@UMedId INT=NULL,
@UserId nvarchar(450)=NULL,
@BillNumber nvarchar(200)=NULL,
@DoctorId nvarchar(450)=NULL
AS 
BEGIN
If @Flag='I'
BEGIN
BEGIN TRY
 BEGIN TRAN 
 INSERT INTO tblBillEntry(BillNumber,UserId,DocId,DoctorId,CreatedDate,StatusId)
 VALUES(@BillNumber,@UserID,@DoctorId,NULL,GETDATE(),1)
 SET @BillId=@@IDENTITY
 INSERT INTO tblUserMedicine(BillId,MedId,Quantity,Timing)
 SELECT @BillId,MedId,Quantity,Timing
 From OPENJSON(@UserMedicineJson)
 WITH(
 MedId INT '$.MedicineId',
 Quantity INT '$.Quantity',
 Timing NVARCHAR(200) '$.Timing'
 );
  COMMIT TRAN
 END TRY
 BEGIN CATCH
   SELECT '0' STATUS,ERROR_MESSAGE() MSG,ERROR_LINE()
			ROLLBACK TRAN 
			RETURN 
  END CATCH

END
IF @Flag='U'
BEGIN
 BEGIN TRAN
   DELETE FROM tblUserMedicine WHERE Id=@UMedId
    INSERT INTO tblUserMedicine(BillId,MedId,Quantity,Timing)
    SELECT @BillId,MedId,Quantity,Timing
    FROM OPENJSON(@UserMedicineJson)
   WITH(
   MedId INT '$.MedicineId',
   Quantity INT '$.Quantity',
   Timing NVARCHAR(200) '$.Timing'
   );
 COMMIT TRAN
END
If @Flag='G'
BEGIN
 Select M.Id,M.Name AS MedicineName,M.ExpiryDate,M.ImagePath,M.Description,B.UserId,UM.Timing,UM.Quantity FROM tblUserMedicine UM 
 JOIN tblBillEntry B WITH(NOLOCK) ON UM.BillId=B.Id
 JOIN tblMedicine M WITH(NOLOCK) ON UM.MedId=M.Id 
where  M.ExpiryDate>GETDATE()
END
END

