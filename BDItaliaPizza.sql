-- Selecciona la base de datos
USE BDItaliaPizza;

-- Creación de la tabla Productos
CREATE TABLE [dbo].[Productos] (
  [idProductos] INT IDENTITY(1,1) NOT NULL,
  [nombre] NVARCHAR(45) NULL,
  [marca] NVARCHAR(45) NULL,
  [tipo] NVARCHAR(45) NULL,
  [precio] FLOAT NULL,
  [codigoProducto] NVARCHAR(45) NULL,
  CONSTRAINT [PK_Productos] PRIMARY KEY CLUSTERED ([idProductos])
);

-- Creación de la tabla Insumos
CREATE TABLE [dbo].[Insumos] (
  [idInsumos] INT IDENTITY(1,1) NOT NULL,
  [nombre] NVARCHAR(45) NULL,
  [marca] NVARCHAR(45) NULL,
  [tipo] NVARCHAR(45) NULL,
  [cantidadDeEmpaque] NVARCHAR(45) NULL,
  [codigoInsumo] NVARCHAR(45) NULL,
  [unidadDeMedida] NVARCHAR(10) NULL,
  CONSTRAINT [PK_Insumos] PRIMARY KEY CLUSTERED ([idInsumos])
);

-- Creación de la tabla Receta
CREATE TABLE [dbo].[Receta] (
  [idRecetas] INT IDENTITY(1,1) NOT NULL,
  [nombre] NVARCHAR(45) NULL,
  [descripcionPreparacion] NVARCHAR(60) NULL,
  CONSTRAINT [PK_Receta] PRIMARY KEY CLUSTERED ([idRecetas])
);

-- Creación de la tabla Empleados
CREATE TABLE [dbo].[Empleados] (
  [idEmpleados] INT IDENTITY(1,1) NOT NULL,
  [nombre] NVARCHAR(45) NULL,
  [apellidoMaterno] NVARCHAR(45) NULL,
  [apellidoPaterno] NVARCHAR(45) NULL,
  [telefono] NVARCHAR(24) NULL,
  [correo] NVARCHAR(50) NULL,
  [contrasena] NVARCHAR(12) NULL,
  [foto] NVARCHAR(250) NULL,
  [rol] NVARCHAR(45) NULL,
  CONSTRAINT [PK_Empleados] PRIMARY KEY CLUSTERED ([idEmpleados])
);

-- Creación de la tabla Pedidos
CREATE TABLE [dbo].[Pedidos] (
  [idPedidos] INT IDENTITY(1,1) NOT NULL,
  [tipoDePedido] NVARCHAR(45) NULL,
  [domicilioDeEntrega] NVARCHAR(60) NULL,
  [estadoDelPedido] NVARCHAR(45) NULL,
  [precioTotal] FLOAT NULL,
  [nombreCliente] NVARCHAR(50) NULL,
  CONSTRAINT [PK_Pedidos] PRIMARY KEY CLUSTERED ([idPedidos])
);

-- Creación de la tabla Proveedores
CREATE TABLE [dbo].[Proveedores] (
  [idProveedores] INT IDENTITY(1,1) NOT NULL,
  [nombreCompania] NVARCHAR(50) NULL,
  [nombreDelContacto] NVARCHAR(45) NULL,
  [telefono] NVARCHAR(24) NULL,
  [direccion] NVARCHAR(45) NULL,
  [ciudad] NVARCHAR(45) NULL,
  [estado] NVARCHAR(45) NULL,
  [categoriaProveedor] NVARCHAR(45) NULL,
  CONSTRAINT [PK_Proveedores] PRIMARY KEY CLUSTERED ([idProveedores])
);

-- Creación de la tabla Inventario
CREATE TABLE [dbo].[Inventario] (
  [idInventario] INT IDENTITY(1,1) NOT NULL,
  [cantidadMaxima] INT NULL,
  [cantidadMinima] INT NULL,
  CONSTRAINT [PK_Inventario] PRIMARY KEY CLUSTERED ([idInventario])
);

-- Creación de la tabla Clientes
CREATE TABLE [dbo].[Clientes] (
  [idClientes] INT IDENTITY(1,1) NOT NULL,
  [nombre] NVARCHAR(45) NULL,
  [telefono] NVARCHAR(24) NULL,
  [rol] NVARCHAR(45) NULL,
  CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED ([idClientes])
);

-- Creación de la tabla DetallesPedido
CREATE TABLE [dbo].[DetallesPedido] (
  [idDetallesPedido] INT IDENTITY(1,1) NOT NULL,
  [Pedidos_idPedidos] INT NOT NULL,
  [fechaHoraDelPedido] DATETIME NULL,
  [Empleados_idEmpleados] INT NOT NULL,
  CONSTRAINT [PK_DetallesPedido] PRIMARY KEY CLUSTERED ([idDetallesPedido]),
  CONSTRAINT [FK_DetallesPedido_Pedidos] FOREIGN KEY ([Pedidos_idPedidos])
    REFERENCES [dbo].[Pedidos] ([idPedidos])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT [FK_DetallesPedido_Empleados] FOREIGN KEY ([Empleados_idEmpleados])
    REFERENCES [dbo].[Empleados] ([idEmpleados])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
);

-- Creación de la tabla IngredientesReceta
CREATE TABLE [dbo].[IngredientesReceta] (
  [Insumos_idInsumos] INT NOT NULL,
  [Recetas_idRecetas] INT NOT NULL,
  [cantidad] INT NULL,
  CONSTRAINT [PK_IngredientesReceta] PRIMARY KEY CLUSTERED ([Insumos_idInsumos], [Recetas_idRecetas]),
  CONSTRAINT [FK_IngredientesReceta_Insumos] FOREIGN KEY ([Insumos_idInsumos])
    REFERENCES [dbo].[Insumos] ([idInsumos])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT [FK_IngredientesReceta_Recetas] FOREIGN KEY ([Recetas_idRecetas])
    REFERENCES [dbo].[Receta] ([idRecetas])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
);

-- Creación de la tabla PedidoProducto
CREATE TABLE [dbo].[PedidoProducto] (
  [idPedidoProducto] INT IDENTITY(1,1) NOT NULL,
  [Productos_idProductos] INT NOT NULL,
  [Pedidos_idPedidos] INT NOT NULL,
  CONSTRAINT [PK_PedidoProducto] PRIMARY KEY CLUSTERED ([idPedidoProducto]),
  CONSTRAINT [FK_PedidoProducto_Productos] FOREIGN KEY ([Productos_idProductos])
    REFERENCES [dbo].[Productos] ([idProductos])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT [FK_PedidoProducto_Pedidos] FOREIGN KEY ([Pedidos_idPedidos])
    REFERENCES [dbo].[Pedidos] ([idPedidos])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
);

-- Creación de la tabla InventarioDeInsumo
CREATE TABLE [dbo].[InventarioDeInsumo] (
  [idInventarioDeInsumo] INT IDENTITY(1,1) NOT NULL,
  [Insumos_idInsumos] INT NOT NULL,
  [Inventario_idInventario] INT NOT NULL,
  [cantidadTotal] INT NULL,
  CONSTRAINT [PK_InventarioDeInsumo] PRIMARY KEY CLUSTERED ([idInventarioDeInsumo]),
  CONSTRAINT [FK_InventarioDeInsumo_Insumos] FOREIGN KEY ([Insumos_idInsumos])
    REFERENCES [dbo].[Insumos] ([idInsumos])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT [FK_InventarioDeInsumo_Inventario] FOREIGN KEY ([Inventario_idInventario])
    REFERENCES [dbo].[Inventario] ([idInventario])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
);

-- Creación de la tabla InventarioDeProductos
CREATE TABLE [dbo].[InventarioDeProductos] (
  [idInventarioDeProductos] INT IDENTITY(1,1) NOT NULL,
  [Inventario_idInventario] INT NOT NULL,
  [Productos_idProductos] INT NOT NULL,
  [cantidadTotal] INT NULL,
  CONSTRAINT [PK_InventarioDeProductos] PRIMARY KEY CLUSTERED ([idInventarioDeProductos]),
  CONSTRAINT [FK_InventarioDeProductos_Inventario] FOREIGN KEY ([Inventario_idInventario])
    REFERENCES [dbo].[Inventario] ([idInventario])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT [FK_InventarioDeProductos_Productos] FOREIGN KEY ([Productos_idProductos])
    REFERENCES [dbo].[Productos] ([idProductos])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
);

-- Creación de la tabla InsumoReceta
CREATE TABLE [dbo].[InsumoReceta] (
  [idInsumoReceta] INT IDENTITY(1,1) NOT NULL,
  [Insumos_idInsumos] INT NOT NULL,
  [Receta_idRecetas] INT NOT NULL,
  CONSTRAINT [PK_InsumoReceta] PRIMARY KEY CLUSTERED ([idInsumoReceta]),
  CONSTRAINT [FK_InsumoReceta_Insumos] FOREIGN KEY ([Insumos_idInsumos])
    REFERENCES [dbo].[Insumos] ([idInsumos])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT [FK_InsumoReceta_Receta] FOREIGN KEY ([Receta_idRecetas])
    REFERENCES [dbo].[Receta] ([idRecetas])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
);

-- Creación de la tabla RecetasProductos
CREATE TABLE [dbo].[RecetasProductos] (
  [idRecetasProductos] INT IDENTITY(1,1) NOT NULL,
  [Receta_idRecetas] INT NOT NULL,
  [Productos_idProductos] INT NOT NULL,
  CONSTRAINT [PK_RecetasProductos] PRIMARY KEY CLUSTERED ([idRecetasProductos]),
  CONSTRAINT [FK_RecetasProductos_Receta] FOREIGN KEY ([Receta_idRecetas])
    REFERENCES [dbo].[Receta] ([idRecetas])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT [FK_RecetasProductos_Productos] FOREIGN KEY ([Productos_idProductos])
    REFERENCES [dbo].[Productos] ([idProductos])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
);

-- Creación de la tabla CorteDeCaja
CREATE TABLE [dbo].[CorteDeCaja] (
  [idCorteDeCaja] INT IDENTITY(1,1) NOT NULL,
  [fechaDeCorte] DATETIME NULL,
  [totalDeIngresos] FLOAT NULL,
  [dineroRestante] FLOAT NULL,
  [turno] NVARCHAR(45) NULL,
  CONSTRAINT [PK_CorteDeCaja] PRIMARY KEY CLUSTERED ([idCorteDeCaja])
);

--Datos a insertar

INSERT INTO Empleados(
nombre, apellidoPaterno, apellidoMaterno, telefono, correo, contrasena, rol)VALUES
('René Ulises', 'García', 'Velázquez', 9231285172, 'gerente@outlook.com','1111','Gerente'),
('Alvaro', 'Lopez', 'Martinez', 7896541236, 'cajero@outlook.com','1111','Cajero'),
('Bryan Josue', 'Hernandez', 'Marcial', 9231285172, 'cocinero@outlook.com','1111','Cocinero');


INSERT INTO Insumos(
codigoInsumo, nombre, marca, tipo, cantidadDeEmpaque, unidadDeMedida)VALUES
('CI536', 'Harina de Trigo', 'Harimsa', 'Masa y Harina', '1000','Gramo'),
('CI687', 'Salsa de Tomate', 'Multi', 'Salsas y Bases', '400','Gramo'),
('CI980', 'Mozzarella Rallado', 'Galbani', 'Quesos', '200','Gramo'),
('CI365', 'Pepperoni', 'Hormel', 'Toppings y Proteínas', '227','Gramo'),
('CI876', 'Cebolla Morada', 'Del Monte', 'Verduras y Hierbas', '500','Gramo'),
('CI697', 'Aceite de Oliva', 'Gallo', 'Aceites y Condimentos', '750','Mililitro'),
('CI175', 'Tomates Secos', 'Trader Joes', 'Extras Creativos', '200','Gramo');


INSERT INTO Productos(
codigoProducto, nombre,marca,tipo,precio)VALUES
('CP475', 'Pizza de Pepperoni', 'Italia Pizza', 'Pizza', '99.99'),
('CP567', 'Pepsi', 'Pepsico', 'Bebida', '29.99'),
('CP876', 'Pastel de Chocolate', 'Globo', 'Postre', '49.99'),
('CP987', 'Pasta fettuccine', 'Italia Pizza', 'Pasta', '89.99');


INSERT INTO Proveedores(
nombreCompania, nombreDelContacto, telefono, ciudad, direccion, estado, categoriaProveedor)VALUES
('Harimsa', 'Juan Pérez', '9632587412', 'Aguascalientes', 'Av. de los Insurgentes Sur, Col. Nápoles','Aguascalientes', 'Masa y Harina'),
('Multti', 'Bob Moore', '7412589632', 'Ensenada', 'Calle del Trigo, Col. Granjas','Baja California', 'Salsas y Bases'),
('Galbani', 'Antonio Bianchi', '7894561232', 'Calakmul', 'Av.Vallarta, Col. Ciudad Granja','Campeche', 'Quesos'),
('Hormel', 'Dave Anderson', '1234569878', 'Abasolo', 'Av. Constitución, Col. Centro','Coahuila', 'Toppings y Proteínas'),
('Dole', 'Carol White', '9637412585', 'Armeria', 'Calle Tlalpan, Col. Santa Ursula','Colima', 'Verduras y Hierbas'),
('Gallo', 'Joana Silva', '4569517532', 'Acacoyagua', 'Av. Universidad, Col. Juarez','Chiapas', 'Aceites y Condimentos'),
('Goya', 'Anthony Goya', '7531598524', 'Ahumada', 'Av. Americas, Col. Country','Chihuahua', 'Extras Creativos');
