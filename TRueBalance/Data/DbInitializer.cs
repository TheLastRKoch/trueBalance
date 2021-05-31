using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TRueBalance.Data.Entities;
using TRueBalance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRueBalance.Data
{
    public class DbInitializer
    {

        #region Seed MealsCategories
        private static void CreateDefaulCategories(ApplicationDbContext context)
        {
            var Categories = new Category[]
            {
                new Category{ Name = "Plato fuerte", Type="Food"},
                new Category{ Name = "Acompañamiento", Type="Food"},
                new Category{ Name = "Bebida", Type="Food"},
                new Category{ Name = "Adicional", Type="Food"},
                new Category{ Name = "Postre", Type="Food"},
                new Category{ Name = "Otro", Type="Food"},
                new Category{ Name = "Combo", Type="Food"},
                new Category{ Name = "Print", Type="PrintService"},
                new Category{ Name = "Efectivo", Type="Sell"},
                new Category{ Name = "Tarjeta", Type="Sell"},
                new Category{ Name = "Llevar", Type="Order"},
                new Category{ Name = "ComerAqui", Type="Order"},
            };

            foreach (var category in Categories)
            {
                context.Categories.Add(category);
            }
        }
        #endregion

        #region Seed Meals Methods
        private static void CreateDefaulMeals(ApplicationDbContext context)
        {
            var Meals = new Meal[]
            {
                new Meal { Name="Arepa", Price=1500},
                new Meal { Name="Arepa  bisteck", Price=2400},
                new Meal { Name="Arepa  queso", Price=1800},
                new Meal { Name="Arepa  tocineta", Price=2000},
                new Meal { Name="Arepa jamon", Price=2000},
                new Meal { Name="Burrito", Price=1500},
                new Meal { Name="Burrito Queso", Price=1700},
                new Meal { Name="Carnitas Mexicanas", Price=2000},
                new Meal { Name="Corte de Res", Price=1400},
                new Meal { Name="Empanada carne", Price=1000},
                new Meal { Name="Empanada pollo", Price=1000},
                new Meal { Name="Ensalada grd", Price=2500},
                new Meal { Name="Ensalada peq", Price=1600},
                new Meal { Name="Filet de Pollo", Price=1000},
                new Meal { Name="Hamburguesa", Price=1500},
                new Meal { Name="Adic -  jamon", Price=200},
                new Meal { Name="Adic - Queso", Price=200},
                new Meal { Name="Adic - Tocineta", Price=200},
                new Meal { Name="Adic - Torta de carne", Price=400},
                new Meal { Name="Papa Asada jamon", Price=1800},
                new Meal { Name="Papa Asada queso", Price=1200},
                new Meal { Name="Perro americano", Price=1200},
                new Meal { Name="Perro mexicano", Price=2000},
                new Meal { Name="Perro tico", Price=1000},
                new Meal { Name="Pinchos", Price=1500},
                new Meal { Name="Ravioles de Res", Price=1000},
                new Meal { Name="Taco tico", Price=1000},
                new Meal { Name="Tortillas queso", Price=1800},
                new Meal { Name="Acom - 2 Deppers", Price=1000},
                new Meal { Name="Acom - 2 Papas Hash", Price=800},
                new Meal { Name="Acom - 3 dedos queso", Price=1500},
                new Meal { Name="Acom - 3 patacones", Price=1000},
                new Meal { Name="Acom - 4 Nuggets", Price=1300},
                new Meal { Name="Acom - 5 yuquitas", Price=1000},
                new Meal { Name="Acom - Aros de cebolla", Price=1500},
                new Meal { Name="Acom - doraditas", Price=700},
                new Meal { Name="Acom - frijoles molidos", Price=300},
                new Meal { Name="Acom - nachos", Price=800},
                new Meal { Name="Acom - papas francesas", Price=1000},
                new Meal { Name="Acom - salchipapas", Price=1200},
                new Meal { Name="Acom - super papas", Price=2000},
                new Meal { Name="Adic - salsa mayonesa", Price=200},
                new Meal { Name="Adic - salsa pizza", Price=200},
                new Meal { Name="Adic - salsa tomate", Price=100},
                new Meal { Name="Adic - queso cheddar", Price=100},
                new Meal { Name="Bandeja reposteria", Price=1900},
                new Meal { Name="Cafe con leche", Price=600},
                new Meal { Name="Cafe negro", Price=1500},
                new Meal { Name="Chocolate", Price=700},
                new Meal { Name="Jugo de naranja", Price=600},
                new Meal { Name="Smoothie arandano", Price=900},
                new Meal { Name="Smoothie fresa", Price=900},
                new Meal { Name="Suspiros", Price=1800},
                new Meal { Name="Torta chilena", Price=2000},
                new Meal { Name="Torta churchill ", Price=1900}
            };

            foreach (var meal in Meals)
            {
                context.Meals.Add(meal);
            }
        }
        #endregion

        #region Seed Users Methods
        private static List<User> CreateUsers()
        {
            User Sergio = new User()
            {
                Email = "sergio31.96@outlook.com",
                Name = "Sergio",
                ImgURL = "\\assets\\Admin\\images\\users\\default.png",
                LastName = "Segura",
                Birthday = new DateTime(1996, 7, 31),
                Password = "D0xsFH#lY8I3Q",
                RolName = "Developer"
            };

            List<User> Users = new List<User>();
            Users.Add(Sergio);
           
            return Users;
        }

        private static async Task CreateDefaultUsersAndRolesForApplication(UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm, ILogger<DbInitializer> logger)
        {
            List<User> Users = CreateUsers();

            await CreateRole(rm, logger, "Developer");
            await CreateRole(rm, logger, "Administrator");
            await CreateRole(rm, logger, "Cashier");

            foreach (var CurrentUser in Users)
            {
                var user = await CreateUser(um, logger, CurrentUser);
                await SetPasswordForUser(um, logger, CurrentUser, user);
                await AddRoleToUser(um, logger, CurrentUser, user);
            }

        }

        private static async Task CreateRole(RoleManager<IdentityRole> rm, ILogger<DbInitializer> logger, string RoleName)
        {
            logger.LogInformation($"Create the role `{RoleName}` for application");
            var ir = await rm.CreateAsync(new IdentityRole(RoleName));
            if (ir.Succeeded)
            {
                logger.LogDebug($"Created the role `{RoleName}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"Default role `{RoleName}` cannot be created");
                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                throw exception;
            }
        }

        private static async Task<ApplicationUser> CreateUser(UserManager<ApplicationUser> um, ILogger<DbInitializer> logger, User CurrentUser)
        {
            logger.LogInformation($"Create default user with email `{CurrentUser.Email}` for application");
            var user = new ApplicationUser(CurrentUser.Email, CurrentUser.Name, CurrentUser.LastName, CurrentUser.Birthday, CurrentUser.ImgURL);

            var ir = await um.CreateAsync(user);
            if (ir.Succeeded)
            {
                logger.LogDebug($"Created default user `{CurrentUser.Email}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"Default user `{CurrentUser}` cannot be created");
                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                throw exception;
            }

            var createdUser = await um.FindByEmailAsync(CurrentUser.Email);
            return createdUser;
        }

        private static async Task SetPasswordForUser(UserManager<ApplicationUser> um, ILogger<DbInitializer> logger, User CurrentUser, ApplicationUser user)
        {
            logger.LogInformation($"Set password for default user `{CurrentUser.Email}`");
            var ir = await um.AddPasswordAsync(user, CurrentUser.Password);
            if (ir.Succeeded)
            {
                logger.LogTrace($"Set password `{CurrentUser.Password}` for default user `{CurrentUser.Email}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"Password for the user `{CurrentUser.Email}` cannot be set");
                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                throw exception;
            }
        }

        private static async Task AddRoleToUser(UserManager<ApplicationUser> um, ILogger<DbInitializer> logger, User CurrentUser, ApplicationUser user)
        {
            logger.LogInformation($"Add default user `{CurrentUser.Email}` to role '{CurrentUser.RolName}'");
            var ir = await um.AddToRoleAsync(user, CurrentUser.RolName);
            if (ir.Succeeded)
            {
                logger.LogDebug($"Added the role '{CurrentUser.RolName}' to default user `{CurrentUser.Email}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"The role `{CurrentUser.RolName}` cannot be set for the user `{CurrentUser.Email}`");
                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                throw exception;
            }
        }

        private static string GetIdentiryErrorsInCommaSeperatedList(IdentityResult ir)
        {
            string errors = null;
            foreach (var identityError in ir.Errors)
            {
                errors += identityError.Description;
                errors += ", ";
            }
            return errors;
        }
        #endregion

        #region Seed UserConfiguration Methods
        private static void CreateDefaultUserConfig(ApplicationDbContext context)
        {
            var Settings = new UserSetting[]
            {
                new UserSetting() { Key = "CashBoxClosingFeature", Value = "false" },
                new UserSetting() { Key = "FirstTimeDayStart", Value = "true" },
                new UserSetting() { Key = "DayStart", Value = DateTime.Now.Date.ToString("dd/MM/yyyy") }
            };

            foreach (var Setting in Settings)
            {
                context.UserSettings.Add(Setting);
            }
        }
        #endregion

        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager, ILogger<DbInitializer> logger)
        {
            context.Database.EnsureCreated();

            // Look for any users.
            if (context.Users.Any())
            {
                return; // DB has been seeded
            }

            await CreateDefaultUsersAndRolesForApplication(userManager, roleManager, logger);
            CreateDefaulMeals(context);
            CreateDefaultUserConfig(context);
            CreateDefaulCategories(context);
            context.SaveChanges();
        }
    }
}