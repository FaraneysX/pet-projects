using AnimalClinic.DataAccess.Entities;
using AnimalClinic.Service;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimalClinic.DataAccess;

// Класс контекста базы данных, отвечающий за взаимодействие с базой данных
public class AnimalClinicDbContext : DbContext
{
    // Свойства DbSet для доступа к таблицам базы данных
    public DbSet<ClientEntity>? Clients { get; set; }
    public DbSet<AnimalEntity>? Animals { get; set; }
    public DbSet<VisitEntity>? Visits { get; set; }

    // Метод для настройки отношений между сущностями базы данных
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Настройка таблиц для сущностей с использованием триггеров
        modelBuilder.Entity<ClientEntity>().ToTable(tb => tb.HasTrigger("ClientInsertedTrigger"));
        modelBuilder.Entity<AnimalEntity>().ToTable(tb => tb.HasTrigger("AnimalInsertedTrigger"));

        // Настройка отношений "один ко многим" между клиентами и животными, а также между клиентами и записями
        modelBuilder.Entity<ClientEntity>()
            .HasMany(client => client.Animals)
            .WithOne(animal => animal.Client)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ClientEntity>()
            .HasMany(client => client.Visits)
            .WithOne(visit => visit.Client)
            .OnDelete(DeleteBehavior.Cascade);
    }

    // Метод для настройки соединения с базой данных
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Использование SQL Server и строки подключения из настроек
        optionsBuilder.UseSqlServer(Settings.ConnectionString);
    }

    // Метод для асинхронного выполнения миграций базы данных
    public async Task SynchronizationAsync()
    {
        // Сервис для работы с информацией о миграциях
        var migrationsAssembly = this.GetService<IMigrationsAssembly>();

        // Поиск последней миграции в приложении
        var lastMigration = migrationsAssembly.Migrations.LastOrDefault();

        // Если миграция отсутствует, выбрасывается исключение
        if (lastMigration.Key is null)
        {
            throw new Exception("В приложении отсутствуют миграции.");
        }

        try
        {
            // Выполнение миграций
            await Database.MigrateAsync();

            // Получение списка примененных миграций из базы данных
            var appliedMigrations = Database.GetAppliedMigrations().ToList();

            // Проверка соответствия миграций в приложении и в базе данных
            if (!appliedMigrations.Contains(lastMigration.Key))
            {
                throw new Exception("Миграция в приложении не совпадает с миграцией в БД.");
            }
        }
        catch (Exception)
        {
            throw new Exception("Проверьте работоспособность сервера.");
        }
    }
}
