/* ============================================================
   TP#1 - Réservations de lieux - Seigneur des Anneaux
   Script 3/3 : données d'essai
   ============================================================ */
USE [TP1_LSDA];
GO

/* Script ré-exécutable : on vide les tables (Mission d'abord à cause des clés étrangères).
   TRUNCATE remet le compteur d'identité de Mission à 1; Lieu et Personnage reçoivent
   des identifiants explicites (IDENTITY_INSERT). */
TRUNCATE TABLE dbo.Mission;
DELETE FROM dbo.Personnage;
DELETE FROM dbo.Lieu;
GO

/* ---------- Lieux ---------- */
SET IDENTITY_INSERT dbo.Lieu ON;
INSERT INTO dbo.Lieu (Id, NomLieu, Royaume, Dangerosite) VALUES
    (1,  N'Minas Tirith',    N'Gondor',        3),
    (2,  N'Osgiliath',       N'Gondor',        7),
    (3,  N'Fondcombe',       N'Eriador',       1),
    (4,  N'La Comté',        N'Eriador',       1),
    (5,  N'Bree',            N'Eriador',       3),
    (6,  N'Erebor',          N'Rhovanion',     4),
    (7,  N'Edoras',          N'Rohan',         3),
    (8,  N'Gouffre de Helm', N'Rohan',         6),
    (9,  N'Lothlórien',      N'Rhovanion',     2),
    (10, N'Moria',           N'Monts Brumeux', 9),
    (11, N'Isengard',        N'Calenardhon',   8),
    (12, N'Mont Destin',     N'Mordor',       10);
SET IDENTITY_INSERT dbo.Lieu OFF;
GO

/* ---------- Personnages ---------- */
SET IDENTITY_INSERT dbo.Personnage ON;
INSERT INTO dbo.Personnage (Id, Nom, Race, Faction) VALUES
    (1,  N'Aragorn',    N'Homme',   N'Communauté de l''Anneau'),
    (2,  N'Gandalf',    N'Maia',    N'Communauté de l''Anneau'),
    (3,  N'Frodon',     N'Hobbit',  N'Communauté de l''Anneau'),
    (4,  N'Sam',        N'Hobbit',  N'Communauté de l''Anneau'),
    (5,  N'Legolas',    N'Elfe',    N'Communauté de l''Anneau'),
    (6,  N'Gimli',      N'Nain',    N'Communauté de l''Anneau'),
    (7,  N'Boromir',    N'Homme',   N'Gondor'),
    (8,  N'Eowyn',      N'Homme',   N'Rohan'),
    (9,  N'Théoden',    N'Homme',   N'Rohan'),
    (10, N'Elrond',     N'Elfe',    N'Fondcombe'),
    (11, N'Galadriel',  N'Elfe',    N'Lothlórien'),
    (12, N'Faramir',    N'Homme',   N'Gondor');
SET IDENTITY_INSERT dbo.Personnage OFF;
GO

/* ---------- Missions (aucun chevauchement dans un même lieu) ---------- */
INSERT INTO dbo.Mission (LieuId, PersonnageId, DateDebut, DateFin) VALUES
    (3,  10, '2026-11-02T09:00:00', '2026-11-02T17:00:00'), -- Conseil d'Elrond à Fondcombe
    (3,  2,  '2026-11-03T08:00:00', '2026-11-04T12:00:00'), -- Gandalf à Fondcombe
    (1,  1,  '2026-11-05T10:00:00', '2026-11-07T18:00:00'), -- Aragorn à Minas Tirith
    (1,  12, '2026-11-08T08:00:00', '2026-11-08T20:00:00'), -- Faramir à Minas Tirith
    (2,  7,  '2026-11-05T06:00:00', '2026-11-06T06:00:00'), -- Boromir à Osgiliath
    (7,  9,  '2026-11-10T09:00:00', '2026-11-11T09:00:00'), -- Théoden à Edoras
    (8,  8,  '2026-11-12T00:00:00', '2026-11-13T23:00:00'), -- Eowyn au Gouffre de Helm
    (8,  6,  '2026-11-14T08:00:00', '2026-11-14T22:00:00'), -- Gimli au Gouffre de Helm
    (9,  11, '2026-11-15T09:00:00', '2026-11-16T09:00:00'), -- Galadriel en Lothlórien
    (10, 5,  '2026-11-18T07:00:00', '2026-11-19T19:00:00'), -- Legolas en Moria
    (6,  6,  '2026-11-20T10:00:00', '2026-11-22T10:00:00'), -- Gimli à Erebor
    (12, 3,  '2026-11-25T05:00:00', '2026-11-26T23:00:00'), -- Frodon au Mont Destin
    (4,  4,  '2026-11-28T12:00:00', '2026-11-30T12:00:00'); -- Sam dans la Comté
GO
