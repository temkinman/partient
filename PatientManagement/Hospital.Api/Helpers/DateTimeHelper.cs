namespace Hospital.Api.Helpers;

public class DateTimeHelper
{
    public static DateTime GetDateTimeWithUtc(DateTime birthDate)
    {
        switch (birthDate.Kind)
        {
            case DateTimeKind.Unspecified:
                birthDate = DateTime.SpecifyKind(birthDate, DateTimeKind.Utc);
                break;
            case DateTimeKind.Local:
                birthDate = birthDate.ToUniversalTime();
                break;
        }
        
        return birthDate;
    }
}