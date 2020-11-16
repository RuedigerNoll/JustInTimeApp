using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;

namespace JustInTime.Module.DatabaseUpdate
{
    public class Updater : ModuleUpdater
    {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();

            SecuritySystemRole adminRole = 
                ObjectSpace.FindObject<SecuritySystemRole>(new BinaryOperator("Name", SecurityStrategy.AdministratorRoleName));
            
            if (adminRole == null)
            {
                adminRole = ObjectSpace.CreateObject<SecuritySystemRole>();
                adminRole.Name = SecurityStrategy.AdministratorRoleName;
                adminRole.IsAdministrative = true;
            }

            SecuritySystemUser user = 
                ObjectSpace.FindObject<SecuritySystemUser>(new BinaryOperator("UserName", "Administrator"));
            
            if (user == null)
            {
                user = ObjectSpace.CreateObject<SecuritySystemUser>();
                user.UserName = "Administrator";
                user.SetPassword("");
                user.Roles.Add(adminRole);
            }
        }

    }
}
