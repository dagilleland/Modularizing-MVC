CREATE TABLE [SpaceFleet].[ShipDesigns] (
    [ShipDesignId]           INT            IDENTITY (1, 1) NOT NULL,
    [Name]                   NVARCHAR (MAX) NULL,
    [CommissionedDate]       DATETIME       NOT NULL,
    [StandardCrewComplement] INT            NOT NULL,
    CONSTRAINT [PK_SpaceFleet.ShipDesigns] PRIMARY KEY CLUSTERED ([ShipDesignId] ASC)
);

