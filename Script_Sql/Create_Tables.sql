USE Abrazo
GO

/****** Script Users DanceLevel, DanceRolDate: 13/09/2023 12:37:45 p. m. ******/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*********************************************************************************************** DanceLevel *****/


CREATE TABLE [dbo].DanceLevel
(
	DanceLevelId int IDENTITY(1,1) NOT NULL,
	[Name] varchar (250) NOT NULL,	
	CONSTRAINT [PK_DanceLevel] PRIMARY KEY CLUSTERED 
(
	DanceLevelId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/***********************************************************************************************  DanceRol *****/


CREATE TABLE [dbo].DanceRol
(
	DanceRolId [int] IDENTITY(1,1) NOT NULL,
	[Name] varchar (250) NOT NULL,	
 CONSTRAINT [PK_DanceRol] PRIMARY KEY CLUSTERED 
(
	DanceRolId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



/***************************************************************************************************** ProfileDancer *****/

CREATE TABLE [dbo].ProfileDancer
(
	ProfileDanceId int IDENTITY(1,1) NOT NULL,
	DanceLevel_FK int not null,
	DanceRol_FK int not null,
	Height float  null
 CONSTRAINT [PK_ProfileDancer] PRIMARY KEY CLUSTERED 
(
	ProfileDanceId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].ProfileDancer  WITH CHECK ADD  CONSTRAINT [FK_Profile_DanceLevel] FOREIGN KEY(DanceLevel_FK)
REFERENCES [dbo].DanceLevel (DanceLevelId)
GO
ALTER TABLE [dbo].ProfileDancer CHECK CONSTRAINT [FK_Profile_DanceLevel]
GO

ALTER TABLE [dbo].ProfileDancer  WITH CHECK ADD  CONSTRAINT [FK_ProfileDancer_DanceRol] FOREIGN KEY(DanceRol_FK)
REFERENCES [dbo].DanceRol (DanceRolId)
GO
ALTER TABLE [dbo].ProfileDancer CHECK CONSTRAINT [FK_ProfileDancer_DanceRol]
GO



/*********************************************************************************************** ****** Users *****/

CREATE TABLE [dbo].[Users]
(
	UserId int IDENTITY(1,1) NOT NULL,
	[Name] varchar (250) NOT NULL,
	LastName varchar (250)  NULL,
	UserName varchar (250)  NULL,
	Email varchar(100) null,
	Age int  NULL,
	Celphone varchar (250)  NULL,
	AvatarImage varchar  null,
	ProfileDancer_FK int  null,
	UserState bit not null
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	UserId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Users] ADD  DEFAULT ((1)) FOR UserState
GO

ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_User_ProfileDancer] FOREIGN KEY(ProfileDancer_FK)
REFERENCES [dbo].ProfileDancer (ProfileDanceId)
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_User_ProfileDancer]
GO







/*********************************************************************************************** ****** Images ******/

CREATE TABLE [dbo].[Image]
(
	ImageId int IDENTITY(1,1) NOT NULL,
	UserId_fk  int  NOT NULL,
	[Name] varchar (250) NOT NULL,	
	CONSTRAINT ImageId PRIMARY KEY CLUSTERED 
(
	ImageId ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Image]  WITH CHECK ADD  CONSTRAINT [FK_User_Image] FOREIGN KEY(UserId_fk)
REFERENCES [dbo].[Users] (UserId)
GO
ALTER TABLE [dbo].[Image] CHECK CONSTRAINT [FK_User_Image]
GO



/*********************************************************************************************** ***** Country******/


CREATE TABLE [dbo].Country
(
	CountryId int IDENTITY(1,1) NOT NULL,
	[Name] varchar (250) NOT NULL,	
	CONSTRAINT [PK_CountryId] PRIMARY KEY CLUSTERED 
(
	CountryId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/*********************************************************************************************** ****** City ******/

CREATE TABLE [dbo].City
(
	CityId int IDENTITY(1,1) NOT NULL,
	CountryId_FK int not null,
	[Name] varchar (250) NOT NULL,	
	CONSTRAINT [PK_CityId] PRIMARY KEY CLUSTERED 
(
	CityId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].City  WITH CHECK ADD  CONSTRAINT [FK_City_Country] FOREIGN KEY(CountryId_FK)
REFERENCES [dbo].Country (CountryId)
GO
ALTER TABLE [dbo].City CHECK CONSTRAINT [FK_City_Country]
GO


/*********************************************************************************************** ***** Address ******/


CREATE TABLE [dbo].[Address]
(
	AddressId int IDENTITY(1,1) NOT NULL,
	CityId_FK int not null,
	Street varchar (250) NOT NULL,	
	Number int not null,
	DetailAddress varchar (250) null
	CONSTRAINT [PK_AddressId] PRIMARY KEY CLUSTERED 
(
	AddressId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_City] FOREIGN KEY(CityId_FK)
REFERENCES [dbo].City (CityId)
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_City]
GO


/*********************************************************************************************** ****** AddressUser ******/


CREATE TABLE [dbo].[AddressUser]
(
	AddressUserId int IDENTITY(1,1) NOT NULL,
	AddressId_FK int not null,
	UserId_FK int not null,
	StateAddress bit not null
	CONSTRAINT [PK_AddressUserId] PRIMARY KEY CLUSTERED 
(
	AddressUserId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AddressUser] ADD  DEFAULT ((1)) FOR StateAddress
GO

ALTER TABLE [dbo].[AddressUser]  WITH CHECK ADD  CONSTRAINT [FK_AddressUser_User] FOREIGN KEY(UserId_FK)
REFERENCES [dbo].[Users] (UserId)
GO
ALTER TABLE [dbo].[AddressUser] CHECK CONSTRAINT [FK_AddressUser_User]
GO

ALTER TABLE [dbo].[AddressUser]  WITH CHECK ADD  CONSTRAINT [FK_AddressUser_Address] FOREIGN KEY(AddressId_FK)
REFERENCES [dbo].[Address] (AddressId)
GO
ALTER TABLE [dbo].[AddressUser] CHECK CONSTRAINT [FK_AddressUser_Address]
GO




/*********************************************************************************************** ***** Permissions ******/

CREATE TABLE [dbo].[Permissions]
(
	PermissionId int IDENTITY(1,1) NOT NULL,
	[Name] varchar (250) NOT NULL,	
	CONSTRAINT [PK_PermissionId] PRIMARY KEY CLUSTERED 
(
	PermissionId ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO







/*********************************************************************************************** ***** UserPermissions ******/

CREATE TABLE [dbo].UserPermissions
(
	UserPermissionId int IDENTITY(1,1) NOT NULL,
	UserId_FK int not null,
	Permission_FK int not null,
	CONSTRAINT [PK_UserPermissionId] PRIMARY KEY CLUSTERED 
(
	UserPermissionId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].UserPermissions  WITH CHECK ADD  CONSTRAINT [FK_UserPermissions_User] FOREIGN KEY(UserId_FK)
REFERENCES [dbo].[Users] (UserId)
GO
ALTER TABLE [dbo].UserPermissions CHECK CONSTRAINT [FK_UserPermissions_User]
GO

ALTER TABLE [dbo].UserPermissions  WITH CHECK ADD  CONSTRAINT [FK_UserPermissions_Permission] FOREIGN KEY(Permission_FK)
REFERENCES [dbo].[Permissions] (PermissionId)
GO
ALTER TABLE [dbo].UserPermissions CHECK CONSTRAINT [FK_UserPermissions_Permission]
GO




/*********************************************************************************************** ***** EventState ******/

CREATE TABLE [dbo].EventState
(
	EventStateId int IDENTITY(1,1) NOT NULL,
	[Name] varchar (250) NOT NULL,	
	CONSTRAINT [PK_EventStateId] PRIMARY KEY CLUSTERED 
(
	EventStateId ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



/***************************************************************************************************** TypeEvent ******/

CREATE TABLE [dbo].TypeEvent
(
	TypeEventId int IDENTITY(1,1) NOT NULL,
	[Name] varchar (250) NOT NULL,
		CONSTRAINT [PK_TypeEventId] PRIMARY KEY CLUSTERED 
(
	TypeEventId ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



/**************************************************************************************************** TypeEvent_User ******/

CREATE TABLE [dbo].TypeEvent_User
(
	TypeEventUserId int IDENTITY(1,1) NOT NULL,
	TypeEventId_FK int not null,
	UserId_FK int not null,
			CONSTRAINT [PK_TypeEventUserId] PRIMARY KEY CLUSTERED 
(
	TypeEventUserId ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].TypeEvent_User  WITH CHECK ADD  CONSTRAINT [FK_TypeEvent_User] FOREIGN KEY(UserId_FK)
REFERENCES [dbo].[Users] (UserId)
GO
ALTER TABLE [dbo].TypeEvent_User CHECK CONSTRAINT [FK_TypeEvent_User]
GO
ALTER TABLE [dbo].TypeEvent_User  WITH CHECK ADD  CONSTRAINT [FK_TypeEvent_Eevent] FOREIGN KEY(TypeEventId_FK)
REFERENCES [dbo].TypeEvent (TypeEventId)
GO
ALTER TABLE [dbo].TypeEvent_User CHECK CONSTRAINT [FK_TypeEvent_Eevent]
GO




/*********************************************************************************************** ****** [Event] ******/

CREATE TABLE [dbo].[Event]
(
	EventId int IDENTITY(1,1) NOT NULL,
	UserIdCreator_FK int not null,
	[Name] varchar (250) NOT NULL,	
	[Description] varchar (max)  NULL,
	AddressId_fk int not null,
	[Image] varchar (max)  NULL,
	DateInit dateTime not null,
	DateFinish dateTime not null,
	EventStateId_fk int not null,
	TypeEventId_fk int not null
	CONSTRAINT [PK_EventId] PRIMARY KEY CLUSTERED 
(
	EventId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Address] FOREIGN KEY(AddressId_fk)
REFERENCES [dbo].[Address] (AddressId)
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Address]
GO

ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_User] FOREIGN KEY(UserIdCreator_FK)
REFERENCES [dbo].[Users] (UserId)
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_User]
GO

ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_EvenState] FOREIGN KEY(EventStateId_FK)
REFERENCES [dbo].EventState (EventStateId)
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_EvenState]
GO

ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_TypeEvent] FOREIGN KEY(TypeEventId_fk)
REFERENCES [dbo].TypeEvent (TypeEventId)
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_TypeEvent]
GO






/*********************************************************************************************** ***** [Event] ******/

CREATE TABLE [dbo].[CouplesEvent_Date]
(
	CouplesEventId int IDENTITY(1,1) NOT NULL,
	FirstUserId_FK int not null,
	SecondUserId_FK int not null,
	EventId_FK int not null,
	CouplesEventApproved bit not NULL,
	CONSTRAINT [PK_CouplesEventId] PRIMARY KEY CLUSTERED 
(
	CouplesEventId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CouplesEvent_Date]  WITH CHECK ADD  CONSTRAINT [FK_CouplesEvent_firstUser] FOREIGN KEY(FirstUserId_FK)
REFERENCES [dbo].[Users] (UserId)
GO
ALTER TABLE [dbo].[CouplesEvent_Date] CHECK CONSTRAINT [FK_CouplesEvent_firstUser]
GO

ALTER TABLE [dbo].[CouplesEvent_Date]  WITH CHECK ADD  CONSTRAINT [FK_CouplesEvent_secondtUser] FOREIGN KEY(SecondUserId_FK)
REFERENCES [dbo].[Users] (UserId)
GO
ALTER TABLE [dbo].[CouplesEvent_Date] CHECK CONSTRAINT [FK_CouplesEvent_secondtUser]
GO

ALTER TABLE [dbo].[CouplesEvent_Date]  WITH CHECK ADD  CONSTRAINT [FK_CouplesEvent_event] FOREIGN KEY(EventId_FK)
REFERENCES [dbo].[Event] (EventId)
GO
ALTER TABLE [dbo].[CouplesEvent_Date] CHECK CONSTRAINT [FK_CouplesEvent_event]
GO



/*********************************************************************************************** **** WaitList ******/

CREATE TABLE [dbo].WaitList
(
	WaitListId int IDENTITY(1,1) NOT NULL,
	UserId_FK int not null,
	EventId_FK int not null,
	[State] bit not null,
	CONSTRAINT [PK_WaitListId] PRIMARY KEY CLUSTERED 
(
	WaitListId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].WaitList ADD  DEFAULT ((1)) FOR [State]
GO

ALTER TABLE [dbo].WaitList  WITH CHECK ADD  CONSTRAINT [FK_WaitList_User] FOREIGN KEY(UserId_FK)
REFERENCES [dbo].[Users] (UserId)
GO
ALTER TABLE [dbo].WaitList CHECK CONSTRAINT [FK_WaitList_User]
GO
ALTER TABLE [dbo].WaitList  WITH CHECK ADD  CONSTRAINT [FK_WaitList_Eevent] FOREIGN KEY(EventId_FK)
REFERENCES [dbo].[Event] (EventId)
GO
ALTER TABLE [dbo].WaitList CHECK CONSTRAINT [FK_WaitList_Eevent]
GO