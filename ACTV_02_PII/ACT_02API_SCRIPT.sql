--CREATE DATABASE ACTV_02

USE ACTV_02

CREATE TABLE formasPago (
id_forma_pago INT IDENTITY(1,1),
descripcion VARCHAR(50)NOT NULL
CONSTRAINT pk_formasPago PRIMARY KEY(id_forma_pago)
)

CREATE TABLE Articulos(
id_articulo INT IDENTITY(1,1),
nombre VARCHAR(50)NOT NULL,
esta_activo BIT,
stock INT NOT NULL
CONSTRAINT pk_articulo PRIMARY KEY(id_articulo)
)

CREATE TABLE FACTURAS(
nro_factura INT IDENTITY(1,1),
fecha DATETIME NOT NULL,
id_forma_pago INT NOT NULL,
cliente VARCHAR(50)
CONSTRAINT pk_Facturas PRIMARY KEY(nro_factura),
CONSTRAINT fk_facturas_formasPago FOREIGN KEY(id_forma_pago)
		REFERENCES formasPago (id_forma_pago)
)

CREATE TABLE DETALLE_FACTURAS(
id_detalle INT NOT NULL,
nro_factura INT NOT NULL,
id_articulo INT NOT NULL,
cantidad INT NOT NULL,
precio FLOAT NOT NULL
CONSTRAINT pk_detalleFactura PRIMARY KEY(id_detalle),
CONSTRAINT fk_detalleFact_FACTURAS FOREIGN KEY (nro_factura)
		REFERENCES FACTURAS (nro_factura),
CONSTRAINT fk_detalleFact_Articulos FOREIGN KEY(id_articulo)
		REFERENCES Articulos (id_articulo)
)

--ALGUNOS INSERTS
--fp
INSERT INTO formasPago (descripcion) VALUES ('Efectivo')
INSERT INTO formasPago (descripcion) VALUES ('Tarjeta de crédito')
INSERT INTO [dbo].[formasPago] ([descripcion]) VALUES ('Tarjeta de débito')
INSERT INTO [dbo].[formasPago] ([descripcion]) VALUES ('Transferencia bancaria')

--Articulos
INSERT INTO Articulos(nombre,esta_activo,stock) VALUES ('Yerba',1 , 20)
INSERT INTO Articulos(nombre,esta_activo,stock) VALUES ('Tacho',1 , 23)
INSERT INTO Articulos(nombre,esta_activo,stock) VALUES ('CocaCola',1 , 54)
INSERT INTO Articulos(nombre,esta_activo,stock) VALUES ('Mate',1 , 14)




--facturas
INSERT INTO FACTURAS (fecha,id_forma_pago,cliente) VALUES (GETDATE(),1,'Mateo Ventrella')
INSERT INTO FACTURAS (fecha,id_forma_pago,cliente) VALUES ('22/05/2023',3,'Samuel Oggero')
INSERT INTO FACTURAS (fecha,id_forma_pago,cliente) VALUES (GETDATE(),4,'Thiago Pereyra')

--detalle
INSERT INTO DETALLE_FACTURAS (id_detalle,nro_factura,id_articulo,cantidad,precio) VALUES(1,1,1,2,2400)--Mateo
INSERT INTO DETALLE_FACTURAS (id_detalle,nro_factura,id_articulo,cantidad,precio) VALUES(2,1,3,1,1900)--Mateo

INSERT INTO DETALLE_FACTURAS (id_detalle,nro_factura,id_articulo,cantidad,precio) VALUES(3,2,4,1,3400)--Samuel
INSERT INTO DETALLE_FACTURAS (id_detalle,nro_factura,id_articulo,cantidad,precio) VALUES(4,2,2,1,1200)--Samuel

INSERT INTO DETALLE_FACTURAS (id_detalle,nro_factura,id_articulo,cantidad,precio) 
						VALUES(5           ,3          ,1,         2 ,    2400) --Thiago

INSERT INTO DETALLE_FACTURAS (id_detalle,nro_factura,id_articulo,cantidad,precio) 
						VALUES(6       ,3          ,    3,         2 ,    1900)--Thiago





CREATE PROCEDURE RECUPERAR_ARTICULOS							 EXEC RECUPERAR_ARTICULOS
AS
BEGIN

SELECT A.id_articulo, A.nombre,A.stock, A.esta_activo
FROM Articulos A

END
------------------------------------------------------
CREATE PROCEDURE RECUPERAR_ARTICULO_PORID      EXEC RECUPERAR_ARTICULO_PORID @id = 1
@id INT
AS
BEGIN

SELECT A.id_articulo, A.nombre,A.stock, A.esta_activo
FROM Articulos A
WHERE id_articulo = @id

END
---------------------------

CREATE PROCEDURE BORRAR_ARTICULO
@id INT
AS
BEGIN

DELETE FROM Articulos WHERE id_articulo = @id

END

CREATE PROCEDURE SP_UPSERT_ARTICULO
@idArticulo INT,
@nombre varchar(50),
@stock int
AS
BEGIN
	IF @idArticulo = 0
	BEGIN
		INSERT INTO Articulos(nombre, stock, esta_activo)
		VALUES (@nombre, @stock, 1)
	END

	ELSE
	BEGIN
		UPDATE Articulos
		SET nombre = @nombre, stock = @stock
		WHERE id_articulo = @idArticulo
	END
END
GO


--CREATE PROCEDURE SP_INSERTAR_FACTURAS
--@cliente varchar(50),
--@id_formaPago int,
--@nroFactura int OUTPUT
--AS
--BEGIN
--	INSERT INTO FACTURAS(cliente, fecha, id_forma_pago)
--	VALUES (@cliente, GETDATE(),@id_formaPago)
--	SET @nroFactura = SCOPE_IDENTITY()
--END
--GO

--CREATE PROCEDURE SP_INSERTAR_DETALLEFACT
--@nro_factura int,
--@id_detalle int,
--@producto int,
--@cantidad int,
--@precio float
--AS
--BEGIN
--	INSERT INTO DETALLE_FACTURAS(id_detalle, nro_factura, id_articulo, cantidad, precio)
--	VALUES (@id_detalle, @nro_factura, @producto, @cantidad, @precio)
--END
--GO



--CREATE PROCEDURE RECUPERAR_FACTURAS

--AS
--BEGIN

--SELECT F.nro_factura,CONVERT(date,F.fecha)'FECHA',DF.cantidad, DF.precio, FP.descripcion'formaPago', F.cliente
--FROM FACTURAS F
--JOIN DETALLE_FACTURAS DF ON DF.nro_factura = F.nro_factura
--JOIN Articulos A ON A.id_articulo = DF.id_articulo
--JOIN formasPago FP ON FP.id_forma_pago = F.id_forma_pago

--END

--CREATE PROCEDURE RECUPERAR_FACTURAS_PORID
--@nroFactura INT
--AS
--BEGIN

--SELECT F.nro_factura,CONVERT(date,F.fecha)'FECHA',DF.cantidad, DF.precio, FP.descripcion'formaPago', F.cliente
--FROM FACTURAS F
--JOIN DETALLE_FACTURAS DF ON DF.nro_factura = F.nro_factura
--JOIN Articulos A ON A.id_articulo = DF.id_articulo
--JOIN formasPago FP ON FP.id_forma_pago = F.id_forma_pago
--WHERE F.nro_factura = @nroFactura

--END

--------------------------------------------------------
