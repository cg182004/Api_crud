using System;
using proj_crud.Models;

namespace proj_crud.Delegates
{
    public static class UsuarioDelegates
    {
        // Func para validar usuario
        public static Func<Usuario, bool> ValidarUsuario = usuario =>
            !string.IsNullOrWhiteSpace(usuario.Nombre) &&
            !string.IsNullOrWhiteSpace(usuario.Apellido) &&
            usuario.dni.Length >= 7 &&
            usuario.Contrasena.Length >= 6;

        // Action para notificación
        public static Action<Usuario> NotificarCreacion = usuario =>
            Console.WriteLine($"[Notificación] Usuario creado: {usuario.Nombre} {usuario.Apellido} - {DateTime.Now}");

        // Func para obtener el dominio del correo
        public static Func<Usuario, string> ObtenerDominioCorreo = usuario =>
            usuario.correo.Contains("@") ? usuario.correo.Split('@')[1] : "correo inválido";
    }
}
