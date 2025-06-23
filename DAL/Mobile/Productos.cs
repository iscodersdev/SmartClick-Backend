using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class MTraeProveedoresDTO
    {
        public string UAT { get; set; }
        public int Status { get; set; }
        public string Mensaje { get; set; }
        
        public List<ProveedoresDTO> Proveedores  { get; set; }
    }

    public class MTraeFotoProveedoresDTO
    {
        public string UAT { get; set; }
        public int Status { get; set; }
        public string Mensaje { get; set; }
        public int ProveedorId { get; set; }
        public byte[] Foto { get; set; }
    }
    public class MTraeRubrosDTO
    {
        public string UAT { get; set; }
        public int Status { get; set; }
        public string Mensaje { get; set; }
        public List<RubrosDTO> Rubros { get; set; }
    }

    public class RubrosDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public byte[] IconoAPP { get; set; }
        public bool VerEnAPP { get; set; }
    }
    public class ProveedoresDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string CUIT { get; set; }
        public string RazonSocial { get; set; }
        public string Domicilio { get; set; }
        public byte[] Foto { get; set; }


    }
    public class MTraeProductosDTO
    {
        public string UAT { get; set; }
        public int Status { get; set; }
        public string Mensaje { get; set; }
        public  int ProveedoresId { get; set; }
        public List<MProductosDTO> Productos { get; set; }
    }    
    
    public class MTraeProductosPaginadosDTO
    {
        public string UAT { get; set; }
        public int Status { get; set; }
        public string Mensaje { get; set; }
        public int Desde { get; set; }
        public int Hasta { get; set; }
        public List<MProductosDTO> Productos { get; set; }
    }

    public class MTraeProductosFiltradosDTO
    {
        public string UAT { get; set; }
        public int Status { get; set; }
        public string Mensaje { get; set; }
        public string Producto { get; set; }
        public List<MProductosDTO> Productos { get; set; }
    }

    public class MTraeSubProductosDTO
    {
        public string UAT { get; set; }
        public int Status { get; set; }
        public string Mensaje { get; set; }
        public int ProductoId { get; set; }
        public List<MSubProductosDTO> SubProductos { get; set; }
    }

    public class MTraeProductosPorRubroDTO
    {
        public string UAT { get; set; }
        public int Status { get; set; }
        public string Mensaje { get; set; }
        public int RubroId { get; set; }
        public List<MProductosDTO> Productos { get; set; }
    }

    public class MProductosDTO
    {
        public Int64 Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string DescripcionAmpliada { get; set; }
        public byte[] Foto { get; set; }
        public byte[] Foto1 { get; set; }
        public byte[] Foto2 { get; set; }
        public byte[] Foto3 { get; set; }
        public byte[] Foto4 { get; set; }
        public byte[] Foto5 { get; set; }

    }

    public class MSubProductosDTO
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string NombreSubProducto { get; set; }
    }

    public class MVentaDTO
    {
        public string UAT { get; set; }
        public int Status { get; set; }
        public string Mensaje { get; set; }
        public int ProductoId { get; set; }
        public int PrestamoId { get; set; }
    }


}
