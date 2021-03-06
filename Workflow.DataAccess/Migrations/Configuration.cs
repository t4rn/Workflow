namespace Workflow.DataAccess.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Workflow.DataAccess.WorkflowDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            //AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Workflow.DataAccess.WorkflowDbContext context)
        {
            var userManager =
                new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new WorkflowDbContext()));

            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false
            };

            var roleManager =
                new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(new WorkflowDbContext()));

            string name = "admin@workflow.com";
            string password = "Pluralsight#1";
            string firstName = "Admin";
            string roleName = "Admin";

            var role = roleManager.FindByName(roleName);

            if (role == null)
            {
                role = new ApplicationRole(roleName);
                var roleResult = roleManager.Create(role);
            }

            var user = userManager.FindByName(name);

            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name, FirstName = firstName };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            var rolesForUser = userManager.GetRoles(user.Id);

            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }



            string accountNumber = "ABC123";

            context.Customers.AddOrUpdate(
                c => c.AccountNumber,
                new Customer
                {
                    AccountNumber = accountNumber,
                    CompanyName = "IT Best Company",
                    Address = "Marszałkowska 13",
                    City = "Warsaw",
                    State = "Mazowieckie",
                    ZipCode = "02140"
                });

            context.SaveChanges();

            Customer customer = context.Customers.First(c => c.AccountNumber == accountNumber);

            string description = "First work order";

            context.WorkOrders.AddOrUpdate(
                wo => wo.Description,
                new WorkOrder { Description = description, CustomerId = customer.Id, WorkOrderStatus = WorkOrderStatus.Created });

            context.SaveChanges();

            WorkOrder workOrder = context.WorkOrders.First(wo => wo.Description == description);

            context.Parts.AddOrUpdate(
                p => p.InventoryItemCode,
                new Part { InventoryItemCode = "THING1", InventoryItemName = "Thing Number One", Quantity = 1, UnitPrice = 1.23m, WorkOrderId = workOrder.Id });

            context.Labors.AddOrUpdate(
                l => l.ServiceItemCode,
                new Labor { ServiceItemCode = "INSTALL", ServiceItemName = "Installation", LaborHours = 9.87m, Rate = 35.75m, WorkOrderId = workOrder.Id });

            string categoryName = "Devices";

            context.Categories.AddOrUpdate(
                c => c.Name,
                new Category { Name = categoryName });

            context.SaveChanges();

            Category category = context.Categories.First(c => c.Name == categoryName);

            context.InventoryItems.AddOrUpdate(
                ii => ii.Code,
                new InventoryItem { Code = "THING2", Name = "A Second Kind of Thing", UnitPrice = 3.33m, CategoryId = category.Id });

            context.ServiceItems.AddOrUpdate(
                si => si.Code,
                new ServiceItem { Code = "CLEAN", Name = "General Cleaning", Rate = 23.50m });
        }
    }
}
