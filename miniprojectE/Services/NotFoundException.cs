
namespace miniprojectE.Services
{
    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}