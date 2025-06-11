using System;

namespace DAL.Models.Core
{
    public class Compras
    {
        public int Id { get; set; }
        public virtual Clientes Cliente { get; set; }
        public virtual Producto Producto { get; set; }
        public virtual SubProducto SubProducto { get; set; }
        public virtual MovimientoBilletera Movimiento { get; set; }
        public virtual Prestamos Prestamo { get; set; }
        public DateTime? FechaCompra { get; set; }
        public virtual EstadoCompra EstadoCompra { get; set; }
        public virtual TipoCompra TipoCompra { get; set; }
        public DateTime FechaAnulacion { get; set; }
    }
    //public enum estadocompra
    //{
    //    efectuado,
    //    pendiente,
    //    anulado
    //}

    //public enum TipoCompra
    //{
    //    Financiado,
    //    DebitoFinanciado,
    //    Debito
    //}
}
