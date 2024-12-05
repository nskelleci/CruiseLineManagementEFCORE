using CruiseLineManagementEFCORE.Module.BusinessObjects.CruiseObjects;
using CruiseLineManagementEFCORE.Module.BusinessObjects.VesselObjects;
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

namespace CruiseLineManagementEFCORE.Module.BusinessObjects.SeasonObjects
{
    // Register this entity in your DbContext (usually in the BusinessObjects folder of your project) with the "public DbSet<Season> Seasons { get; set; }" syntax.
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("Name")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    // You do not need to implement the INotifyPropertyChanged interface - EF Core implements it automatically.
    // (see https://learn.microsoft.com/en-us/ef/core/change-tracking/change-detection#change-tracking-proxies for details).
    public class Season : BaseObject
    {
        public Season()
        {
            
        }

        
        public override void OnSaving()
        {
            base.OnSaving();
           

            // Tarih validasyonu
            if (StartDate > EndDate)
            {
                throw new InvalidOperationException("StartDate cannot be greater than EndDate.");
            }
        }

        public virtual string Name { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual ICollection<SeasonVessel> SeasonVessels { get; set; } = new ObservableCollection<SeasonVessel>();
        public virtual ICollection<Cruise> Cruises { get; set; } = new ObservableCollection<Cruise>();
    }
}