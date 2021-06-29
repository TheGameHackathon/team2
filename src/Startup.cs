using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using thegame.Services.FieldFactory;
using AutoMapper;
using AutoMapper.Configuration;
using thegame.Domain;
using thegame.Domain.Game;
using thegame.Models;

namespace thegame
{
    public class Startup
    {
        public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IFieldFactory, FieldFactory>();
            services.AddMvc();
            services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<GameDto, GameBase>()
                    .ForMember(dest => dest.gameField,
                        opt => opt.MapFrom(x => OneDimensionCellsToTwoDimenstion(x.Cells, x.Width, x.Height)));
                cfg.CreateMap<GameBase, GameDto>()
                    .ForMember(dest => dest.Cells, opt => opt.MapFrom(x => x.gameField.Cells.SelectMany(a => a)))
                    .ForMember(dest => dest.Height, opt => opt.MapFrom(x => x.gameField.Height))
                    .ForMember(dest => dest.Width, opt => opt.MapFrom(x => x.gameField.Width));
            });
        }
        private Cell[][] OneDimensionCellsToTwoDimenstion(CellDto[] cells, int width, int height)
        {
            var newCells = new Cell[height][];
            for (var i = 0; i < width; i++)
                newCells[i] = new Cell[width];
            foreach (var cell in cells)
                newCells[cell.Pos.Y][cell.Pos.X] = new Cell(new Vector(cell.Pos.X, cell.Pos.Y), TypeColorToColor(cell.Type));
            return newCells;
        }

        private Color TypeColorToColor(string type)
        {
            if (type == "color1")
                return Color.Blue;
            if (type == "color2")
                return Color.Red;
            if (type == "color3")
                return Color.Green;
            if (type == "color4")
                return Color.Cyan;
            return Color.Magenta;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
            app.Use((context, next) =>
            {
                context.Request.Path = "/index.html";
                return next();
            });
            app.UseStaticFiles();
        }
    }
}