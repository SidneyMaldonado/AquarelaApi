using AquarelaApi.Contexts;
using AquarelaApi.DTOs;
using AquarelaApi.Models;
using AquarelaApi.Repositories;
using AquarelaApi.UseCases;
using AquarelaTest.Helpers;
using Microsoft.EntityFrameworkCore;

namespace AquarelaTest.UseCaseTests;

[TestClass]
public class UsuarioUseCaseRefactoredTest
{
    private AppDbContext CriarContextoInMemory()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [TestMethod]
    public async Task GetAllAsync_DeveRetornarTodosUsuarios()
    {
        // Arrange
        var context = CriarContextoInMemory();
        context.Usuarios.AddRange(
            new Usuario { NmUsuario = "Usuario1", DsEmail = "user1@test.com", DsSenha = "senha1", DmAtivo = true },
            new Usuario { NmUsuario = "Usuario2", DsEmail = "user2@test.com", DsSenha = "senha2", DmAtivo = true }
        );
        context.SaveChanges();

        var repository = new UsuarioRepository(context);
        var mapper = MapperHelper.CreateUsuarioMapper();
        var useCase = new UsuarioUseCase(repository, mapper);

        // Act
        var result = await useCase.GetAllAsync();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count());
        var first = result.First();
        Assert.IsInstanceOfType(first, typeof(UsuarioResponse));
    }

    [TestMethod]
    public async Task GetByIdAsync_ComIdValido_DeveRetornarUsuarioResponse()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var usuario = new Usuario { NmUsuario = "Usuario Test", DsEmail = "test@test.com", DsSenha = "senha", DmAtivo = true };
        context.Usuarios.Add(usuario);
        context.SaveChanges();

        var repository = new UsuarioRepository(context);
        var mapper = MapperHelper.CreateUsuarioMapper();
        var useCase = new UsuarioUseCase(repository, mapper);

        // Act
        var result = await useCase.GetByIdAsync(usuario.IdUsuario);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(UsuarioResponse));
        Assert.AreEqual("Usuario Test", result.NmUsuario);
        Assert.AreEqual("test@test.com", result.DsEmail);
    }

    [TestMethod]
    public async Task CreateAsync_ComDTO_DeveCriarUsuario()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var repository = new UsuarioRepository(context);
        var mapper = MapperHelper.CreateUsuarioMapper();
        var useCase = new UsuarioUseCase(repository, mapper);

        var request = new CreateUsuarioRequest(
            "Novo Usuario",
            "novo@test.com",
            "senha123",
            true
        );

        // Act
        var result = await useCase.CreateAsync(request);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(UsuarioResponse));
        Assert.IsTrue(result.IdUsuario > 0);
        Assert.AreEqual("Novo Usuario", result.NmUsuario);
        Assert.AreEqual("novo@test.com", result.DsEmail);

        // Verificar no banco
        var usuarioNoBanco = await context.Usuarios.FindAsync(result.IdUsuario);
        Assert.IsNotNull(usuarioNoBanco);
        Assert.AreEqual("senha123", usuarioNoBanco.DsSenha);
    }

    [TestMethod]
    public async Task UpdateAsync_ComDTO_DeveAtualizarUsuario()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var usuario = new Usuario 
        { 
            NmUsuario = "Original", 
            DsEmail = "original@test.com", 
            DsSenha = "senha", 
            DmAtivo = true 
        };
        context.Usuarios.Add(usuario);
        context.SaveChanges();

        var repository = new UsuarioRepository(context);
        var mapper = MapperHelper.CreateUsuarioMapper();
        var useCase = new UsuarioUseCase(repository, mapper);

        var request = new UpdateUsuarioRequest(
            usuario.IdUsuario,
            "Atualizado",
            "atualizado@test.com",
            false
        );

        // Act
        var result = await useCase.UpdateAsync(usuario.IdUsuario, request);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(UsuarioResponse));
        Assert.AreEqual("Atualizado", result.NmUsuario);
        Assert.AreEqual("atualizado@test.com", result.DsEmail);
        Assert.AreEqual(false, result.DmAtivo);

        // Verificar no banco que senha não foi alterada
        var usuarioNoBanco = await context.Usuarios.FindAsync(usuario.IdUsuario);
        Assert.AreEqual("senha", usuarioNoBanco!.DsSenha);
    }

    [TestMethod]
    public async Task DeleteAsync_DeveRemoverUsuario()
    {
        // Arrange
        var context = CriarContextoInMemory();
        var usuario = new Usuario { NmUsuario = "Para Deletar", DsEmail = "deletar@test.com", DsSenha = "senha", DmAtivo = true };
        context.Usuarios.Add(usuario);
        context.SaveChanges();
        var id = usuario.IdUsuario;

        var repository = new UsuarioRepository(context);
        var mapper = MapperHelper.CreateUsuarioMapper();
        var useCase = new UsuarioUseCase(repository, mapper);

        // Act
        await useCase.DeleteAsync(id);

        // Assert
        var result = await useCase.GetByIdAsync(id);
        Assert.IsNull(result);
    }
}
