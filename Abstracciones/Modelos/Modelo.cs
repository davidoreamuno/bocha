using System;
using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class ModeloBase
    {
        [Required(ErrorMessage = "El nombre del modelo es requerido")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre del modelo debe tener entre 2 y 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Id de la marca es requerido")]
        public Guid IdMarca { get; set; }
    }

    public class ModeloRequest : ModeloBase
    {

    }

    public class ModeloResponse : ModeloBase
    {
        public Guid Id { get; set; }


        public string? NombreMarca { get; set; }
    }
}
