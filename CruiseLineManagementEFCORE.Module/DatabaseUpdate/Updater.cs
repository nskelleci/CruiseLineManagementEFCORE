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
using DevExpress.Xpo;
using CruiseLineManagementEFCORE.Module.BusinessObjects.CrewObjects;
using CruiseLineManagementEFCORE.Module.AppSecurity;

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
        
        ObjectSpace.CommitChanges(); //This line persists created object(s).

        UserManager userManager = ObjectSpace.ServiceProvider.GetRequiredService<UserManager>();
        // If a user named 'User' doesn't exist in the database, create this user
        if(userManager.FindUserByName<GlobalUser>(ObjectSpace, "GlobalUser") == null) {
            // Set a password if the standard authentication type is used
            string EmptyPassword = "";
            _ = userManager.CreateUser<GlobalUser>(ObjectSpace, "GlobalUser", EmptyPassword, (user) => {
                // Add the Users role to the user
                user.Roles.Add(defaultRole);
                user.UserType = UserType.GlobalUser;
            });
        }

        // If a user named 'Admin' doesn't exist in the database, create this user
        if(userManager.FindUserByName<BaseUser>(ObjectSpace, "SuperAdmin") == null) {
            // Set a password if the standard authentication type is used
            string EmptyPassword = "";
            _ = userManager.CreateUser<BaseUser>(ObjectSpace, "SuperAdmin", EmptyPassword, (user) => {
                // Add the Administrators role to the user
                user.Roles.Add(adminRole);
                user.UserType = UserType.SuperAdmin;
            });
        }
        createRoles();
        ObjectSpace.CommitChanges(); //This line persists created object(s).
#endif
    }
    public override void UpdateDatabaseBeforeUpdateSchema() {
        base.UpdateDatabaseBeforeUpdateSchema();
    }
    private BaseRole CreateAdminRole() {
        BaseRole adminRole = ObjectSpace.FirstOrDefault<BaseRole>(r => r.Name == "GlobalAdministrators");
        if(adminRole == null) {
            adminRole = ObjectSpace.CreateObject<BaseRole>();
            adminRole.Name = "GlobalAdministrators";
            adminRole.IsAdministrative = true;

        }
        return adminRole;
    }
    private BaseRole CreateDefaultRole() {
        BaseRole defaultRole = ObjectSpace.FirstOrDefault<BaseRole>(role => role.Name == "DefaultGlobalRole");
        if(defaultRole == null) {
            defaultRole = ObjectSpace.CreateObject<BaseRole>();
            defaultRole.Name = "DefaultGlobalRole";

            defaultRole.AddObjectPermissionFromLambda<GlobalUser>(SecurityOperations.Read, cm => cm.ID == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
            defaultRole.AddMemberPermissionFromLambda<GlobalUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", cm => cm.ID == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            defaultRole.AddMemberPermissionFromLambda<GlobalUser>(SecurityOperations.Write, "StoredPassword", cm => cm.ID == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            defaultRole.AddTypePermissionsRecursively<BaseRole>(SecurityOperations.Read, SecurityPermissionState.Deny);
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

  
    private CrewRole CreateDefaultVesselRole(Vessel vessel)
    {
        var roleexists = vessel.CrewRoles.FirstOrDefault(r => r.Name == vessel.Name + "DefaultCrewRole");
        if (roleexists==null)
        {
            var role = ObjectSpace.CreateObject<CrewRole>();
            role.Name = vessel.Name+ "DefaultCrewRole";
            role.Vessel = vessel;

            //ApplicationUser
            role.AddObjectPermissionFromLambda<Crew>(SecurityOperations.Read, au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            //role.AddObjectPermissionFromLambda<Crew>(SecurityOperations.Read, au => au.VesselID == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
          
            //VesselObjects
            role.AddObjectPermissionFromLambda<Vessel>(SecurityOperations.Read, d => d.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<VesselSide>(SecurityOperations.Read, d => d.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<VesselLocation>(SecurityOperations.Read, d => d.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<Deck>(SecurityOperations.Read, d => d.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            //VesselSafetyObjects
            role.AddObjectPermissionFromLambda<MusterStation>(SecurityOperations.Read, d => d.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<SurvivalCraft>(SecurityOperations.Read, d => d.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<SurvivalCraftType>(SecurityOperations.Read, d => d.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            ////CabinObjects
            role.AddObjectPermissionFromLambda<Cabin>(SecurityOperations.Read, d => d.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<CabinCategory>(SecurityOperations.Read, d => d.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<CabinBedType>(SecurityOperations.Read, d => d.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<CabinType>(SecurityOperations.Read, d => d.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            //SeasonObjects
            role.AddObjectPermissionFromLambda<Season>(SecurityOperations.Read, d => d.SeasonVessels.Any(sv => sv.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId())), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<SeasonVessel>(SecurityOperations.Read, d => d.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);

            //CruiseObjects
            role.AddObjectPermissionFromLambda<Cruise>(SecurityOperations.Read, d => d.SeasonVessel.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<ItineraryDay>(SecurityOperations.Read, d => d.Cruise.SeasonVessel.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);

            //PassengerObjects
            role.AddObjectPermissionFromLambda<CruisePassenger>(SecurityOperations.Read, d => d.Cruise.SeasonVessel.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<Passenger>(SecurityOperations.Read, d => d.PastCruises.Any(cp => cp.Cruise.SeasonVessel.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId())), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<PassengerFolio>(SecurityOperations.Read, d => d.CruisePassenger.Cruise.SeasonVessel.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);


            //SalesObjects
            role.AddObjectPermissionFromLambda<Transaction>(SecurityOperations.Read, d => d.PassengerFolio.CruisePassenger.Cruise.SeasonVessel.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            
            
            
            return role;

        }

        return roleexists;          
        
    }

    private CrewRole CreateSYSADMINRole(Vessel vessel)
    {
        
        var roleexists = vessel.CrewRoles.FirstOrDefault(r => r.Name ==vessel.Name+ "SysAdminRole");
        if (roleexists == null)
        {
            var role = ObjectSpace.CreateObject<CrewRole>();
            role.Name = vessel.Name +"SysAdminRole";
            role.Vessel = vessel;

            //ApplicationUser
            role.AddObjectPermissionFromLambda<Crew>(SecurityOperations.FullObjectAccess, au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<Crew>(SecurityOperations.ReadWriteAccess, au => au.Vessel.Crews.Any(c=> c.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);

            //VesselObjects
            role.AddObjectPermissionFromLambda<Vessel>(SecurityOperations.ReadWriteAccess, d => d.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<VesselSide>(SecurityOperations.ReadWriteAccess, d => d.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<VesselLocation>(SecurityOperations.ReadWriteAccess, d => d.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<Deck>(SecurityOperations.ReadWriteAccess, d => d.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            //VesselSafetyObjects
            role.AddObjectPermissionFromLambda<MusterStation>(SecurityOperations.ReadWriteAccess, d => d.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<SurvivalCraft>(SecurityOperations.ReadWriteAccess, d => d.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<SurvivalCraftType>(SecurityOperations.ReadWriteAccess, d => d.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            ////CabinObjects
            role.AddObjectPermissionFromLambda<Cabin>(SecurityOperations.ReadWriteAccess, d => d.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<CabinCategory>(SecurityOperations.ReadWriteAccess, d => d.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<CabinBedType>(SecurityOperations.ReadWriteAccess, d => d.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<CabinType>(SecurityOperations.ReadWriteAccess, d => d.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            //SeasonObjects
            role.AddObjectPermissionFromLambda<Season>(SecurityOperations.ReadWriteAccess, d => d.SeasonVessels.Any(sv => sv.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId())), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<SeasonVessel>(SecurityOperations.ReadWriteAccess, d => d.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);

            //CruiseObjects
            role.AddObjectPermissionFromLambda<Cruise>(SecurityOperations.ReadWriteAccess, d => d.SeasonVessel.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<ItineraryDay>(SecurityOperations.ReadWriteAccess, d => d.Cruise.SeasonVessel.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);

            //PassengerObjects
            role.AddObjectPermissionFromLambda<CruisePassenger>(SecurityOperations.ReadWriteAccess, d => d.Cruise.SeasonVessel.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<Passenger>(SecurityOperations.ReadWriteAccess, d => d.PastCruises.Any(cp => cp.Cruise.SeasonVessel.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId())), SecurityPermissionState.Allow);
            role.AddObjectPermissionFromLambda<PassengerFolio>(SecurityOperations.ReadWriteAccess, d => d.CruisePassenger.Cruise.SeasonVessel.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);


            //SalesObjects
            role.AddObjectPermissionFromLambda<Transaction>(SecurityOperations.ReadWriteAccess, d => d.PassengerFolio.CruisePassenger.Cruise.SeasonVessel.Vessel.Crews.Any(au => au.ID == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);



            return role;

        }

        return roleexists;

    }

    private void createRoles()
    {
        var vessels = ObjectSpace.GetObjects<Vessel>();
        foreach (var vessel in vessels)
        {
            CreateDefaultVesselRole(vessel);
            CreateSYSADMINRole(vessel);
            ObjectSpace.CommitChanges();
        }
    }
 }
