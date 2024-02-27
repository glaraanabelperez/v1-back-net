namespace Models
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public string Name { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        public ICollection<UserPermission>? UserPermissions { get; set; } = new List<UserPermission>();

        //public ICollection<User>? Users { get; set; } = new List<User>();

    }
}