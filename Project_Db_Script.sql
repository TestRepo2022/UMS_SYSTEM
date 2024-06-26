USE [project_db]
GO
/****** Object:  Table [dbo].[msg_templatemaster]    Script Date: 6/22/2024 11:54:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[msg_templatemaster](
	[Rid] [int] IDENTITY(1,1) NOT NULL,
	[template] [varchar](max) NULL,
	[templatecode] [int] NULL,
	[Isactive] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Rid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[templatecode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[sms_log]    Script Date: 6/22/2024 11:54:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sms_log](
	[Rid] [int] IDENTITY(1,1) NOT NULL,
	[message] [varchar](max) NULL,
	[templatecode] [int] NULL,
	[userid] [int] NULL,
	[sendon] [datetime] NULL,
	[Isactive] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Rid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_UserMaster]    Script Date: 6/22/2024 11:54:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_UserMaster](
	[Rid] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [varchar](20) NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Mobile] [varchar](13) NULL,
	[Password] [varchar](500) NULL,
	[Gender] [varchar](11) NULL,
	[IsActive] [tinyint] NULL,
	[LockCount] [tinyint] NULL,
	[UnLockDatetime] [datetime] NULL,
	[ProfilePic] [varchar](500) NULL,
	[Department] [int] NULL,
	[Desgination] [varchar](50) NULL,
	[PsswdOTP] [varchar](10) NULL,
	[ExpireOTP] [datetime] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Rid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[GenerateOTP]    Script Date: 6/22/2024 11:54:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[GenerateOTP](
@Rid int
)
AS
BEGIN
    DECLARE @v_otp  varchar(6);
    Declare @template varchar(200);
    declare @mobile varchar(15)

	IF(NOT EXISTS(SELECT 1 FROM tbl_UserMaster(NOLOCK) WHERE Rid=@Rid AND ISNULL(Mobile,'')<>''))
	BEGIN
	RAISERROR('Mobile number not found. please update in system first!',16,1);
	return 
	END

	IF( EXISTS(SELECT 1 FROM tbl_UserMaster(NOLOCK) WHERE Rid=@Rid and ISNULL(UnLockDatetime,getdate())>GETDATE()))
	BEGIN
	RAISERROR('Your account is locked please contact to adminisitrator!',16,1);
	return 
	END

	begin try
	begin transaction trn_generate_otp
	
	select @mobile=Mobile from tbl_UserMaster(nolock) where Rid=@Rid
    
	IF(EXISTS(SELECT 1 FROM tbl_UserMaster WHERE Rid=1 AND DATEDIFF(MINUTE,DATEADD(MINUTE,-5,ExpireOTP),GETDATE())<1))
	BEGIN
	SELECT @v_otp=PsswdOTP from tbl_UserMaster where Rid=@Rid
	END
	ELSE
	BEGIN
	-- Generate a random 6-digit OTP
    SET @v_otp = FLOOR(RAND() * 900000) + 100000;
    -- Insert OTP record into the OTP table
    UPDATE tbl_UserMaster set PsswdOTP=@v_otp,ExpireOTP=DATEADD(MINUTE,5,GETDATE()) where Rid=@Rid
    END
	
	Select @template= template from msg_templatemaster where templatecode=1
    set @template=REPLACE (@template,'@otp',@v_otp)
	
	INSERT INTO SMS_LOG (message,
					templatecode,
					userid,
					sendon,
					Isactive)
				VALUES
				(
				@template,
				1,
				@Rid,
				GETDATE(),
				1)

	select @template msg, @mobile mobile
		commit transaction trn_generate_otp
		end try
		begin catch
		exec RethrowError
	    rollback transaction trn_generate_otp
		end
 catch
END


GO
/****** Object:  StoredProcedure [dbo].[LOGINUSER]    Script Date: 6/22/2024 11:54:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Author: Darshit Chaudhary
Created on: 20-04-2024
Description:- This is for the LogIn of the User it check the Credential and then USER will Log in if it is Authentic
*/--
 
CREATE PROC [dbo].[LOGINUSER] 
@USERNAME varchar(50),
@PASSWORD VARCHAR(500)
AS
BEGIN
 
	BEGIN TRY
		----RAISE ERROR IF EMAIL IS NULL----
		IF(ISNULL(@USERNAME,'')='')
		BEGIN
				RAISERROR('EMAIL ID IS EMPTY',16,1)
				RETURN 0
		END
 
		----RAISE ERROR IS PASSWORD IS NULL----
		IF(ISNULL(@PASSWORD,'')='')
		BEGIN
				RAISERROR('PASSWORD IS EMPTY',16,1)
				RETURN 0
		END
 
		----RAISE ERROR IF THE USER IS INACTIVE----
 
		IF(EXISTS(SELECT 1 FROM tbl_UserMaster (NOLOCK) WHERE IsActive=0 AND Email=@USERNAME ))
			BEGIN 
			RAISERROR('USER IS INACTIVE',16,1);
			RETURN 0
			END
		----RAISE ERROR IF THE MAXIMUM ATTEMPT IS 4 AND USER IS LOCKED THEN COUNT THE LOCK COUNT----
 
		IF(EXISTS(SELECT 1 FROM tbl_UserMaster (NOLOCK) WHERE lockCount>=3 AND Email=@USERNAME ))
			BEGIN 
			RAISERROR('YOUR ACCOUNT IS LOCKED PLEASE CONTACT WITH THE ADMINISTRATOR',16,1);
			RETURN 0
			END


 
		---RAISE ERROR IF USERNAME AND PASSWORD NOT MATCH---
		IF(EXISTS(SELECT 1 FROM tbl_UserMaster(NOLOCK) WHERE Email=@USERNAME AND PASSWORD=@PASSWORD))
			BEGIN
			UPDATE tbl_UserMaster SET lockCount=0,UnLockDatetime=null where Email=@USERNAME

			SELECT Rid,
					UserId,
					FirstName,
					LastName,
					Email,
					Mobile,
					Gender,
					IsActive,
					ProfilePic,
					Department,
					Desgination
			 FROM tbl_UserMaster where Email=@USERNAME
			END
		ELSE
		BEGIN
		UPDATE tbl_UserMaster SET LockCount=ISNULL(LockCount,0)+1 WHERE EMAIL=@USERNAME
		UPDATE tbl_UserMaster SET UnLockDatetime=DATEADD(MINUTE,10,GETDATE()) WHERE EMAIL=@USERNAME AND LockCount>=3
		 
		RAISERROR('Invalid UserName/Password!',16,1)
		END
	END TRY
    BEGIN CATCH 
	exec RethrowError
	END	CATCH
 
END
GO
/****** Object:  StoredProcedure [dbo].[RethrowError]    Script Date: 6/22/2024 11:54:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RethrowError] AS  
    /* RETURN IF THERE IS NO ERROR INFORMATION TO RETRIEVE. */  
    IF ERROR_NUMBER() IS NULL  
        RETURN;  
 
    DECLARE  
        @ErrorMessage    NVARCHAR(4000),  
        @ErrorNumber     INT,  
        @ErrorSeverity   INT,  
        @ErrorState      INT,  
        @ErrorLine       INT,  
        @ErrorProcedure  NVARCHAR(200);  
 
    /* ASSIGN VARIABLES TO ERROR-HANDLING FUNCTIONS THAT  
       CAPTURE INFORMATION FOR RAISERROR. */  
 
    SELECT  
        @ErrorNumber = ERROR_NUMBER(),  
        @ErrorSeverity = ERROR_SEVERITY(),  
        @ErrorState = ERROR_STATE(),  
        @ErrorLine = ERROR_LINE(),  
        @ErrorProcedure = ISNULL(ERROR_PROCEDURE(), '-');  
 
    /* BUILDING THE MESSAGE STRING THAT WILL CONTAIN ORIGINAL  
       ERROR INFORMATION. */  
 
    SELECT @ErrorMessage =  
        N'Error %d, Level %d, State %d, Procedure %s, Line %d, ' +  
         'Message: '+ ERROR_MESSAGE();  
 
    /* RAISE AN ERROR: MSG_STR PARAMETER OF RAISERROR WILL CONTAIN  
    THE ORIGINAL ERROR INFORMATION. */  
 
    RAISERROR(@ErrorMessage, @ErrorSeverity, 1,  
        @ErrorNumber,    /* PARAMETER: ORIGINAL ERROR NUMBER. */  
        @ErrorSeverity,  /* PARAMETER: ORIGINAL ERROR SEVERITY. */  
        @ErrorState,     /* PARAMETER: ORIGINAL ERROR STATE. */  
        @ErrorProcedure, /* PARAMETER: ORIGINAL ERROR PROCEDURE NAME. */  
        @ErrorLine       /* PARAMETER: ORIGINAL ERROR LINE NUMBER. */  
        );  



GO
/****** Object:  StoredProcedure [dbo].[USP_Validate_OTP]    Script Date: 6/22/2024 11:54:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_Validate_OTP]
@UserRid INT,
@OTP VARCHAR(6)
AS
BEGIN


IF( EXISTS(SELECT 1 FROM tbl_UserMaster(NOLOCK) WHERE Rid=@UserRid and ISNULL(UnLockDatetime,getdate())>GETDATE()))
	BEGIN
	RAISERROR('Your account is locked please contact to administrator!',16,1);
	return 
	END

IF( EXISTS(SELECT 1 FROM tbl_UserMaster(NOLOCK) WHERE Rid=@UserRid and ExpireOTP<GETDATE()))
	BEGIN
	RAISERROR('OTP Has been expired!',16,1);
	return 
	END

IF(NOT EXISTS(SELECT 1 FROM tbl_UserMaster(NOLOCK) WHERE Rid=@UserRid and PsswdOTP=@OTP))
	BEGIN
	RAISERROR('Invalid OTP !',16,1);
	return 
	END

update tbl_UserMaster set PsswdOTP=null, ExpireOTP=null where Rid=@UserRid
select 1 as Result

END
GO
