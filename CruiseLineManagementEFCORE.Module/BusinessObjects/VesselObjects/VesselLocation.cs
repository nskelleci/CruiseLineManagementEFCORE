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
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CruiseLineManagementEFCORE.Module.BusinessObjects.VesselObjects
{
    // Register this entity in your DbContext (usually in the BusinessObjects folder of your project) with the "public DbSet<VesselLocations> VesselLocationss { get; set; }" syntax.
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("Name")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    // You do not need to implement the INotifyPropertyChanged interface - EF Core implements it automatically.
    // (see https://learn.microsoft.com/en-us/ef/core/change-tracking/change-detection#change-tracking-proxies for details).
    public class VesselLocation : BaseObject
    {
        public VesselLocation()
        {
            // In the constructor, initialize collection properties, e.g.: 
            // this.AssociatedEntities = new ObservableCollection<AssociatedEntityObject>();
        }

        public virtual string Name { get; set; }
        public virtual string Description { get; set; } = string.Empty;


        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public virtual Guid VesselID { get; set; }
        public virtual Vessel Vessel { get; set; }

        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public virtual Guid DeckID { get; set; }
        public virtual Deck Deck { get; set; }

        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public virtual Guid VesselSideID { get; set; }
        public virtual VesselSide VesselSide { get; set; }

    }
}