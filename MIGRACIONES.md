# Instrucciones para Crear la Base de Datos

## Paso 1: Abrir la Consola del Administrador de Paquetes

En Visual Studio:
1. Ve a **Herramientas** → **Administrador de Paquetes NuGet** → **Consola del Administrador de Paquetes**

## Paso 2: Ejecutar la Migración

En la consola, ejecuta el siguiente comando:

```powershell
Update-Database
```

Esto creará la base de datos con todas las tablas necesarias.

## Paso 3: Verificar la Base de Datos

La base de datos se creará en:
- **Ubicación**: `App_Data\aspnet-TrabajoAutomotriza-20251209105603.mdf`
- **Tipo**: SQL Server LocalDB

## Tablas Creadas

Las siguientes tablas se crearán automáticamente:

1. **Autos** - Información de vehículos
2. **Clientes** - Información de clientes
3. **Mecanicos** - Información de mecánicos
4. **OrdenMantenimiento** - Órdenes de mantenimiento
5. **ServicioAsignado** - Servicios asignados a mecánicos
6. **Presupuesto** - Presupuestos de trabajos
7. **DetallePresupuesto** - Detalles de presupuestos
8. **Factura** - Facturas generadas
9. **ServicioRealizado** - Servicios realizados
10. **Pago** - Pagos realizados

## Notas Importantes

- Las migraciones ya están configuradas en el proyecto
- El archivo `Configuration.cs` contiene la configuración de migraciones
- Si necesitas agregar más cambios a la BD, crea una nueva migración con:
  ```powershell
  Add-Migration NombreDeLaMigracion
  ```

## Solución de Problemas

Si tienes errores al ejecutar `Update-Database`:

1. Asegúrate de que el proyecto está compilado correctamente
2. Verifica que el archivo `Web.config` tiene la cadena de conexión correcta
3. Si la BD ya existe y quieres recrearla:
   ```powershell
   Update-Database -TargetMigration 0
   Update-Database
   ```
