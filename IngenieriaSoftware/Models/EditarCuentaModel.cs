namespace IngenieriaSoftware.Models
{
    public class EditarCuentaModel
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string CurrentPass { get; set; }
        public string NewPass { get; set; }
    }
}
