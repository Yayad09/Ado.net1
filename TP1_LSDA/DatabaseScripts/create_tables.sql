/* ============================================================
   TP#1 - Réservations de lieux - Seigneur des Anneaux
   Script 2/3 : création des tables, clés et contraintes
   ============================================================ */
USE [TP1_LSDA];
GO

/* Suppression dans l'ordre inverse des dépendances (script ré-exécutable) */
IF OBJECT_ID(N'dbo.Mission', N'U') IS NOT NULL DROP TABLE dbo.Mission;
IF OBJECT_ID(N'dbo.Personnage', N'U') IS NOT NULL DROP TABLE dbo.Personnage;
IF OBJECT_ID(N'dbo.Lieu', N'U') IS NOT NULL DROP TABLE dbo.Lieu;
GO

/* ---------- Table Lieu ---------- */
CREATE TABLE dbo.Lieu
(
    Id          INT IDENTITY(1,1) NOT NULL,
    NomLieu     NVARCHAR(100)     NOT NULL,
    Royaume     NVARCHAR(50)      NOT NULL,
    Dangerosite INT               NOT NULL CONSTRAINT DF_Lieu_Dangerosite DEFAULT (1),

    CONSTRAINT PK_Lieu PRIMARY KEY (Id),
    CONSTRAINT UQ_Lieu_NomLieu UNIQUE (NomLieu),
    CONSTRAINT CK_Lieu_NomLieu CHECK (LEN(LTRIM(RTRIM(NomLieu))) > 0),
    CONSTRAINT CK_Lieu_Royaume CHECK (LEN(LTRIM(RTRIM(Royaume))) > 0),
    CONSTRAINT CK_Lieu_Dangerosite CHECK (Dangerosite BETWEEN 1 AND 10)
);
GO

/* ---------- Table Personnage ---------- */
CREATE TABLE dbo.Personnage
(
    Id      INT IDENTITY(1,1) NOT NULL,
    Nom     NVARCHAR(100)     NOT NULL,
    Race    NVARCHAR(50)      NOT NULL,
    Faction NVARCHAR(50)      NOT NULL,

    CONSTRAINT PK_Personnage PRIMARY KEY (Id),
    CONSTRAINT UQ_Personnage_Nom UNIQUE (Nom),
    CONSTRAINT CK_Personnage_Nom CHECK (LEN(LTRIM(RTRIM(Nom))) > 0),
    CONSTRAINT CK_Personnage_Race CHECK (LEN(LTRIM(RTRIM(Race))) > 0),
    CONSTRAINT CK_Personnage_Faction CHECK (LEN(LTRIM(RTRIM(Faction))) > 0)
);
GO

/* ---------- Table Mission ---------- */
CREATE TABLE dbo.Mission
(
    Id           INT IDENTITY(1,1) NOT NULL,
    LieuId       INT               NOT NULL,
    PersonnageId INT               NOT NULL,
    DateDebut    DATETIME          NOT NULL,
    DateFin      DATETIME          NOT NULL,

    CONSTRAINT PK_Mission PRIMARY KEY (Id),
    CONSTRAINT FK_Mission_Lieu FOREIGN KEY (LieuId)
        REFERENCES dbo.Lieu (Id),
    CONSTRAINT FK_Mission_Personnage FOREIGN KEY (PersonnageId)
        REFERENCES dbo.Personnage (Id),
    CONSTRAINT CK_Mission_Dates CHECK (DateFin > DateDebut)
);
GO

/* Index pour accélérer la détection de chevauchement par lieu */
CREATE INDEX IX_Mission_LieuId_Dates ON dbo.Mission (LieuId, DateDebut, DateFin);
GO

/* Contrainte : pas de chevauchement temporel pour un même lieu.
   L'application vérifie le conflit AVANT l'insertion (mode connecté);
   ce déclencheur est un filet de sécurité côté serveur. */
CREATE TRIGGER dbo.TR_Mission_PasDeChevauchement
ON dbo.Mission
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS
    (
        SELECT 1
        FROM inserted AS i
        INNER JOIN dbo.Mission AS m
            ON  m.LieuId = i.LieuId
            AND m.Id <> i.Id
            AND m.DateDebut < i.DateFin
            AND m.DateFin   > i.DateDebut
    )
    BEGIN
        RAISERROR (N'Conflit : une mission existe déjà dans ce lieu sur cette plage horaire.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END
GO
