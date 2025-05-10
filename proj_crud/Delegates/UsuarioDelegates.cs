using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using proj_crud.Models; // Importa tu modelo

namespace proj_crud.Delegates
{
    public static class UsuarioDelegates
    {
        public static Func<usuario, List<string>> ValidarUsuario = usuario =>
        {
            var errores = new List<string>();

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                errores.Add("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.Apellido))
                errores.Add("El apellido es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.dni) || usuario.dni.Length < 7)
                errores.Add("El DNI debe tener al menos 7 caracteres.");

            if (string.IsNullOrWhiteSpace(usuario.Contrasena) || usuario.Contrasena.Length < 6)
                errores.Add("La contraseña debe tener al menos 6 caracteres.");

            // Validación de correo (verifica si contiene un @ y un dominio)
            if (string.IsNullOrWhiteSpace(usuario.correo) || !usuario.correo.Contains("@") || !usuario.correo.Contains("."))
                errores.Add("El correo electrónico no es válido.");


            return errores;
        };

     
        public static Action<usuario> NotificarCreacion = usuario =>
            Console.WriteLine($"[Notificación] Nuevo Usuario creado: {usuario.Nombre} {usuario.Apellido} - {DateTime.Now}");

        public static Action<usuario> NotificarEliminacion = usuario =>
        Console.WriteLine($"[Notificación] Usuario eliminado: {usuario.Nombre} {usuario.Apellido} - {DateTime.Now}");

        public static Action<usuario> NotificarEdicion = usuario =>
        Console.WriteLine($"[Notificación] Usuario editado: {usuario.Nombre} {usuario.Apellido} - {DateTime.Now}");


        public static Action<usuario> NotificarEdicion = usuario =>
        Console.WriteLine($"[Notificación] Usuario editado: {usuario.Nombre} {usuario.Apellido} - {DateTime.Now}");



    }

}
