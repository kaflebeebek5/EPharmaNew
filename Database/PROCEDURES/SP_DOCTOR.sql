IF OBJECT_ID('SP_DOCTOR') IS NOT NULL
DROP PROCEDURE SP_DOCTOR
GO
CREATE PROCEDURE SP_DOCTOR
@Flag CHAR =NULL,
@Name NVARCHAR(100)=NULL,
@Email NVARCHAR(20)=NULL,
@ImagePath NVARCHAR(MAX)=NULL,
@Specialist NVARCHAR(MAX)=NULL,
@PhoneNumber NVARCHAR(20)=NULL,
@Address NVARCHAR(MAX)=NULL,
@CreatedBy NVARCHAR(450)=NULL,
@QuantityAvailable INT=NULL,
@Id INT =NULL,
@GenderId INT=NULL
AS
BEGIN
If @Flag='I'
 BEGIN
     INSERT INTO tblDoctor(Name,Email,PhoneNumber,Address,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,StatusId,ImagePath,Specialist,GenderID)
	 VALUES (@Name,@Email,@PhoneNumber,@Address,@CreatedBy,GETDATE(),@CreatedBy,GETDATE(),1,@ImagePath,@Specialist,@GenderId)
 END
 If @Flag='U'
 BEGIN
   BEGIN TRY
     Update tblDoctor Set 
	 Name=@Name,
	 Email=@Email,
	 ImagePath=@ImagePath,
	 Address=@Address,
	 PhoneNumber=@PhoneNumber,
	 CreatedBy=@CreatedBy,
	 LastModifiedBy=@CreatedBy,
	 LastModifiedOn=GETDATE(),
	 Specialist=@Specialist
	 WHERE Id=@Id
   END TRY
   BEGIN CATCH
    SELECT '0' STATUS,ERROR_MESSAGE() MSG,ERROR_LINE()
   END CATCH
 END
 If @Flag='G'
 BEGIN
  SELECT D.ID,G.Name as Gender,D.Name as Name,Email,Address,Specialist,PhoneNumber,ImagePath,GenderId FROM tblDoctor D
  join Genders G with(nolock) on D.GenderId=G.Id where StatusId=1
 END
  If @Flag='D'
 BEGIN
  Update tblDoctor set StatusId=2 where Id=@Id
 END
END
