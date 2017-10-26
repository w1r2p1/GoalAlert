using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalAlert
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://beybegi.ru/xmlschema.xsd")]
    [System.Xml.Serialization.XmlRootAttribute("livescore-feed-document", Namespace = "http://beybegi.ru/xmlschema.xsd", IsNullable = false)]
    public class LiveFeed
    {

        private Matcheslist matcheslistField;

        private uint timestamp_createdField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("matches-list")]
        public Matcheslist matcheslist
        {
            get
            {
                return this.matcheslistField;
            }
            set
            {
                this.matcheslistField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint timestamp_created
        {
            get
            {
                return this.timestamp_createdField;
            }
            set
            {
                this.timestamp_createdField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://beybegi.ru/xmlschema.xsd")]
    public class Matcheslist
    {

        private Match matchField;

        private byte totalcountField;

        /// <remarks/>
        public Match match
        {
            get
            {
                return this.matchField;
            }
            set
            {
                this.matchField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("total-count")]
        public byte totalcount
        {
            get
            {
                return this.totalcountField;
            }
            set
            {
                this.totalcountField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://beybegi.ru/xmlschema.xsd")]
    public class Match
    {

        private Contest contestField;

        private Teams teamsField;

        private string scoreField;

        private MatchEvents matcheventsField;

        private string idField;

        private uint timestampstartsField;

        private string statusField;

        /// <remarks/>
        public Contest contest
        {
            get
            {
                return this.contestField;
            }
            set
            {
                this.contestField = value;
            }
        }

        /// <remarks/>
        public Teams teams
        {
            get
            {
                return this.teamsField;
            }
            set
            {
                this.teamsField = value;
            }
        }

        /// <remarks/>
        public string score
        {
            get
            {
                return this.scoreField;
            }
            set
            {
                this.scoreField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("match-events")]
        public MatchEvents matchevents
        {
            get
            {
                return this.matcheventsField;
            }
            set
            {
                this.matcheventsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("timestamp-starts")]
        public uint timestampstarts
        {
            get
            {
                return this.timestampstartsField;
            }
            set
            {
                this.timestampstartsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://beybegi.ru/xmlschema.xsd")]
    public class Contest
    {

        private string valueField;

        private string valueField1;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField1;
            }
            set
            {
                this.valueField1 = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://beybegi.ru/xmlschema.xsd")]
    public class Teams
    {

        private string hostsField;

        private string guestsField;

        /// <remarks/>
        public string hosts
        {
            get
            {
                return this.hostsField;
            }
            set
            {
                this.hostsField = value;
            }
        }

        /// <remarks/>
        public string guests
        {
            get
            {
                return this.guestsField;
            }
            set
            {
                this.guestsField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://beybegi.ru/xmlschema.xsd")]
    public class MatchEvents
    {

        private Event eventField;

        /// <remarks/>
        public Event @event
        {
            get
            {
                return this.eventField;
            }
            set
            {
                this.eventField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://beybegi.ru/xmlschema.xsd")]
    public class Event
    {

        private Team teamField;

        private string playerField;

        private string minuteField;

        private string scoreField;

        private string typeField;

        /// <remarks/>
        public Team team
        {
            get
            {
                return this.teamField;
            }
            set
            {
                this.teamField = value;
            }
        }

        /// <remarks/>
        public string player
        {
            get
            {
                return this.playerField;
            }
            set
            {
                this.playerField = value;
            }
        }

        /// <remarks/>
        public string minute
        {
            get
            {
                return this.minuteField;
            }
            set
            {
                this.minuteField = value;
            }
        }

        /// <remarks/>
        public string score
        {
            get
            {
                return this.scoreField;
            }
            set
            {
                this.scoreField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://beybegi.ru/xmlschema.xsd")]
    public class Team
    {

        private string valueField;

        private string valueField1;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField1;
            }
            set
            {
                this.valueField1 = value;
            }
        }
    }
}
