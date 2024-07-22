using KurikulumPascasarjanaFisika.Domain;
using System.Net.Http.Json;

namespace KurikulumPascasarjanaFisika.Services
{
    public class KatalogService(HttpClient client, IAddressService addressService) : IKatalogService
    {
        private readonly HttpClient _client = client;
        private readonly IAddressService _addressService = addressService;

        public async Task<IEnumerable<MataKuliah>?> GetKatalog(string programStudi)
        {
            var baseAddress = _addressService.GetBaseAddress(programStudi);

            return await _client.GetFromJsonAsync<IEnumerable<MataKuliah>>($"{baseAddress}Katalog.json");
        }

        public async Task<MataKuliah?> GetMataKuliah(string kodeMK, string programStudi)
        {
            var allKatalog = await GetKatalog(programStudi);

            if(allKatalog is null)
                return null;

            return allKatalog.FirstOrDefault(x => x.Kode ==  kodeMK);
        }

        public async Task<Dictionary<string, int>?> GetMataKuliahByJenis(string programStudi)
        {
            var allKatalog = await GetKatalog(programStudi);
            if (allKatalog is null)
                return null;

            var matakuliahbyjenis = new Dictionary<string, int>();

            var group = allKatalog.GroupBy(x => x.Jenis).OrderByDescending(x => x.Key).ToList();

            group.ForEach(x => matakuliahbyjenis.Add(x.Key, x.Count()));

            return matakuliahbyjenis.ToDictionary();
        }
    }
}
