using System;
using System.Collections.Generic;

namespace LaConcordia.DTO
{
    public class GuardarDireccionRequestDTO
    {
        public string Celular { get; set; } = null!;
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public string? Calle { get; set; }
    }

    public class PedidoDTO
    {
        public string Celular { get; set; } = null!;
        public decimal Origenlat { get; set; }
        public decimal Origenlog { get; set; }
        public decimal Destinolat { get; set; }
        public decimal Destinolog { get; set; }
        public int? Tiempodemora { get; set; }
        public string? Ruc { get; set; }
        public DateTime Fecharegistro { get; set; }
        public string? Usuario { get; set; }
        public decimal? Base { get; set; }
        public string? Unidad { get; set; }
        public string? Ciconductor { get; set; }
        public string? Conductor { get; set; }
        public string? Unidadsiguiente { get; set; }
        public string? Ciconductorsiguiente { get; set; }
        public string? Conductorsiguiente { get; set; }
        public decimal? Precio { get; set; }
        public decimal? Km { get; set; }
        public string? Numvoucher { get; set; }
        public string? Valija { get; set; }
        public string? Empleado { get; set; }
        public string? Recorrido { get; set; }
        public string? Estado { get; set; }
        public string? Autorizado { get; set; }
    }

    public class ConductorInfoDTO
    {
        public string Cedula { get; set; } = null!;
        public string? NombreCompleto { get; set; }
    }

    public class PrecioKmDTO
    {
        public decimal Precio { get; set; }
        public decimal Km { get; set; }
    }

    public class PedidosPorUsuarioDTO
    {
        public string Usuario { get; set; } = null!;
        public int Cantidad { get; set; }
    }

    public class PedidosPorUnidadDTO
    {
        public string Unidad { get; set; } = null!;
        public int Cantidad { get; set; }
    }

    public class PedidoOperadoraDTO
    {
        public DateTime Fecharegistro { get; set; }
        public string? CalleOrigen { get; set; }
        public string? CalleDestino { get; set; }
        public string? Usuario { get; set; }
        public string? Unidad { get; set; }
        public decimal? Precio { get; set; }
    }

    // App del conductor (Taxista)
    public class InfoConductorDTO
    {
        public string Cedula { get; set; } = null!;
        public string? NombreCompleto { get; set; }
        public string? Unidad { get; set; }
    }

    public class PedidoIdentificadorDTO
    {
        public string Celular { get; set; } = null!;
        public decimal Origenlat { get; set; }
        public decimal Origenlog { get; set; }
        public decimal Destinolat { get; set; }
        public decimal Destinolog { get; set; }
        public DateTime FechaRegistroPedido { get; set; }
    }

    public class TomarCarreraRequestDTO : PedidoIdentificadorDTO
    {
        public decimal LatInicio { get; set; }
        public decimal LogInicio { get; set; }
    }

    public class FinalizarCarreraRequestDTO : PedidoIdentificadorDTO
    {
        public decimal LatFin { get; set; }
        public decimal LogFin { get; set; }
        public decimal DistanciaKm { get; set; }
        public decimal PrecioFinal { get; set; }
    }

    public class CarreraHistorialDTO
    {
        public DateTime Fecha { get; set; }
        public decimal? Precio { get; set; }
        public decimal? DistanciaKm { get; set; }
    }

    public class GananciasConductorDTO
    {
        public int CantidadCarreras { get; set; }
        public decimal TotalGanado { get; set; }
        public List<CarreraHistorialDTO> Historial { get; set; } = new();
    }

    public class CalificarCarreraRequestDTO : PedidoIdentificadorDTO
    {
        public int Calificacion { get; set; }
        public string? Comentario { get; set; }
    }
}
