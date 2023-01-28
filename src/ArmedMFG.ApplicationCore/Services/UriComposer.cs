using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG;

namespace ArmedMFG.ApplicationCore.Services;

public class UriComposer : IUriComposer
{
    private readonly ProductsSettings _productsSettings;

    public UriComposer(ProductsSettings productsSettings) => _productsSettings = productsSettings;

    public string ComposePicUri(string uriTemplate)
    {
        return uriTemplate.Replace("http://catalogbaseurltobereplaced", _productsSettings.ProductsBaseUrl);
    }
}
