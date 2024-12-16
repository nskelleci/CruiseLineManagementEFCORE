using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.EF;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Persistent.BaseImpl.EFCore.AuditTrail;
using CruiseLineManagementEFCORE.Module.BusinessObjects;
using Microsoft.Extensions.DependencyInjection;
using CruiseLineManagementEFCORE.Module.BusinessObjects.VesselObjects;
using System.Security.AccessControl;
using System.Linq;
using CruiseLineManagementEFCORE.Module.BusinessObjects.CruiseObjects;
using CruiseLineManagementEFCORE.Module.BusinessObjects.SeasonObjects;
using System.Reflection;
using CruiseLineManagementEFCORE.Module.BusinessObjects.CruisePortObjects;
using CruiseLineManagementEFCORE.Module.BusinessObjects.PassengerObjects;
using CruiseLineManagementEFCORE.Module.BusinessObjects.SalesObjects;
using CruiseLineManagementEFCORE.Module.BusinessObjects.VesselObjects.CabinObjects;
using CruiseLineManagementEFCORE.Module.BusinessObjects.VesselObjects.VesselSafetyObjects;

namespace CruiseLineManagementEFCORE.Module.DatabaseUpdate;
//https://localhost:44318/Vessel_DetailView/c9cbc4d1-666f-4d3b-ec47-08dd1304055e
// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
public class Updater : ModuleUpdater {
    public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
        base(objectSpace, currentDBVersion) {
    }
    public override void UpdateDatabaseAfterUpdateSchema() {
        base.UpdateDatabaseAfterUpdateSchema();
       
        // The code below creates users and roles for testing purposes only.
        // In production code, you can create users and assign roles to them automatically, as described in the following help topic:
        // https://docs.devexpress.com/eXpressAppFramework/119064/data-security-and-safety/security-system/authentication
#if !RELEASE
        // If a role doesn't exist in the database, create this role
        var defaultRole = CreateDefaultRole();
        var adminRole = CreateAdminRole();
        CreateVesselUserRole();
        CreateVesselAdminRole();
        ObjectSpace.CommitChanges(); //This line persists created object(s).

        UserManager userManager = ObjectSpace.ServiceProvider.GetRequiredService<UserManager>();
        // If a user named 'User' doesn't exist in the database, create this user
        if(userManager.FindUserByName<ApplicationUser>(ObjectSpace, "User") == null) {
            // Set a password if the standard authentication type is used
            string EmptyPassword = "";
            _ = userManager.CreateUser<ApplicationUser>(ObjectSpace, "User", EmptyPassword, (user) => {
                // Add the Users role to the user
                user.Roles.Add(defaultRole);
            });
        }

        // If a user named 'Admin' doesn't exist in the database, create this user
        if(userManager.FindUserByName<ApplicationUser>(ObjectSpace, "Admin") == null) {
            // Set a password if the standard authentication type is used
            string EmptyPassword = "";
            _ = userManager.CreateUser<ApplicationUser>(ObjectSpace, "Admin", EmptyPassword, (user) => {
                // Add the Administrators role to the user
                user.Roles.Add(adminRole);
            });
        }

        ObjectSpace.CommitChanges(); //This line persists created object(s).
#endif
    }
    public override void UpdateDatabaseBeforeUpdateSchema() {
        base.UpdateDatabaseBeforeUpdateSchema();
    }
    private PermissionPolicyRole CreateAdminRole() {
        PermissionPolicyRole adminRole = ObjectSpace.FirstOrDefault<PermissionPolicyRole>(r => r.Name == "Administrators");
        if(adminRole == null) {
            adminRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
            adminRole.Name = "Administrators";
            adminRole.IsAdministrative = true;
        }
        return adminRole;
    }
    private PermissionPolicyRole CreateDefaultRole() {
        PermissionPolicyRole defaultRole = ObjectSpace.FirstOrDefault<PermissionPolicyRole>(role => role.Name == "Default");
        if(defaultRole == null) {
            defaultRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
            defaultRole.Name = "Default";

            defaultRole.AddObjectPermissionFromLambda<ApplicationUser>(SecurityOperations.Read, cm => cm.ID == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
            defaultRole.AddMemberPermissionFromLambda<ApplicationUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", cm => cm.ID == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            defaultRole.AddMemberPermissionFromLambda<ApplicationUser>(SecurityOperations.Write, "StoredPassword", cm => cm.ID == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            defaultRole.AddTypePermissionsRecursively<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Deny);
            defaultRole.AddObjectPermission<ModelDifference>(SecurityOperations.ReadWriteAccess, "UserId = ToStr(CurrentUserId())", SecurityPermissionState.Allow);
            defaultRole.AddObjectPermission<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, "Owner.UserId = ToStr(CurrentUserId())", SecurityPermissionState.Allow);
			defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Create, SecurityPermissionState.Allow);
            defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Create, SecurityPermissionState.Allow);
            defaultRole.AddTypePermission<AuditDataItemPersistent>(SecurityOperations.Read, SecurityPermissionState.Deny);
            defaultRole.AddObjectPermissionFromLambda<AuditDataItemPersistent>(SecurityOperations.Read, a => a.UserObject.Key == CurrentUserIdOperator.CurrentUserId().ToString(), SecurityPermissionState.Allow);
            defaultRole.AddTypePermission<AuditEFCoreWeakReference>(SecurityOperations.Read, SecurityPermissionState.Allow);
        }
        return defaultRole;
    }

  
    private PermissionPolicyRole CreateVesselUserRole()
    {
        var roleexists = ObjectSpace.FirstOrDefault<PermissionPolicyRole>(r => r.Name == "VesselUserRole");
        if (roleexists==null)
        {
            var role = ObjectSpace.CreateObject<PermissionPolicyRole>();
            role.Name = "VesselUserRole";

            //ApplicationUser
            role.AddObjectPermissionFromLambda<ApplicationUser>(SecurityOperations.Read, au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<ApplicationUser>(SecurityOperations.Read, au => au.AssignedVessels.Any(au2 => au2.AssignedUsers.Any(au3 => au3.ID == (Guid)CurrentUserIdOperator.CurrentUserId())), SecurityPermissionState.Allow);

            //VesselObjects
            role.AddObjectPermissionFromLambda<Vessel>(SecurityOperations.Read, d => d.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<VesselSide>(SecurityOperations.Read, d => d.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<VesselLocation>(SecurityOperations.Read, d => d.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<Deck>(SecurityOperations.Read, d => d.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            //VesselSafetyObjects
            role.AddObjectPermissionFromLambda<MusterStation>(SecurityOperations.Read, d => d.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<SurvivalCraft>(SecurityOperations.Read, d => d.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<SurvivalCraftType>(SecurityOperations.Read, d => d.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            ////CabinObjects
            role.AddObjectPermissionFromLambda<Cabin>(SecurityOperations.Read, d => d.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<CabinCategory>(SecurityOperations.Read, d => d.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<CabinBedType>(SecurityOperations.Read, d => d.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<CabinType>(SecurityOperations.Read, d => d.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            //SeasonObjects
            role.AddObjectPermissionFromLambda<Season>(SecurityOperations.Read, d => d.SeasonVessels.Any(sv => sv.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId())), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<SeasonVessel>(SecurityOperations.Read, d => d.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);

            //CruiseObjects
            role.AddObjectPermissionFromLambda<Cruise>(SecurityOperations.Read, d => d.SeasonVessel.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<ItineraryDay>(SecurityOperations.Read, d => d.Cruise.SeasonVessel.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);

            //PassengerObjects
            role.AddObjectPermissionFromLambda<CruisePassenger>(SecurityOperations.Read, d => d.Cruise.SeasonVessel.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<Passenger>(SecurityOperations.Read, d => d.PastCruises.Any(cp => cp.Cruise.SeasonVessel.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId())), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<PassengerFolio>(SecurityOperations.Read, d => d.CruisePassenger.Cruise.SeasonVessel.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);


            //SalesObjects
            role.AddObjectPermissionFromLambda<Transaction>(SecurityOperations.Read, d => d.PassengerFolio.CruisePassenger.Cruise.SeasonVessel.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            
            
            
            return role;

        }

        return roleexists;          
        
    }

    private PermissionPolicyRole CreateVesselAdminRole()
    {
        var roleexists = ObjectSpace.FirstOrDefault<PermissionPolicyRole>(r => r.Name == "VesselAdminRole");
        if (roleexists == null)
        {
            var role = ObjectSpace.CreateObject<PermissionPolicyRole>();
            role.Name = "VesselAdminRole";

            //ApplicationUser
            role.AddObjectPermissionFromLambda<ApplicationUser>(SecurityOperations.ReadWriteAccess, au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<ApplicationUser>(SecurityOperations.ReadWriteAccess, au => au.AssignedVessels.Any(au2 => au2.AssignedUsers.Any(au3 => au3.ID == (Guid)CurrentUserIdOperator.CurrentUserId())), SecurityPermissionState.Allow);

            //VesselObjects
            role.AddObjectPermissionFromLambda<Vessel>(SecurityOperations.ReadWriteAccess, d => d.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<VesselSide>(SecurityOperations.ReadWriteAccess, d => d.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<VesselLocation>(SecurityOperations.ReadWriteAccess, d => d.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<Deck>(SecurityOperations.ReadWriteAccess, d => d.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            //VesselSafetyObjects
            role.AddObjectPermissionFromLambda<MusterStation>(SecurityOperations.ReadWriteAccess, d => d.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<SurvivalCraft>(SecurityOperations.ReadWriteAccess, d => d.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<SurvivalCraftType>(SecurityOperations.ReadWriteAccess, d => d.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            ////CabinObjects
            role.AddObjectPermissionFromLambda<Cabin>(SecurityOperations.ReadWriteAccess, d => d.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<CabinCategory>(SecurityOperations.ReadWriteAccess, d => d.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<CabinBedType>(SecurityOperations.ReadWriteAccess, d => d.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<CabinType>(SecurityOperations.ReadWriteAccess, d => d.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            //SeasonObjects
            role.AddObjectPermissionFromLambda<Season>(SecurityOperations.ReadWriteAccess, d => d.SeasonVessels.Any(sv => sv.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId())), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<SeasonVessel>(SecurityOperations.ReadWriteAccess, d => d.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);

            //CruiseObjects
            role.AddObjectPermissionFromLambda<Cruise>(SecurityOperations.ReadWriteAccess, d => d.SeasonVessel.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<ItineraryDay>(SecurityOperations.ReadWriteAccess, d => d.Cruise.SeasonVessel.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);

            //PassengerObjects
            role.AddObjectPermissionFromLambda<CruisePassenger>(SecurityOperations.ReadWriteAccess, d => d.Cruise.SeasonVessel.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<Passenger>(SecurityOperations.ReadWriteAccess, d => d.PastCruises.Any(cp => cp.Cruise.SeasonVessel.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId())), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<PassengerFolio>(SecurityOperations.ReadWriteAccess, d => d.CruisePassenger.Cruise.SeasonVessel.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);


            //SalesObjects
            role.AddObjectPermissionFromLambda<Transaction>(SecurityOperations.ReadWriteAccess, d => d.PassengerFolio.CruisePassenger.Cruise.SeasonVessel.Vessel.AssignedUsers.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);



            return role;

        }

        return roleexists;

    }

}
