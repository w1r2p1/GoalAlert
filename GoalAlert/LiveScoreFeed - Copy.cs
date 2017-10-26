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
    public class LiveScoreFeed
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

        private Match[] matchField;

        private byte totalcountField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("match")]
        public Match[] match
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

        private Contest scontestField;

        private Teams steamsField;

        private string _scoreField;

        private MatchEvent[] _matcheventsField;

        private string _idField;

        private uint _timestampstartsField;

        private string statusField;

        /// <remarks/>
        public Contest contest
        {
            get
            {
                return this.scontestField;
            }
            set
            {
                this.scontestField = value;
            }
        }

        /// <remarks/>
        public Teams teams
        {
            get
            {
                return this.steamsField;
            }
            set
            {
                this.steamsField = value;
            }
        }

        /// <remarks/>
        public string score
        {
            get
            {
                return this._scoreField;
            }
            set
            {
                this._scoreField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute("match-events", IsNullable =true)]
        [System.Xml.Serialization.XmlArrayItemAttribute("event", IsNullable = false)]
        public MatchEvent[] matchevents
        {
            get
            {
                return this._matcheventsField;
            }
            set
            {
                this._matcheventsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get
            {
                return this._idField;
            }
            set
            {
                this._idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("timestamp-starts")]
        public uint timestampstarts
        {
            get
            {
                return this._timestampstartsField;
            }
            set
            {
                this._timestampstartsField = value;
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

        private string svalueField;

        private string svalueField1;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.svalueField;
            }
            set
            {
                this.svalueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.svalueField1;
            }
            set
            {
                this.svalueField1 = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://beybegi.ru/xmlschema.xsd")]
    public class Teams
    {

        private string shostsField;

        private string sguestsField;

        /// <remarks/>
        public string hosts
        {
            get
            {
                return this.shostsField;
            }
            set
            {
                this.shostsField = value;
            }
        }

        /// <remarks/>
        public string guests
        {
            get
            {
                return this.sguestsField;
            }
            set
            {
                this.sguestsField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://beybegi.ru/xmlschema.xsd")]
    public class MatchEvent
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

        private string svalueField;

        private string svalueField1;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.svalueField;
            }
            set
            {
                this.svalueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.svalueField1;
            }
            set
            {
                this.svalueField1 = value;
            }
        }
    }
}




