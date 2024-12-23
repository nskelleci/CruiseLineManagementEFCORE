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

namespace CruiseLineManagementEFCORE.Module.BusinessObjects
{
    // Register this entity in your DbContext (usually in the BusinessObjects folder of your project) with the "public DbSet<GlobalUser> GlobalUsers { get; set; }" syntax.
    [DefaultClassOptions]
    public class GlobalUser : ApplicationUser
    {
        public GlobalUser()
        {
            // In the constructor, initialize collection properties, e.g.: 
            // this.AssociatedEntities = new ObservableCollection<AssociatedEntityObject>();
        }

       public virtual ICollection<Vessel> AssignedVessels { get; set; } = new ObservableCollection<Vessel>();
        public virtual ICollection<ExtendedRole> GlobalRoles { get; set; } = new ObservableCollection<ExtendedRole>();
    }
}