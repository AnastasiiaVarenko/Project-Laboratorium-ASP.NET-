namespace Laboratorium_3.Models
{
    public class CurrentDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
