﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Registro_y_control_de_extintores_Movil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Registro_y_control_de_extintores_Movil.Activities
{
    [Activity(Label = "recuperarContrasena")]
    public class recuperarContrasena : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.recuperarContrasena);

            Button button = FindViewById<Button>(Resource.Id.recuperarButton);

            button.Click += BtnRestablecer_Click;
        }

        private void BtnRestablecer_Click(object sender, EventArgs e)
        {

            TextView dato_del_usuario = FindViewById<TextView>(Resource.Id.usuarioCorreo);
            if(dato_del_usuario.Text != "") {
                StringBuilder builder = new StringBuilder();
                Random random = new Random();
                char ch;
                for (int i = 0; i < 8; i++)
                {
                    ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                    builder.Append(ch);
                }



                UsuarioCrud uc = new UsuarioCrud();

                Boolean verificarEnvioDeCorreo = false;

                if (uc.verificacionDeUsuario(dato_del_usuario.Text))
                {
                    verificarEnvioDeCorreo = enviarCorreoDeRestablecimiento(dato_del_usuario.Text, builder.ToString());

                    if (verificarEnvioDeCorreo) StartActivity(typeof(MensajeRestablecer));
                    StartActivity(typeof(login));
                }
                
                 StartActivity(typeof(MensajeRestablecer));
                
            }
        }

        private Boolean enviarCorreoDeRestablecimiento(string correo, string v)
        {
            // Parámetros
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("emanuellejs1999@gmail.com");
            try
            {
                mail.To.Add(correo);
            }
            catch(System.FormatException exception)
            {
                Toast.MakeText(this, "Error en el formato del correo", ToastLength.Short).Show();
                return false;
            }
            
            mail.Subject = "Restablecimiento de la contraseña";
            mail.Body = "Se otorgará una contraseña temporal para poder acceder a la plataforma de extintores. \n " +
                "Contraseña: " + v;

            // Archivos
            //System.Net.Mail.Attachment attachment;
            //attachment = new System.Net.Mail.Attachment(filename);
            //mail.Attachments.Add(attachment);
            //end email attachment part

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("emanuellejs1999@gmail.com", "lhixmsiuljrqhcey");
            SmtpServer.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors) {
                return true;
            };
            try
            {
                SmtpServer.Send(mail);
                UsuarioCrud uc = new UsuarioCrud();

                uc.cambiarContraseña(v, correo);

            }
            catch (Exception exception)
            { 
                Toast.MakeText(this, "Error a la hora de enviar un correo", ToastLength.Short).Show();
                return false;
            }
            return true;
        }
    }
}