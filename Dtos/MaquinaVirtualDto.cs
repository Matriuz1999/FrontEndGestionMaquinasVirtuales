namespace FrontEndGestionMaquinasVirtuales.Dtos
{
    public class MaquinaVirtualDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Cores { get; set; }
        public int RAM { get; set; }
        public int Disco { get; set; }
        public string OS { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }

    public class MaquinaVirtualCreateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public int Cores { get; set; } = 1;
        public int RAM { get; set; } = 1;
        public int Disco { get; set; } = 10;
        public string OS { get; set; } = "Windows";
        public string Estado { get; set; } = "Activo";
    }

    public class MaquinaVirtualUpdateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public int Cores { get; set; }
        public int RAM { get; set; }
        public int Disco { get; set; }
        public string OS { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}