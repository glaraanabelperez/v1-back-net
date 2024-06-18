

namespace api.abrazos.Validators
{
    public static class Validations
    {
        public static bool IdisNOtNull(int? id = null, string? idFirebase = null)
        {
            if (id == null && idFirebase!=null && (!idFirebase.Any() || idFirebase.Equals("")))
            {
                return false;
            }
            return true;
        }
    }
}
