using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1_RandomStory
{
    internal class Actor
    {
        // fields
        private string name;
        private string pronoun1;
        private string pronoun2;
        private string pronoun3;
        private string description;
        private string occupation;

        /// <summary>
        /// Get for name
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// Get for pronoun1
        /// </summary>
        public string Pronoun1
        {
            get
            {
                return pronoun1;
            }
        }

        /// <summary>
        /// Get for pronoun2
        /// </summary>
        public string Pronoun2
        {
            get
            {
                return pronoun2;
            }
        }

        /// <summary>
        /// Get for pronoun3
        /// </summary>
        public string Pronoun3
        {
            get
            {
                return pronoun3;
            }
        }

        /// <summary>
        /// Get for description
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
        }

        /// <summary>
        /// Get for occupation
        /// </summary>
        public string Occupation
        {
            get
            {
                return occupation;
            }
        }


        // constructor
        /// <summary>
        /// constructor for Actor class
        /// </summary>
        /// <param name="name">name of actor</param>
        /// <param name="pronoun1">first pronoun</param>
        /// <param name="pronoun2">second pronoun</param>
        /// <param name="pronoun3">third pronoun</param>
        /// <param name="description">description of actor</param>
        /// <param name="occupation">occupation of actor</param>
        public Actor(string name, string pronoun1, string pronoun2, string pronoun3, string description, string occupation)
        {
            this.name = name;
            this.pronoun1 = pronoun1;
            this.pronoun2 = pronoun2;
            this.pronoun3 = pronoun3;
            this.description = description;
            this.occupation = occupation;
        }
    }
}
