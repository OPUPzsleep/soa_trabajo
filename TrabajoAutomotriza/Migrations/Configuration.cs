namespace TrabajoAutomotriza.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TrabajoAutomotriza.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TrabajoAutomotriza.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            // Agregar datos de prueba (opcional)
            // Descomenta el código siguiente para agregar datos de prueba

            /*
            // Agregar Autos
            context.Autos.AddOrUpdate(
                a => a.placa,
                new Auto { placa = "ABC-123", marca = "Toyota" },
                new Auto { placa = "XYZ-789", marca = "Honda" }
            );

            // Agregar Mecánicos
            context.Mecanicos.AddOrUpdate(
                m => m.idMecanico,
                new Mecanico { nombres = "Juan", apellidos = "Pérez", telefono = "123456789", especialidad = "Motor" },
                new Mecanico { nombres = "Carlos", apellidos = "García", telefono = "987654321", especialidad = "Transmisión" }
            );

            context.SaveChanges();
            */
        }
    }
}
