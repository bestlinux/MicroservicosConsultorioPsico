using Microsoft.EntityFrameworkCore;
using PacienteService.Domain.Entities;

namespace PacienteService.Persistence.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }

    public DbSet<Paciente> Pacientes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        builder.Entity<Paciente>().HasKey(t => t.Id);
        builder.Entity<Paciente>().Property(p => p.Nome).HasMaxLength(600).IsRequired();
        builder.Entity<Paciente>().Property(p => p.Telefone).HasMaxLength(50).IsRequired();
        builder.Entity<Paciente>().Property(p => p.DataNascimento).IsRequired();
        builder.Entity<Paciente>().Property(p => p.Sexo).IsRequired();
        builder.Entity<Paciente>().Property(p => p.Email).HasMaxLength(100).IsRequired();
        builder.Entity<Paciente>().Property(p => p.CPF).HasMaxLength(100).IsRequired();
        builder.Entity<Paciente>().Property(p => p.TipoPagamento).HasMaxLength(100).IsRequired();
        builder.Entity<Paciente>().Property(p => p.ValorSessao).HasPrecision(10, 2);
        builder.Entity<Paciente>().Property(p => p.Ativo).IsRequired();


		foreach (var relationship in builder.Model.GetEntityTypes()
				.SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

	}
}
