﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GoalAlertEntities : DbContext
    {
        public GoalAlertEntities()
            : base("name=GoalAlertEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<EventLog> EventLogs { get; set; }
        public virtual DbSet<Fixture> Fixtures { get; set; }
        public virtual DbSet<KeyDB> KeyDBs { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<PastFixture> PastFixtures { get; set; }
        public virtual DbSet<Tolerant> Tolerants { get; set; }
        public virtual DbSet<UserDB> UserDBs { get; set; }
        public virtual DbSet<League> Leagues { get; set; }
        public virtual DbSet<FixtureEvent> FixtureEvents { get; set; }
        public virtual DbSet<PastFixtureEvent> PastFixtureEvents { get; set; }
    }
}
