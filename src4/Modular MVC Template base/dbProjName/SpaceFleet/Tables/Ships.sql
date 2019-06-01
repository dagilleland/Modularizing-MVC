CREATE TABLE [SpaceFleet].[Ships] (
    [RegistryNumber] NVARCHAR (128) NOT NULL,
    [Name]           NVARCHAR (MAX) NULL,
    [LaunchDate]     NVARCHAR (MAX) NULL,
    [ShipDesignId]   INT            NOT NULL,
    CONSTRAINT [PK_SpaceFleet.Ships] PRIMARY KEY CLUSTERED ([RegistryNumber] ASC),
    CONSTRAINT [FK_SpaceFleet.Ships_SpaceFleet.ShipDesigns_ShipDesignId] FOREIGN KEY ([ShipDesignId]) REFERENCES [SpaceFleet].[ShipDesigns] ([ShipDesignId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ShipDesignId]
    ON [SpaceFleet].[Ships]([ShipDesignId] ASC);

