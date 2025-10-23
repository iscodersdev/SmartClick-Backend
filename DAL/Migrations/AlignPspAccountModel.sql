IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [AspNetFunctions] (
        [Id] nvarchar(450) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [DeletedDate] datetime2 NULL,
        [DeletedById] nvarchar(max) NULL,
        [LastEditTime] datetime2 NOT NULL,
        [LastEditById] nvarchar(max) NULL,
        [Display] bit NOT NULL,
        [Show] bit NOT NULL,
        [Name] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [RoutesJson] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetFunctions] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [ShowName] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [Enabled] bit NOT NULL,
        [Show] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [Expiration] datetime2 NULL,
        [WorkSpaceId] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [Bancos] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NULL,
        CONSTRAINT [PK_Bancos] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [Colores] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NULL,
        CONSTRAINT [PK_Colores] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [Conceptos] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NULL,
        [Signo] int NOT NULL,
        CONSTRAINT [PK_Conceptos] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [DatosEstructura] (
        [Id] int NOT NULL IDENTITY,
        [Calle] nvarchar(max) NULL,
        [Sigla] nvarchar(max) NULL,
        [Numero] nvarchar(max) NULL,
        [CodigoPostal] nvarchar(max) NULL,
        [Localidad] nvarchar(max) NULL,
        [Provincia] nvarchar(max) NULL,
        [CUIT] nvarchar(max) NULL,
        [Telefono] nvarchar(max) NULL,
        [FAX] nvarchar(max) NULL,
        [Sucursal] nvarchar(max) NULL,
        [CBU] nvarchar(max) NULL,
        [Convenio] nvarchar(max) NULL,
        [Entidad] nvarchar(max) NULL,
        [NombreOrganismo] nvarchar(max) NULL,
        [NombreDependencia] nvarchar(max) NULL,
        [URLReportes] nvarchar(max) NULL,
        [UsuarioReportes] nvarchar(max) NULL,
        [CredencialReportes] nvarchar(max) NULL,
        CONSTRAINT [PK_DatosEstructura] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [DestinoFondos] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NULL,
        CONSTRAINT [PK_DestinoFondos] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [EstadosCiviles] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NULL,
        CONSTRAINT [PK_EstadosCiviles] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [EstadosDeudas] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NULL,
        CONSTRAINT [PK_EstadosDeudas] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [EstadosPrestamos] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NULL,
        [AnulablePersona] bit NOT NULL,
        [ConfirmablePersona] bit NOT NULL,
        [Transferido] bit NOT NULL,
        [Color] nvarchar(max) NULL,
        [EstadoCGEId] int NOT NULL,
        CONSTRAINT [PK_EstadosPrestamos] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [FormasPago] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NULL,
        CONSTRAINT [PK_FormasPago] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [Genero] (
        [Id] int NOT NULL IDENTITY,
        [Descripcion] nvarchar(max) NOT NULL,
        [Abreviatura] nvarchar(max) NULL,
        CONSTRAINT [PK_Genero] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [Grupos] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NULL,
        CONSTRAINT [PK_Grupos] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [InstitucionesFinancieras] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NULL,
        CONSTRAINT [PK_InstitucionesFinancieras] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [Localidad] (
        [Id] int NOT NULL IDENTITY,
        [Latitud] nvarchar(max) NULL,
        [Longitud] nvarchar(max) NULL,
        [IdDepartamento] int NOT NULL,
        [IdProvincia] int NOT NULL,
        [Descripcion] nvarchar(max) NULL,
        [ProvinciaNombre] nvarchar(max) NULL,
        CONSTRAINT [PK_Localidad] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [MatrizConsecuencias] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NULL,
        CONSTRAINT [PK_MatrizConsecuencias] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [MatrizProbabilidades] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NULL,
        CONSTRAINT [PK_MatrizProbabilidades] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [MockServicios] (
        [Id] int NOT NULL IDENTITY,
        [CodigoBarras] nvarchar(max) NULL,
        [CodigoServicioFactura] nvarchar(max) NULL,
        [NombreServicio] nvarchar(max) NULL,
        [Monto] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_MockServicios] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [Monedas] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NULL,
        CONSTRAINT [PK_Monedas] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [OrigenMovimiento] (
        [Id] int NOT NULL IDENTITY,
        [TipoOrigen] int NOT NULL,
        [Descripcion] nvarchar(max) NULL,
        [IdAsociado] int NOT NULL,
        CONSTRAINT [PK_OrigenMovimiento] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [Paises] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NULL,
        CONSTRAINT [PK_Paises] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [Provincia] (
        [Id] int NOT NULL IDENTITY,
        [Latitud] nvarchar(max) NULL,
        [Longitud] nvarchar(max) NULL,
        [Descripcion] nvarchar(max) NULL,
        [DescripcionCompleta] nvarchar(max) NULL,
        CONSTRAINT [PK_Provincia] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [SistemasFinanciacion] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NULL,
        CONSTRAINT [PK_SistemasFinanciacion] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [TipoDocumento] (
        [Id] int NOT NULL IDENTITY,
        [Descripcion] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_TipoDocumento] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [TipoMovimientoBilletera] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NULL,
        [Credito] bit NOT NULL,
        [Debito] bit NOT NULL,
        CONSTRAINT [PK_TipoMovimientoBilletera] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [TipoPuesto] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NULL,
        CONSTRAINT [PK_TipoPuesto] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [TiposAccesos] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NULL,
        CONSTRAINT [PK_TiposAccesos] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [TiposClientes] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NULL,
        [CantidadActividadesSemanales] int NOT NULL,
        CONSTRAINT [PK_TiposClientes] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [TipoServicio] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NULL,
        CONSTRAINT [PK_TipoServicio] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [TiposMovimientos] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NULL,
        [Credito] bit NOT NULL,
        [Debito] bit NOT NULL,
        CONSTRAINT [PK_TiposMovimientos] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [TiposPersonas] (
        [Id] int NOT NULL IDENTITY,
        [nombre] nvarchar(max) NULL,
        [LimiteCuotas] int NOT NULL,
        [TopePrestamo] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_TiposPersonas] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [Vendedores] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NULL,
        [Domicilio] nvarchar(max) NULL,
        [Telefono] nvarchar(max) NULL,
        [Mail] nvarchar(max) NULL,
        CONSTRAINT [PK_Vendedores] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [AspNetRoleFunctions] (
        [Id] nvarchar(450) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [DeletedDate] datetime2 NULL,
        [RoleId] nvarchar(450) NULL,
        [FunctionId] nvarchar(450) NULL,
        CONSTRAINT [PK_AspNetRoleFunctions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleFunctions_AspNetFunctions_FunctionId] FOREIGN KEY ([FunctionId]) REFERENCES [AspNetFunctions] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_AspNetRoleFunctions_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [Empresas] (
        [Id] int NOT NULL IDENTITY,
        [CUIT] bigint NOT NULL,
        [EntidadIdCGE] int NOT NULL,
        [TokenCGE] nvarchar(max) NULL,
        [PasswordCGE] nvarchar(max) NULL,
        [RazonSocial] nvarchar(max) NULL,
        [Domicilio] nvarchar(max) NULL,
        [Telefono] nvarchar(max) NULL,
        [Mail] nvarchar(max) NULL,
        [ColorFontCarnet] nvarchar(max) NULL,
        [ColorCarnet] nvarchar(max) NULL,
        [Twitter] nvarchar(max) NULL,
        [Facebook] nvarchar(max) NULL,
        [Instagram] nvarchar(max) NULL,
        [WhatsApp] nvarchar(max) NULL,
        [FondoMobile] varbinary(max) NULL,
        [GIFLogoMutual] varbinary(max) NULL,
        [LogoMutual] varbinary(max) NULL,
        [ImagenLogin] varbinary(max) NULL,
        [GrupoId] int NULL,
        [Abreviatura] nvarchar(max) NULL,
        [ColorFondo] nvarchar(max) NULL,
        [ColorBotones] nvarchar(max) NULL,
        [ColorLogin] nvarchar(max) NULL,
        [FechaBaja] datetime2 NULL,
        CONSTRAINT [PK_Empresas] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Empresas_Grupos_GrupoId] FOREIGN KEY ([GrupoId]) REFERENCES [Grupos] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [LineasPrestamos] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NULL,
        [CapitalMinimo] decimal(18,2) NOT NULL,
        [CapitalMaximo] decimal(18,2) NOT NULL,
        [CuotaMinima] decimal(18,2) NOT NULL,
        [CuotaMaxima] decimal(18,2) NOT NULL,
        [CantidadCuotasMinima] int NOT NULL,
        [CantidadCuotasMaxima] int NOT NULL,
        [SistemaFinanciacionId] int NULL,
        [MonedaId] int NULL,
        [TipoDescuentoCGEId] int NOT NULL,
        CONSTRAINT [PK_LineasPrestamos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_LineasPrestamos_Monedas_MonedaId] FOREIGN KEY ([MonedaId]) REFERENCES [Monedas] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_LineasPrestamos_SistemasFinanciacion_SistemaFinanciacionId] FOREIGN KEY ([SistemaFinanciacionId]) REFERENCES [SistemasFinanciacion] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [Personas] (
        [Id] int NOT NULL IDENTITY,
        [FechaActualizacion] datetime2 NOT NULL,
        [TipoDocumentoId] int NOT NULL,
        [NroDocumento] nvarchar(max) NULL,
        [Nombre] nvarchar(max) NOT NULL,
        [Apellido] nvarchar(max) NOT NULL,
        [FechaNacimiento] datetime2 NULL,
        [Foto] varbinary(max) NULL,
        [Cuil] nvarchar(max) NULL,
        [GeneroID] int NOT NULL,
        CONSTRAINT [PK_Personas] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Personas_Genero_GeneroID] FOREIGN KEY ([GeneroID]) REFERENCES [Genero] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Personas_TipoDocumento_TipoDocumentoId] FOREIGN KEY ([TipoDocumentoId]) REFERENCES [TipoDocumento] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [Campanas] (
        [Id] int NOT NULL IDENTITY,
        [Fecha] datetime2 NOT NULL,
        [FechaBaja] datetime2 NULL,
        [EmpresaId] int NULL,
        [Observaciones] nvarchar(max) NULL,
        [TextoMail] nvarchar(max) NULL,
        [ProvinciaId] int NOT NULL,
        [MinimoDisponible] decimal(18,2) NOT NULL,
        [MaximoDisponible] decimal(18,2) NOT NULL,
        [UnidadId] int NOT NULL,
        [CantidadPersonas] int NOT NULL,
        [CantidadUnidades] int NOT NULL,
        [CantidadProvincias] int NOT NULL,
        CONSTRAINT [PK_Campanas] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Campanas_Empresas_EmpresaId] FOREIGN KEY ([EmpresaId]) REFERENCES [Empresas] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [Novedades] (
        [Id] int NOT NULL IDENTITY,
        [Fecha] datetime2 NOT NULL,
        [Titulo] nvarchar(max) NULL,
        [Foto] nvarchar(max) NULL,
        [Texto] nvarchar(max) NULL,
        [Publica] bit NOT NULL,
        [EmpresaId] int NULL,
        [ColorId] int NULL,
        CONSTRAINT [PK_Novedades] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Novedades_Colores_ColorId] FOREIGN KEY ([ColorId]) REFERENCES [Colores] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Novedades_Empresas_EmpresaId] FOREIGN KEY ([EmpresaId]) REFERENCES [Empresas] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [Puestos] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NULL,
        [EmpresaId] int NULL,
        [TipoId] int NULL,
        [Coordenadas] nvarchar(max) NULL,
        [FechaBaja] datetime2 NULL,
        CONSTRAINT [PK_Puestos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Puestos_Empresas_EmpresaId] FOREIGN KEY ([EmpresaId]) REFERENCES [Empresas] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Puestos_TipoPuesto_TipoId] FOREIGN KEY ([TipoId]) REFERENCES [TipoPuesto] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [Servicios] (
        [Id] int NOT NULL IDENTITY,
        [EmpresaId] int NULL,
        [Activo] bit NOT NULL,
        [FechaBaja] datetime2 NULL,
        [Nombre] nvarchar(max) NULL,
        [TopeAnualFinde] int NOT NULL,
        [TopeMensualFinde] int NOT NULL,
        [TopePendienteFinde] int NOT NULL,
        [TopeAnualSemana] int NOT NULL,
        [TopeMensualSemana] int NOT NULL,
        [TopePendienteSemana] int NOT NULL,
        [DiasProgramacion] int NOT NULL,
        [TopePendienteGlobal] int NOT NULL,
        [Cupo] int NOT NULL,
        [Icono] varbinary(max) NULL,
        [FechaUnica] datetime2 NULL,
        [Observaciones] nvarchar(max) NULL,
        [TipoId] int NULL,
        CONSTRAINT [PK_Servicios] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Servicios_Empresas_EmpresaId] FOREIGN KEY ([EmpresaId]) REFERENCES [Empresas] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Servicios_TipoServicio_TipoId] FOREIGN KEY ([TipoId]) REFERENCES [TipoServicio] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [LineasPrestamosPlanes] (
        [Id] int NOT NULL IDENTITY,
        [LineaId] int NULL,
        [MontoPrestado] decimal(18,2) NOT NULL,
        [CantidadCuotas] int NOT NULL,
        [MontoCuota] decimal(18,2) NOT NULL,
        [CFT] decimal(18,2) NOT NULL,
        [MargenDisponible] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_LineasPrestamosPlanes] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_LineasPrestamosPlanes_LineasPrestamos_LineaId] FOREIGN KEY ([LineaId]) REFERENCES [LineasPrestamos] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        [PersonaId] int NULL,
        [EmpresaId] int NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUsers_Empresas_EmpresaId] FOREIGN KEY ([EmpresaId]) REFERENCES [Empresas] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_AspNetUsers_Personas_PersonaId] FOREIGN KEY ([PersonaId]) REFERENCES [Personas] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [CampanasRenglones] (
        [Id] int NOT NULL IDENTITY,
        [CabeceraId] int NULL,
        [DNI] bigint NOT NULL,
        [Apellido] nvarchar(max) NULL,
        [Nombres] nvarchar(max) NULL,
        [eMail] nvarchar(max) NULL,
        [Celular] nvarchar(max) NULL,
        [Disponible] decimal(18,2) NOT NULL,
        [ImporteMaximo] decimal(18,2) NOT NULL,
        [UnidadId] int NOT NULL,
        [ProvinciaId] int NOT NULL,
        [Unidad] nvarchar(max) NULL,
        [Provincia] nvarchar(max) NULL,
        [TipoPersona] nvarchar(max) NULL,
        CONSTRAINT [PK_CampanasRenglones] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_CampanasRenglones_Campanas_CabeceraId] FOREIGN KEY ([CabeceraId]) REFERENCES [Campanas] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [PuestosCodigos] (
        [Id] int NOT NULL IDENTITY,
        [PuestoId] int NULL,
        [ValidoDesde] datetime2 NOT NULL,
        [ValidoHasta] datetime2 NOT NULL,
        [HashQR] nvarchar(max) NULL,
        [Imagen] varbinary(max) NULL,
        CONSTRAINT [PK_PuestosCodigos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PuestosCodigos_Puestos_PuestoId] FOREIGN KEY ([PuestoId]) REFERENCES [Puestos] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [ClientesServicios] (
        [Id] int NOT NULL IDENTITY,
        [TipoClienteId] int NULL,
        [ServicioId] int NULL,
        [TopeMensual] int NOT NULL,
        [TopeSemanal] int NOT NULL,
        CONSTRAINT [PK_ClientesServicios] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ClientesServicios_Servicios_ServicioId] FOREIGN KEY ([ServicioId]) REFERENCES [Servicios] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ClientesServicios_TiposClientes_TipoClienteId] FOREIGN KEY ([TipoClienteId]) REFERENCES [TiposClientes] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [Horarios] (
        [Id] int NOT NULL IDENTITY,
        [ServicioId] int NULL,
        [Nombre] nvarchar(max) NULL,
        [Orden] int NOT NULL,
        [DiaSemana] int NOT NULL,
        [Activo] bit NOT NULL,
        [FechaBaja] datetime2 NULL,
        CONSTRAINT [PK_Horarios] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Horarios_Servicios_ServicioId] FOREIGN KEY ([ServicioId]) REFERENCES [Servicios] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [Clientes] (
        [Id] int NOT NULL IDENTITY,
        [TipoClienteId] int NULL,
        [TipoDocumentoId] int NULL,
        [PaisId] int NULL,
        [UsuarioId] nvarchar(450) NULL,
        [NumeroDocumento] bigint NOT NULL,
        [CUIL] bigint NOT NULL,
        [RazonSocial] nvarchar(max) NULL,
        [Apellido] nvarchar(max) NULL,
        [Nombres] nvarchar(max) NULL,
        [Domicilio] nvarchar(max) NULL,
        [Foto] varbinary(max) NULL,
        [CBU] nvarchar(max) NULL,
        [Telefono] nvarchar(max) NULL,
        [Celular] nvarchar(max) NULL,
        [NumeroCliente] nvarchar(max) NULL,
        [FechaNacimiento] datetime2 NOT NULL,
        [Mail] nvarchar(max) NULL,
        [DeviceId] nvarchar(max) NULL,
        [RecordarPassword] bit NOT NULL,
        [Password] nvarchar(max) NULL,
        [EmpresaId] int NULL,
        [EstadoCivilId] int NULL,
        [CantidadHijos] int NOT NULL,
        [FechaIngresoLaboral] datetime2 NOT NULL,
        [NumeroLegajoLaboral] nvarchar(max) NULL,
        [CategoriaLaboral] nvarchar(max) NULL,
        [DestinoLaboral] nvarchar(max) NULL,
        [NumeroAsociado] int NOT NULL,
        [CodeudorId] int NULL,
        [PersonaPoliticamenteExpuesta] bit NOT NULL,
        [EsMilitar] bit NOT NULL,
        [TipoPersonaId] int NULL,
        [FechaIngreso] datetime2 NOT NULL,
        [FechaBaja] datetime2 NULL,
        CONSTRAINT [PK_Clientes] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Clientes_Clientes_CodeudorId] FOREIGN KEY ([CodeudorId]) REFERENCES [Clientes] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Clientes_Empresas_EmpresaId] FOREIGN KEY ([EmpresaId]) REFERENCES [Empresas] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Clientes_EstadosCiviles_EstadoCivilId] FOREIGN KEY ([EstadoCivilId]) REFERENCES [EstadosCiviles] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Clientes_Paises_PaisId] FOREIGN KEY ([PaisId]) REFERENCES [Paises] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Clientes_TiposClientes_TipoClienteId] FOREIGN KEY ([TipoClienteId]) REFERENCES [TiposClientes] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Clientes_TipoDocumento_TipoDocumentoId] FOREIGN KEY ([TipoDocumentoId]) REFERENCES [TipoDocumento] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Clientes_TiposPersonas_TipoPersonaId] FOREIGN KEY ([TipoPersonaId]) REFERENCES [TiposPersonas] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Clientes_AspNetUsers_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [Billeteras] (
        [Id] int NOT NULL IDENTITY,
        [ClienteId] int NULL,
        [Saldo] decimal(18,2) NOT NULL,
        [QRCobro] nvarchar(max) NULL,
        [AliasCVU] nvarchar(max) NULL,
        [CVU] nvarchar(max) NULL,
        CONSTRAINT [PK_Billeteras] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Billeteras_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [CuentaCorriente] (
        [Id] int NOT NULL IDENTITY,
        [Fecha] datetime2 NOT NULL,
        [Vencimiento] datetime2 NULL,
        [Observaciones] nvarchar(max) NULL,
        [Importe] decimal(18,2) NOT NULL,
        [Saldo] decimal(18,2) NOT NULL,
        [ClienteId] int NULL,
        [ConceptoId] int NULL,
        CONSTRAINT [PK_CuentaCorriente] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_CuentaCorriente_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_CuentaCorriente_Conceptos_ConceptoId] FOREIGN KEY ([ConceptoId]) REFERENCES [Conceptos] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [Invitaciones] (
        [Id] int NOT NULL IDENTITY,
        [Apellido] nvarchar(max) NULL,
        [Nombres] nvarchar(max) NULL,
        [Foto] varbinary(max) NULL,
        [NumeroDocumento] bigint NOT NULL,
        [ClienteId] int NULL,
        [Desde] datetime2 NOT NULL,
        [Hasta] datetime2 NOT NULL,
        [Link] nvarchar(max) NULL,
        [QR] nvarchar(max) NULL,
        [Hash] nvarchar(max) NULL,
        [Observaciones] nvarchar(max) NULL,
        [Patente] nvarchar(max) NULL,
        [Baja] datetime2 NULL,
        [Estado] int NOT NULL,
        [Completado] datetime2 NULL,
        CONSTRAINT [PK_Invitaciones] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Invitaciones_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [NotificacionesPersonas] (
        [Id] int NOT NULL IDENTITY,
        [ClienteId] int NULL,
        [FechaHora] datetime2 NOT NULL,
        [Titulo] nvarchar(max) NULL,
        [Descripcion] nvarchar(max) NULL,
        [TomaConocimiento] datetime2 NULL,
        CONSTRAINT [PK_NotificacionesPersonas] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_NotificacionesPersonas_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [Prestamos] (
        [Id] int NOT NULL IDENTITY,
        [ClienteId] int NULL,
        [Domicilio] nvarchar(max) NULL,
        [LineaId] int NULL,
        [DestinoFondosId] int NULL,
        [Capital] decimal(18,2) NOT NULL,
        [EstadoActualId] int NULL,
        [FechaSolicitado] datetime2 NULL,
        [FechaAprobacion] datetime2 NULL,
        [FechaLiquidacion] datetime2 NULL,
        [CantidadCuotas] int NOT NULL,
        [FechaPrimerVencimiento] datetime2 NULL,
        [IngresadoPorId] nvarchar(450) NULL,
        [AprobadoPorId] nvarchar(450) NULL,
        [LiquidadoPorId] nvarchar(450) NULL,
        [Observaciones] nvarchar(max) NULL,
        [OficialCuentaId] nvarchar(450) NULL,
        [CBU] nvarchar(max) NULL,
        [FechaPago] datetime2 NULL,
        [FormaPagoId] int NULL,
        [FechaAnulacion] datetime2 NULL,
        [ObservacionesAnulacion] nvarchar(max) NULL,
        [PrestamoCGEId] int NOT NULL,
        [FotoDNIAnverso] varbinary(max) NULL,
        [FotoDNIReverso] varbinary(max) NULL,
        [FotoSosteniendoDNI] varbinary(max) NULL,
        [LegajoElectronico] varbinary(max) NULL,
        [FirmaOlografica] varbinary(max) NULL,
        [MontoCuota] decimal(18,2) NOT NULL,
        [FirmaOlograficaConfirmacion] varbinary(max) NULL,
        [PrestamoNumero] int NOT NULL,
        [Saldo] decimal(18,2) NOT NULL,
        [CuotasRestantes] int NOT NULL,
        [CapitalEnLetras] nvarchar(max) NULL,
        [CuotasEnLetras] nvarchar(max) NULL,
        [MontoCuotaEnLetras] nvarchar(max) NULL,
        [CFT] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Prestamos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Prestamos_AspNetUsers_AprobadoPorId] FOREIGN KEY ([AprobadoPorId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Prestamos_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Prestamos_DestinoFondos_DestinoFondosId] FOREIGN KEY ([DestinoFondosId]) REFERENCES [DestinoFondos] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Prestamos_EstadosPrestamos_EstadoActualId] FOREIGN KEY ([EstadoActualId]) REFERENCES [EstadosPrestamos] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Prestamos_FormasPago_FormaPagoId] FOREIGN KEY ([FormaPagoId]) REFERENCES [FormasPago] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Prestamos_AspNetUsers_IngresadoPorId] FOREIGN KEY ([IngresadoPorId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Prestamos_LineasPrestamos_LineaId] FOREIGN KEY ([LineaId]) REFERENCES [LineasPrestamos] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Prestamos_AspNetUsers_LiquidadoPorId] FOREIGN KEY ([LiquidadoPorId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Prestamos_AspNetUsers_OficialCuentaId] FOREIGN KEY ([OficialCuentaId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [Reservas] (
        [Id] int NOT NULL IDENTITY,
        [HorarioId] int NULL,
        [Fecha] datetime2 NOT NULL,
        [Observaciones] nvarchar(max) NULL,
        [ClienteId] int NULL,
        [FechaAnulada] datetime2 NULL,
        [ClienteAnulaId] int NULL,
        CONSTRAINT [PK_Reservas] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Reservas_Clientes_ClienteAnulaId] FOREIGN KEY ([ClienteAnulaId]) REFERENCES [Clientes] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Reservas_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Reservas_Horarios_HorarioId] FOREIGN KEY ([HorarioId]) REFERENCES [Horarios] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [UAT] (
        [Id] int NOT NULL IDENTITY,
        [PersonaId] int NULL,
        [ClienteId] int NULL,
        [UsuarioId] nvarchar(450) NULL,
        [Token] nvarchar(max) NULL,
        [FechaHora] datetime2 NOT NULL,
        CONSTRAINT [PK_UAT] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UAT_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_UAT_Personas_PersonaId] FOREIGN KEY ([PersonaId]) REFERENCES [Personas] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_UAT_AspNetUsers_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [ContactosBilletera] (
        [Id] int NOT NULL IDENTITY,
        [ClienteContactoId] int NULL,
        [Detalle] nvarchar(max) NULL,
        [FechaCreacion] datetime2 NOT NULL,
        [Activo] bit NOT NULL,
        [BilleteraId] int NULL,
        CONSTRAINT [PK_ContactosBilletera] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ContactosBilletera_Billeteras_BilleteraId] FOREIGN KEY ([BilleteraId]) REFERENCES [Billeteras] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ContactosBilletera_Clientes_ClienteContactoId] FOREIGN KEY ([ClienteContactoId]) REFERENCES [Clientes] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [CuentasBancarias] (
        [Id] int NOT NULL IDENTITY,
        [Numero] nvarchar(max) NULL,
        [CBU] nvarchar(max) NULL,
        [Titular] nvarchar(max) NULL,
        [Alias] nvarchar(max) NULL,
        [Descripcion] nvarchar(max) NULL,
        [Terceros] bit NOT NULL,
        [BancoId] int NULL,
        [BilleteraId] int NULL,
        CONSTRAINT [PK_CuentasBancarias] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_CuentasBancarias_Bancos_BancoId] FOREIGN KEY ([BancoId]) REFERENCES [Bancos] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_CuentasBancarias_Billeteras_BilleteraId] FOREIGN KEY ([BilleteraId]) REFERENCES [Billeteras] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [ServiciosBilletera] (
        [Id] int NOT NULL IDENTITY,
        [CodigoServicioFactura] nvarchar(max) NULL,
        [Nombre] nvarchar(max) NULL,
        [BilleteraId] int NULL,
        CONSTRAINT [PK_ServiciosBilletera] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ServiciosBilletera_Billeteras_BilleteraId] FOREIGN KEY ([BilleteraId]) REFERENCES [Billeteras] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [Tarjetas] (
        [Id] int NOT NULL IDENTITY,
        [Numero] nvarchar(max) NULL,
        [Titular] nvarchar(max) NULL,
        [Vencimiento] nvarchar(max) NULL,
        [BancoId] int NULL,
        [InstitucionFinancieraId] int NULL,
        [BilleteraId] int NULL,
        CONSTRAINT [PK_Tarjetas] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Tarjetas_Bancos_BancoId] FOREIGN KEY ([BancoId]) REFERENCES [Bancos] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Tarjetas_Billeteras_BilleteraId] FOREIGN KEY ([BilleteraId]) REFERENCES [Billeteras] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Tarjetas_InstitucionesFinancieras_InstitucionFinancieraId] FOREIGN KEY ([InstitucionFinancieraId]) REFERENCES [InstitucionesFinancieras] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [CuentasCorrientes] (
        [Id] int NOT NULL IDENTITY,
        [ClienteId] int NULL,
        [Fecha] datetime2 NULL,
        [Credito] decimal(18,2) NOT NULL,
        [Debito] decimal(18,2) NOT NULL,
        [Saldo] decimal(18,2) NOT NULL,
        [TipoMovimientoId] int NULL,
        [PrestamoId] int NULL,
        CONSTRAINT [PK_CuentasCorrientes] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_CuentasCorrientes_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_CuentasCorrientes_Prestamos_PrestamoId] FOREIGN KEY ([PrestamoId]) REFERENCES [Prestamos] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_CuentasCorrientes_TiposMovimientos_TipoMovimientoId] FOREIGN KEY ([TipoMovimientoId]) REFERENCES [TiposMovimientos] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [MatrizRiesgoCabeceras] (
        [Id] int NOT NULL IDENTITY,
        [ClienteId] int NULL,
        [Fecha] datetime2 NOT NULL,
        [PrestamoId] int NULL,
        [ResidenteZonaLimistrofe] bit NOT NULL,
        [FrecuenciaAnualCreditos] int NOT NULL,
        [DeclaraBienesInmuebles] bit NOT NULL,
        [DeclaraBienesMueblesRegistrables] bit NOT NULL,
        [CuentaCorrientePesos] bit NOT NULL,
        [CuentaCorrienteDolares] bit NOT NULL,
        [OtrasInversiones] bit NOT NULL,
        CONSTRAINT [PK_MatrizRiesgoCabeceras] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_MatrizRiesgoCabeceras_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_MatrizRiesgoCabeceras_Prestamos_PrestamoId] FOREIGN KEY ([PrestamoId]) REFERENCES [Prestamos] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [Accesos] (
        [Id] int NOT NULL IDENTITY,
        [ClienteId] int NULL,
        [PuestoId] int NULL,
        [TipoAccesoId] int NULL,
        [Coordenadas] nvarchar(max) NULL,
        [ValidadoPorGPS] bit NOT NULL,
        [FechaHora] datetime2 NOT NULL,
        [Observaciones] nvarchar(max) NULL,
        [UATPuestoId] int NULL,
        [Deuda] decimal(18,2) NOT NULL,
        [SinCuentaCorriente] bit NOT NULL,
        [EstadoDeudaId] int NULL,
        [Turnos] nvarchar(max) NULL,
        CONSTRAINT [PK_Accesos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Accesos_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Accesos_EstadosDeudas_EstadoDeudaId] FOREIGN KEY ([EstadoDeudaId]) REFERENCES [EstadosDeudas] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Accesos_Puestos_PuestoId] FOREIGN KEY ([PuestoId]) REFERENCES [Puestos] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Accesos_TiposAccesos_TipoAccesoId] FOREIGN KEY ([TipoAccesoId]) REFERENCES [TiposAccesos] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Accesos_UAT_UATPuestoId] FOREIGN KEY ([UATPuestoId]) REFERENCES [UAT] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [MovimientosBilletera] (
        [Id] int NOT NULL IDENTITY,
        [OrigenAsociadoId] int NULL,
        [Fecha] datetime2 NOT NULL,
        [TipoMovimientoId] int NULL,
        [Monto] decimal(18,2) NOT NULL,
        [QR] nvarchar(max) NULL,
        [CBU] nvarchar(max) NULL,
        [BilleteraId] int NULL,
        [CuentaBancariaId] int NULL,
        [TarjetaId] int NULL,
        CONSTRAINT [PK_MovimientosBilletera] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_MovimientosBilletera_Billeteras_BilleteraId] FOREIGN KEY ([BilleteraId]) REFERENCES [Billeteras] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_MovimientosBilletera_CuentasBancarias_CuentaBancariaId] FOREIGN KEY ([CuentaBancariaId]) REFERENCES [CuentasBancarias] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_MovimientosBilletera_OrigenMovimiento_OrigenAsociadoId] FOREIGN KEY ([OrigenAsociadoId]) REFERENCES [OrigenMovimiento] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_MovimientosBilletera_Tarjetas_TarjetaId] FOREIGN KEY ([TarjetaId]) REFERENCES [Tarjetas] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_MovimientosBilletera_TipoMovimientoBilletera_TipoMovimientoId] FOREIGN KEY ([TipoMovimientoId]) REFERENCES [TipoMovimientoBilletera] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE TABLE [MatrizRiesgoRenglones] (
        [Id] int NOT NULL IDENTITY,
        [CabeceraId] int NULL,
        [ProbabilidadId] int NULL,
        [ConsecuenciaId] int NULL,
        CONSTRAINT [PK_MatrizRiesgoRenglones] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_MatrizRiesgoRenglones_MatrizRiesgoCabeceras_CabeceraId] FOREIGN KEY ([CabeceraId]) REFERENCES [MatrizRiesgoCabeceras] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_MatrizRiesgoRenglones_MatrizConsecuencias_ConsecuenciaId] FOREIGN KEY ([ConsecuenciaId]) REFERENCES [MatrizConsecuencias] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_MatrizRiesgoRenglones_MatrizProbabilidades_ProbabilidadId] FOREIGN KEY ([ProbabilidadId]) REFERENCES [MatrizProbabilidades] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Accesos_ClienteId] ON [Accesos] ([ClienteId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Accesos_EstadoDeudaId] ON [Accesos] ([EstadoDeudaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Accesos_PuestoId] ON [Accesos] ([PuestoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Accesos_TipoAccesoId] ON [Accesos] ([TipoAccesoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Accesos_UATPuestoId] ON [Accesos] ([UATPuestoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_AspNetRoleFunctions_FunctionId] ON [AspNetRoleFunctions] ([FunctionId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_AspNetRoleFunctions_RoleId] ON [AspNetRoleFunctions] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_AspNetUsers_EmpresaId] ON [AspNetUsers] ([EmpresaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_AspNetUsers_PersonaId] ON [AspNetUsers] ([PersonaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Billeteras_ClienteId] ON [Billeteras] ([ClienteId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Campanas_EmpresaId] ON [Campanas] ([EmpresaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_CampanasRenglones_CabeceraId] ON [CampanasRenglones] ([CabeceraId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE UNIQUE INDEX [IX_Clientes_CodeudorId] ON [Clientes] ([CodeudorId]) WHERE [CodeudorId] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Clientes_EmpresaId] ON [Clientes] ([EmpresaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Clientes_EstadoCivilId] ON [Clientes] ([EstadoCivilId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Clientes_PaisId] ON [Clientes] ([PaisId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Clientes_TipoClienteId] ON [Clientes] ([TipoClienteId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Clientes_TipoDocumentoId] ON [Clientes] ([TipoDocumentoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Clientes_TipoPersonaId] ON [Clientes] ([TipoPersonaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Clientes_UsuarioId] ON [Clientes] ([UsuarioId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_ClientesServicios_ServicioId] ON [ClientesServicios] ([ServicioId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_ClientesServicios_TipoClienteId] ON [ClientesServicios] ([TipoClienteId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_ContactosBilletera_BilleteraId] ON [ContactosBilletera] ([BilleteraId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_ContactosBilletera_ClienteContactoId] ON [ContactosBilletera] ([ClienteContactoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_CuentaCorriente_ClienteId] ON [CuentaCorriente] ([ClienteId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_CuentaCorriente_ConceptoId] ON [CuentaCorriente] ([ConceptoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_CuentasBancarias_BancoId] ON [CuentasBancarias] ([BancoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_CuentasBancarias_BilleteraId] ON [CuentasBancarias] ([BilleteraId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_CuentasCorrientes_ClienteId] ON [CuentasCorrientes] ([ClienteId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_CuentasCorrientes_PrestamoId] ON [CuentasCorrientes] ([PrestamoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_CuentasCorrientes_TipoMovimientoId] ON [CuentasCorrientes] ([TipoMovimientoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Empresas_GrupoId] ON [Empresas] ([GrupoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Horarios_ServicioId] ON [Horarios] ([ServicioId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Invitaciones_ClienteId] ON [Invitaciones] ([ClienteId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_LineasPrestamos_MonedaId] ON [LineasPrestamos] ([MonedaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_LineasPrestamos_SistemaFinanciacionId] ON [LineasPrestamos] ([SistemaFinanciacionId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_LineasPrestamosPlanes_LineaId] ON [LineasPrestamosPlanes] ([LineaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_MatrizRiesgoCabeceras_ClienteId] ON [MatrizRiesgoCabeceras] ([ClienteId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_MatrizRiesgoCabeceras_PrestamoId] ON [MatrizRiesgoCabeceras] ([PrestamoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_MatrizRiesgoRenglones_CabeceraId] ON [MatrizRiesgoRenglones] ([CabeceraId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_MatrizRiesgoRenglones_ConsecuenciaId] ON [MatrizRiesgoRenglones] ([ConsecuenciaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_MatrizRiesgoRenglones_ProbabilidadId] ON [MatrizRiesgoRenglones] ([ProbabilidadId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_MovimientosBilletera_BilleteraId] ON [MovimientosBilletera] ([BilleteraId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_MovimientosBilletera_CuentaBancariaId] ON [MovimientosBilletera] ([CuentaBancariaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_MovimientosBilletera_OrigenAsociadoId] ON [MovimientosBilletera] ([OrigenAsociadoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_MovimientosBilletera_TarjetaId] ON [MovimientosBilletera] ([TarjetaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_MovimientosBilletera_TipoMovimientoId] ON [MovimientosBilletera] ([TipoMovimientoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_NotificacionesPersonas_ClienteId] ON [NotificacionesPersonas] ([ClienteId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Novedades_ColorId] ON [Novedades] ([ColorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Novedades_EmpresaId] ON [Novedades] ([EmpresaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Personas_GeneroID] ON [Personas] ([GeneroID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Personas_TipoDocumentoId] ON [Personas] ([TipoDocumentoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Prestamos_AprobadoPorId] ON [Prestamos] ([AprobadoPorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Prestamos_ClienteId] ON [Prestamos] ([ClienteId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Prestamos_DestinoFondosId] ON [Prestamos] ([DestinoFondosId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Prestamos_EstadoActualId] ON [Prestamos] ([EstadoActualId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Prestamos_FormaPagoId] ON [Prestamos] ([FormaPagoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Prestamos_IngresadoPorId] ON [Prestamos] ([IngresadoPorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Prestamos_LineaId] ON [Prestamos] ([LineaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Prestamos_LiquidadoPorId] ON [Prestamos] ([LiquidadoPorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Prestamos_OficialCuentaId] ON [Prestamos] ([OficialCuentaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Puestos_EmpresaId] ON [Puestos] ([EmpresaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Puestos_TipoId] ON [Puestos] ([TipoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_PuestosCodigos_PuestoId] ON [PuestosCodigos] ([PuestoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Reservas_ClienteAnulaId] ON [Reservas] ([ClienteAnulaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Reservas_ClienteId] ON [Reservas] ([ClienteId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Reservas_HorarioId] ON [Reservas] ([HorarioId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Servicios_EmpresaId] ON [Servicios] ([EmpresaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Servicios_TipoId] ON [Servicios] ([TipoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_ServiciosBilletera_BilleteraId] ON [ServiciosBilletera] ([BilleteraId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Tarjetas_BancoId] ON [Tarjetas] ([BancoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Tarjetas_BilleteraId] ON [Tarjetas] ([BilleteraId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_Tarjetas_InstitucionFinancieraId] ON [Tarjetas] ([InstitucionFinancieraId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_UAT_ClienteId] ON [UAT] ([ClienteId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_UAT_PersonaId] ON [UAT] ([PersonaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    CREATE INDEX [IX_UAT_UsuarioId] ON [UAT] ([UsuarioId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721135823_mig1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210721135823_mig1', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210803005638_mig2')
BEGIN
    ALTER TABLE [DestinoFondos] ADD [Activo] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210803005638_mig2')
BEGIN
    CREATE TABLE [Organismos] (
        [Id] int NOT NULL IDENTITY,
        [Descripcion] nvarchar(max) NULL,
        [Orden] int NOT NULL,
        [Activo] bit NOT NULL,
        CONSTRAINT [PK_Organismos] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210803005638_mig2')
BEGIN
    CREATE TABLE [Proveedores] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NULL,
        [CUIT] bigint NOT NULL,
        [RazonSocial] nvarchar(max) NULL,
        [Domicilio] nvarchar(max) NULL,
        [UsuarioId] nvarchar(450) NULL,
        [EmpresaId] int NULL,
        [Activo] bit NOT NULL,
        CONSTRAINT [PK_Proveedores] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Proveedores_Empresas_EmpresaId] FOREIGN KEY ([EmpresaId]) REFERENCES [Empresas] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Proveedores_AspNetUsers_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210803005638_mig2')
BEGIN
    CREATE TABLE [Rubros] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NULL,
        [Descripcion] nvarchar(max) NULL,
        [Activo] bit NOT NULL,
        CONSTRAINT [PK_Rubros] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210803005638_mig2')
BEGIN
    CREATE TABLE [DestinoFondosRubros] (
        [Id] int NOT NULL IDENTITY,
        [DestinosFondosId] int NULL,
        [RubroId] int NULL,
        CONSTRAINT [PK_DestinoFondosRubros] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_DestinoFondosRubros_DestinoFondos_DestinosFondosId] FOREIGN KEY ([DestinosFondosId]) REFERENCES [DestinoFondos] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_DestinoFondosRubros_Rubros_RubroId] FOREIGN KEY ([RubroId]) REFERENCES [Rubros] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210803005638_mig2')
BEGIN
    CREATE TABLE [Productos] (
        [Id] int NOT NULL IDENTITY,
        [Descripcion] nvarchar(max) NULL,
        [Foto] varbinary(max) NULL,
        [ProveedorId] int NULL,
        [RubroId] int NULL,
        [Activo] bit NOT NULL,
        CONSTRAINT [PK_Productos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Productos_Proveedores_ProveedorId] FOREIGN KEY ([ProveedorId]) REFERENCES [Proveedores] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Productos_Rubros_RubroId] FOREIGN KEY ([RubroId]) REFERENCES [Rubros] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210803005638_mig2')
BEGIN
    CREATE TABLE [ProveedorRubros] (
        [Id] int NOT NULL IDENTITY,
        [ProveedorId] int NULL,
        [RubroId] int NULL,
        CONSTRAINT [PK_ProveedorRubros] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProveedorRubros_Proveedores_ProveedorId] FOREIGN KEY ([ProveedorId]) REFERENCES [Proveedores] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ProveedorRubros_Rubros_RubroId] FOREIGN KEY ([RubroId]) REFERENCES [Rubros] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210803005638_mig2')
BEGIN
    CREATE INDEX [IX_DestinoFondosRubros_DestinosFondosId] ON [DestinoFondosRubros] ([DestinosFondosId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210803005638_mig2')
BEGIN
    CREATE INDEX [IX_DestinoFondosRubros_RubroId] ON [DestinoFondosRubros] ([RubroId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210803005638_mig2')
BEGIN
    CREATE INDEX [IX_Productos_ProveedorId] ON [Productos] ([ProveedorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210803005638_mig2')
BEGIN
    CREATE INDEX [IX_Productos_RubroId] ON [Productos] ([RubroId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210803005638_mig2')
BEGIN
    CREATE INDEX [IX_Proveedores_EmpresaId] ON [Proveedores] ([EmpresaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210803005638_mig2')
BEGIN
    CREATE INDEX [IX_Proveedores_UsuarioId] ON [Proveedores] ([UsuarioId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210803005638_mig2')
BEGIN
    CREATE INDEX [IX_ProveedorRubros_ProveedorId] ON [ProveedorRubros] ([ProveedorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210803005638_mig2')
BEGIN
    CREATE INDEX [IX_ProveedorRubros_RubroId] ON [ProveedorRubros] ([RubroId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210803005638_mig2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210803005638_mig2', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    ALTER TABLE [AspNetUsers] DROP CONSTRAINT [FK_AspNetUsers_Empresas_EmpresaId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    ALTER TABLE [AspNetUsers] DROP CONSTRAINT [FK_AspNetUsers_Personas_PersonaId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    ALTER TABLE [Clientes] DROP CONSTRAINT [FK_Clientes_EstadosCiviles_EstadoCivilId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    ALTER TABLE [Clientes] DROP CONSTRAINT [FK_Clientes_Paises_PaisId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    ALTER TABLE [Clientes] DROP CONSTRAINT [FK_Clientes_TipoDocumento_TipoDocumentoId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    ALTER TABLE [Clientes] DROP CONSTRAINT [FK_Clientes_TiposPersonas_TipoPersonaId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    ALTER TABLE [Personas] DROP CONSTRAINT [FK_Personas_Genero_GeneroID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    ALTER TABLE [Personas] DROP CONSTRAINT [FK_Personas_TipoDocumento_TipoDocumentoId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    ALTER TABLE [Proveedores] DROP CONSTRAINT [FK_Proveedores_AspNetUsers_UsuarioId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    DROP INDEX [IX_Proveedores_UsuarioId] ON [Proveedores];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    DROP INDEX [IX_Clientes_EstadoCivilId] ON [Clientes];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    DROP INDEX [IX_Clientes_PaisId] ON [Clientes];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    DROP INDEX [IX_Clientes_TipoDocumentoId] ON [Clientes];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    DROP INDEX [IX_Clientes_UsuarioId] ON [Clientes];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Proveedores]') AND [c].[name] = N'UsuarioId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Proveedores] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Proveedores] DROP COLUMN [UsuarioId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Clientes]') AND [c].[name] = N'Apellido');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Clientes] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Clientes] DROP COLUMN [Apellido];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Clientes]') AND [c].[name] = N'CUIL');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Clientes] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Clientes] DROP COLUMN [CUIL];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Clientes]') AND [c].[name] = N'CantidadHijos');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Clientes] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Clientes] DROP COLUMN [CantidadHijos];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Clientes]') AND [c].[name] = N'DeviceId');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Clientes] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Clientes] DROP COLUMN [DeviceId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Clientes]') AND [c].[name] = N'EstadoCivilId');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Clientes] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Clientes] DROP COLUMN [EstadoCivilId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Clientes]') AND [c].[name] = N'FechaNacimiento');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Clientes] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Clientes] DROP COLUMN [FechaNacimiento];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Clientes]') AND [c].[name] = N'Foto');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Clientes] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [Clientes] DROP COLUMN [Foto];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Clientes]') AND [c].[name] = N'Mail');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Clientes] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Clientes] DROP COLUMN [Mail];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Clientes]') AND [c].[name] = N'Nombres');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Clientes] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [Clientes] DROP COLUMN [Nombres];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Clientes]') AND [c].[name] = N'NumeroDocumento');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Clientes] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [Clientes] DROP COLUMN [NumeroDocumento];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Clientes]') AND [c].[name] = N'PaisId');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Clientes] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [Clientes] DROP COLUMN [PaisId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Clientes]') AND [c].[name] = N'Password');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Clientes] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [Clientes] DROP COLUMN [Password];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Clientes]') AND [c].[name] = N'TipoDocumentoId');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Clientes] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [Clientes] DROP COLUMN [TipoDocumentoId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    EXEC sp_rename N'[Personas].[GeneroID]', N'GeneroId', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    EXEC sp_rename N'[Personas].[Nombre]', N'Nombres', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    EXEC sp_rename N'[Personas].[IX_Personas_GeneroID]', N'IX_Personas_GeneroId', N'INDEX';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    EXEC sp_rename N'[Clientes].[TipoPersonaId]', N'PersonaId', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    EXEC sp_rename N'[Clientes].[RecordarPassword]', N'ClienteValidado', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    EXEC sp_rename N'[Clientes].[IX_Clientes_TipoPersonaId]', N'IX_Clientes_PersonaId', N'INDEX';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    EXEC sp_rename N'[AspNetUsers].[PersonaId]', N'VendedoresId', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    EXEC sp_rename N'[AspNetUsers].[EmpresaId]', N'ProveedorId', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    EXEC sp_rename N'[AspNetUsers].[IX_AspNetUsers_PersonaId]', N'IX_AspNetUsers_VendedoresId', N'INDEX';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    EXEC sp_rename N'[AspNetUsers].[IX_AspNetUsers_EmpresaId]', N'IX_AspNetUsers_ProveedorId', N'INDEX';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    ALTER TABLE [TiposPersonas] ADD [OrganismoId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Personas]') AND [c].[name] = N'TipoDocumentoId');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Personas] DROP CONSTRAINT [' + @var14 + '];');
    ALTER TABLE [Personas] ALTER COLUMN [TipoDocumentoId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    DECLARE @var15 sysname;
    SELECT @var15 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Personas]') AND [c].[name] = N'GeneroId');
    IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [Personas] DROP CONSTRAINT [' + @var15 + '];');
    ALTER TABLE [Personas] ALTER COLUMN [GeneroId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    ALTER TABLE [Personas] ADD [CantidadHijos] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    ALTER TABLE [Personas] ADD [EstadoCivilId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    ALTER TABLE [Personas] ADD [PaisId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    ALTER TABLE [Personas] ADD [TipoPersonaId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [DeviceId] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Mail] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Password] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [RecordarPassword] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    CREATE INDEX [IX_TiposPersonas_OrganismoId] ON [TiposPersonas] ([OrganismoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    CREATE INDEX [IX_Personas_EstadoCivilId] ON [Personas] ([EstadoCivilId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    CREATE INDEX [IX_Personas_PaisId] ON [Personas] ([PaisId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    CREATE INDEX [IX_Personas_TipoPersonaId] ON [Personas] ([TipoPersonaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    CREATE UNIQUE INDEX [IX_Clientes_UsuarioId] ON [Clientes] ([UsuarioId]) WHERE [UsuarioId] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_Proveedores_ProveedorId] FOREIGN KEY ([ProveedorId]) REFERENCES [Proveedores] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_Vendedores_VendedoresId] FOREIGN KEY ([VendedoresId]) REFERENCES [Vendedores] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    ALTER TABLE [Clientes] ADD CONSTRAINT [FK_Clientes_Personas_PersonaId] FOREIGN KEY ([PersonaId]) REFERENCES [Personas] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    ALTER TABLE [Personas] ADD CONSTRAINT [FK_Personas_EstadosCiviles_EstadoCivilId] FOREIGN KEY ([EstadoCivilId]) REFERENCES [EstadosCiviles] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    ALTER TABLE [Personas] ADD CONSTRAINT [FK_Personas_Genero_GeneroId] FOREIGN KEY ([GeneroId]) REFERENCES [Genero] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    ALTER TABLE [Personas] ADD CONSTRAINT [FK_Personas_Paises_PaisId] FOREIGN KEY ([PaisId]) REFERENCES [Paises] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    ALTER TABLE [Personas] ADD CONSTRAINT [FK_Personas_TipoDocumento_TipoDocumentoId] FOREIGN KEY ([TipoDocumentoId]) REFERENCES [TipoDocumento] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    ALTER TABLE [Personas] ADD CONSTRAINT [FK_Personas_TiposPersonas_TipoPersonaId] FOREIGN KEY ([TipoPersonaId]) REFERENCES [TiposPersonas] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    ALTER TABLE [TiposPersonas] ADD CONSTRAINT [FK_TiposPersonas_Organismos_OrganismoId] FOREIGN KEY ([OrganismoId]) REFERENCES [Organismos] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804004008_mig3')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210804004008_mig3', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804154614_mig4')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Token] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804154614_mig4')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210804154614_mig4', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804225144_mig5')
BEGIN
    ALTER TABLE [Clientes] ADD [RecibirPublicidad] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210804225144_mig5')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210804225144_mig5', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210805212341_mig6')
BEGIN
    ALTER TABLE [Clientes] ADD [NroDocReferido] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210805212341_mig6')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210805212341_mig6', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210805235917_mig7')
BEGIN
    ALTER TABLE [Prestamos] ADD [VendedorId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210805235917_mig7')
BEGIN
    CREATE INDEX [IX_Prestamos_VendedorId] ON [Prestamos] ([VendedorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210805235917_mig7')
BEGIN
    ALTER TABLE [Prestamos] ADD CONSTRAINT [FK_Prestamos_Vendedores_VendedorId] FOREIGN KEY ([VendedorId]) REFERENCES [Vendedores] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210805235917_mig7')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210805235917_mig7', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210809011542_mig8')
BEGIN
    ALTER TABLE [Productos] ADD [DescripcionAmpliada] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210809011542_mig8')
BEGIN
    ALTER TABLE [Productos] ADD [Precio] decimal(18,2) NOT NULL DEFAULT 0.0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210809011542_mig8')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210809011542_mig8', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210809040421_mig9')
BEGIN
    ALTER TABLE [Proveedores] ADD [Foto] varbinary(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210809040421_mig9')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210809040421_mig9', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210810135801_mig10')
BEGIN
    ALTER TABLE [Vendedores] ADD [NroDocumento] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210810135801_mig10')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210810135801_mig10', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210811150309_mig11')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Administradores] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210811150309_mig11')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [PersonasId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210811150309_mig11')
BEGIN
    CREATE INDEX [IX_AspNetUsers_PersonasId] ON [AspNetUsers] ([PersonasId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210811150309_mig11')
BEGIN
    ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_Personas_PersonasId] FOREIGN KEY ([PersonasId]) REFERENCES [Personas] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210811150309_mig11')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210811150309_mig11', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210811181648_mig12')
BEGIN
    DECLARE @var16 sysname;
    SELECT @var16 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Prestamos]') AND [c].[name] = N'FotoDNIAnverso');
    IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Prestamos] DROP CONSTRAINT [' + @var16 + '];');
    ALTER TABLE [Prestamos] DROP COLUMN [FotoDNIAnverso];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210811181648_mig12')
BEGIN
    DECLARE @var17 sysname;
    SELECT @var17 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Prestamos]') AND [c].[name] = N'FotoDNIReverso');
    IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Prestamos] DROP CONSTRAINT [' + @var17 + '];');
    ALTER TABLE [Prestamos] DROP COLUMN [FotoDNIReverso];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210811181648_mig12')
BEGIN
    DECLARE @var18 sysname;
    SELECT @var18 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Prestamos]') AND [c].[name] = N'FotoSosteniendoDNI');
    IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [Prestamos] DROP CONSTRAINT [' + @var18 + '];');
    ALTER TABLE [Prestamos] DROP COLUMN [FotoSosteniendoDNI];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210811181648_mig12')
BEGIN
    ALTER TABLE [Clientes] ADD [FirmaOlografica] varbinary(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210811181648_mig12')
BEGIN
    ALTER TABLE [Clientes] ADD [FirmaOlograficaConfirmacion] varbinary(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210811181648_mig12')
BEGIN
    ALTER TABLE [Clientes] ADD [FotoDNIAnverso] varbinary(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210811181648_mig12')
BEGIN
    ALTER TABLE [Clientes] ADD [FotoDNIReverso] varbinary(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210811181648_mig12')
BEGIN
    ALTER TABLE [Clientes] ADD [FotoSosteniendoDNI] varbinary(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210811181648_mig12')
BEGIN
    ALTER TABLE [Clientes] ADD [LegajoElectronico] varbinary(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210811181648_mig12')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210811181648_mig12', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210812152517_mig13')
BEGIN
    ALTER TABLE [Productos] ADD [Financiable] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210812152517_mig13')
BEGIN
    ALTER TABLE [Productos] ADD [Oferta] decimal(18,2) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210812152517_mig13')
BEGIN
    CREATE TABLE [FinanciacionProductos] (
        [Id] int NOT NULL IDENTITY,
        [ProductoId] int NULL,
        [CantidadCuotas] int NOT NULL,
        [InteresesPorCuota] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_FinanciacionProductos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_FinanciacionProductos_Productos_ProductoId] FOREIGN KEY ([ProductoId]) REFERENCES [Productos] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210812152517_mig13')
BEGIN
    CREATE INDEX [IX_FinanciacionProductos_ProductoId] ON [FinanciacionProductos] ([ProductoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210812152517_mig13')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210812152517_mig13', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210824033335_mig14')
BEGIN
    ALTER TABLE [LineasPrestamosPlanes] ADD [Activo] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210824033335_mig14')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210824033335_mig14', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210826005548_mig15')
BEGIN
    CREATE TABLE [LineasPrestamosTiposPersonas] (
        [Id] int NOT NULL IDENTITY,
        [LineaPrestamoId] int NULL,
        [TipoPersonaId] int NULL,
        CONSTRAINT [PK_LineasPrestamosTiposPersonas] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_LineasPrestamosTiposPersonas_LineasPrestamos_LineaPrestamoId] FOREIGN KEY ([LineaPrestamoId]) REFERENCES [LineasPrestamos] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_LineasPrestamosTiposPersonas_TiposPersonas_TipoPersonaId] FOREIGN KEY ([TipoPersonaId]) REFERENCES [TiposPersonas] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210826005548_mig15')
BEGIN
    CREATE INDEX [IX_LineasPrestamosTiposPersonas_LineaPrestamoId] ON [LineasPrestamosTiposPersonas] ([LineaPrestamoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210826005548_mig15')
BEGIN
    CREATE INDEX [IX_LineasPrestamosTiposPersonas_TipoPersonaId] ON [LineasPrestamosTiposPersonas] ([TipoPersonaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210826005548_mig15')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210826005548_mig15', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210827213425_mig16')
BEGIN
    ALTER TABLE [LineasPrestamos] ADD [Intervalo] decimal(18,2) NOT NULL DEFAULT 0.0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210827213425_mig16')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210827213425_mig16', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210901193514_mig17')
BEGIN
    ALTER TABLE [Prestamos] ADD [FotoCertificadoDescuento] varbinary(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210901193514_mig17')
BEGIN
    ALTER TABLE [Prestamos] ADD [FotoReciboHaber] varbinary(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210901193514_mig17')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210901193514_mig17', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210928190402_mig18')
BEGIN
    DECLARE @var19 sysname;
    SELECT @var19 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Vendedores]') AND [c].[name] = N'Nombre');
    IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [Vendedores] DROP CONSTRAINT [' + @var19 + '];');
    ALTER TABLE [Vendedores] DROP COLUMN [Nombre];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210928190402_mig18')
BEGIN
    DECLARE @var20 sysname;
    SELECT @var20 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Vendedores]') AND [c].[name] = N'NroDocumento');
    IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [Vendedores] DROP CONSTRAINT [' + @var20 + '];');
    ALTER TABLE [Vendedores] DROP COLUMN [NroDocumento];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210928190402_mig18')
BEGIN
    ALTER TABLE [Vendedores] ADD [PersonaId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210928190402_mig18')
BEGIN
    CREATE INDEX [IX_Vendedores_PersonaId] ON [Vendedores] ([PersonaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210928190402_mig18')
BEGIN
    ALTER TABLE [Vendedores] ADD CONSTRAINT [FK_Vendedores_Personas_PersonaId] FOREIGN KEY ([PersonaId]) REFERENCES [Personas] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210928190402_mig18')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210928190402_mig18', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210930191045_mig19')
BEGIN
    ALTER TABLE [Clientes] ADD [ReferenciaAId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210930191045_mig19')
BEGIN
    ALTER TABLE [Clientes] ADD [ReferenciaBId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210930191045_mig19')
BEGIN
    CREATE TABLE [Referencias] (
        [Id] int NOT NULL IDENTITY,
        [NombreCompleto] nvarchar(max) NULL,
        [Vinculo] nvarchar(max) NULL,
        [Telefono] nvarchar(max) NULL,
        CONSTRAINT [PK_Referencias] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210930191045_mig19')
BEGIN
    CREATE INDEX [IX_Clientes_ReferenciaAId] ON [Clientes] ([ReferenciaAId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210930191045_mig19')
BEGIN
    CREATE INDEX [IX_Clientes_ReferenciaBId] ON [Clientes] ([ReferenciaBId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210930191045_mig19')
BEGIN
    ALTER TABLE [Clientes] ADD CONSTRAINT [FK_Clientes_Referencias_ReferenciaAId] FOREIGN KEY ([ReferenciaAId]) REFERENCES [Referencias] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210930191045_mig19')
BEGIN
    ALTER TABLE [Clientes] ADD CONSTRAINT [FK_Clientes_Referencias_ReferenciaBId] FOREIGN KEY ([ReferenciaBId]) REFERENCES [Referencias] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210930191045_mig19')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210930191045_mig19', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211012225825_mig20')
BEGIN
    ALTER TABLE [Campanas] ADD [Imagen] varbinary(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211012225825_mig20')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211012225825_mig20', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211014002642_mig21')
BEGIN
    ALTER TABLE [Clientes] ADD [CodigoPostal] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211014002642_mig21')
BEGIN
    ALTER TABLE [Clientes] ADD [LocalidadId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211014002642_mig21')
BEGIN
    ALTER TABLE [Clientes] ADD [ProvinciaId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211014002642_mig21')
BEGIN
    CREATE INDEX [IX_Clientes_LocalidadId] ON [Clientes] ([LocalidadId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211014002642_mig21')
BEGIN
    CREATE INDEX [IX_Clientes_ProvinciaId] ON [Clientes] ([ProvinciaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211014002642_mig21')
BEGIN
    ALTER TABLE [Clientes] ADD CONSTRAINT [FK_Clientes_Localidad_LocalidadId] FOREIGN KEY ([LocalidadId]) REFERENCES [Localidad] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211014002642_mig21')
BEGIN
    ALTER TABLE [Clientes] ADD CONSTRAINT [FK_Clientes_Provincia_ProvinciaId] FOREIGN KEY ([ProvinciaId]) REFERENCES [Provincia] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211014002642_mig21')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211014002642_mig21', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211014175629_mig22')
BEGIN
    ALTER TABLE [Prestamos] ADD [AdjuntoTransferencia] varbinary(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211014175629_mig22')
BEGIN
    ALTER TABLE [Prestamos] ADD [ExtensionAdjuntoTransferencia] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211014175629_mig22')
BEGIN
    ALTER TABLE [Empresas] ADD [CabeceraMail] varbinary(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211014175629_mig22')
BEGIN
    ALTER TABLE [Empresas] ADD [CasillaMail] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211014175629_mig22')
BEGIN
    ALTER TABLE [Empresas] ADD [MailBienvenida] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211014175629_mig22')
BEGIN
    ALTER TABLE [Empresas] ADD [PasswordMail] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211014175629_mig22')
BEGIN
    ALTER TABLE [Empresas] ADD [PuertoMail] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211014175629_mig22')
BEGIN
    ALTER TABLE [Empresas] ADD [SSLMail] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211014175629_mig22')
BEGIN
    ALTER TABLE [Empresas] ADD [UrlMail] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211014175629_mig22')
BEGIN
    ALTER TABLE [Empresas] ADD [UsernameMail] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211014175629_mig22')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211014175629_mig22', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211019224255_mig23')
BEGIN
    ALTER TABLE [Prestamos] ADD [Tipos] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211019224255_mig23')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211019224255_mig23', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211029172220_mig24')
BEGIN
    ALTER TABLE [Clientes] ADD [Password] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211029172220_mig24')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211029172220_mig24', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211125212408_mig25')
BEGIN
    ALTER TABLE [Organismos] ADD [CuotaId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211125212408_mig25')
BEGIN
    CREATE TABLE [ComprasProductos] (
        [Id] int NOT NULL IDENTITY,
        [ClienteId] int NULL,
        [ProductoId] int NULL,
        [MovimientoId] int NULL,
        [PrestamoId] int NULL,
        [FechaCompra] datetime2 NOT NULL,
        [Estado] int NOT NULL,
        [TipoCompra] int NOT NULL,
        [FechaAnulacion] datetime2 NOT NULL,
        CONSTRAINT [PK_ComprasProductos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ComprasProductos_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ComprasProductos_MovimientosBilletera_MovimientoId] FOREIGN KEY ([MovimientoId]) REFERENCES [MovimientosBilletera] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ComprasProductos_Prestamos_PrestamoId] FOREIGN KEY ([PrestamoId]) REFERENCES [Prestamos] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ComprasProductos_Productos_ProductoId] FOREIGN KEY ([ProductoId]) REFERENCES [Productos] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211125212408_mig25')
BEGIN
    CREATE TABLE [CuotasSociales] (
        [Id] int NOT NULL IDENTITY,
        [ValorCuota] decimal(18,2) NOT NULL,
        [ImpusoCuota] nvarchar(max) NULL,
        [Organismo] int NOT NULL,
        CONSTRAINT [PK_CuotasSociales] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211125212408_mig25')
BEGIN
    CREATE TABLE [Inversores] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NULL,
        [Domicilio] nvarchar(max) NULL,
        [Tasa] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Inversores] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211125212408_mig25')
BEGIN
    CREATE INDEX [IX_Organismos_CuotaId] ON [Organismos] ([CuotaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211125212408_mig25')
BEGIN
    CREATE INDEX [IX_ComprasProductos_ClienteId] ON [ComprasProductos] ([ClienteId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211125212408_mig25')
BEGIN
    CREATE INDEX [IX_ComprasProductos_MovimientoId] ON [ComprasProductos] ([MovimientoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211125212408_mig25')
BEGIN
    CREATE INDEX [IX_ComprasProductos_PrestamoId] ON [ComprasProductos] ([PrestamoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211125212408_mig25')
BEGIN
    CREATE INDEX [IX_ComprasProductos_ProductoId] ON [ComprasProductos] ([ProductoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211125212408_mig25')
BEGIN
    ALTER TABLE [Organismos] ADD CONSTRAINT [FK_Organismos_CuotasSociales_CuotaId] FOREIGN KEY ([CuotaId]) REFERENCES [CuotasSociales] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211125212408_mig25')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211125212408_mig25', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211126002336_mig26')
BEGIN
    ALTER TABLE [Inversores] ADD [CUIT] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211126002336_mig26')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211126002336_mig26', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211202001257_mig27')
BEGIN
    DECLARE @var21 sysname;
    SELECT @var21 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inversores]') AND [c].[name] = N'Tasa');
    IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [Inversores] DROP CONSTRAINT [' + @var21 + '];');
    ALTER TABLE [Inversores] DROP COLUMN [Tasa];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211202001257_mig27')
BEGIN
    ALTER TABLE [Inversores] ADD [Activo] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211202001257_mig27')
BEGIN
    ALTER TABLE [Inversores] ADD [TasaActualId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211202001257_mig27')
BEGIN
    CREATE TABLE [TasasInversores] (
        [Id] int NOT NULL IDENTITY,
        [Tasa] decimal(18,2) NOT NULL,
        [Inversor] int NOT NULL,
        CONSTRAINT [PK_TasasInversores] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211202001257_mig27')
BEGIN
    CREATE INDEX [IX_Inversores_TasaActualId] ON [Inversores] ([TasaActualId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211202001257_mig27')
BEGIN
    ALTER TABLE [Inversores] ADD CONSTRAINT [FK_Inversores_TasasInversores_TasaActualId] FOREIGN KEY ([TasaActualId]) REFERENCES [TasasInversores] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211202001257_mig27')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211202001257_mig27', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211203203456_mig28')
BEGIN
    ALTER TABLE [Prestamos] ADD [Pagare] decimal(18,2) NOT NULL DEFAULT 0.0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211203203456_mig28')
BEGIN
    ALTER TABLE [Prestamos] ADD [PagareEnLetras] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211203203456_mig28')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211203203456_mig28', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211220201357_mig29')
BEGIN
    ALTER TABLE [Organismos] DROP CONSTRAINT [FK_Organismos_CuotasSociales_CuotaId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211220201357_mig29')
BEGIN
    DROP INDEX [IX_Organismos_CuotaId] ON [Organismos];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211220201357_mig29')
BEGIN
    DECLARE @var22 sysname;
    SELECT @var22 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Organismos]') AND [c].[name] = N'CuotaId');
    IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [Organismos] DROP CONSTRAINT [' + @var22 + '];');
    ALTER TABLE [Organismos] DROP COLUMN [CuotaId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211220201357_mig29')
BEGIN
    EXEC sp_rename N'[CuotasSociales].[Organismo]', N'TipoPersona', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211220201357_mig29')
BEGIN
    ALTER TABLE [TiposPersonas] ADD [CuotaId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211220201357_mig29')
BEGIN
    CREATE INDEX [IX_TiposPersonas_CuotaId] ON [TiposPersonas] ([CuotaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211220201357_mig29')
BEGIN
    ALTER TABLE [TiposPersonas] ADD CONSTRAINT [FK_TiposPersonas_CuotasSociales_CuotaId] FOREIGN KEY ([CuotaId]) REFERENCES [CuotasSociales] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211220201357_mig29')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211220201357_mig29', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211221185606_mig30')
BEGIN
    ALTER TABLE [Prestamos] ADD [InversorId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211221185606_mig30')
BEGIN
    ALTER TABLE [Prestamos] ADD [TotalCapitalInversor] decimal(18,2) NOT NULL DEFAULT 0.0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211221185606_mig30')
BEGIN
    CREATE INDEX [IX_Prestamos_InversorId] ON [Prestamos] ([InversorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211221185606_mig30')
BEGIN
    ALTER TABLE [Prestamos] ADD CONSTRAINT [FK_Prestamos_Inversores_InversorId] FOREIGN KEY ([InversorId]) REFERENCES [Inversores] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211221185606_mig30')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211221185606_mig30', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220106000522_mig31')
BEGIN
    ALTER TABLE [CuotasSociales] ADD [ValorCuotaEnLetras] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220106000522_mig31')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220106000522_mig31', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120195808_mig32')
BEGIN
    ALTER TABLE [Organismos] ADD [CUIT] bigint NOT NULL DEFAULT CAST(0 AS bigint);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120195808_mig32')
BEGIN
    ALTER TABLE [Organismos] ADD [CodigoPostal] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120195808_mig32')
BEGIN
    ALTER TABLE [Organismos] ADD [Domicilio] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120195808_mig32')
BEGIN
    ALTER TABLE [Organismos] ADD [LocalidadId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120195808_mig32')
BEGIN
    ALTER TABLE [Organismos] ADD [ProvinciaId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120195808_mig32')
BEGIN
    ALTER TABLE [Organismos] ADD [Telefono] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120195808_mig32')
BEGIN
    CREATE INDEX [IX_Organismos_LocalidadId] ON [Organismos] ([LocalidadId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120195808_mig32')
BEGIN
    CREATE INDEX [IX_Organismos_ProvinciaId] ON [Organismos] ([ProvinciaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120195808_mig32')
BEGIN
    ALTER TABLE [Organismos] ADD CONSTRAINT [FK_Organismos_Localidad_LocalidadId] FOREIGN KEY ([LocalidadId]) REFERENCES [Localidad] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120195808_mig32')
BEGIN
    ALTER TABLE [Organismos] ADD CONSTRAINT [FK_Organismos_Provincia_ProvinciaId] FOREIGN KEY ([ProvinciaId]) REFERENCES [Provincia] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120195808_mig32')
BEGIN
    ALTER TABLE [Prestamos] ADD [CapitalInversorEnLetras] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120195808_mig32')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220120195808_mig32', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120204044_mig33')
BEGIN
    ALTER TABLE [Prestamos] ADD [CFTSinImpuesto] decimal(18,2) NOT NULL DEFAULT 0.0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120204044_mig33')
BEGIN
    ALTER TABLE [Inversores] ADD [TEAId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120204044_mig33')
BEGIN
    ALTER TABLE [Inversores] ADD [TNAId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120204044_mig33')
BEGIN
    CREATE TABLE [TasasEfectivaAnual] (
        [Id] int NOT NULL IDENTITY,
        [Tasa] decimal(18,2) NOT NULL,
        [Inversor] int NOT NULL,
        CONSTRAINT [PK_TasasEfectivaAnual] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120204044_mig33')
BEGIN
    CREATE TABLE [TasasNominalAnual] (
        [Id] int NOT NULL IDENTITY,
        [Tasa] decimal(18,2) NOT NULL,
        [Inversor] int NOT NULL,
        CONSTRAINT [PK_TasasNominalAnual] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120204044_mig33')
BEGIN
    CREATE INDEX [IX_Inversores_TEAId] ON [Inversores] ([TEAId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120204044_mig33')
BEGIN
    CREATE INDEX [IX_Inversores_TNAId] ON [Inversores] ([TNAId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120204044_mig33')
BEGIN
    ALTER TABLE [Inversores] ADD CONSTRAINT [FK_Inversores_TasasEfectivaAnual_TEAId] FOREIGN KEY ([TEAId]) REFERENCES [TasasEfectivaAnual] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120204044_mig33')
BEGIN
    ALTER TABLE [Inversores] ADD CONSTRAINT [FK_Inversores_TasasNominalAnual_TNAId] FOREIGN KEY ([TNAId]) REFERENCES [TasasNominalAnual] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120204044_mig33')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220120204044_mig33', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126175643_mig34')
BEGIN
    DECLARE @var23 sysname;
    SELECT @var23 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Prestamos]') AND [c].[name] = N'CFTSinImpuesto');
    IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [Prestamos] DROP CONSTRAINT [' + @var23 + '];');
    ALTER TABLE [Prestamos] DROP COLUMN [CFTSinImpuesto];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126175643_mig34')
BEGIN
    ALTER TABLE [Inversores] ADD [CFTSinImpuestoId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126175643_mig34')
BEGIN
    CREATE TABLE [CFTSinImpuesto] (
        [Id] int NOT NULL IDENTITY,
        [Tasa] decimal(18,2) NOT NULL,
        [Inversor] int NOT NULL,
        CONSTRAINT [PK_CFTSinImpuesto] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126175643_mig34')
BEGIN
    CREATE INDEX [IX_Inversores_CFTSinImpuestoId] ON [Inversores] ([CFTSinImpuestoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126175643_mig34')
BEGIN
    ALTER TABLE [Inversores] ADD CONSTRAINT [FK_Inversores_CFTSinImpuesto_CFTSinImpuestoId] FOREIGN KEY ([CFTSinImpuestoId]) REFERENCES [CFTSinImpuesto] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126175643_mig34')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220126175643_mig34', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220127195813_mig35')
BEGIN
    ALTER TABLE [Prestamos] ADD [SueldoNetoMensual] decimal(18,2) NOT NULL DEFAULT 0.0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220127195813_mig35')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220127195813_mig35', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220128210546_mig36')
BEGIN
    ALTER TABLE [Prestamos] ADD [CFTSinImpuesto] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220128210546_mig36')
BEGIN
    ALTER TABLE [Prestamos] ADD [TEA] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220128210546_mig36')
BEGIN
    ALTER TABLE [Prestamos] ADD [TNA] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220128210546_mig36')
BEGIN
    ALTER TABLE [Prestamos] ADD [TasaInversor] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220128210546_mig36')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220128210546_mig36', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220223213655_mig37')
BEGIN
    ALTER TABLE [Inversores] ADD [CFTConImpuestoId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220223213655_mig37')
BEGIN
    ALTER TABLE [Inversores] ADD [TEMId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220223213655_mig37')
BEGIN
    CREATE TABLE [CFTConImpuesto] (
        [Id] int NOT NULL IDENTITY,
        [Tasa] decimal(18,2) NOT NULL,
        [Inversor] int NOT NULL,
        CONSTRAINT [PK_CFTConImpuesto] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220223213655_mig37')
BEGIN
    CREATE TABLE [TEM] (
        [Id] int NOT NULL IDENTITY,
        [Tasa] decimal(18,2) NOT NULL,
        [Inversor] int NOT NULL,
        CONSTRAINT [PK_TEM] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220223213655_mig37')
BEGIN
    CREATE INDEX [IX_Inversores_CFTConImpuestoId] ON [Inversores] ([CFTConImpuestoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220223213655_mig37')
BEGIN
    CREATE INDEX [IX_Inversores_TEMId] ON [Inversores] ([TEMId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220223213655_mig37')
BEGIN
    ALTER TABLE [Inversores] ADD CONSTRAINT [FK_Inversores_CFTConImpuesto_CFTConImpuestoId] FOREIGN KEY ([CFTConImpuestoId]) REFERENCES [CFTConImpuesto] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220223213655_mig37')
BEGIN
    ALTER TABLE [Inversores] ADD CONSTRAINT [FK_Inversores_TEM_TEMId] FOREIGN KEY ([TEMId]) REFERENCES [TEM] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220223213655_mig37')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220223213655_mig37', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220225014229_mig38')
BEGIN
    ALTER TABLE [Personas] ADD [LugarNacimientoId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220225014229_mig38')
BEGIN
    CREATE INDEX [IX_Personas_LugarNacimientoId] ON [Personas] ([LugarNacimientoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220225014229_mig38')
BEGIN
    ALTER TABLE [Personas] ADD CONSTRAINT [FK_Personas_Provincia_LugarNacimientoId] FOREIGN KEY ([LugarNacimientoId]) REFERENCES [Provincia] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220225014229_mig38')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220225014229_mig38', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220225021852_mig39')
BEGIN
    ALTER TABLE [Prestamos] ADD [CFTConImpuesto] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220225021852_mig39')
BEGIN
    ALTER TABLE [Prestamos] ADD [TEM] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220225021852_mig39')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220225021852_mig39', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220422211935_mig40')
BEGIN
    ALTER TABLE [Campanas] ADD [Link] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220422211935_mig40')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220422211935_mig40', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220506202937_mig41')
BEGIN
    ALTER TABLE [TiposPersonas] ADD [MontoAmpliacion] decimal(18,2) NOT NULL DEFAULT 0.0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220506202937_mig41')
BEGIN
    ALTER TABLE [Prestamos] ADD [Ampliado] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220506202937_mig41')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220506202937_mig41', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220511180243_mig42')
BEGIN
    ALTER TABLE [Prestamos] ADD [MontoMensualDisponible] decimal(18,2) NOT NULL DEFAULT 0.0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220511180243_mig42')
BEGIN
    ALTER TABLE [Clientes] ADD [MontoMensualDisponible] decimal(18,2) NOT NULL DEFAULT 0.0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220511180243_mig42')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220511180243_mig42', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512204721_mig43')
BEGIN
    ALTER TABLE [Prestamos] ADD [MontoCuotaAmpliacion] decimal(18,2) NOT NULL DEFAULT 0.0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512204721_mig43')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220512204721_mig43', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220520004534_mig44')
BEGIN
    ALTER TABLE [LineasPrestamosPlanes] ADD [UsuarioCargaId] nvarchar(450) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220520004534_mig44')
BEGIN
    CREATE INDEX [IX_LineasPrestamosPlanes_UsuarioCargaId] ON [LineasPrestamosPlanes] ([UsuarioCargaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220520004534_mig44')
BEGIN
    ALTER TABLE [LineasPrestamosPlanes] ADD CONSTRAINT [FK_LineasPrestamosPlanes_AspNetUsers_UsuarioCargaId] FOREIGN KEY ([UsuarioCargaId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220520004534_mig44')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220520004534_mig44', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524133036_mig45')
BEGIN
    ALTER TABLE [Prestamos] ADD [FechaPresentacionDeInversor] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524133036_mig45')
BEGIN
    ALTER TABLE [Clientes] ADD [Altura] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524133036_mig45')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220524133036_mig45', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220531225506_mig46')
BEGIN
    ALTER TABLE [Campanas] ADD [ImagenUrl] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220531225506_mig46')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220531225506_mig46', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220627171559_mig47')
BEGIN
    ALTER TABLE [TiposPersonas] ADD [TopeCantCuotasAmpliacion] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220627171559_mig47')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220627171559_mig47', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220713235730_mig48')
BEGIN
    ALTER TABLE [Inversores] ADD [FirmaReporte] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220713235730_mig48')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220713235730_mig48', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220822203257_mig49')
BEGIN
    ALTER TABLE [Inversores] ADD [Logo] varbinary(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220822203257_mig49')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220822203257_mig49', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220908152124_mig50')
BEGIN
    ALTER TABLE [LineasPrestamosPlanes] ADD [TEM] decimal(18,2) NOT NULL DEFAULT 0.0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220908152124_mig50')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220908152124_mig50', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220913011834_mig51')
BEGIN
    ALTER TABLE [Prestamos] ADD [TEMAmprom] decimal(18,2) NOT NULL DEFAULT 0.0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220913011834_mig51')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220913011834_mig51', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220914175651_mig52')
BEGIN
    ALTER TABLE [Prestamos] ADD [TNAAmprom] decimal(18,2) NOT NULL DEFAULT 0.0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220914175651_mig52')
BEGIN
    ALTER TABLE [LineasPrestamosPlanes] ADD [TNA] decimal(18,2) NOT NULL DEFAULT 0.0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220914175651_mig52')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220914175651_mig52', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916214820_mig53')
BEGIN
    ALTER TABLE [Prestamos] ADD [CapitalAmprom] decimal(18,2) NOT NULL DEFAULT 0.0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916214820_mig53')
BEGIN
    ALTER TABLE [LineasPrestamosPlanes] ADD [CapitalSmartClick] decimal(18,2) NOT NULL DEFAULT 0.0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220916214820_mig53')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220916214820_mig53', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220920193126_mig54')
BEGIN
    ALTER TABLE [Clientes] ADD [BloquearPrestamos] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220920193126_mig54')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220920193126_mig54', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221017203923_mig55')
BEGIN
    ALTER TABLE [Personas] ADD [CreadoPorUsuarioId] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221017203923_mig55')
BEGIN
    ALTER TABLE [Personas] ADD [FechaCreacionModificacion] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221017203923_mig55')
BEGIN
    ALTER TABLE [Clientes] ADD [CreadoPorUsuarioId] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221017203923_mig55')
BEGIN
    ALTER TABLE [Clientes] ADD [FechaCreacionModificacion] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221017203923_mig55')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221017203923_mig55', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221115225354_mig56')
BEGIN
    ALTER TABLE [Prestamos] ADD [CodigoEstadistico] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221115225354_mig56')
BEGIN
    ALTER TABLE [Prestamos] ADD [FechaEmisionAnexo] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221115225354_mig56')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221115225354_mig56', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221215011043_mig57')
BEGIN
    CREATE TABLE [ClientesSinDisponible] (
        [Id] int NOT NULL IDENTITY,
        [ClienteId] int NULL,
        [Fecha] datetime2 NOT NULL,
        [Disponible] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_ClientesSinDisponible] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ClientesSinDisponible_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221215011043_mig57')
BEGIN
    CREATE INDEX [IX_ClientesSinDisponible_ClienteId] ON [ClientesSinDisponible] ([ClienteId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221215011043_mig57')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221215011043_mig57', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230106212403_Mig58')
BEGIN
    DECLARE @var24 sysname;
    SELECT @var24 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Prestamos]') AND [c].[name] = N'FotoCertificadoDescuento');
    IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [Prestamos] DROP CONSTRAINT [' + @var24 + '];');
    ALTER TABLE [Prestamos] DROP COLUMN [FotoCertificadoDescuento];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230106212403_Mig58')
BEGIN
    CREATE TABLE [Adjuntos] (
        [Id] int NOT NULL IDENTITY,
        [Adjunto] varbinary(max) NULL,
        [Extension] nvarchar(max) NULL,
        [Fecha] datetime2 NOT NULL,
        [PrestamosId] int NULL,
        CONSTRAINT [PK_Adjuntos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Adjuntos_Prestamos_PrestamosId] FOREIGN KEY ([PrestamosId]) REFERENCES [Prestamos] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230106212403_Mig58')
BEGIN
    CREATE INDEX [IX_Adjuntos_PrestamosId] ON [Adjuntos] ([PrestamosId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230106212403_Mig58')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230106212403_Mig58', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230110202630_mig59')
BEGIN
    ALTER TABLE [Prestamos] ADD [Integracion] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230110202630_mig59')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230110202630_mig59', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231109174109_mig60')
BEGIN
    ALTER TABLE [Organismos] ADD [APIEjercito] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231109174109_mig60')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231109174109_mig60', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113153632_mig61')
BEGIN
    ALTER TABLE [Prestamos] ADD [ConstanciaCBU] varbinary(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113153632_mig61')
BEGIN
    ALTER TABLE [Clientes] ADD [NumeroCelularDetectado] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113153632_mig61')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231113153632_mig61', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231214203255_mig62')
BEGIN
    ALTER TABLE [Provincia] ADD [PaisId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231214203255_mig62')
BEGIN
    ALTER TABLE [Localidad] ADD [Activo] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231214203255_mig62')
BEGIN
    ALTER TABLE [Localidad] ADD [ProvinciaId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231214203255_mig62')
BEGIN
    CREATE INDEX [IX_Provincia_PaisId] ON [Provincia] ([PaisId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231214203255_mig62')
BEGIN
    CREATE INDEX [IX_Localidad_ProvinciaId] ON [Localidad] ([ProvinciaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231214203255_mig62')
BEGIN
    ALTER TABLE [Localidad] ADD CONSTRAINT [FK_Localidad_Provincia_ProvinciaId] FOREIGN KEY ([ProvinciaId]) REFERENCES [Provincia] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231214203255_mig62')
BEGIN
    ALTER TABLE [Provincia] ADD CONSTRAINT [FK_Provincia_Paises_PaisId] FOREIGN KEY ([PaisId]) REFERENCES [Paises] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231214203255_mig62')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231214203255_mig62', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218025423_mig63')
BEGIN
    ALTER TABLE [Prestamos] ADD [LocalidadId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218025423_mig63')
BEGIN
    ALTER TABLE [Prestamos] ADD [PaisId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218025423_mig63')
BEGIN
    ALTER TABLE [Prestamos] ADD [ProvinciaId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218025423_mig63')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231218025423_mig63', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218135441_mig64')
BEGIN
    DECLARE @var25 sysname;
    SELECT @var25 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Prestamos]') AND [c].[name] = N'ProvinciaId');
    IF @var25 IS NOT NULL EXEC(N'ALTER TABLE [Prestamos] DROP CONSTRAINT [' + @var25 + '];');
    ALTER TABLE [Prestamos] ALTER COLUMN [ProvinciaId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218135441_mig64')
BEGIN
    DECLARE @var26 sysname;
    SELECT @var26 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Prestamos]') AND [c].[name] = N'PaisId');
    IF @var26 IS NOT NULL EXEC(N'ALTER TABLE [Prestamos] DROP CONSTRAINT [' + @var26 + '];');
    ALTER TABLE [Prestamos] ALTER COLUMN [PaisId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218135441_mig64')
BEGIN
    DECLARE @var27 sysname;
    SELECT @var27 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Prestamos]') AND [c].[name] = N'LocalidadId');
    IF @var27 IS NOT NULL EXEC(N'ALTER TABLE [Prestamos] DROP CONSTRAINT [' + @var27 + '];');
    ALTER TABLE [Prestamos] ALTER COLUMN [LocalidadId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218135441_mig64')
BEGIN
    CREATE INDEX [IX_Prestamos_LocalidadId] ON [Prestamos] ([LocalidadId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218135441_mig64')
BEGIN
    CREATE INDEX [IX_Prestamos_PaisId] ON [Prestamos] ([PaisId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218135441_mig64')
BEGIN
    CREATE INDEX [IX_Prestamos_ProvinciaId] ON [Prestamos] ([ProvinciaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218135441_mig64')
BEGIN
    ALTER TABLE [Prestamos] ADD CONSTRAINT [FK_Prestamos_Localidad_LocalidadId] FOREIGN KEY ([LocalidadId]) REFERENCES [Localidad] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218135441_mig64')
BEGIN
    ALTER TABLE [Prestamos] ADD CONSTRAINT [FK_Prestamos_Paises_PaisId] FOREIGN KEY ([PaisId]) REFERENCES [Paises] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218135441_mig64')
BEGIN
    ALTER TABLE [Prestamos] ADD CONSTRAINT [FK_Prestamos_Provincia_ProvinciaId] FOREIGN KEY ([ProvinciaId]) REFERENCES [Provincia] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218135441_mig64')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231218135441_mig64', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218135937_mig65')
BEGIN
    ALTER TABLE [Prestamos] ADD [DomicilioLocalidadId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218135937_mig65')
BEGIN
    ALTER TABLE [Prestamos] ADD [DomicilioPaisId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218135937_mig65')
BEGIN
    ALTER TABLE [Prestamos] ADD [DomicilioProvinciaId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218135937_mig65')
BEGIN
    CREATE INDEX [IX_Prestamos_DomicilioLocalidadId] ON [Prestamos] ([DomicilioLocalidadId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218135937_mig65')
BEGIN
    CREATE INDEX [IX_Prestamos_DomicilioPaisId] ON [Prestamos] ([DomicilioPaisId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218135937_mig65')
BEGIN
    CREATE INDEX [IX_Prestamos_DomicilioProvinciaId] ON [Prestamos] ([DomicilioProvinciaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218135937_mig65')
BEGIN
    ALTER TABLE [Prestamos] ADD CONSTRAINT [FK_Prestamos_Localidad_DomicilioLocalidadId] FOREIGN KEY ([DomicilioLocalidadId]) REFERENCES [Localidad] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218135937_mig65')
BEGIN
    ALTER TABLE [Prestamos] ADD CONSTRAINT [FK_Prestamos_Paises_DomicilioPaisId] FOREIGN KEY ([DomicilioPaisId]) REFERENCES [Paises] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218135937_mig65')
BEGIN
    ALTER TABLE [Prestamos] ADD CONSTRAINT [FK_Prestamos_Provincia_DomicilioProvinciaId] FOREIGN KEY ([DomicilioProvinciaId]) REFERENCES [Provincia] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218135937_mig65')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231218135937_mig65', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218141146_mig66')
BEGIN
    ALTER TABLE [Prestamos] DROP CONSTRAINT [FK_Prestamos_Localidad_DomicilioLocalidadId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218141146_mig66')
BEGIN
    ALTER TABLE [Prestamos] DROP CONSTRAINT [FK_Prestamos_Paises_DomicilioPaisId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218141146_mig66')
BEGIN
    ALTER TABLE [Prestamos] DROP CONSTRAINT [FK_Prestamos_Provincia_DomicilioProvinciaId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218141146_mig66')
BEGIN
    DROP INDEX [IX_Prestamos_DomicilioLocalidadId] ON [Prestamos];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218141146_mig66')
BEGIN
    DROP INDEX [IX_Prestamos_DomicilioPaisId] ON [Prestamos];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218141146_mig66')
BEGIN
    DROP INDEX [IX_Prestamos_DomicilioProvinciaId] ON [Prestamos];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218141146_mig66')
BEGIN
    DECLARE @var28 sysname;
    SELECT @var28 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Prestamos]') AND [c].[name] = N'DomicilioLocalidadId');
    IF @var28 IS NOT NULL EXEC(N'ALTER TABLE [Prestamos] DROP CONSTRAINT [' + @var28 + '];');
    ALTER TABLE [Prestamos] DROP COLUMN [DomicilioLocalidadId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218141146_mig66')
BEGIN
    DECLARE @var29 sysname;
    SELECT @var29 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Prestamos]') AND [c].[name] = N'DomicilioPaisId');
    IF @var29 IS NOT NULL EXEC(N'ALTER TABLE [Prestamos] DROP CONSTRAINT [' + @var29 + '];');
    ALTER TABLE [Prestamos] DROP COLUMN [DomicilioPaisId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218141146_mig66')
BEGIN
    DECLARE @var30 sysname;
    SELECT @var30 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Prestamos]') AND [c].[name] = N'DomicilioProvinciaId');
    IF @var30 IS NOT NULL EXEC(N'ALTER TABLE [Prestamos] DROP CONSTRAINT [' + @var30 + '];');
    ALTER TABLE [Prestamos] DROP COLUMN [DomicilioProvinciaId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231218141146_mig66')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231218141146_mig66', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231220150757_mig67')
BEGIN
    ALTER TABLE [Organismos] ADD [CodigoDescuento] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231220150757_mig67')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231220150757_mig67', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231221150746_mig68')
BEGIN
    ALTER TABLE [Prestamos] ADD [Calle] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231221150746_mig68')
BEGIN
    ALTER TABLE [Prestamos] ADD [CalleNro] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231221150746_mig68')
BEGIN
    ALTER TABLE [Prestamos] ADD [CodPostal] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231221150746_mig68')
BEGIN
    ALTER TABLE [Prestamos] ADD [Dpto] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231221150746_mig68')
BEGIN
    ALTER TABLE [Prestamos] ADD [Piso] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231221150746_mig68')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231221150746_mig68', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240109215632_mig69')
BEGIN
    ALTER TABLE [LineasPrestamos] ADD [FechaBaja] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240109215632_mig69')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240109215632_mig69', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240110212335_mig70')
BEGIN
    ALTER TABLE [Prestamos] ADD [CertificadoDescuento] varbinary(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240110212335_mig70')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240110212335_mig70', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240112191816_mig71')
BEGIN
    ALTER TABLE [Clientes] ADD [EsUsuarioInterno] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240112191816_mig71')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240112191816_mig71', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240318023923_mig72')
BEGIN
    CREATE TABLE [SubProductos] (
        [Id] int NOT NULL IDENTITY,
        [ProductoId] int NULL,
        [NombreSubProducto] nvarchar(max) NULL,
        CONSTRAINT [PK_SubProductos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_SubProductos_Productos_ProductoId] FOREIGN KEY ([ProductoId]) REFERENCES [Productos] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240318023923_mig72')
BEGIN
    CREATE INDEX [IX_SubProductos_ProductoId] ON [SubProductos] ([ProductoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240318023923_mig72')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240318023923_mig72', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240321140957_mig73')
BEGIN
    ALTER TABLE [SubProductos] ADD [FechaBaja] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240321140957_mig73')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240321140957_mig73', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240325162739_mig74')
BEGIN
    ALTER TABLE [ComprasProductos] ADD [SubProductoId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240325162739_mig74')
BEGIN
    CREATE INDEX [IX_ComprasProductos_SubProductoId] ON [ComprasProductos] ([SubProductoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240325162739_mig74')
BEGIN
    ALTER TABLE [ComprasProductos] ADD CONSTRAINT [FK_ComprasProductos_SubProductos_SubProductoId] FOREIGN KEY ([SubProductoId]) REFERENCES [SubProductos] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240325162739_mig74')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240325162739_mig74', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240405205113_mig75')
BEGIN
    ALTER TABLE [Rubros] ADD [IconoAPP] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240405205113_mig75')
BEGIN
    ALTER TABLE [Rubros] ADD [VerEnAPP] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240405205113_mig75')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240405205113_mig75', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240407145721_mig76')
BEGIN
    ALTER TABLE [Productos] ADD [Foto1] varbinary(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240407145721_mig76')
BEGIN
    ALTER TABLE [Productos] ADD [Foto2] varbinary(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240407145721_mig76')
BEGIN
    ALTER TABLE [Productos] ADD [Foto3] varbinary(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240407145721_mig76')
BEGIN
    ALTER TABLE [Productos] ADD [Foto4] varbinary(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240407145721_mig76')
BEGIN
    ALTER TABLE [Productos] ADD [Foto5] varbinary(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240407145721_mig76')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240407145721_mig76', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240414231412_mig_78')
BEGIN
    ALTER TABLE [Campanas] ADD [MailPrueba] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240414231412_mig_78')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240414231412_mig_78', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250406192234_mig79')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250406192234_mig79', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250406192652_mig80')
BEGIN
    CREATE TABLE [UatBot] (
        [Id] int NOT NULL IDENTITY,
        [Dni] nvarchar(max) NULL,
        [Celular] nvarchar(max) NULL,
        [Uat] nvarchar(max) NULL,
        CONSTRAINT [PK_UatBot] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250406192652_mig80')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250406192652_mig80', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250425030409_mig81')
BEGIN
    ALTER TABLE [UatBot] ADD [Apellido] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250425030409_mig81')
BEGIN
    ALTER TABLE [UatBot] ADD [CantidadCuotas] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250425030409_mig81')
BEGIN
    ALTER TABLE [UatBot] ADD [FirmaOlografica] varbinary(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250425030409_mig81')
BEGIN
    ALTER TABLE [UatBot] ADD [FotoDNIAnverso] varbinary(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250425030409_mig81')
BEGIN
    ALTER TABLE [UatBot] ADD [FotoDNIReverso] varbinary(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250425030409_mig81')
BEGIN
    ALTER TABLE [UatBot] ADD [FotoSosteniendoDNI] varbinary(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250425030409_mig81')
BEGIN
    ALTER TABLE [UatBot] ADD [ImporteSolicitado] decimal(18,2) NOT NULL DEFAULT 0.0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250425030409_mig81')
BEGIN
    ALTER TABLE [UatBot] ADD [MontoCuota] decimal(18,2) NOT NULL DEFAULT 0.0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250425030409_mig81')
BEGIN
    ALTER TABLE [UatBot] ADD [Nombre] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250425030409_mig81')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250425030409_mig81', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250425035857_mig82')
BEGIN
    ALTER TABLE [UatBot] ADD [TipoPersonaId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250425035857_mig82')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250425035857_mig82', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250430020312_mig83')
BEGIN
    ALTER TABLE [UatBot] ADD [LineaPrestamoId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250430020312_mig83')
BEGIN
    ALTER TABLE [Prestamos] ADD [Canal] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250430020312_mig83')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250430020312_mig83', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250507021908_mig84')
BEGIN
    ALTER TABLE [UatBot] ADD [Email] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250507021908_mig84')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250507021908_mig84', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250930221011_AddPSPAccount')
BEGIN
    CREATE TABLE [PSPAccounts] (
        [Id] int NOT NULL IDENTITY,
        [ClienteId] int NULL,
        [UsuarioId] int NULL,
        [PSPUserId] nvarchar(max) NULL,
        [UserName] nvarchar(max) NULL,
        [Identifier] nvarchar(max) NULL,
        [EntityId] int NULL,
        [AccountNumber] nvarchar(max) NULL,
        [EncryptedUserToken] nvarchar(max) NULL,
        [TokenExpiry] datetime2 NULL,
        [Status] nvarchar(max) NULL,
        [ErrorMessage] nvarchar(max) NULL,
        [RequestId] nvarchar(max) NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        [TributaryIdentifier] nvarchar(max) NULL,
        CONSTRAINT [PK_PSPAccounts] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250930221011_AddPSPAccount')
BEGIN
    CREATE TABLE [PSPAccountFiles] (
        [Id] int NOT NULL IDENTITY,
        [PSPAccountId] int NOT NULL,
        [FileKey] nvarchar(max) NULL,
        [FileName] nvarchar(max) NULL,
        [StoragePath] nvarchar(max) NULL,
        [UploadedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_PSPAccountFiles] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PSPAccountFiles_PSPAccounts_PSPAccountId] FOREIGN KEY ([PSPAccountId]) REFERENCES [PSPAccounts] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250930221011_AddPSPAccount')
BEGIN
    CREATE INDEX [IX_PSPAccountFiles_PSPAccountId] ON [PSPAccountFiles] ([PSPAccountId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250930221011_AddPSPAccount')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250930221011_AddPSPAccount', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251020195334_AddTraceabilityToPSPAccount')
BEGIN
    DECLARE @var31 sysname;
    SELECT @var31 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PSPAccounts]') AND [c].[name] = N'EntityId');
    IF @var31 IS NOT NULL EXEC(N'ALTER TABLE [PSPAccounts] DROP CONSTRAINT [' + @var31 + '];');
    ALTER TABLE [PSPAccounts] ALTER COLUMN [EntityId] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251020195334_AddTraceabilityToPSPAccount')
BEGIN
    DECLARE @var32 sysname;
    SELECT @var32 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PSPAccounts]') AND [c].[name] = N'EncryptedUserToken');
    IF @var32 IS NOT NULL EXEC(N'ALTER TABLE [PSPAccounts] DROP CONSTRAINT [' + @var32 + '];');
    ALTER TABLE [PSPAccounts] ALTER COLUMN [EncryptedUserToken] text NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251020195334_AddTraceabilityToPSPAccount')
BEGIN
    ALTER TABLE [PSPAccounts] ADD [CVU] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251020195334_AddTraceabilityToPSPAccount')
BEGIN
    ALTER TABLE [PSPAccounts] ADD [EncryptedPassword] text NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251020195334_AddTraceabilityToPSPAccount')
BEGIN
    ALTER TABLE [PSPAccounts] ADD [LastC1ResponseJson] text NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251020195334_AddTraceabilityToPSPAccount')
BEGIN
    ALTER TABLE [PSPAccounts] ADD [LastC7ResponseJson] text NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251020195334_AddTraceabilityToPSPAccount')
BEGIN
    ALTER TABLE [PSPAccounts] ADD [LastStatusCheck] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251020195334_AddTraceabilityToPSPAccount')
BEGIN
    ALTER TABLE [PSPAccounts] ADD [UsuarioId1] nvarchar(450) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251020195334_AddTraceabilityToPSPAccount')
BEGIN
    CREATE INDEX [IX_PSPAccounts_ClienteId] ON [PSPAccounts] ([ClienteId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251020195334_AddTraceabilityToPSPAccount')
BEGIN
    CREATE INDEX [IX_PSPAccounts_UsuarioId1] ON [PSPAccounts] ([UsuarioId1]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251020195334_AddTraceabilityToPSPAccount')
BEGIN
    ALTER TABLE [PSPAccounts] ADD CONSTRAINT [FK_PSPAccounts_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251020195334_AddTraceabilityToPSPAccount')
BEGIN
    ALTER TABLE [PSPAccounts] ADD CONSTRAINT [FK_PSPAccounts_AspNetUsers_UsuarioId1] FOREIGN KEY ([UsuarioId1]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251020195334_AddTraceabilityToPSPAccount')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251020195334_AddTraceabilityToPSPAccount', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251022165818_AddPspAccountColumns')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251022165818_AddPspAccountColumns', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251022191722_AlignPspAccountModel')
BEGIN
    ALTER TABLE [PSPAccounts] DROP CONSTRAINT [FK_PSPAccounts_AspNetUsers_UsuarioId1];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251022191722_AlignPspAccountModel')
BEGIN
    DROP INDEX [IX_PSPAccounts_UsuarioId1] ON [PSPAccounts];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251022191722_AlignPspAccountModel')
BEGIN
    EXEC sp_rename N'[PSPAccounts].[UsuarioId1]', N'TributaryIdentifierType', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251022191722_AlignPspAccountModel')
BEGIN
    DECLARE @var33 sysname;
    SELECT @var33 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PSPAccounts]') AND [c].[name] = N'UsuarioId');
    IF @var33 IS NOT NULL EXEC(N'ALTER TABLE [PSPAccounts] DROP CONSTRAINT [' + @var33 + '];');
    ALTER TABLE [PSPAccounts] ALTER COLUMN [UsuarioId] nvarchar(450) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251022191722_AlignPspAccountModel')
BEGIN
    DECLARE @var34 sysname;
    SELECT @var34 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PSPAccounts]') AND [c].[name] = N'TributaryIdentifierType');
    IF @var34 IS NOT NULL EXEC(N'ALTER TABLE [PSPAccounts] DROP CONSTRAINT [' + @var34 + '];');
    ALTER TABLE [PSPAccounts] ALTER COLUMN [TributaryIdentifierType] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251022191722_AlignPspAccountModel')
BEGIN
    ALTER TABLE [PSPAccounts] ADD [AccountTypeId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251022191722_AlignPspAccountModel')
BEGIN
    ALTER TABLE [PSPAccounts] ADD [CVU_CBUAlias] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251022191722_AlignPspAccountModel')
BEGIN
    ALTER TABLE [PSPAccounts] ADD [CurrencyDescription] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251022191722_AlignPspAccountModel')
BEGIN
    ALTER TABLE [PSPAccounts] ADD [CurrencyName] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251022191722_AlignPspAccountModel')
BEGIN
    ALTER TABLE [PSPAccounts] ADD [CurrencySymbol] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251022191722_AlignPspAccountModel')
BEGIN
    ALTER TABLE [PSPAccounts] ADD [CurrencyTypeId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251022191722_AlignPspAccountModel')
BEGIN
    ALTER TABLE [PSPAccounts] ADD [DeleteAccountSolicitude] bit NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251022191722_AlignPspAccountModel')
BEGIN
    ALTER TABLE [PSPAccounts] ADD [EntityStatus] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251022191722_AlignPspAccountModel')
BEGIN
    ALTER TABLE [PSPAccounts] ADD [EntityStatusDescription] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251022191722_AlignPspAccountModel')
BEGIN
    ALTER TABLE [PSPAccounts] ADD [StatusDescription] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251022191722_AlignPspAccountModel')
BEGIN
    CREATE INDEX [IX_PSPAccounts_UsuarioId] ON [PSPAccounts] ([UsuarioId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251022191722_AlignPspAccountModel')
BEGIN
    ALTER TABLE [PSPAccounts] ADD CONSTRAINT [FK_PSPAccounts_AspNetUsers_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20251022191722_AlignPspAccountModel')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251022191722_AlignPspAccountModel', N'2.2.6-servicing-10079');
END;

GO

