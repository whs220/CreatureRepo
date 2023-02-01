// Jake Wardell
// 2/1/2023
// Class: Setting
// Stores setting data

using System.Globalization;

namespace HW1_RandomStory
{
    internal class Setting
    {
        // ==== FEILDS ====

        private string strLocation;
        private string strWeather;


        // ==== PROPERTIES ====

        /// <summary>
        /// Returns the location so accessible in the main method
        /// </summary>
        public string Location
        {
            get
            {
                return strLocation;
            }
        }

        /// <summary>
        /// Returns the season
        /// </summary>
        public string Weather
        {
            get
            {
                return strWeather;
            }
        }


        // ==== Constructor ====
        public Setting(string location, string weather)
        {
            this.strLocation = location;
            this.strWeather = weather;

        }
    }
}
