﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Abstracciones.Modelos
{
    public class VehiculoBase

    { 
        [Required(ErrorMessage = "La propiedad placa es requerida")]
        [RegularExpression(@"[A-Za-z]{3}-[0-9]{3}", ErrorMessage ="El formato de la placa debe de ser ###-ABC")]
        public string Placa {  get; set; }

        [Required(ErrorMessage = "La propiedad placa es requerida")]
        [StringLength(40, ErrorMessage ="La propiedad color debe ser mayor a 4 caracteres y menos a 40", MinimumLength =4)]
        public string Color { get; set; }
        [Required(ErrorMessage = "La propiedad placa es requerida")]
        [RegularExpression(@"(19|20)\d\d", ErrorMessage ="El formato del años no es valido")]
        public int Anio { get; set; }
        [Required(ErrorMessage = "La propiedad placa es requerida")]
        public Decimal Precio { get; set; }
        [Required(ErrorMessage = "La propiedad placa es requerida")]
        [EmailAddress]
        [DisplayName("Correo del Propietario")]
        public string CorreoPropietario { get; set; }
        [Required(ErrorMessage = "La propiedad placa es requerida")]
        [Phone]
        public string TelefonoPropietario { get; set; }

    }
    public class VehiculoRequest : VehiculoBase
    {

        public Guid IdModelo { get; set; }

    }
     public class VehiculoResponse : VehiculoBase 
    {

        public Guid Id { get; set; }
        public string? Modelo { get; set; }
        public string? Marca { get; set; }

    }
    public class VehiculoDetalle : VehiculoResponse
    {
        public bool RevisionValida { get; set; }
            public bool RegistroValido { get; set; }
    }

}
