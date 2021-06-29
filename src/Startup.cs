using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using thegame.Services.FieldFactory;
using AutoMapper;
using AutoMapper.Configuration;
using thegame.Domain;
using thegame.Domain.Game;
using thegame.Models;
using thegame.Services;

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
                cfg.CreateMap<GameDto, SmartGame>()
                    .ForMember(dest => dest.gameField,
                        opt => opt.MapFrom(x =>
                            new GameField(OneDimensionCellsToTwoDimenstion(x.Cells, x.Width, x.Height),
                                x.Width, x.Height)));
                cfg.CreateMap<SmartGame, GameDto>()
                    .ForMember(dest => dest.Cells, opt => opt.MapFrom(
                        x => x.gameField.Cells.SelectMany(a => a).Select(a =>
                            new CellDto($"h{a.Coordinates.Y}w{a.Coordinates.X}",
                                new VectorDto(a.Coordinates.X, a.Coordinates.Y), a.Color.TypeColorToColor(), "", 0))))
                    .ForMember(dest => dest.Height, opt => opt.MapFrom(x => x.gameField.Height))
                    .ForMember(dest => dest.Width, opt => opt.MapFrom(x => x.gameField.Width));
            });
            services.AddSingleton<GamesRepo, GamesRepo>();
        }

        private Cell[][] OneDimensionCellsToTwoDimenstion(CellDto[] cells, int width, int height)
        {
            var newCells = new Cell[height][];
            for (var i = 0; i < width; i++)
                newCells[i] = new Cell[width];
            foreach (var cell in cells)
                newCells[cell.Pos.Y][cell.Pos.X] = new Cell(new Vector(cell.Pos.X, cell.Pos.Y), cell.Type.TypeColorToColor());
            return newCells;
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

    public static class StartupExtensions
    {
        public static string TypeColorToColor(this Color color)
        {
            if (color == Color.Blue)
                return "color1";
            if (color == Color.Red)
                return "color2";
            if (color == Color.Green)
                return "color3";
            if (color == Color.Cyan)
                return "color4";
            return "color5";
        }

        public static Color TypeColorToColor(this string type)
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
    }
    
}