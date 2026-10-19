/* ============================================================
   TP#1 - Réservations de lieux - Seigneur des Anneaux
   Script 1/3 : création de la base de données TP1_LSDA
   Exécuter connecté sur la base [master].
   ============================================================ */
USE [master];
GO

IF DB_ID(N'TP1_LSDA') IS NULL
BEGIN
    CREATE DATABASE [TP1_LSDA];
END
GO
