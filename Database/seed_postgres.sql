-- script para insertar los datos maestros (tipos) necesarios para la aplicación
-- Limpiamos las tablas (CASCADE borrará Items si los hubiera)
TRUNCATE TABLE AmmountType, ItemType, PeriodType, DebtType CASCADE;

-- Insertamos forzando los IDs exactos usando OVERRIDING SYSTEM VALUE
INSERT INTO AmmountType (AmmountTypeId, AmmountTypeDesc) OVERRIDING SYSTEM VALUE VALUES 
(1, 'Fijo'), 
(2, 'Variable');

INSERT INTO ItemType (ItemTypeId, ItemTypeDesc) OVERRIDING SYSTEM VALUE VALUES 
(1, 'Ingreso'), 
(2, 'Gasto'), 
(3, 'Mixto');

INSERT INTO PeriodType (PeriodTypeId, PeriodTypeDesc) OVERRIDING SYSTEM VALUE VALUES 
(1, 'Exporadico'), 
(2, 'Recurrente');

INSERT INTO DebtType (DebtTypeId, DebtTypeDesc) OVERRIDING SYSTEM VALUE VALUES 
(1, 'Entrante'), 
(2, 'Saliente');
