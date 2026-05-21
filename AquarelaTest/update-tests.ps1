# Script para atualizar testes dos UseCases
# Adiciona mapper em todos os construtores de UseCases

$files = @(
	"AquarelaTest\UseCaseTests\UsuarioUseCaseTest.cs",
	"AquarelaTest\UseCaseTests\ContaUseCaseTest.cs",
	"AquarelaTest\UseCaseTests\DividaUseCaseTest.cs"
)

foreach ($file in $files) {
	Write-Host "Processando $file..."

	$content = Get-Content $file -Raw

	# Adicionar imports se não existirem
	if ($content -notmatch "using AquarelaTest.Helpers;") {
		$content = $content -replace "(using Microsoft.EntityFrameworkCore;)", "`$1`nusing AquarelaTest.Helpers;"
	}

	if ($content -notmatch "using AquarelaApi.DTOs;") {
		$content = $content -replace "(using AquarelaApi.Models;)", "using AquarelaApi.DTOs;`n`$1"
	}

	# Substituir construtores dos UseCases
	$content = $content -replace "new UsuarioUseCase\(repository\);", "new UsuarioUseCase(repository, MapperHelper.CreateMapper());"
	$content = $content -replace "new ContaUseCase\(repository\);", "new ContaUseCase(repository, MapperHelper.CreateMapper());"
	$content = $content -replace "new DividaUseCase\(repository\);", "new DividaUseCase(repository, MapperHelper.CreateMapper());"

	Set-Content $file $content -NoNewline
	Write-Host "✓ $file atualizado"
}

Write-Host "`n✅ Todos os arquivos foram atualizados!"
Write-Host "⚠️  ATENÇÃO: Ainda é necessário atualizar manualmente:"
Write-Host "   - CreateAsync: trocar Entity por CreateRequest DTO"
Write-Host "   - UpdateAsync: trocar UpdateAsync(entity) por UpdateAsync(id, request)"
