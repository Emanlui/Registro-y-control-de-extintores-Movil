﻿using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text; 
using System.Data.SqlClient;
using MySqlConnector;

namespace Registro_y_control_de_extintores_Movil.Models
{
    class ExtintorCrud
    {
        public Extintor extintor { set; get; }

        public byte[] obtenerByteArray(Bitmap bm)
        {
            System.IO.MemoryStream bs = new System.IO.MemoryStream();
            bm.Compress(Bitmap.CompressFormat.Jpeg, 100, bs);
            return bs.ToArray();
        } 
        internal void EliminarImagen(string activo)
        {
            Conexion conexion = new Conexion();
            conexion.con.Open();
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.CommandText = "UPDATE extintor SET imagen = null WHERE activo=@activo; ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conexion.con;

                cmd.Parameters.Add("@activo", MySqlDbType.VarChar).Value = activo;

                cmd.ExecuteNonQuery();
                conexion.con.Close();
            }
        }

        internal List<Extintor> ObtenerRegistros()
        {
            List<Extintor> lista_de_extintores = new List<Extintor>();
            Conexion conexion = new Conexion();
            conexion.con.Open();
            using (MySqlCommand cmd = new MySqlCommand())
            {
                Context mContext = Android.App.Application.Context;
                AppPreferences ap = new AppPreferences(mContext);

                cmd.CommandText = "Select * from extintor,usuario,centro_de_trabajo WHERE usuario.id_centro=centro_de_trabajo.id and centro_de_trabajo.id = extintor.id_centro and correo=@correo;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conexion.con;
                cmd.Parameters.Add("@correo", MySqlDbType.Text).Value = ap.getCorreoKey();
                MySqlDataReader registros = cmd.ExecuteReader();
                while (registros.Read())
                {
                    Extintor e = new Extintor();
                    e.Activo = registros["activo"].ToString();
                    e.Tipo = registros["tipo"].ToString();
                    e.Ubicacion_geografica = registros["ubicacion_geografica"].ToString();
                    e.Ubicacion = registros["ubicacion"].ToString();
                    e.Agente_extintor = registros["agente_extintor"].ToString();
                    e.Capacidad = (int)registros["capacidad"];
                    e.Ultima_prueba_hidrostatica = registros["ultima_prueba_hidrostatica"].ToString();
                    e.Proxima_prueba_hidrostatica = registros["proxima_prueba_hidrostatica"].ToString();
                    e.Proximo_mantenimiento = registros["proximo_mantenimiento"].ToString();
                    e.Presion = (int)(ulong)registros["presion"];
                    e.Rotulacion = (int)registros["rotulacion"];
                    e.Acceso_a_extintor = (int)(ulong)registros["acceso_a_extintor"];
                    e.Condicion_extintor = (int)(ulong)registros["condicion_extintor"];
                    e.Seguro_y_marchamo = (int)(ulong)registros["seguro_y_marchamo"];
                    e.Collarin = (int)(ulong)registros["collarin"];
                    e.Condicion_manguera = (int)(ulong)registros["condicion_manguera"];
                    e.Condicion_boquilla = (int)(ulong)registros["condicion_boquilla"];
                    try { 
                        e.Imagen = (byte[])registros["imagen"];
                    }
                    catch (System.InvalidCastException exception)
                    {
                        e.Imagen = null;
                    }
                    System.Console.WriteLine(e.ToString());
                    lista_de_extintores.Add(e);
                }

                conexion.con.Close();
            }

            return lista_de_extintores;
        }

        internal void ModificarFotoExtintor(Bitmap bitmap, string activo)
        {
            Conexion conexion = new Conexion();
            conexion.con.Open();

            Byte[] arreglo_imagen = obtenerByteArray(bitmap);

            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.CommandText = "UPDATE extintor SET imagen = @imagen_a_insertar WHERE activo = @activo;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conexion.con;
                cmd.Parameters.Add("@imagen_a_insertar", MySqlDbType.Blob).Value = arreglo_imagen;
                cmd.Parameters.Add("@activo", MySqlDbType.Text).Value = activo;

                cmd.ExecuteNonQuery();
                conexion.con.Close();
            }

        }

        internal List<Extintor> ObtenerRegistroPorActivo(string text)
        {
            List<Extintor> lista_de_extintores = new List<Extintor>();
            Conexion conexion = new Conexion();
            conexion.con.Open();
            using (MySqlCommand cmd = new MySqlCommand())
            {
                Context mContext = Android.App.Application.Context;
                AppPreferences ap = new AppPreferences(mContext);

                cmd.CommandText = "SELECT * FROM extintor,usuario,centro_de_trabajo WHERE extintor.activo = @activo and usuario.id_centro=centro_de_trabajo.id and centro_de_trabajo.id = extintor.id_centro and correo=@correo;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conexion.con;
                cmd.Parameters.Add("@activo", MySqlDbType.Text).Value = text;
                cmd.Parameters.Add("@correo", MySqlDbType.Text).Value = ap.getCorreoKey();
                MySqlDataReader registros = cmd.ExecuteReader();
                while (registros.Read())
                {
                    Extintor e = new Extintor();
                    e.Activo = registros["activo"].ToString();
                    e.Tipo = registros["tipo"].ToString();
                    e.Ubicacion_geografica = registros["ubicacion_geografica"].ToString();
                    e.Ubicacion = registros["ubicacion"].ToString();
                    e.Agente_extintor = registros["agente_extintor"].ToString();
                    e.Capacidad = (int)registros["capacidad"];
                    e.Ultima_prueba_hidrostatica = registros["ultima_prueba_hidrostatica"].ToString();
                    e.Proxima_prueba_hidrostatica = registros["proxima_prueba_hidrostatica"].ToString();
                    e.Proximo_mantenimiento = registros["proximo_mantenimiento"].ToString();
                    e.Presion = (int)(ulong)registros["presion"];
                    e.Rotulacion = (int)registros["rotulacion"];
                    e.Acceso_a_extintor = (int)(ulong)registros["acceso_a_extintor"];
                    e.Condicion_extintor = (int)(ulong)registros["condicion_extintor"];
                    e.Seguro_y_marchamo = (int)(ulong)registros["seguro_y_marchamo"];
                    e.Collarin = (int)(ulong)registros["collarin"];
                    e.Condicion_manguera = (int)(ulong)registros["condicion_manguera"];
                    e.Condicion_boquilla = (int)(ulong)registros["condicion_boquilla"];
                    try
                    {
                        e.Imagen = (byte[])registros["imagen"];
                    }
                    catch (System.InvalidCastException exception)
                    {
                        e.Imagen = null;
                    }
                    
                    lista_de_extintores.Add(e);
                }

                conexion.con.Close();
            }

            return lista_de_extintores;
        }
    }
}