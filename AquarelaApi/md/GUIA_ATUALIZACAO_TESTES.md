# Guia de Atualização dos Testes

## Problema
Os testes foram criados antes da refatoração e usam a API antiga dos UseCases.

## Mudanças Necessárias

### 1. Adicionar import do Helper
```csharp
using AquarelaTest.Helpers;
using AquarelaApi.DTOs;
```

### 2. Padrão de atualização em CADA teste

#### ANTES:
```csharp
var repository = new UsuarioRepository(context);
var useCase = new UsuarioUseCase(repository);
```

#### DEPOIS:
```csharp
var repository = new UsuarioRepository(context);
var mapper = MapperHelper.CreateMapper();
var useCase = new UsuarioUseCase(repository, mapper);
```

### 3. Mudar CreateAsync para usar DTOs

#### ANTES:
```csharp
var novoUsuario = new Usuario
{
	NmUsuario = "Novo",
	DsEmail = "novo@test.com",
	DsSenha = "senha",
	DmAtivo = true
};
var result = await useCase.CreateAsync(novoUsuario);
```

#### DEPOIS:
```csharp
var request = new CreateUsuarioRequest(
	"Novo",
	"novo@test.com",
	"senha",
	true
);
var result = await useCase.CreateAsync(request);
```

### 4. Mudar UpdateAsync para usar DTOs

#### ANTES:
```csharp
var usuario = await _repository.GetByIdAsync(id);
usuario.NmUsuario = "Atualizado";
var result = await useCase.UpdateAsync(usuario);
```

#### DEPOIS:
```csharp
var request = new UpdateUsuarioRequest(
	id,
	"Atualizado",
	"email@test.com",
	true
);
var result = await useCase.UpdateAsync(id, request);
```

### 5. Testes que precisam atualizar - UsuarioUseCaseTest.cs

Buscar e substituir em TODOS os testes:
- `new UsuarioUseCase(repository)` → `new UsuarioUseCase(repository, MapperHelper.CreateMapper())`
- `new ContaUseCase(repository)` → `new ContaUseCase(repository, MapperHelper.CreateMapper())`
- `new DividaUseCase(repository)` → `new DividaUseCase(repository, MapperHelper.CreateMapper())`

Métodos que precisam mudar a chamada:
- **CreateAsync**: Trocar `Entity` por `CreateRequest DTO`
- **UpdateAsync**: Trocar `UpdateAsync(entity)` por `UpdateAsync(id, request)`

## Script de Busca e Substituição

Use o Find/Replace no VS Code ou Visual Studio:

### Replace 1:
**Buscar**: `new UsuarioUseCase\(repository\);`
**Substituir**: `new UsuarioUseCase(repository, MapperHelper.CreateMapper());`

### Replace 2:
**Buscar**: `new ContaUseCase\(repository\);`
**Substituir**: `new ContaUseCase(repository, MapperHelper.CreateMapper());`

### Replace 3:
**Buscar**: `new DividaUseCase\(repository\);`
**Substituir**: `new DividaUseCase(repository, MapperHelper.CreateMapper());`

## Comandos para testar cada arquivo individualmente

```bash
# Testar apenas UsuarioUseCaseTest
dotnet test --filter FullyQualifiedName~UsuarioUseCaseTest

# Testar apenas ContaUseCaseTest
dotnet test --filter FullyQualifiedName~ContaUseCaseTest

# Testar apenas DividaUseCaseTest
dotnet test --filter FullyQualifiedName~DividaUseCaseTest
```

## Prioridade de Correção

1. ✅ MapperHelper criado
2. ⚠️ UsuarioUseCaseTest - COMEÇADO (falta completar)
3. ⚠️ ContaUseCaseTest - PENDENTE
4. ⚠️ DividaUseCaseTest - PENDENTE

## SOLUÇÃO RÁPIDA

Se quiser apenas fazer compilar rapidamente para testar a API:

1. Comentar os testes temporariamente
2. Executar apenas a API: `dotnet run --project AquarelaApi/AquarelaApi.csproj`
3. Testar via Swagger
4. Depois corrigir os testes um por um
