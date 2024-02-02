using AUTHIO.APPLICATION.Domain.Entity;
using AUTHIO.APPLICATION.Infra.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AUTHIO.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;
        private readonly AuthIoContext _dbContext1;

        public WeatherForecastController(AuthIoContext dbContext, ILogger<WeatherForecastController> logger, SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager)
        {
            _dbContext1 = dbContext;
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var tentant = await _dbContext1.Tenants.AddAsync(new TenantEntity
            {
                Created = DateTime.Now,
                 Description = "Teste 1",
                 Name = $"HYPER.IO_{DateTime.Now}",
                 Status = APPLICATION.Domain.Enums.Status.Ativo,
            });

            var user = new UserEntity
            {
                Email = "Leo.fgfgfdgdfgfd@outlook.com",
                Name = "Leo",
                LastName = "Almeida",
                Status = APPLICATION.Domain.Enums.Status.Ativo,
                EmailConfirmed = true,
                UserName = $"Leo.Almeida_{DateTime.Now}",
                TenantId = tentant.Entity.Id
            };

            _dbContext1.SaveChanges();

            // Generate a password hash.
            user.PasswordHash = new PasswordHasher<UserEntity>().HashPassword(user, "Leo@77990912");

            // Create user.
            var resault = await _userManager.CreateAsync(user);

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var tenants = await _dbContext1.Tenants.Include(x => x.Users).ToListAsync();

            var user = tenants.FirstOrDefault().Users;

            return new ObjectResult(tenants);
        }

        [HttpGet("user/GetAll")]
        public async Task<IActionResult> UserGetAll()
        {
            var users = _userManager.Users.ToList();

            return new ObjectResult(users);
        }
    }
}
