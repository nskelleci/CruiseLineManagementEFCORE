using System.Collections.ObjectModel;
using System.ComponentModel;
using CruiseLineManagementEFCORE.Module.AppSecurity;
using CruiseLineManagementEFCORE.Module.BusinessObjects.VesselObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Xpo;

namespace CruiseLineManagementEFCORE.Module.BusinessObjects;

[DefaultProperty(nameof(UserName))]
public class ApplicationUser : PermissionPolicyUser, ISecurityUserWithLoginInfo, ISecurityUserLockout {
    [Browsable(false)]
    public virtual int AccessFailedCount { get; set; }

    [Browsable(false)]
    public virtual DateTime LockoutEnd { get; set; }

    [Browsable(false)]
    [DevExpress.ExpressApp.DC.Aggregated]
    public virtual IList<ApplicationUserLoginInfo> UserLogins { get; set; } = new ObservableCollection<ApplicationUserLoginInfo>();
    public ApplicationUser()
    {
      
    }
    
    private UserType userType;
    public virtual UserType UserType
    {
        get; set;
    }
    //public virtual ICollection<Vessel> AssignedVessels{get; set;} = new ObservableCollection<Vessel>();

    IEnumerable<ISecurityUserLoginInfo> IOAuthSecurityUser.UserLogins => UserLogins.OfType<ISecurityUserLoginInfo>();

    ISecurityUserLoginInfo ISecurityUserWithLoginInfo.CreateUserLoginInfo(string loginProviderName, string providerUserKey) {
        ApplicationUserLoginInfo result = ((IObjectSpaceLink)this).ObjectSpace.CreateObject<ApplicationUserLoginInfo>();
        result.LoginProviderName = loginProviderName;
        result.ProviderUserKey = providerUserKey;
        result.User = this;
        return result;
    }
}
