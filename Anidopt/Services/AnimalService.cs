﻿using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Services;

public class AnimalService : IAnimalService
{
    private readonly AnidoptContext _context;

    public AnimalService(AnidoptContext context)
    {
        _context = context;
    }

    public async Task<List<AnimalType>> GetAnimalTypes() => await _context.AnimalType.ToListAsync();

    public async Task<List<Animal>> GetAnimals() => await _context.Animal.ToListAsync();

    public async Task<Animal?> GetAnimalById(int id) => await _context.Animal.FindAsync(id);

    public async Task<bool> AnimalExistsById(int id) => await _context.Animal.AnyAsync(e => e.Id == id);

    public async Task ConfirmAnimalDeletionById(int id)
    {
        var animal = await GetAnimalById(id);
        if (animal != null)
            _context.Animal.Remove(animal);
        await _context.SaveChangesAsync();
    }
}