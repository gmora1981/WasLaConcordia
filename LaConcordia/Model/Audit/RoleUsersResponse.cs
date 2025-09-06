namespace LaConcordia.Model
{

    // Clase para deserializar la respuesta del API
    public class RoleUsersResponse
    {
        public string RoleId { get; set; } = "";
        public int UserCount { get; set; }
        public List<UserDTO> Users { get; set; } = new();
    }
}
