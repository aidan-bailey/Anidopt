using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Services;

public class AnimalTypeService : IAnimalTypeService
{
    private readonly AnidoptContext _context;

    public AnimalTypeService(AnidoptContext context)
    {
        _context = context;
    }

    public List<AnimalType> GetAnimalTypes() => _context.AnimalType.ToList();
}
