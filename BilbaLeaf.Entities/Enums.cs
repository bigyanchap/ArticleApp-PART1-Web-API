//using Nancy.Diagnostics;
//using Nancy.Json;
using Nancy.Json;
using System;
using System.Linq;
namespace BilbaLeaf.Entities
{
    
    public class Enums
    {
        public enum CategoryStatus
        {
            PUBLISHED = 1,
            DRAFT = 2
        }
        public enum ArticleStatus
        {
            PUBLISHED=1,
            DRAFT=2
        }
        public enum Language
        {
            Sanskrit = 10,
            Nepali = 20,
            English = 30,
            Hindi = 40,
            Tamil = 50
        }
        public enum ApplicationType
        {
            JavaScript = 0,
            NativeConfidential = 1
        };
        enum ItemVisibility
        {
            BeforeLogin = 1,
            AfterLogin = 2,
            AfterSubscription = 3
        }

        public enum AccessLevelEnum
        {
            ALL,
            LOGGEDIN,
            SUBSCRIBED
        }

        public enum UserRoles
        {
            ADMINISTRATOR,
            USER,
            STOREOWNER,
            ADVERTISER
        }
        
        public enum DatingEnum
        {
            anytime = 0,
            tomorrow = 1,
            today = 2,
            yesterday = 3,
            onOrAfter = 4,
            on = 5,
            OnOrBefore = 6,
            between = 7,
            nextWeek = 8,
            thisWeek = 9,
            lastWeek = 10,
            nextMonth = 11,
            thisMonth = 12,
            lastMonth = 13,
            nextYear = 14,
            thisYear = 15,
            lastYear = 16
        }
        /* 
       ExportEnum<T>()
       Returns key-value pairs of an Enum:
       for example: 
       [{"key":"Inactive","value":0},{"key":"Active","value":1}]
       */
        public object ExportEnum<T>()
        {
            var type = typeof(T);
            var values = Enum.GetValues(type).Cast<T>();
            var dict = values.ToDictionary(e => e.ToString(), e => Convert.ToInt32(e));
            var json = new JavaScriptSerializer().Serialize(dict);
            return json;
        }
    }
}
