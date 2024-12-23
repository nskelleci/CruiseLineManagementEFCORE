using CruiseLineManagementEFCORE.Module.BusinessObjects.CrewObjects;
using CruiseLineManagementEFCORE.Module.BusinessObjects.VesselObjects;
using DevExpress.CodeParser;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CruiseLineManagementEFCORE.Module.BusinessObjects
{
    // Register this entity in your DbContext (usually in the BusinessObjects folder of your project) with the "public DbSet<ExtendedRole> ExtendedRoles { get; set; }" syntax.
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("Name")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    // You do not need to implement the INotifyPropertyChanged interface - EF Core implements it automatically.
    // (see https://learn.microsoft.com/en-us/ef/core/change-tracking/change-detection#change-tracking-proxies for details).
    public class ExtendedRole : PermissionPolicyRole
    {
        public ExtendedRole()
        {
            // In the constructor, initialize collection properties, e.g.: 
            // this.AssociatedEntities = new ObservableCollection<AssociatedEntityObject>();
        }

       public virtual bool IsGlobalRole { get; set; } = false;

        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public virtual Guid VesselID { get; set; }

       [Appearance("VesselVisibility", Criteria = "IsGlobalRole = true and IsAdministrative=true", Visibility = ViewItemVisibility.Hide)]
       public virtual Vessel Vessel { get; set; }

        [Appearance("GlobalUsersVisibility", Criteria = "IsGlobalRole = true", Visibility = ViewItemVisibility.Show)]
        public virtual ICollection<GlobalUser> GlobalUsers { get; set; } = new ObservableCollection<GlobalUser>();

        [Appearance("CrewsVisibility", Criteria = "IsGlobalRole = false and IsAdministrative=false", Visibility = ViewItemVisibility.Show)]
        public virtual ICollection<Crew> Crews { get; set; } = new ObservableCollection<Crew>();

    }
}