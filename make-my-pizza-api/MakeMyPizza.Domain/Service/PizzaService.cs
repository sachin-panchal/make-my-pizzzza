using AutoMapper;
using Microsoft.Extensions.Logging;

using MakeMyPizza.Data.IRepository;
using MakeMyPizza.Data.Models;
using MakeMyPizza.Domain.IService;

namespace MakeMyPizza.Domain.Service;
public class PizzaService : IPizzaService
{
    private readonly IBaseRepository<Pizza> _pizzaRepo;
    private readonly IBaseRepository<PizzaPrice> _pizzaPriceRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<PizzaService> _logger;
    public PizzaService(IBaseRepository<Pizza> pizzaRepo,
                        IBaseRepository<PizzaPrice> pizzaPriceRepo,
                        IUnitOfWork unitOfWork,
                        IMapper mapper,
                        ILogger<PizzaService> logger)
    {
        _pizzaRepo = pizzaRepo;
        _pizzaPriceRepo = pizzaPriceRepo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public List<PizzaDetailDto> GetPizzaList()
    {
        _logger.LogInformation("Get all pizzalist started in PizzaService");
        var pizzaList = _pizzaRepo.Get();
        var pizzaPrices = _pizzaPriceRepo.Get();

        var pizzaDetails = _mapper.Map<List<PizzaDetailDto>>(pizzaList);
        pizzaDetails?.ForEach(p => {
            p.Price = pizzaPrices.Find(pr => pr.PizzaId == p.Id && pr.Size == CrustSize.Medium)?.Price ?? 0;
            p.Size = CrustSize.Medium;
        });
        return pizzaDetails;
    }

}