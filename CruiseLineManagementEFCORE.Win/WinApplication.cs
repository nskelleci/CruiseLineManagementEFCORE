using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Win.Utils;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.ClientServer;
using Microsoft.EntityFrameworkCore;
using DevExpress.ExpressApp.EFCore;
using DevExpress.EntityFrameworkCore.Security;
using CruiseLineManagementEFCORE.Module;
using CruiseLineManagementEFCORE.Module.BusinessObjects;
using System.Data.Common;
using DevExpress.Persistent.BaseImpl.EFCore.AuditTrail;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.Win.ApplicationBuilder;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;

namespace CruiseLineManagementEFCORE.Win;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Win.WinApplication._members
public class CruiseLineManagementEFCOREWindowsFormsApplication : WinApplication {
    public CruiseLineManagementEFCOREWindowsFormsApplication() {
		SplashScreen = new DXSplashScreen(typeof(XafSplashScreen), new DefaultOverlayFormOptions());
        ApplicationName = "CruiseLineManagementEFCORE";
        CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
        UseOldTemplates = false;
        DatabaseVersionMismatch += CruiseLineManagementEFCOREWindowsFormsApplication_DatabaseVersionMismatch;
        CustomizeLanguagesList += CruiseLineManagementEFCOREWindowsFormsApplication_CustomizeLanguagesList;
    }
    private void CruiseLineManagementEFCOREWindowsFormsApplication_CustomizeLanguagesList(object sender, CustomizeLanguagesListEventArgs e) {
        string userLanguageName = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
        if(userLanguageName != "en-US" && e.Languages.IndexOf(userLanguageName) == -1) {
            e.Languages.Add(userLanguageName);
        }
    }
    private void CruiseLineManagementEFCOREWindowsFormsApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e) {
#if EASYTEST
        e.Updater.Update();
        e.Handled = true;
#else
        if(System.Diagnostics.Debugger.IsAttached) {
            e.Updater.Update();
            e.Handled = true;
        }
        else {
			string message = "The application cannot connect to the specified database, " +
				"because the database doesn't exist, its version is older " +
				"than that of the application or its schema does not match " +
				"the ORM data model structure. To avoid this error, use one " +
				"of the solutions from the https://www.devexpress.com/kb=T367835 KB Article.";

			if(e.CompatibilityError != null && e.CompatibilityError.Exception != null) {
				message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;
			}
			throw new InvalidOperationException(message);
        }
#endif
    }

    public static WinApplication BuildApplication(string connectionString)
    {
        var builder = WinApplication.CreateBuilder();
        builder.UseApplication<CruiseLineManagementEFCOREWindowsFormsApplication>();
        builder.Modules
            .AddConditionalAppearance()
            .AddValidation(options => {
                options.AllowValidationDetailsAccess = false;
            })
            .Add<CruiseLineManagementEFCORE.Module.CruiseLineManagementEFCOREModule>()
            .Add<CruiseLineManagementEFCOREWinModule>();
        builder.ObjectSpaceProviders
            .AddSecuredEFCore().WithDbContext<CruiseLineManagementEFCORE.Module.BusinessObjects.CruiseLineManagementEFCOREEFCoreDbContext>((application, options) => {
                // Uncomment this code to use an in-memory database. This database is recreated each time the server starts. With the in-memory database, you don't need to make a migration when the data model is changed.
                // Do not use this code in production environment to avoid data loss.
                // We recommend that you refer to the following help topic before you use an in-memory database: https://docs.microsoft.com/en-us/ef/core/testing/in-memory
                //options.UseInMemoryDatabase("InMemory");
                options.UseSqlServer(connectionString);
                options.UseChangeTrackingProxies();
                options.UseObjectSpaceLinkProxies();
            })
            .AddNonPersistent();
        builder.Security
            .UseIntegratedMode(options => {
                options.RoleType = typeof(PermissionPolicyRole);
                options.UserType = typeof(CruiseLineManagementEFCORE.Module.BusinessObjects.ApplicationUser);
                options.UserLoginInfoType = typeof(CruiseLineManagementEFCORE.Module.BusinessObjects.ApplicationUserLoginInfo);
                options.Events.OnSecurityStrategyCreated = securityStrategy => {
                    ((SecurityStrategy)securityStrategy).AssociationPermissionsMode = AssociationPermissionsMode.Manual;
                };
            })
            .UsePasswordAuthentication();
        builder.AddBuildStep(application => {
            application.ConnectionString = connectionString;
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached && application.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema)
            {
                application.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
            }
#endif
        });
        var winApplication = builder.Build();
        return winApplication;
    }

    XafApplication IDesignTimeApplicationFactory()
    {
        return BuildApplication(XafApplication.DesignTimeConnectionString);
    }
}
