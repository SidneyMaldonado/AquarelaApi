using AquarelaApi.Mappings;

namespace AquarelaTest.Helpers;

public static class MapperHelper
{
    public static ContaMapper CreateContaMapper() => new();
    public static DividaMapper CreateDividaMapper() => new();
    public static UsuarioMapper CreateUsuarioMapper() => new();
}
