using CruiseLineManagementEFCORE.Module.BusinessObjects.CrewObjects;
using CruiseLineManagementEFCORE.Module.BusinessObjects.VesselObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
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

namespace CruiseLineManagementEFCORE.Module.AppSecurity
{
    // Register this entity in your DbContext (usually in the BusinessObjects folder of your project) with the "public DbSet<Role> Roles { get; set; }" syntax.
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("Name")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    // You do not need to implement the INotifyPropertyChanged interface - EF Core implements it automatically.
    // (see https://learn.microsoft.com/en-us/ef/core/change-tracking/change-detection#change-tracking-proxies for details).
    public class BaseRole : PermissionPolicyRoleBase, IPermissionPolicyRoleWithUsers
    {
        public BaseRole()
        {
            // In the constructor, initialize collection properties, e.g.: 
            // this.AssociatedEntities = new ObservableCollection<AssociatedEntityObject>();
        }


        public virtual IList<BaseUser> Users { get; set; } = new ObservableCollection<BaseUser>();
        IEnumerable<IPermissionPolicyUser> IPermissionPolicyRoleWithUsers.Users
        {
            get { return Users.OfType<IPermissionPolicyUser>(); }
        }
    }
}