﻿using System.Configuration;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.Win.ApplicationBuilder;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Win;
using DevExpress.Persistent.Base;
using Microsoft.EntityFrameworkCore;
using DevExpress.ExpressApp.EFCore;
using DevExpress.EntityFrameworkCore.Security;
using DevExpress.XtraEditors;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.ExpressApp.Design;

namespace CruiseLineManagementEFCORE.Win;

public class ApplicationBuilder : IDesignTimeApplicationFactory {
    public static WinApplication BuildApplication(string connectionString) {
        var builder = WinApplication.CreateBuilder();
        // Register custom services for Dependency Injection. For more information, refer to the following topic: https://docs.devexpress.com/eXpressAppFramework/404430/
        // builder.Services.AddScoped<CustomService>();
        // Register 3rd-party IoC containers (like Autofac, Dryloc, etc.)
        // builder.UseServiceProviderFactory(new DryIocServiceProviderFactory());
        // builder.UseServiceProviderFactory(new AutofacServiceProviderFactory());

        builder.UseApplication<CruiseLineManagementEFCOREWindowsFormsApplication>();
        builder.Modules
            .AddAuditTrailEFCore()
            .AddCharts()
            .AddConditionalAppearance()
            .AddDashboards(options => {
                options.DashboardDataType = typeof(DevExpress.Persistent.BaseImpl.EF.DashboardData);
                options.DesignerFormStyle = DevExpress.XtraBars.Ribbon.RibbonFormStyle.Ribbon;
            })
            .AddFileAttachments()
            .AddKpi()
            .AddNotifications()
            .AddOffice()
            .AddPivotChart()
            .AddPivotGrid()
            .AddReports(options => {
                options.EnableInplaceReports = true;
                options.ReportDataType = typeof(DevExpress.Persistent.BaseImpl.EF.ReportDataV2);
                options.ReportStoreMode = DevExpress.ExpressApp.ReportsV2.ReportStoreModes.XML;
            })
            .AddScheduler()
#if DEBUG
            .AddScriptRecorder()
#endif
            .AddTreeListEditors()
            .AddValidation(options => {
                options.AllowValidationDetailsAccess = false;
            })
            .AddViewVariants()
            .Add<CruiseLineManagementEFCORE.Module.CruiseLineManagementEFCOREModule>()
        	.Add<CruiseLineManagementEFCOREWinModule>();
        builder.ObjectSpaceProviders
            .AddSecuredEFCore(options => options.PreFetchReferenceProperties())
                .WithAuditedDbContext(contexts => {
                    contexts.Configure<CruiseLineManagementEFCORE.Module.BusinessObjects.CruiseLineManagementEFCOREEFCoreDbContext, CruiseLineManagementEFCORE.Module.BusinessObjects.CruiseLineManagementEFCOREAuditingDbContext>(
                        (application, businessObjectDbContextOptions) => {
                            // Uncomment this code to use an in-memory database. This database is recreated each time the server starts. With the in-memory database, you don't need to make a migration when the data model is changed.
                            // Do not use this code in production environment to avoid data loss.
                            // We recommend that you refer to the following help topic before you use an in-memory database: https://docs.microsoft.com/en-us/ef/core/testing/in-memory
                            //businessObjectDbContextOptions.UseInMemoryDatabase("InMemory");
                            businessObjectDbContextOptions.UseSqlServer(connectionString);
                            businessObjectDbContextOptions.UseChangeTrackingProxies();
                            businessObjectDbContextOptions.UseObjectSpaceLinkProxies();
                        },
                        (application, auditHistoryDbContextOptions) => {
                            auditHistoryDbContextOptions.UseSqlServer(connectionString);
                            auditHistoryDbContextOptions.UseChangeTrackingProxies();
                            auditHistoryDbContextOptions.UseObjectSpaceLinkProxies();
                        });
                })
            .AddNonPersistent();
        builder.Security
            .UseIntegratedMode(options => {
                options.Lockout.Enabled = true;

                options.RoleType = typeof(PermissionPolicyRole);
                options.UserType = typeof(CruiseLineManagementEFCORE.Module.BusinessObjects.ApplicationUser);
                options.UserLoginInfoType = typeof(CruiseLineManagementEFCORE.Module.BusinessObjects.ApplicationUserLoginInfo);
                options.Events.OnSecurityStrategyCreated += securityStrategy => {
                   // Use the 'PermissionsReloadMode.NoCache' option to load the most recent permissions from the database once
                   // for every DbContext instance when secured data is accessed through this instance for the first time.
                   // Use the 'PermissionsReloadMode.CacheOnFirstAccess' option to reduce the number of database queries.
                   // In this case, permission requests are loaded and cached when secured data is accessed for the first time
                   // and used until the current user logs out. 
                   // See the following article for more details: https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Security.SecurityStrategy.PermissionsReloadMode.
                   ((SecurityStrategy)securityStrategy).PermissionsReloadMode = PermissionsReloadMode.NoCache;
                };
            })
            .AddPasswordAuthentication();
        builder.AddBuildStep(application => {
            application.ConnectionString = connectionString;
#if DEBUG
            if(System.Diagnostics.Debugger.IsAttached && application.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema) {
                application.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
            }
#endif
        });
        var winApplication = builder.Build();
        return winApplication;
    }

    XafApplication IDesignTimeApplicationFactory.Create()
        => BuildApplication(XafApplication.DesignTimeConnectionString);
}
