using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TARge25Shop.ApplicationServices.Services;
using TARge25Shop.Core.ServiceInterface;
using TARge25Shop.Data;
using TARge25Shop_SpaceshipTest.Macros;
using TARge25Shop_SpaceshipTest.Mock;
using System;
using System.Collections.Generic;
using System.Text;

namespace TARge25Shop_SpaceshipTest
{
    public abstract class TestBase
    {
        protected IServiceProvider serviceProvider { get; set; }

        protected TestBase()
        {
            var services = new ServiceCollection();
            SetupServices(services);
            serviceProvider = services.BuildServiceProvider();

        }

        /// <summary>
        /// Seame üles testide läbiviimiseks vajalikud teenused mujalt projektist
        /// See meetod annab ka mälusoleva andmebaasi mida testideks kasutada,
        /// toimib kui "program.cs"-i sisu testide jooksutamiseks, ent lühidal kujul.
        /// </summary>
        /// <param name="services"></param>tühi ServiceCollection-tüüpi muutuja kuhu asetame
        /// teenused, sealhulgas ka andmebaasi.
        public virtual void SetupServices(ServiceCollection services)
        {
            
            services.AddScoped<ISpaceshipServices, SpaceshipServices>();
            services.AddScoped<IFileServices, FileServices>();
            services.AddScoped<IHostEnvironment, MockIHostEnvironment>();

            services.AddDbContext<TARge25ShopContext>(
                x =>
                {
                    x.UseInMemoryDatabase("TEST");
                    x.ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                });
            RegisterMacros(services);
        }

        public void Dispose()
        {

        }
        /// <summary>
        /// Leia üles üks kindel teenus teenusepakkujalt.
        /// serviceProvider omab teenuseid, GetService hangib X tüüpi teenuse.
        /// C# on ükskõik mis tüüpi võimalik ilma tüübita näidata tähe "T"-ga ehk "Template".
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>

        protected T Svc<T>()
        {
            return serviceProvider.GetService<T>();
        }

        /// <summary>
        /// Registreerib macrodest teenuseid, kui nad ei ole liidesed ja ei ole abstraktsed
        /// On vaja testi setupide seadistuseks.
        /// </summary>
        /// <param name="services"></param>
        private void RegisterMacros(ServiceCollection services)
        {
            var macroBaseType = typeof(IMacros);

            var macros = macroBaseType.Assembly.GetTypes()
                .Where(t => macroBaseType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
            foreach (var macro in macros)
            {
                services.AddSingleton(macro);
            }
        }
    }
}
