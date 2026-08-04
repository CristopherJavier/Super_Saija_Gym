using System.Security.Cryptography;

namespace Proyecto_Gym_ProgramacionIII_Jeovanny.Seguridad
{
    public static class PasswordHelper
    {
        private const int TamanoSal = 16;
        private const int TamanoHash = 32;
        private const int CantidadIteraciones = 100_000;

        public static void CrearHash(
            string contrasena,
            out string contrasenaHash,
            out string contrasenaSalt)
        {
            if (string.IsNullOrWhiteSpace(contrasena))
            {
                throw new ArgumentException("La contraseña debe tener contenido.", nameof(contrasena));
            }

            byte[] salt = RandomNumberGenerator.GetBytes(TamanoSal);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                contrasena,
                salt,
                CantidadIteraciones,
                HashAlgorithmName.SHA256,
                TamanoHash);

            contrasenaHash = Convert.ToBase64String(hash);
            contrasenaSalt = Convert.ToBase64String(salt);
        }

        public static bool VerificarContrasena(
            string contrasena,
            string contrasenaHash,
            string contrasenaSalt)
        {
            if (string.IsNullOrWhiteSpace(contrasena) ||
                string.IsNullOrWhiteSpace(contrasenaHash) ||
                string.IsNullOrWhiteSpace(contrasenaSalt))
            {
                return false;
            }

            try
            {
                byte[] hashGuardado = Convert.FromBase64String(contrasenaHash);
                byte[] saltGuardado = Convert.FromBase64String(contrasenaSalt);
                byte[] hashCalculado = Rfc2898DeriveBytes.Pbkdf2(
                    contrasena,
                    saltGuardado,
                    CantidadIteraciones,
                    HashAlgorithmName.SHA256,
                    TamanoHash);

                return CryptographicOperations.FixedTimeEquals(hashCalculado, hashGuardado);
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
