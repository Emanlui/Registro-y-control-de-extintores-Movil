﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Registro_y_control_de_extintores_Movil.Models
{
    class Extintor
    {
        public int Id_centro { get; set; }

        public string Activo { get; set; }

        public string Tipo { get; set; }

        public string Ubicacion_geografica { get; set; }

        public string Ubicacion { get; set; }

        public string Agente_extintor { get; set; }

        public int Capacidad { get; set; }

        public string Ultima_prueba_hidrostatica { get; set; }

        public string Proxima_prueba_hidrostatica { get; set; }

        public string Proximo_mantenimiento { get; set; }

        public Boolean Presion { get; set; }

        public int Rotulacion { get; set; }

        public Boolean Acceso_a_extintor { get; set; }

        public Boolean Condicion_extintor { get; set; }

        public Boolean Seguro_y_marchamo { get; set; }

        public Boolean Collarin { get; set; }

        public Boolean Condicion_manguera { get; set; }

        public Boolean Condicion_boquilla { get; set; }

        public Byte[] Imagen { get; set; }

    }
}