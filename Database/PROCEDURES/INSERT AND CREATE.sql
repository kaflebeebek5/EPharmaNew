--CREATE TABLE tblSubCategory
--(Id int PRIMARY KEY IDENTITY(1,1),
--NAME NVARCHAR(100),
--CreatedBy [nvarchar](450) NOT NULL Foreign key(CreatedBy) references [Identity].Users(Id),
--CreatedOn DateTime ,
--LastModifiedBy [nvarchar](450) Foreign key(LastModifiedBy) references [Identity].Users(Id),
--LastModifiedOn Datetime,
--)

INSERT INTO tblsubcategory(Name,CreatedBy,LastModifiedBy,LastModifiedOn,CreatedOn)
SELECT 'Acne','89d203a5-96de-48a9-b0e9-547f9dbd85bb','89d203a5-96de-48a9-b0e9-547f9dbd85bb',GETDATE(),GETDATE()
UNION ALL
SELECT 'Viral','89d203a5-96de-48a9-b0e9-547f9dbd85bb','89d203a5-96de-48a9-b0e9-547f9dbd85bb',GETDATE(),GETDATE()
UNION ALL
SELECT 'Allergies','89d203a5-96de-48a9-b0e9-547f9dbd85bb','89d203a5-96de-48a9-b0e9-547f9dbd85bb',GETDATE(),GETDATE()
UNION ALL
SELECT 'Anaemia','89d203a5-96de-48a9-b0e9-547f9dbd85bb','89d203a5-96de-48a9-b0e9-547f9dbd85bb',GETDATE(),GETDATE()
UNION ALL
SELECT 'Anxiety','89d203a5-96de-48a9-b0e9-547f9dbd85bb','89d203a5-96de-48a9-b0e9-547f9dbd85bb',GETDATE(),GETDATE()
UNION ALL
SELECT 'Fever','89d203a5-96de-48a9-b0e9-547f9dbd85bb','89d203a5-96de-48a9-b0e9-547f9dbd85bb',GETDATE(),GETDATE()
UNION ALL
SELECT 'Digestion','89d203a5-96de-48a9-b0e9-547f9dbd85bb','89d203a5-96de-48a9-b0e9-547f9dbd85bb',GETDATE(),GETDATE()
UNION ALL
SELECT 'Depression','89d203a5-96de-48a9-b0e9-547f9dbd85bb','89d203a5-96de-48a9-b0e9-547f9dbd85bb',GETDATE(),GETDATE()
UNION ALL
SELECT 'Constipation','89d203a5-96de-48a9-b0e9-547f9dbd85bb','89d203a5-96de-48a9-b0e9-547f9dbd85bb',GETDATE(),GETDATE()
UNION ALL
SELECT 'Fungal','89d203a5-96de-48a9-b0e9-547f9dbd85bb','89d203a5-96de-48a9-b0e9-547f9dbd85bb',GETDATE(),GETDATE()
UNION ALL
SELECT 'Hyperpigmentation','89d203a5-96de-48a9-b0e9-547f9dbd85bb','89d203a5-96de-48a9-b0e9-547f9dbd85bb',GETDATE(),GETDATE()
UNION ALL
SELECT 'Glaucoma','89d203a5-96de-48a9-b0e9-547f9dbd85bb','89d203a5-96de-48a9-b0e9-547f9dbd85bb',GETDATE(),GETDATE()
UNION ALL
SELECT 'General','89d203a5-96de-48a9-b0e9-547f9dbd85bb','89d203a5-96de-48a9-b0e9-547f9dbd85bb',GETDATE(),GETDATE()



