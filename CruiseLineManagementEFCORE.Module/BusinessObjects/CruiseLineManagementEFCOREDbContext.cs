using DevExpress.ExpressApp.EFCore.Updating;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EFCore.DesignTime;
using DevExpress.Persistent.BaseImpl.EFCore.AuditTrail;
using DevExpress.Persistent.BaseImpl.EF.Kpi;
using CruiseLineManagementEFCORE.Module.BusinessObjects.VesselObjects.VesselSafetyObjects;
using CruiseLineManagementEFCORE.Module.BusinessObjects.VesselObjects;
using CruiseLineManagementEFCORE.Module.BusinessObjects.SeasonObjects;
using CruiseLineManagementEFCORE.Module.BusinessObjects.VesselObjects.CabinObjects;
using CruiseLineManagementEFCORE.Module.BusinessObjects.CruiseObjects;
using CruiseLineManagementEFCORE.Module.BusinessObjects.PassengerObjects;
using CruiseLineManagementEFCORE.Module.BusinessObjects.SalesObjects;
using CruiseLineManagementEFCORE.Module.BusinessObjects.CruisePortObjects;
using CruiseLineManagementEFCORE.Module.BusinessObjects.CrewObjects;

namespace CruiseLineManagementEFCORE.Module.BusinessObjects;

// This code allows our Model Editor to get relevant EF Core metadata at design time.
// For details, please refer to https://supportcenter.devexpress.com/ticket/details/t933891/core-prerequisites-for-design-time-model-editor-with-entity-framework-core-data-model.
public class CruiseLineManagementEFCOREContextInitializer : DbContextTypesInfoInitializerBase {
	protected override DbContext CreateDbContext() {
		var optionsBuilder = new DbContextOptionsBuilder<CruiseLineManagementEFCOREEFCoreDbContext>()
            .UseSqlServer(";")//.UseSqlite(";") wrong for a solution with SqLite, see https://isc.devexpress.com/internal/ticket/details/t1240173
            .UseChangeTrackingProxies()
            .UseObjectSpaceLinkProxies();
        return new CruiseLineManagementEFCOREEFCoreDbContext(optionsBuilder.Options);
	}
}
//This factory creates DbContext for design-time services. For example, it is required for database migration.
public class CruiseLineManagementEFCOREDesignTimeDbContextFactory : IDesignTimeDbContextFactory<CruiseLineManagementEFCOREEFCoreDbContext> {
	public CruiseLineManagementEFCOREEFCoreDbContext CreateDbContext(string[] args) {
		//throw new InvalidOperationException("Make sure that the database connection string and connection provider are correct. After that, uncomment the code below and remove this exception.");
        var optionsBuilder = new DbContextOptionsBuilder<CruiseLineManagementEFCOREEFCoreDbContext>();
        optionsBuilder.UseSqlServer("Integrated Security=SSPI;Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CruiseLineManagementEFCORE");
        optionsBuilder.UseChangeTrackingProxies();
        optionsBuilder.UseObjectSpaceLinkProxies();
        return new CruiseLineManagementEFCOREEFCoreDbContext(optionsBuilder.Options);
    }
}
[TypesInfoInitializer(typeof(CruiseLineManagementEFCOREContextInitializer))]
public class CruiseLineManagementEFCOREEFCoreDbContext : DbContext {
	public CruiseLineManagementEFCOREEFCoreDbContext(DbContextOptions<CruiseLineManagementEFCOREEFCoreDbContext> options) : base(options) {
	}
	//public DbSet<ModuleInfo> ModulesInfo { get; set; }
	public DbSet<ModelDifference> ModelDifferences { get; set; }
	public DbSet<ModelDifferenceAspect> ModelDifferenceAspects { get; set; }
	public DbSet<PermissionPolicyRole> Roles { get; set; }
    //public DbSet<VesselRole> VesselSpecificRoles { get; set; }
    //public DbSet<VesselUser> OnBoardUsers { get; set; }
    public DbSet<CruiseLineManagementEFCORE.Module.BusinessObjects.ApplicationUser> Users { get; set; }
    public DbSet<CruiseLineManagementEFCORE.Module.BusinessObjects.ApplicationUserLoginInfo> UserLoginInfos { get; set; }
	public DbSet<FileData> FileData { get; set; }
	public DbSet<ReportDataV2> ReportDataV2 { get; set; }
	public DbSet<KpiDefinition> KpiDefinition { get; set; }
	public DbSet<KpiInstance> KpiInstance { get; set; }
	public DbSet<KpiHistoryItem> KpiHistoryItem { get; set; }
	public DbSet<KpiScorecard> KpiScorecard { get; set; }
	public DbSet<DashboardData> DashboardData { get; set; }
    public DbSet<AuditDataItemPersistent> AuditData { get; set; }
    public DbSet<AuditEFCoreWeakReference> AuditEFCoreWeakReference { get; set; }
    public DbSet<Event> Event { get; set; }
    public DbSet<Analysis> Analysis { get; set; }


    #region BusinessObjects

    public DbSet<Crew> Crews { get; set; }

    public DbSet<GlobalUser> GlobalUsers { get; set; }
    public DbSet<VesselRole> VesselRoles { get; set; }
    public DbSet<Vessel> Vessels { get; set; }
    public DbSet<Deck> Decks { get; set; }
    public DbSet<VesselLocation> VesselLocations { get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<MusterStation> MusterStations { get; set; }
    public DbSet<SurvivalCraft> SurvivalCrafts { get; set; }
    public DbSet<SurvivalCraftType> SurvivalCraftsTypes { get; set; }
    public DbSet<Cabin> Cabins { get; set; }
    public DbSet<CabinBedType> CabinBedTypes { get; set; }
    public DbSet<CabinCategory> CabinCategories { get; set; }
    public DbSet<CabinType> CabinTypes { get; set; }

    public DbSet<SeasonVessel> SeasonVessels { get; set; }
    public DbSet<Cruise> Cruises { get; set; }
    public DbSet<CruisePort> CruisePorts { get; set; }
    public DbSet<CruisePortCity> CruisePortCities { get; set; }
    public DbSet<CruisePortCountry> CruisePortCountries { get; set; }
    public DbSet<CruisePassenger> CruisePassengers { get; set; }
    public DbSet<Passenger> Passengers { get; set; }
    public DbSet<PassengerFolio> PassengerFolios { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public DbSet<VesselSide> VesselSides { get; set; }


    #endregion
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.SetOneToManyAssociationDeleteBehavior(DeleteBehavior.SetNull, DeleteBehavior.Cascade);
        modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotificationsWithOriginalValues);
        modelBuilder.UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);
        modelBuilder.Entity<CruiseLineManagementEFCORE.Module.BusinessObjects.ApplicationUserLoginInfo>(b => {
            b.HasIndex(nameof(DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.LoginProviderName), nameof(DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.ProviderUserKey)).IsUnique();
        });
        modelBuilder.Entity<AuditEFCoreWeakReference>()
            .HasMany(p => p.AuditItems)
            .WithOne(p => p.AuditedObject);
        modelBuilder.Entity<AuditEFCoreWeakReference>()
            .HasMany(p => p.OldItems)
            .WithOne(p => p.OldObject);
        modelBuilder.Entity<AuditEFCoreWeakReference>()
            .HasMany(p => p.NewItems)
            .WithOne(p => p.NewObject);
        modelBuilder.Entity<AuditEFCoreWeakReference>()
            .HasMany(p => p.UserItems)
            .WithOne(p => p.UserObject);
        modelBuilder.Entity<ModelDifference>()
            .HasMany(t => t.Aspects)
            .WithOne(t => t.Owner)
            .OnDelete(DeleteBehavior.Cascade);


        #region BusinessObjects

        modelBuilder.Entity<SeasonVessel>()
            .HasKey(ss => ss.ID);
        modelBuilder.Entity<SeasonVessel>()
            .HasOne(ss => ss.Season);
        modelBuilder.Entity<SeasonVessel>()
            .HasOne(ss => ss.Vessel);

        // SeasonShip relationships
        modelBuilder.Entity<SeasonVessel>()
            .HasOne(ss => ss.Season)
            .WithMany(s => s.SeasonVessels)
            .HasForeignKey(ss => ss.SeasonID);

        modelBuilder.Entity<SeasonVessel>()
            .HasOne(ss => ss.Vessel)
            .WithMany(s => s.SeasonVessels)
            .HasForeignKey(ss => ss.VesselID);

        // Cruise relationships
        modelBuilder.Entity<Cruise>()
            .HasOne(c => c.SeasonVessel)
            .WithMany(ss => ss.Cruises)
            .HasForeignKey(c => new { c.SeasonVesselID });

        modelBuilder.Entity<Cruise>()
            .HasMany(c => c.CruisePassengers)
            .WithOne(cp => cp.Cruise)
            .HasForeignKey(cp => cp.CruiseID)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Cruise>()
            .HasMany(c => c.ItineraryDays)
            .WithOne(id => id.Cruise)
            .HasForeignKey(id => id.CruiseID)
            .OnDelete(DeleteBehavior.NoAction);


        // CruisePassenger primary key
        modelBuilder.Entity<CruisePassenger>()
            .HasKey(cp => new { cp.CruiseID, cp.PassengerID });

        // CruisePassenger relationships
        modelBuilder.Entity<CruisePassenger>()
            .HasKey(cp => cp.ID);
        modelBuilder.Entity<CruisePassenger>()
            .HasOne(cp => cp.Cruise)
            .WithMany(c => c.CruisePassengers)
            .HasForeignKey(cp => cp.CruiseID);

        modelBuilder.Entity<CruisePassenger>()
            .HasOne(cp => cp.Passenger)
            .WithMany(p => p.PastCruises)
            .HasForeignKey(cp => cp.PassengerID);

        // PassengerFolio one-to-one relationship with CruisePassenger
        modelBuilder.Entity<PassengerFolio>()
            .HasKey(pf => pf.ID);

        modelBuilder.Entity<PassengerFolio>()            
            .HasOne(pf => pf.CruisePassenger)
            .WithOne(cp => cp.Folio)
            .HasForeignKey<PassengerFolio>(pf => pf.CruisePassengerID);

        // Transaction relationships
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.PassengerFolio)
            .WithMany(pf => pf.Transactions)
            .HasForeignKey(t => t.PassengerFolioID);


        modelBuilder.Entity<Vessel>()
            .HasMany(v => v.Decks)
            .WithOne(d => d.Vessel)
            .HasForeignKey(d => d.VesselID);
        modelBuilder.Entity<Vessel>()
            .HasMany(v => v.VesselSides)
            .WithOne(vs => vs.Vessel);
        modelBuilder.Entity<Vessel>()
            .HasMany(v => v.VesselLocations)
            .WithOne(vl => vl.Vessel);
        modelBuilder.Entity<Vessel>()
            .HasMany(v => v.Cabins)
            .WithOne(c => c.Vessel);
        modelBuilder.Entity<Vessel>()
            .HasMany(v => v.CabinCategories)
            .WithOne(u => u.Vessel);
        modelBuilder.Entity<Vessel>()
            .HasMany(v => v.CabinTypes)
            .WithOne(u => u.Vessel);
        modelBuilder.Entity<Vessel>()
            .HasMany(v => v.CabinBedTypes)
            .WithOne(u => u.Vessel);

        modelBuilder.Entity<Vessel>()
            .HasMany(v => v.SurvivalCrafts)
            .WithOne(sc => sc.Vessel);
        modelBuilder.Entity<Vessel>()
            .HasMany(v => v.SurvivalCraftTypes)
            .WithOne(sct => sct.Vessel);
        modelBuilder.Entity<Vessel>()
            .HasMany(v => v.MusterStations)
            .WithOne(ms => ms.Vessel);
        modelBuilder.Entity<Vessel>()
            .HasMany(v => v.Crews)
            .WithOne(c => c.Vessel);
        modelBuilder.Entity<Vessel>()
            .HasMany(v => v.GlobalUsers)
            .WithMany(gu => gu.AssignedVessels);
        modelBuilder.Entity<Vessel>()
            .HasMany(v => v.Roles)
            .WithOne(r => r.Vessel);









        modelBuilder.Entity<Crew>()
            .HasOne(c => c.Vessel)
            .WithMany(v => v.Crews)
            .HasForeignKey(c => c.VesselID)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Crew>()
            .HasMany(c => c.VesselRoles)
            .WithMany(vr => vr.CrewMembers);















        modelBuilder.Entity<Deck>()
            .HasMany(d => d.VesselLocations)
            .WithOne(vl => vl.Deck)
            .HasForeignKey(vl => vl.DeckID);

        modelBuilder.Entity<Deck>()
            .HasMany(d => d.Cabins)
            .WithOne(c => c.Deck)
            .HasForeignKey(c => c.DeckID);
        modelBuilder.Entity<Deck>()
            .HasOne(d => d.Vessel)
            .WithMany(v => v.Decks)
            .HasForeignKey(d => d.VesselID);
       

        modelBuilder.Entity<Cabin>()
            .HasOne(c => c.Vessel)
            .WithMany(v => v.Cabins)
            .HasForeignKey(c => c.VesselID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Cabin>()
            .HasOne(c => c.Deck)
            .WithMany(d => d.Cabins)
            .HasForeignKey(c => c.DeckID)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Cabin>()
            .HasOne(c => c.CabinCategory)
            .WithMany(cc => cc.Cabins)
            .HasForeignKey(c => c.CabinCategoryID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Cabin>()
            .HasOne(c => c.CabinType)
            .WithMany(ct => ct.Cabins)
            .HasForeignKey(c => c.CabinTypeID)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Cabin>()
            .HasOne(c => c.CabinBedType)
            .WithMany(cb => cb.Cabins)
            .HasForeignKey(c => c.CabinBedTypeID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Cabin>()
            .HasOne(c => c.VesselSide)
            .WithMany(vs => vs.Cabins)
            .HasForeignKey(c => c.VesselSideID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Cabin>()
            .HasOne(c => c.MusterStation)
            .WithMany(ms => ms.Cabins)
            .HasForeignKey(c => c.MusterStationID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<CabinCategory>()
            .HasOne(cc => cc.Vessel)
            .WithMany(v => v.CabinCategories)
            .HasForeignKey(cc => cc.VesselID);
        modelBuilder.Entity<CabinCategory>()
            .HasMany(cc => cc.Cabins)
            .WithOne(c => c.CabinCategory)
            .HasForeignKey(c => c.CabinCategoryID);

        modelBuilder.Entity<CabinType>()
            .HasOne(ct => ct.Vessel)
            .WithMany(v => v.CabinTypes)
            .HasForeignKey(ct => ct.VesselID);
        modelBuilder.Entity<CabinType>()
            .HasMany(ct => ct.Cabins)
            .WithOne(c => c.CabinType)
            .HasForeignKey(c => c.CabinTypeID);

        modelBuilder.Entity<CabinBedType>()
            .HasOne(cb => cb.Vessel)
            .WithMany(v => v.CabinBedTypes)
            .HasForeignKey(cb => cb.VesselID);
        modelBuilder.Entity<CabinBedType>()
            .HasMany(cb => cb.Cabins)
            .WithOne(c => c.CabinBedType)
            .HasForeignKey(c => c.CabinBedTypeID);

        modelBuilder.Entity<VesselSide>()
            .HasOne(vs => vs.Vessel)
            .WithMany(v => v.VesselSides)
            .HasForeignKey(vs => vs.VesselID);
        modelBuilder.Entity<VesselSide>()
            .HasMany(vs => vs.VesselLocations)
            .WithOne(vl => vl.VesselSide)
            .HasForeignKey(vl => vl.VesselSideID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<VesselSide>()
            .HasMany(vs => vs.Cabins)
            .WithOne(c => c.VesselSide)
            .HasForeignKey(c => c.VesselSideID);

        modelBuilder.Entity <VesselLocation>()
            .HasOne(vl => vl.Vessel)
            .WithMany(v => v.VesselLocations)
            .HasForeignKey(vl => vl.VesselID);
        modelBuilder.Entity<VesselLocation>()
            .HasOne(vl => vl.Deck)
            .WithMany(d => d.VesselLocations)
            .HasForeignKey(vl => vl.DeckID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<VesselLocation>()
            .HasOne(vl => vl.VesselSide)
            .WithMany(vs => vs.VesselLocations)
            .HasForeignKey(vl => vl.VesselSideID)
            .OnDelete(DeleteBehavior.NoAction);



        modelBuilder.Entity<MusterStation>()
            .HasOne(ms => ms.Vessel)
            .WithMany(v => v.MusterStations)
            .HasForeignKey(ms => ms.VesselID)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<MusterStation>()
            .HasMany(ms => ms.SurvivalCrafts)
            .WithOne(sc => sc.MusterStation)
            .HasForeignKey(sc => sc.MusterStationID)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<MusterStation>()
            .HasMany(ms => ms.Cabins)
            .WithOne(sc => sc.MusterStation)
            .HasForeignKey(sc => sc.MusterStationID)
            .OnDelete(DeleteBehavior.NoAction);



        modelBuilder.Entity<SurvivalCraft>()
            .HasOne(sc => sc.MusterStation)
            .WithMany(ms => ms.SurvivalCrafts)
            .HasForeignKey(sc => sc.MusterStationID)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<SurvivalCraft>()
            .HasOne(sc => sc.SurvivalCraftType)
            .WithMany(sct => sct.SurvivalCrafts)
            .HasForeignKey(sc => sc.SurvivalCraftTypeID)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<SurvivalCraft>()
            .HasOne(sc => sc.Vessel)
            .WithMany(v => v.SurvivalCrafts)
            .HasForeignKey(sc => sc.VesselID)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<SurvivalCraft>()
            .HasOne(sc => sc.VesselLocation);
        modelBuilder.Entity<SurvivalCraft>()
            .HasMany(sc => sc.Cabins)
            .WithOne(c => c.SurvivalCraft)
            .HasForeignKey(c => c.SurvivalCraftID)
            .OnDelete(DeleteBehavior.NoAction);


        modelBuilder.Entity<SurvivalCraftType>()
            .HasOne(sct => sct.Vessel)
            .WithMany(v => v.SurvivalCraftTypes)
            .HasForeignKey(sct => sct.VesselID)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<SurvivalCraftType>()
            .HasMany(sct => sct.SurvivalCrafts)
            .WithOne(sc => sc.SurvivalCraftType)
            .HasForeignKey(sc => sc.SurvivalCraftTypeID)
            .OnDelete(DeleteBehavior.NoAction);


        modelBuilder.Entity<CruisePort>()
            .HasOne(cp => cp.CruisePortCity)
            .WithMany(cpc => cpc.CruisePorts)
            .HasForeignKey(cp => cp.CruisePortCityID)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<CruisePort>()
            .HasMany(cp=> cp.ItineraryDays)
            .WithOne(id => id.CruisePort)
            .HasForeignKey(id => id.CruisePortID)
            .OnDelete(DeleteBehavior.NoAction);



        modelBuilder.Entity<CruisePortCity>()
            .HasMany(cpc => cpc.CruisePorts)
            .WithOne(cp => cp.CruisePortCity)
            .HasForeignKey(cp => cp.CruisePortCityID)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<CruisePortCity>()
            .HasOne(cpc => cpc.CruisePortCountry)
            .WithMany(c => c.CruisePortCities)
            .HasForeignKey(cpc => cpc.CruisePortCountryID)
            .OnDelete(DeleteBehavior.NoAction);


        modelBuilder.Entity<CruisePortCountry>()
            .HasMany(cpc => cpc.CruisePortCities)
            .WithOne(c => c.CruisePortCountry)
            .HasForeignKey(cpc => cpc.CruisePortCountryID)
            .OnDelete(DeleteBehavior.NoAction);


        modelBuilder.Entity<ItineraryDay>()
            .HasOne(id => id.Cruise)
            .WithMany(c => c.ItineraryDays)
            .HasForeignKey(id => id.CruiseID)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<ItineraryDay>()
            .HasOne(id => id.CruisePort)
            .WithMany(cp => cp.ItineraryDays)
            .HasForeignKey(id => id.CruisePortID)
            .OnDelete(DeleteBehavior.NoAction);


        #endregion

    }
}

public class CruiseLineManagementEFCOREAuditingDbContext : DbContext {
    public CruiseLineManagementEFCOREAuditingDbContext(DbContextOptions<CruiseLineManagementEFCOREAuditingDbContext> options) : base(options) {
    }
    public DbSet<AuditDataItemPersistent> AuditData { get; set; }
    public DbSet<AuditEFCoreWeakReference> AuditEFCoreWeakReference { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotificationsWithOriginalValues);
        modelBuilder.Entity<AuditEFCoreWeakReference>()
            .HasMany(p => p.AuditItems)
            .WithOne(p => p.AuditedObject);
        modelBuilder.Entity<AuditEFCoreWeakReference>()
            .HasMany(p => p.OldItems)
            .WithOne(p => p.OldObject);
        modelBuilder.Entity<AuditEFCoreWeakReference>()
            .HasMany(p => p.NewItems)
            .WithOne(p => p.NewObject);
        modelBuilder.Entity<AuditEFCoreWeakReference>()
            .HasMany(p => p.UserItems)
            .WithOne(p => p.UserObject);
    }
}
