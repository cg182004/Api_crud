using proj_crud.Models;
using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace proj_crud.Factories
{
    public static class UsuarioFactory
    {
        public static usuario CrearUsuarioNormal(string dni, string nombre, string apellido, string sexo)
        {
            // Sanitizar nombre y apellido (sin tildes, espacios ni caracteres especiales)
            string nombreSan = RemoverCaracteresEspeciales(nombre).ToLower();
            string apellidoSan = RemoverCaracteresEspeciales(apellido).ToLower();

            // Generar correo y contraseña
            string correo = $"{nombreSan[0]}{apellidoSan}@dominio.com";
            string contrasena = $"{nombreSan.Substring(0, Math.Min(4, nombreSan.Length))}2025!";

            return new usuario
            {
                dni = dni,
                Nombre = nombre,
                Apellido = apellido,
                sexo = sexo,
                correo = correo,
                Contrasena = contrasena
            };
        }

        private static string RemoverCaracteresEspeciales(string texto)
        {
            string normalizado = texto.Normalize(NormalizationForm.FormD);
            Regex regex = new Regex("[^a-zA-Z0-9]");
            return regex.Replace(normalizado, "");
        }
    }
}
