using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRueBalance.Repositories.Dates
{ 
    
    public class DateManageRepository : IDateManageRepository
    {
        public DateTime GetCurrentDate()
        {
            DateTime timeUtc = DateTime.UtcNow;
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
        }
    }
}
