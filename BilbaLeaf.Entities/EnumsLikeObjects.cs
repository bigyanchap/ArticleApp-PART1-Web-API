using System;
using System.Collections.Generic;
using System.Text;

namespace BilbaLeaf.Entities
{
    /*THESE MODELS ARE NOT ASSOCIATED TO DATABASE.*/
    public class Season
    {
        public int SeasonId { get; set; }
        public string Name { get; set; }
        public int StartMonth { get; set; }
        public int StartDay { get; set; }
        public int EndMonth { get; set; }
        public int EndDay { get; set; }
        public static List<Season> SeasonList
        {
            get
            {
                return new List<Season>
                {
                    new Season{SeasonId=0,Name="any",StartMonth=-1, StartDay=-1,EndMonth=-1,EndDay=-1},
                    new Season{SeasonId=1,Name="Basanta (mid March ~ mid May)",StartMonth=3, StartDay=15,EndMonth=5,EndDay=14}, //[mid-March to mid-May]
                    new Season{SeasonId=2,Name="Grishma (mid May ~ mid July)",StartMonth=5, StartDay=15,EndMonth=7,EndDay=14}, //[mid-May to mid-July]
                    new Season{SeasonId=3,Name="Barsha (mid July ~ mid September)",StartMonth=7, StartDay=15,EndMonth=9,EndDay=14}, // so on
                    new Season{SeasonId=4,Name="Sharad (mid September ~ mid November)",StartMonth=9, StartDay=15,EndMonth=11,EndDay=14},
                    new Season{SeasonId=5,Name="Hemanta (mid November ~ mid January)",StartMonth=11, StartDay=15,EndMonth=1,EndDay=14},
                    new Season{SeasonId=6,Name="Shishir (mid January ~ mid March)",StartMonth=1, StartDay=15,EndMonth=3,EndDay=14}
                };
            }
        }
    }
    public enum SeasonEnum
    {
        any=0,
        basanta=1,
        grishma=2,
        barsha=3,
        sharad=4,
        hemanta=5,
        shishir=6
    }
    public enum TwentyFourHourTimingEnum
    {
        any = 0,
        brahmaMuhurta = 1,
        dawn = 2,
        morning = 3,
        afternoon = 4,
        dusk = 5,
        evening = 6,
        timeToSleep = 7,
        night = 8,
        wholeMorning=9,
        wholeDay=10,
        wholeEvening=11
    }
    public class TwentyFourHourTiming
    {
        public int TwentyFourHourTimingId { get; set; }
        public string Name { get; set; }
        public int StartHourByDefault { get; set; }
        public int StartMinuteByDefault { get; set; }
        public int EndHourByDefault { get; set; }
        public int EndMinuteByDefault { get; set; }
        public static List<TwentyFourHourTiming> TwentyFourHourTimingList
        {
            get
            {
                return new List<TwentyFourHourTiming>
                {
                    new TwentyFourHourTiming{TwentyFourHourTimingId=0,Name="any",StartHourByDefault=-1, StartMinuteByDefault=-1,EndHourByDefault=-1,EndMinuteByDefault=-1},
                    new TwentyFourHourTiming{TwentyFourHourTimingId=1,Name="Brahma Muhurta (3:30-6:00)",StartHourByDefault=3, StartMinuteByDefault=30,EndHourByDefault=6,EndMinuteByDefault=0},
                    new TwentyFourHourTiming{TwentyFourHourTimingId=2,Name="Dawn (6:01-7:00)",StartHourByDefault=6, StartMinuteByDefault=1,EndHourByDefault=7,EndMinuteByDefault=0},
                    new TwentyFourHourTiming{TwentyFourHourTimingId=3,Name="Morning (7:01-12:00)",StartHourByDefault=7, StartMinuteByDefault=1,EndHourByDefault=12,EndMinuteByDefault=0},
                    new TwentyFourHourTiming{TwentyFourHourTimingId=4,Name="Afternoon (12:01-17:00)",StartHourByDefault=12, StartMinuteByDefault=1,EndHourByDefault=17,EndMinuteByDefault=0},
                    new TwentyFourHourTiming{TwentyFourHourTimingId=5,Name="Dusk (17:01-18:30)",StartHourByDefault=17, StartMinuteByDefault=1,EndHourByDefault=18,EndMinuteByDefault=30},
                    new TwentyFourHourTiming{TwentyFourHourTimingId=6,Name="Evening (18:31-21:00)",StartHourByDefault=18, StartMinuteByDefault=31,EndHourByDefault=21,EndMinuteByDefault=00},
                    new TwentyFourHourTiming{TwentyFourHourTimingId=7,Name="Time To Sleep (21:01-23:59)",StartHourByDefault=21, StartMinuteByDefault=1,EndHourByDefault=23,EndMinuteByDefault=59},
                    new TwentyFourHourTiming{TwentyFourHourTimingId=8,Name="Deep Night (0:00-3:29)",StartHourByDefault=0, StartMinuteByDefault=0,EndHourByDefault=3,EndMinuteByDefault=29},
                    new TwentyFourHourTiming{TwentyFourHourTimingId=9,Name="Whole Morning (3:30-12:00)",StartHourByDefault=3, StartMinuteByDefault=30,EndHourByDefault=12,EndMinuteByDefault=0},
                    new TwentyFourHourTiming{TwentyFourHourTimingId=10,Name="Whole Day (12:01-18:00)",StartHourByDefault=12, StartMinuteByDefault=1,EndHourByDefault=18,EndMinuteByDefault=0},
                    new TwentyFourHourTiming{TwentyFourHourTimingId=11,Name="Whole Evening (18:01-22:00)",StartHourByDefault=18, StartMinuteByDefault=1,EndHourByDefault=22,EndMinuteByDefault=0},
                };
            }
        }
    }

}