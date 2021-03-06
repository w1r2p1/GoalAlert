//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class FixtureEvent
    {
        public int EventID { get; set; }
        public string MatchID { get; set; }
        public int NotificationID { get; set; }
        public string Team { get; set; }
        public string Player { get; set; }
        public string Minute { get; set; }
        public string ScoreLine { get; set; }
        public int IsPushed { get; set; }
        public Nullable<System.DateTime> Timestamped { get; set; }
    
        public virtual Fixture Fixture { get; set; }
        public virtual Notification Notification { get; set; }
    }
}
