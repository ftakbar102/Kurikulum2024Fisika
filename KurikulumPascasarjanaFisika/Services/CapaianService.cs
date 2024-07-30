using KurikulumPascasarjanaFisika.Domain;
using System.Net.Http.Json;

namespace KurikulumPascasarjanaFisika.Services
{
    public class CapaianService(HttpClient client, IKatalogService katalogService, IAddressService addressService) : ICapaianService
    {
        private readonly HttpClient _client = client;
        private readonly IKatalogService _katalogService = katalogService;
        private readonly IAddressService _addressService = addressService;

        public async Task<IEnumerable<CapaianLulusan>?> GetCPLProdi(string programStudi)
        {
            var baseAddress = _addressService.GetBaseAddress(programStudi);

            return await _client.GetFromJsonAsync<IEnumerable<CapaianLulusan>>($"{baseAddress}CPL.json");
        }

        public async Task<IEnumerable<CapaianMataKuliah>?> GetCPMK(string kodeMK, string programStudi)
        {
            var baseAddress = _addressService.GetBaseAddress(programStudi);

            var allCpl = await GetCPLProdi(programStudi);
            var allCpmk = await _client.GetFromJsonAsync<IEnumerable<CapaianMataKuliah>>($"{baseAddress}CPMK.json");

            if (allCpl is null || allCpmk is null)
                return null;

            var cpmk = allCpmk.Where(x => x.KodeMK == kodeMK)
                .Select(y => y with { CPL = allCpl.First(z => z.KodeCPL == y.KodeCPL) });

            return cpmk;

        }

        public async Task<Dictionary<MataKuliah, IEnumerable<bool>>?> GetCapaianMataKuliahMapping(string programStudi)
        {
            var baseAddress = _addressService.GetBaseAddress(programStudi);

            var katalogMataKuliah = await _katalogService.GetKatalog(programStudi);
            var allCpl = await GetCPLProdi(programStudi);
            var allCpmk = await _client.GetFromJsonAsync<IEnumerable<CapaianMataKuliah>>($"{baseAddress}CPMK.json");

            if (allCpl is null || allCpmk is null || katalogMataKuliah is null)
                return null;

            return katalogMataKuliah.ToDictionary(x => x,
                x =>
                {
                    var capaianMap = allCpmk.Where(y => y.KodeMK == x.Kode).Select(x => x.KodeCPL).Distinct().ToList();
                    return allCpl.Select(x => capaianMap.Contains(x.KodeCPL));
                }
            );
        }

        public async Task<Dictionary<string, int>?> JumlahMataKuliahPerCapaian(string programStudi, KategoriMataKuliah kategori = KategoriMataKuliah.Semua)
        {
            var baseAddress = _addressService.GetBaseAddress(programStudi);

            var katalogMataKuliah = await _katalogService.GetKatalog(programStudi);
            var allCpl = await GetCPLProdi(programStudi);
            var allCpmk = await _client.GetFromJsonAsync<IEnumerable<CapaianMataKuliah>>($"{baseAddress}CPMK.json");

            if (allCpl is null || allCpmk is null || katalogMataKuliah is null)
                return null;

            var katalogFiltered = kategori switch
            {
                KategoriMataKuliah.Semua => katalogMataKuliah.Select(x => x),
                KategoriMataKuliah.Wajib => katalogMataKuliah.Where(x => x.Jenis == JenisMataKuliah.MKWI || x.Jenis == JenisMataKuliah.MKWP),
                KategoriMataKuliah.Pilihan => katalogMataKuliah.Where(x => x.Jenis != JenisMataKuliah.MKWI && x.Jenis != JenisMataKuliah.MKWP),
                _ => throw new Exception("Kategori tidak valid")
            };


            return allCpmk.Where(cpmk => katalogFiltered.Any(mk => mk.Kode == cpmk.KodeMK))
                .GroupBy(cpmk => cpmk.KodeCPL)
                .OrderBy(group => group.Key)
                .ToDictionary(x => x.Key, y => y.GroupBy(cpmk => cpmk.KodeMK).Count());
        }
    }
}
