using CruiseLineManagementEFCORE.Module.BusinessObjects;
using CruiseLineManagementEFCORE.Module.BusinessObjects.CrewObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CruiseLineManagementEFCORE.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ViewController.
    public partial class VesselRolesViewController : ViewController<ListView>
    {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public VesselRolesViewController()
        {
            InitializeComponent();
            

            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.

            var user = SecuritySystem.CurrentUser as ApplicationUser;
            if (user.UserType == UserType.GlobalUser)
            {
                var globalUser = user as GlobalUser;
                //var vessel = globalUser.AssignedVessels.FirstOrDefault();               
                var criteria = CriteriaOperator.Parse("VesselID = ?", globalUser.AssignedVessels.FirstOrDefault().ID);
                View.CollectionSource.Criteria ["VesselRoles"] = criteria;
            }

            if (user.UserType == UserType.CrewMember)
            {
                var crew = user as Crew;
                var criteria = CriteriaOperator.Parse("VesselID = ?", crew.VesselID);
                View.CollectionSource.Criteria["VesselRoles"] = criteria;
            }
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
