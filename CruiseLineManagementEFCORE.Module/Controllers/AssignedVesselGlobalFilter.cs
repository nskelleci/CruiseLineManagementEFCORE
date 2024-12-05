using CruiseLineManagementEFCORE.Module.BusinessObjects.VesselObjects;
using CruiseLineManagementEFCORE.Module.BusinessObjects;
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
using System.Reflection;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.ExpressApp.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using CruiseLineManagementEFCORE.Module.BusinessObjects.SeasonObjects;
using CruiseLineManagementEFCORE.Module.BusinessObjects.CruiseObjects;
using Aqua.EnumerableExtensions;

namespace CruiseLineManagementEFCORE.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class AssignedVesselGlobalFilter : ViewController
    {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public AssignedVesselGlobalFilter()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();

            var currentUser = (ApplicationUser)SecuritySystem.CurrentUser;

            if (currentUser != null && currentUser.AssignedVessels.Any())
            {
                var assignedVesselIds = currentUser.AssignedVessels.Select(v => v.ID).ToList();
                if (View is ListView listView)
                {
                    var objectType = View.ObjectTypeInfo.Type;
                    var propertyPath = FindVesselRelationshipPath(objectType);

                    if (!string.IsNullOrEmpty(propertyPath))
                    {
                        ApplyFilter(propertyPath, assignedVesselIds);
                    }
                }
            }
        }

        private void ApplyFilter(string propertyPath, IList<Guid> ids)
        {
            if (View is ListView listView)
            {
                if (propertyPath =="ID")
                {
                    var filterCriteria = new InOperator(propertyPath, ids);
                    listView.CollectionSource.Criteria["GlobalFilter"] = filterCriteria;
                }else {
                    var filterCriteria = new InOperator(propertyPath+".ID", ids);
                    listView.CollectionSource.Criteria["GlobalFilter"] = filterCriteria;
                }
                
            }
        }

        private string FindVesselRelationshipPath(Type objectType)
        {
            if (objectType == typeof(Vessel))
            {
                return "ID";
            }
            // Tüm BaseObject türevli sınıflar üzerinde çalış
            var visitedTypes = new HashSet<Type>();
            return FindVesselPathRecursive(objectType, visitedTypes);
        }

        private string FindVesselPathRecursive(Type type, HashSet<Type> visitedTypes)
        {
            // Ziyaret edilen tipleri kontrol et, tekrar kontrol yapma
            if (visitedTypes.Contains(type)) return null;
            visitedTypes.Add(type);

            // Tipin tüm public property'lerini al
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                // Eğer property doğrudan Vessel ise, ilişki yolunu döndür
                if (property.PropertyType == typeof(Vessel))
                {
                    return property.Name; // Örn: "Vessel"
                }

                // Eğer property başka bir BaseObject türevi ise, rekürsif olarak kontrol et
                if (typeof(BaseObject).IsAssignableFrom(property.PropertyType))
                {
                    var nestedPath = FindVesselPathRecursive(property.PropertyType, visitedTypes);
                    if (!string.IsNullOrEmpty(nestedPath))
                    {
                        return $"{property.Name}.{nestedPath}"; // Örn: "Route.Vessel"
                    }
                }
            }

            return null; // Vessel ile ilişki bulunamadı
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            if (View is ListView listView)
                listView.CollectionSource.Criteria.Remove("GlobalFilter");

            base.OnDeactivated();
        }
    }
}
