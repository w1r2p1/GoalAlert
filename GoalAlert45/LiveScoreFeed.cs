namespace GoalAlert
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://api.goalapi.com/v01/XSLF")]
    [System.Xml.Serialization.XmlRootAttribute("livescore-feed", Namespace = "http://api.goalapi.com/v01/XSLF", IsNullable = false)]
    public class livescorefeed
    {

        private livescorefeedMatches matchesField;

        private uint timestampcreatedField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public livescorefeedMatches matches
        {
            get
            {
                return this.matchesField;
            }
            set
            {
                this.matchesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("timestamp-created")]
        public uint timestampcreated
        {
            get
            {
                return this.timestampcreatedField;
            }
            set
            {
                this.timestampcreatedField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://api.goalapi.com/v01/XSLF")]
    public  class livescorefeedMatches
    {

        private livescorefeedMatchesItem[] itemField;

        private int totalcountField;

        private string firstField;

        private string lastField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("item", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public livescorefeedMatchesItem[] item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("total-count")]
        public int totalcount
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string first
        {
            get
            {
                return this.firstField;
            }
            set
            {
                this.firstField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string last
        {
            get
            {
                return this.lastField;
            }
            set
            {
                this.lastField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://api.goalapi.com/v01/XSLF")]
    public  class livescorefeedMatchesItem
    {

        private string scoreField;

        private livescorefeedMatchesItemTeams teamsField;

        private livescorefeedMatchesItemCurrentstate[] currentstateField;

        private livescorefeedMatchesItemDetails[] detailsField;

        private livescorefeedMatchesItemEventsEvent[] eventsField;

        private string contestField;

        private string idField;

        private string statusField;

        private uint timestampstartsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [System.Xml.Serialization.XmlElementAttribute("teams", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public livescorefeedMatchesItemTeams teams
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
        [System.Xml.Serialization.XmlElementAttribute("current-state", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public livescorefeedMatchesItemCurrentstate[] currentstate
        {
            get
            {
                return this.currentstateField;
            }
            set
            {
                this.currentstateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("details", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public livescorefeedMatchesItemDetails[] details
        {
            get
            {
                return this.detailsField;
            }
            set
            {
                this.detailsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("event", typeof(livescorefeedMatchesItemEventsEvent), Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public livescorefeedMatchesItemEventsEvent[] events
        {
            get
            {
                return this.eventsField;
            }
            set
            {
                this.eventsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string contest
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
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://api.goalapi.com/v01/XSLF")]
    public  class livescorefeedMatchesItemTeams
    {

        private livescorefeedMatchesItemTeamsHosts hostsField;

        private livescorefeedMatchesItemTeamsGuests guestsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("hosts", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public livescorefeedMatchesItemTeamsHosts hosts
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
        [System.Xml.Serialization.XmlElementAttribute("guests", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public livescorefeedMatchesItemTeamsGuests guests
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://api.goalapi.com/v01/XSLF")]
    public class livescorefeedMatchesItemTeamsHosts
    {

        private string nameField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
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
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://api.goalapi.com/v01/XSLF")]
    public  class livescorefeedMatchesItemTeamsGuests
    {

        private string nameField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
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
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://api.goalapi.com/v01/XSLF")]
    public  class livescorefeedMatchesItemCurrentstate
    {

        private string periodField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string period
        {
            get
            {
                return this.periodField;
            }
            set
            {
                this.periodField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://api.goalapi.com/v01/XSLF")]
    public  class livescorefeedMatchesItemDetails
    {

        private string fixtureinfoField;

        private livescorefeedMatchesItemDetailsContest[] contestField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("fixture-info", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string fixtureinfo
        {
            get
            {
                return this.fixtureinfoField;
            }
            set
            {
                this.fixtureinfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("contest", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public livescorefeedMatchesItemDetailsContest[] contest
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
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://api.goalapi.com/v01/XSLF")]
    public  class livescorefeedMatchesItemDetailsContest
    {

        private string seasonField;

        private livescorefeedMatchesItemDetailsContestCompetition[] competitionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string season
        {
            get
            {
                return this.seasonField;
            }
            set
            {
                this.seasonField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("competition", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public livescorefeedMatchesItemDetailsContestCompetition[] competition
        {
            get
            {
                return this.competitionField;
            }
            set
            {
                this.competitionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://api.goalapi.com/v01/XSLF")]
    public  class livescorefeedMatchesItemDetailsContestCompetition
    {

        private string titleField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
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
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://api.goalapi.com/v01/XSLF")]
    [System.Xml.Serialization.XmlInclude(typeof(GoalEvent))]
    [System.Xml.Serialization.XmlInclude(typeof(CardEvent))]
    public  class livescorefeedMatchesItemEventsEvent
    {

        private string playerField;

        private string scoreField;

        private string minuteField;

        private string typeField;

        private string teamField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string team
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
    }


    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://api.goalapi.com/v01/XSLF")]
    public class GoalEvent: livescorefeedMatchesItemEventsEvent
    {

    }


    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://api.goalapi.com/v01/XSLF")]
    public class CardEvent : livescorefeedMatchesItemEventsEvent
    {

    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://api.goalapi.com/v01/XSLF")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://api.goalapi.com/v01/XSLF", IsNullable = false)]
    public partial class NewDataSet
    {

        private livescorefeed[] itemsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("livescore-feed")]
        public livescorefeed[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

}