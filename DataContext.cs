﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HdHrMonitor
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Server=.;Database=HdHrMonitor;Trusted_Connection=True;TrustServerCertificate=true;");
        }

        public DbSet<Data> Data { get; set; }
    }

    public enum Authorization
    {
        None = 0,
        NotSubscribed,
        Subscribed,
    }

    public enum Protection
    {
        None = 0,
        Unrestricted,
        ProtectedCopyNever,
        ProtectedCopyOnce
    }
    public enum Lock
    {
        None = 0,
        Locked,
    }

    public class Data
    {
        [Key]
        public Guid Id { get; set;  }
        public DateTimeOffset DateTimeUtc { get; set; }
        public int TunerNumber { get; set; }
        public string Channel { get; set; }
        public string ChannelFrequency { get; set; }
        public string ProgramNumber { get; set; }
        public string Authorization { get; set; }
        public Authorization? AuthorizationEnum { get; set;  }
        public string CCIProtection { get; set; }
        public Protection? CCIProtectionEnum { get; set; }
        public string ModulationLock { get; set; }
        public string PCRLock { get; set; }
        public Lock? PCRLockEnum { get; set; }

        //public string SignalStrength { get; set; }
        //public string SignalQuality { get; set; }
        //public string SymbolQuality { get; set; }

        public decimal? SignalStrengthPct { get; set; }
        public decimal? SignalStrengthDbm { get; set; }

        public decimal? SignalQualityPct { get; set; }
        public decimal? SignalQualityDbm { get; set; }

        public decimal? SymbolQualityPct { get; set; }

        public string StreamingRateRaw { get; set; }
        public string ResourceLock { get; set; }
    }
}
