using CruiseLineManagementEFCORE.Module.BusinessObjects.PassengerObjects;
using CruiseLineManagementEFCORE.Module.BusinessObjects.SeasonObjects;
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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CruiseLineManagementEFCORE.Module.BusinessObjects.CruiseObjects
{
    // Register this entity in your DbContext (usually in the BusinessObjects folder of your project) with the "public DbSet<Cruise> Cruises { get; set; }" syntax.
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("Name")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    // You do not need to implement the INotifyPropertyChanged interface - EF Core implements it automatically.
    // (see https://learn.microsoft.com/en-us/ef/core/change-tracking/change-detection#change-tracking-proxies for details).
    public class Cruise : BaseObject
    {
        public Cruise()
        {
            // In the constructor, initialize collection properties, e.g.: 
            // this.AssociatedEntities = new ObservableCollection<AssociatedEntityObject>();
        }

        public virtual string Name { get; set; }

        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        public virtual Guid SeasonVesselID { get; set; }

        [VisibleInListView(true)]
        [VisibleInDetailView(true)]
        [DisplayName("Season | Vessel")]
        [VisibleInLookupListView(true)]
        public virtual SeasonVessel SeasonVessel { get; set; }
     
        public virtual DateTime StartDateTime
        {
            get;set;
        }

        public virtual DateTime EndDateTime
        {
            get; set;

        }





        public virtual ICollection<CruisePassenger> CruisePassengers { get; set; } = new ObservableCollection<CruisePassenger>();
        public virtual ICollection<ItineraryDay> ItineraryDays { get; set; } = new ObservableCollection<ItineraryDay>();


        //[NotMapped]
        //public virtual string VesselAndSeasonName
        //{
        //    get
        //    {
        //        if (SeasonVessel == null)
        //        {
        //            return "";
        //        }
        //        return SeasonVessel.Season.Name +" | "+ SeasonVessel.Vessel.Name;
        //    }
        //}
    }
}