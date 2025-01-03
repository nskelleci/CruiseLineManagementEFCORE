using CruiseLineManagementEFCORE.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.Security;
using DevExpress.Persistent.BaseImpl.EF;
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
    // Register this entity in your DbContext (usually in the BusinessObjects folder of your project) with the "public DbSet<BaseUser> BaseUsers { get; set; }" syntax.
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("Name")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    // You do not need to implement the INotifyPropertyChanged interface - EF Core implements it automatically.
    // (see https://learn.microsoft.com/en-us/ef/core/change-tracking/change-detection#change-tracking-proxies for details).
    public class BaseUser : BaseUserEntity,
        ISecurityUser,
        IAuthenticationStandardUser,
        ISecurityUserWithRoles,
        IPermissionPolicyUser,
        ISecurityUserWithLoginInfo,
        ISecurityUserLockout
    {
        public BaseUser()
        {
            // In the constructor, initialize collection properties, e.g.: 
            // this.AssociatedEntities = new ObservableCollection<AssociatedEntityObject>();
        }

        public virtual UserType UserType
        {
            get; set;
        }


        public virtual bool IsActive { get; set; } = true;
        [RuleRequiredField("EmployeeUserNameRequired", DefaultContexts.Save)]
        [RuleUniqueValue("EmployeeUserNameIsUnique", DefaultContexts.Save,
            "The login with the entered user name was already registered within the system.")]
        public virtual string UserName { get; set; }
        public virtual bool ChangePasswordOnFirstLogon { get; set; }
        [Browsable(false), FieldSize(FieldSizeAttribute.Unlimited), SecurityBrowsable]
        public virtual string StoredPassword { get; set; }
        public bool ComparePassword(string password)
        {
            return PasswordCryptographer.VerifyHashedPasswordDelegate(StoredPassword, password);
        }
        public void SetPassword(string password)
        {
            StoredPassword = PasswordCryptographer.HashPasswordDelegate(password);
        }

        public ISecurityUserLoginInfo CreateUserLoginInfo(string loginProviderName, string providerUserKey)
        {
            throw new NotImplementedException();
        }

        IList<ISecurityRole> ISecurityUserWithRoles.Roles
        {
            get
            {
                IList<ISecurityRole> result = new List<ISecurityRole>();
                foreach (BaseRole role in Roles)
                {
                    result.Add(role);
                }
                return result;
            }
        }
        [RuleRequiredField("EmployeeRoleIsRequired", DefaultContexts.Save,
            TargetCriteria = "IsActive",
            CustomMessageTemplate = "An active employee must have at least one role assigned")]
        public virtual IList<BaseRole> Roles { get; set; } = new ObservableCollection<BaseRole>();

        IEnumerable<IPermissionPolicyRole> IPermissionPolicyUser.Roles
        {
            get { return Roles.OfType<IPermissionPolicyRole>(); }
        }

        [Browsable(false)]
        [Aggregated]
        public virtual IList<BaseUserLoginInfo> BaseUserLogins { get; set; } = new ObservableCollection<BaseUserLoginInfo>();

        IEnumerable<ISecurityUserLoginInfo> IOAuthSecurityUser.UserLogins => BaseUserLogins.OfType<ISecurityUserLoginInfo>();

        [Browsable(false)]
        public virtual int AccessFailedCount { get; set; }

        [Browsable(false)]
        public virtual DateTime LockoutEnd { get; set; }
        ISecurityUserLoginInfo ISecurityUserWithLoginInfo.CreateUserLoginInfo(string loginProviderName, string providerUserKey)
        {
            BaseUserLoginInfo result = ((IObjectSpaceLink)this).ObjectSpace.CreateObject<BaseUserLoginInfo>();
            result.LoginProviderName = loginProviderName;
            result.ProviderUserKey = providerUserKey;
            result.User = this;
            return result;
        }
    }

    public class BaseUserEntity : BaseObject
    {

    }
}