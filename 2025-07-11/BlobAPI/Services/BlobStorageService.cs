using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.DataProtection;

namespace BlobAPI.Services;

public class BlobStorageService
{
    private BlobContainerClient? _containerClient;
    private readonly IConfiguration _configuration;
    public BlobStorageService(IConfiguration configuration)
    {
        _configuration = configuration;
        // var sasUrl = configuration["AzureBlob:ContainerSasUrl"];
        // _containerClient = new BlobContainerClient(new Uri(sasUrl!));
    }

    public async Task UpdateContainerClient()
    {
        var vaultUrl = _configuration["AzureBlob:KeyVaultUrl"];
        SecretClient secretClient = new SecretClient(new Uri(vaultUrl!), new DefaultAzureCredential());
        KeyVaultSecret secret = await secretClient.GetSecretAsync("BlobStorageSasUrl");
        var blobUrl = secret.Value;
        _containerClient = new BlobContainerClient(new Uri(blobUrl));

    }
    public async Task UploadFile(Stream fileStream, string fileName)
    {
        await UpdateContainerClient();
        var blobClient = _containerClient!.GetBlobClient(fileName);
        await blobClient.UploadAsync(fileStream, overwrite: true);
    }
    public async Task<Stream?> DownloadFile(string fileName)
    {
        await UpdateContainerClient();
        var blobClient = _containerClient!.GetBlobClient(fileName);
        if (await blobClient.ExistsAsync())
        {
            var downloadInfo = await blobClient.DownloadStreamingAsync();
            return downloadInfo.Value.Content;
        }
        return null;
    }
}