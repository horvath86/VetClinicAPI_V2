using VetClinicAPI_V2.Enums;

namespace VetClinicAPI_V2.Params
{
    public class UserQueryParameters
    {
        public RoleEnum? Role { get; set; } = null;
        public bool? includeDeleted { get; set; } = false;

        public string? search { get; set; } = null;
    }

    
}
