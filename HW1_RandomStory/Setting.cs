

using System.Globalization;

namespace HW1_RandomStory
{
    internal class Setting
    {
        // ==== FEILDS ====

        private string strLocation;
        private string strSeason;


        // ==== PROPERTIES ====

        /// <summary>
        /// Returns the location so accessible in the main method
        /// </summary>
        public string StrLocation
        {
            get
            {
                return strLocation;
            }
        }

        /// <summary>
        /// Returns the season
        /// </summary>
        public string StrSeason
        {
            get
            {
                return strSeason;
            }
        }


        // ==== Constructor ====
        public Setting(string location, string season)
        {
            this.strLocation = location;
            this.strSeason = season;

        }
    }
}
