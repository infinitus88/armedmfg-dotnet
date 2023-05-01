using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmedMFG.ApplicationCore.Entities;
public class Calendar : BaseEntity
{
    public DateTime Date { get; set; }
    public byte WeekDay { get; set; }
    public bool IsHoliday { get; set; }
    public bool IsWeekend { get; set; }

    public Calendar(DateTime date, byte weekDay, bool isHoliday, bool isWeekend)
    {
        Date = date;
        WeekDay = weekDay;
        IsHoliday = IsHoliday;
        IsWeekend = isWeekend;
    }
}
