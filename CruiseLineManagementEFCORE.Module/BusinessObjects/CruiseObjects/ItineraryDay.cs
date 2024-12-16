using CruiseLineManagementEFCORE.Module.BusinessObjects.CruisePortObjects;
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

namespace CruiseLineManagementEFCORE.Module.BusinessObjects.CruiseObjects
{
    // Register this entity in your DbContext (usually in the BusinessObjects folder of your project) with the "public DbSet<ItineraryDay> ItineraryDays { get; set; }" syntax.
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("Name")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    // You do not need to implement the INotifyPropertyChanged interface - EF Core implements it automatically.
    // (see https://learn.microsoft.com/en-us/ef/core/change-tracking/change-detection#change-tracking-proxies for details).
    public class ItineraryDay : BaseObject
    {
        public ItineraryDay()
        {
            // In the constructor, initialize collection properties, e.g.: 
            // this.AssociatedEntities = new ObservableCollection<AssociatedEntityObject>();
        }

        public override void OnSaving()
        {
            base.OnSaving();
            
        }

        public virtual int DayNumber { get; set; }


        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public virtual Guid CruiseID { get; set; }
        public virtual Cruise Cruise { get; set; }


        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public virtual Guid CruisePortID { get; set; }
        public virtual CruisePort CruisePort { get; set; }

        [ModelDefault("DisplayFormat", "{0:MM.dd.yyyy hh:mm:ss}")]
        [ModelDefault("EditMask", "MM.dd.yyyy hh:mm:ss")]
        public virtual DateTime ArrivalDateTime
        {
            get; set;
        }

        [ModelDefault("DisplayFormat", "{0:MM.dd.yyyy hh:mm:ss}")]
        [ModelDefault("EditMask", "MM.dd.yyyy hh:mm:ss")]
        public virtual DateTime DepartureDateTime
        {
            get;set;

        }

    }
}