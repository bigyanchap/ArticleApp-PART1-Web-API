using BilbaLeaf.Entities;
using BilbaLeaf.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilbaLeaf.Service
{
    public class CommonService : ICommonService
    {

        public Tuple<DateTime?, DateTime?> getDateRange(int DatedOn, DateTime? Fromval, DateTime? Tillval)
        {
            DateTime? From = Fromval;
            DateTime? Till = Tillval;
            switch (DatedOn)
            {
                case (int)Enums.DatingEnum.anytime:
                    From = null;
                    Till = null;
                    break;
                case (int)Enums.DatingEnum.tomorrow:
                    From = DateTime.Now.Date.AddDays(1);
                    Till = DateTime.Now.Date.AddDays(1);
                    break;
                case (int)Enums.DatingEnum.today:
                    From = DateTime.Now.Date;
                    Till = DateTime.Now.Date;
                    break;
                case (int)Enums.DatingEnum.yesterday:
                    From = DateTime.Now.Date.AddDays(-1);
                    Till = DateTime.Now.Date.AddDays(-1);
                    break;
                case (int)Enums.DatingEnum.onOrAfter:
                    Till = null;
                    break;
                case (int)Enums.DatingEnum.on:
                    Till = From;
                    break;
                case (int)Enums.DatingEnum.OnOrBefore:
                    From = null;
                    break;
                case (int)Enums.DatingEnum.between:
                    break;
                case (int)Enums.DatingEnum.nextWeek:
                    From = DateTime.Now.Date.AddDays(7).AddDays(-Convert.ToInt16(DateTime.Now.DayOfWeek));
                    Till = From.Value.AddDays(6);
                    break;
                case (int)Enums.DatingEnum.thisWeek:
                    From = DateTime.Now.Date.AddDays(-Convert.ToInt16(DateTime.Now.DayOfWeek));
                    Till = From.Value.AddDays(6);
                    break;
                case (int)Enums.DatingEnum.lastWeek:
                    From = DateTime.Now.Date.AddDays(-7).AddDays(-Convert.ToInt16(DateTime.Now.DayOfWeek));
                    Till = From.Value.AddDays(6);
                    break;
                case (int)Enums.DatingEnum.nextMonth:
                    From = DateTime.Now.Date.AddMonths(1).AddDays(-DateTime.Now.Day + 1);
                    Till = From.Value.AddMonths(1).AddDays(-1);
                    break;
                case (int)Enums.DatingEnum.thisMonth:
                    From = DateTime.Now.Date.AddDays(-DateTime.Now.Day + 1);
                    Till = From.Value.Date.AddMonths(1).AddDays(-1);
                    break;
                case (int)Enums.DatingEnum.lastMonth:
                    From = DateTime.Now.Date.AddMonths(-1).AddDays(-DateTime.Now.Day + 1);
                    Till = From.Value.AddMonths(1).AddDays(-1);
                    break;
                case (int)Enums.DatingEnum.nextYear:
                    From = DateTime.Now.Date.AddYears(1).AddDays(-DateTime.Now.DayOfYear + 1);
                    Till = From.Value.AddYears(1).AddDays(-1);
                    break;
                case (int)Enums.DatingEnum.thisYear:
                    From = DateTime.Now.Date.AddDays(-DateTime.Now.DayOfYear + 1);
                    Till = From.Value.AddYears(1).AddDays(-1);
                    break;
                case (int)Enums.DatingEnum.lastYear:
                    From = DateTime.Now.Date.AddYears(-1).AddDays(-DateTime.Now.DayOfYear + 1);
                    Till = From.Value.Date.AddYears(1).AddDays(-1);
                    break;
            }

            if (Till.HasValue)
            {
                Till = Till.Value.AddMinutes(24 * 60);
            }

            return new Tuple<DateTime?, DateTime?>(From, Till);
        }

    }
}
