using System;
using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class MarcaBase
    {
        [Required(ErrorMessage = "El nombre de la marca es requerido")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Debe tener entre 2 y 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;
    }

    public class MarcaRequest : MarcaBase
    {
        
        public Guid IdMarca { get; set; }
    }

    public class MarcaResponse : MarcaBase
    {
        public Guid Id { get; set; }
    }
}
