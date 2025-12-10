using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabajoAutomotriza.Models
{
    [Table("Autos")]
    public class Auto
    {
        [Key]
        public int idAuto { get; set; }

        [Required]
        [StringLength(50)]
        public string placa { get; set; }

        [Required]
        [StringLength(100)]
        public string marca { get; set; }

        [Required]
        [StringLength(100)]
        public string modelo { get; set; }

        public int? ano { get; set; }

        [StringLength(100)]
        public string propietario { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
        public virtual ICollection<OrdenMantenimiento> OrdenesMantenimiento { get; set; } = new List<OrdenMantenimiento>();
    }

    [Table("Clientes")]
    public class Cliente
    {
        [Key]
        public int idCliente { get; set; }

        [Required]
        [StringLength(100)]
        public string nombres { get; set; }

        [Required]
        [StringLength(100)]
        public string apellidos { get; set; }

        [StringLength(20)]
        public string telefono { get; set; }

        public int idAuto { get; set; }

        [ForeignKey("idAuto")]
        public virtual Auto Auto { get; set; }

        public virtual ICollection<OrdenMantenimiento> OrdenesMantenimiento { get; set; } = new List<OrdenMantenimiento>();
    }

    [Table("Mecanicos")]
    public class Mecanico
    {
        [Key]
        public int idMecanico { get; set; }

        [Required]
        [StringLength(100)]
        public string nombres { get; set; }

        [Required]
        [StringLength(100)]
        public string apellidos { get; set; }

        [StringLength(20)]
        public string telefono { get; set; }

        [StringLength(100)]
        public string especialidad { get; set; }

        public virtual ICollection<ServicioAsignado> ServiciosAsignados { get; set; } = new List<ServicioAsignado>();
    }

    [Table("OrdenMantenimiento")]
    public class OrdenMantenimiento
    {
        [Key]
        public int idOrden { get; set; }

        public int idAuto { get; set; }

        [ForeignKey("idAuto")]
        public virtual Auto Auto { get; set; }

        public int idCliente { get; set; }

        [ForeignKey("idCliente")]
        public virtual Cliente Cliente { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime fechaOrden { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? fechaFinalizacion { get; set; }

        [StringLength(500)]
        public string descripcionProblema { get; set; }

        [StringLength(50)]
        public string estado { get; set; } = "Pendiente"; // Pendiente, En Progreso, Completada

        public virtual ICollection<ServicioAsignado> ServiciosAsignados { get; set; } = new List<ServicioAsignado>();
        public virtual ICollection<Presupuesto> Presupuestos { get; set; } = new List<Presupuesto>();
        public virtual ICollection<ServicioRealizado> ServiciosRealizados { get; set; } = new List<ServicioRealizado>();
    }

    [Table("ServicioAsignado")]
    public class ServicioAsignado
    {
        [Key]
        public int idServicio { get; set; }

        public int idOrden { get; set; }

        [ForeignKey("idOrden")]
        public virtual OrdenMantenimiento OrdenMantenimiento { get; set; }

        public int idMecanico { get; set; }

        [ForeignKey("idMecanico")]
        public virtual Mecanico Mecanico { get; set; }

        [StringLength(200)]
        public string descripcionServicio { get; set; }

        public decimal costoServicio { get; set; }
    }

    [Table("Presupuesto")]
    public class Presupuesto
    {
        [Key]
        public int idPresupuesto { get; set; }

        public int idOrden { get; set; }

        [ForeignKey("idOrden")]
        public virtual OrdenMantenimiento OrdenMantenimiento { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime fechaPresupuesto { get; set; }

        public decimal montoEstimado { get; set; }

        [StringLength(50)]
        public string estado { get; set; } = "Pendiente"; // Pendiente, Aprobado

        public virtual ICollection<DetallePresupuesto> DetallesPresupuesto { get; set; } = new List<DetallePresupuesto>();
    }

    [Table("DetallePresupuesto")]
    public class DetallePresupuesto
    {
        [Key]
        public int idDetalle { get; set; }

        public int idPresupuesto { get; set; }

        [ForeignKey("idPresupuesto")]
        public virtual Presupuesto Presupuesto { get; set; }

        [StringLength(200)]
        public string concepto { get; set; }

        public decimal costo { get; set; }
    }

    [Table("Factura")]
    public class Factura
    {
        [Key]
        public int idFactura { get; set; }

        public int idPresupuesto { get; set; }

        [ForeignKey("idPresupuesto")]
        public virtual Presupuesto Presupuesto { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime fechaFactura { get; set; }

        public decimal totalFactura { get; set; }

        [StringLength(50)]
        public string estado { get; set; } = "Emitida"; // Emitida, Pagada
    }

    [Table("ServicioRealizado")]
    public class ServicioRealizado
    {
        [Key]
        public int idServicio { get; set; }

        public int idOrden { get; set; }

        [ForeignKey("idOrden")]
        public virtual OrdenMantenimiento OrdenMantenimiento { get; set; }

        [StringLength(200)]
        public string descripcionServicio { get; set; }

        public decimal costo { get; set; }
    }

    [Table("Pago")]
    public class Pago
    {
        [Key]
        public int idPago { get; set; }

        public int idFactura { get; set; }

        [ForeignKey("idFactura")]
        public virtual Factura Factura { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime fechaPago { get; set; }

        public decimal montoPagado { get; set; }
    }
}
