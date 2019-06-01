CREATE TABLE [SpaceFleet].[CrewMembers] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [FullName]     NVARCHAR (MAX) NULL,
    [Rank]         INT            NOT NULL,
    [Division]     INT            NOT NULL,
    [ShipRegistry] NVARCHAR (128) NULL,
    CONSTRAINT [PK_SpaceFleet.CrewMembers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SpaceFleet.CrewMembers_SpaceFleet.Ships_ShipRegistry] FOREIGN KEY ([ShipRegistry]) REFERENCES [SpaceFleet].[Ships] ([RegistryNumber])
);


GO
CREATE NONCLUSTERED INDEX [IX_ShipRegistry]
    ON [SpaceFleet].[CrewMembers]([ShipRegistry] ASC);

