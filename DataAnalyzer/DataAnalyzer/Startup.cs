using DataAnalyzer.BusinessLogic;
using DataAnalyzer.DataAccess;
using DataAnalyzer.DataEntities;
using DataAnalyzer.ServiceAgents;
using DataAnalyzer.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAnalyzer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddDefaultJsonSettings().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvcCore().AddVersionedApiExplorer(o => o.GroupNameFormat = "'v'V");
            services.AddLowerCaseUrls();
            services.AddGzipCompression();
            services.AddVersioning();

            services.Configure<Settings>(settings =>
                {
                    settings.ConnectionString = Configuration.GetSection("ConnectionStrings:PBDConnection").Value;
                    settings.MachineLearningServer = Configuration.GetSection("MachineLearningServer").Value;
                });

            services.AddScoped<IMetaDataAccess, MetaDataAccess>();
            services.AddScoped<IMachineLearningAgent, MachineLearningAgent>();
            services.AddScoped<IAnalyzer, Analyzer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
