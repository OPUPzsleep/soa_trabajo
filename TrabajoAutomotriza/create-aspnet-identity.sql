-- Esquema básico de ASP.NET Identity para MVC 5 / Identity 2.x

IF OBJECT_ID('dbo.AspNetUsers', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[AspNetUsers] (
        [Id]               NVARCHAR(128) NOT NULL,
        [Email]            NVARCHAR(256) NULL,
        [EmailConfirmed]   BIT           NOT NULL DEFAULT(0),
        [PasswordHash]     NVARCHAR(MAX) NULL,
        [SecurityStamp]    NVARCHAR(MAX) NULL,
        [PhoneNumber]      NVARCHAR(MAX) NULL,
        [PhoneNumberConfirmed] BIT       NOT NULL DEFAULT(0),
        [TwoFactorEnabled] BIT           NOT NULL DEFAULT(0),
        [LockoutEndDateUtc] DATETIME     NULL,
        [LockoutEnabled]   BIT           NOT NULL DEFAULT(0),
        [AccessFailedCount] INT          NOT NULL DEFAULT(0),
        [UserName]         NVARCHAR(256) NOT NULL,
        CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY ([Id])
    );

    CREATE UNIQUE INDEX [UserNameIndex] ON [dbo].[AspNetUsers]([UserName]);
END
GO

IF OBJECT_ID('dbo.AspNetRoles', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[AspNetRoles] (
        [Id]   NVARCHAR(128) NOT NULL,
        [Name] NVARCHAR(256) NOT NULL,
        CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY ([Id])
    );

    CREATE UNIQUE INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]([Name]);
END
GO

IF OBJECT_ID('dbo.AspNetUserRoles', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[AspNetUserRoles] (
        [UserId] NVARCHAR(128) NOT NULL,
        [RoleId] NVARCHAR(128) NOT NULL,
        CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
            FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers]([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
            FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles]([Id]) ON DELETE CASCADE
    );

    CREATE INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]([UserId]);
    CREATE INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]([RoleId]);
END
GO

IF OBJECT_ID('dbo.AspNetUserClaims', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[AspNetUserClaims] (
        [Id]        INT IDENTITY(1,1) NOT NULL,
        [UserId]    NVARCHAR(128) NOT NULL,
        [ClaimType] NVARCHAR(MAX) NULL,
        [ClaimValue] NVARCHAR(MAX) NULL,
        CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
            FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers]([Id]) ON DELETE CASCADE
    );

    CREATE INDEX [IX_UserId] ON [dbo.AspNetUserClaims]([UserId]);
END
GO

IF OBJECT_ID('dbo.AspNetUserLogins', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[AspNetUserLogins] (
        [LoginProvider] NVARCHAR(128) NOT NULL,
        [ProviderKey]   NVARCHAR(128) NOT NULL,
        [UserId]        NVARCHAR(128) NOT NULL,
        CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey], [UserId]),
        CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
            FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers]([Id]) ON DELETE CASCADE
    );

    CREATE INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]([UserId]);
END
GO
