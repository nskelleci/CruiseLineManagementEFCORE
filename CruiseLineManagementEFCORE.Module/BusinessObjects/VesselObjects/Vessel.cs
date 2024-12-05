using CruiseLineManagementEFCORE.Module.BusinessObjects.CruiseObjects;
using CruiseLineManagementEFCORE.Module.BusinessObjects.SeasonObjects;
using CruiseLineManagementEFCORE.Module.BusinessObjects.VesselObjects.CabinObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CruiseLineManagementEFCORE.Module.BusinessObjects.VesselObjects
{
    // Register this entity in your DbContext (usually in the BusinessObjects folder of your project) with the "public DbSet<Vessel> Vessels { get; set; }" syntax.
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("Name")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    // You do not need to implement the INotifyPropertyChanged interface - EF Core implements it automatically.
    // (see https://learn.microsoft.com/en-us/ef/core/change-tracking/change-detection#change-tracking-proxies for details).
    [NavigationItem("Ship Configurations")]
    public class Vessel : BaseObject
    {
        public Vessel()
        {
            // In the constructor, initialize collection properties, e.g.: 
            // this.AssociatedEntities = new ObservableCollection<AssociatedEntityObject>();
        }

        public virtual string Name { get; set; }
        public virtual string IMO { get; set; }

        public virtual string Length { get; set; }


        public virtual ICollection<Deck> Decks { get; set; } = new ObservableCollection<Deck>();
        public virtual ICollection<VesselSide> VesselSides { get; set; } = new ObservableCollection<VesselSide>();
        public virtual ICollection<VesselLocation> VesselLocations { get; set; } = new ObservableCollection<VesselLocation>();
        public virtual ICollection<Cabin> Cabins { get; set; } = new ObservableCollection<Cabin>();
        
        public virtual ICollection<CabinCategory> CabinCategories { get; set; } = new ObservableCollection<CabinCategory>();
        public virtual ICollection<CabinType> CabinTypes{ get; set; } = new ObservableCollection<CabinType>();
        public virtual ICollection<CabinBedType> CabinBedTypes { get; set; } = new ObservableCollection<CabinBedType>();

        public virtual ICollection<ApplicationUser> AssignedUsers { get; set; } = new ObservableCollection<ApplicationUser>();
        public virtual ICollection<SeasonVessel> SeasonVessels { get; set; } = new ObservableCollection<SeasonVessel>();
        //public virtual ICollection<Cruise> Cruises { get; set; } = new ObservableCollection<Cruise>();
        [NotMapped]
        public virtual ICollection<Cruise> Cruises
        {
            get
            {
                return SeasonVessels
                    .SelectMany(sv => sv.Cruises).ToList();
                    
            }
        }

    }
}