using KurikulumPascasarjanaFisika.Domain;
using System.Net.Http.Json;

namespace KurikulumPascasarjanaFisika.Services
{
    public class CapaianService(HttpClient client, IKatalogService katalogService, IAddressService addressService) : ICapaianService
    {
        private readonly HttpClient _client = client;
        private readonly IKatalogService _katalogService = katalogService;
        private readonly IAddressService _addressService = addressService;

        public async Task<IEnumerable<CapaianLulusan>?> GetCPL(string programStudi)
        {
            var baseAddress = _addressService.GetBaseAddress(programStudi);

            return await _client.GetFromJsonAsync<IEnumerable<CapaianLulusan>>($"{baseAddress}CPL.json");
        }

        public async Task<IEnumerable<CapaianMataKuliah>?> GetCPMK(string kodeMK, string programStudi)
        {
            var baseAddress = _addressService.GetBaseAddress(programStudi);

            var allCpl = await GetCPL(programStudi);
            var allCpmk = await _client.GetFromJsonAsync<IEnumerable<CapaianMataKuliah>>($"{baseAddress}CPMK.json");

            if (allCpl is null || allCpmk is null)
                return null;

            var cpmk = allCpmk.Where(x => x.KodeMK == kodeMK);
            foreach (var cp in cpmk)
            {
                cp.CPL = allCpl.First(x => x.KodeCPL == cp.KodeCPL);
            }

            return cpmk;

        }

        public async Task<Dictionary<MataKuliah, IEnumerable<bool>>?> GetCapaianMapping(string programStudi)
        {
            var baseAddress = _addressService.GetBaseAddress(programStudi);

            var katalogMataKuliah = await _katalogService.GetKatalog(programStudi);
            var allCpl = await GetCPL(programStudi);
            var allCpmk = await _client.GetFromJsonAsync<IEnumerable<CapaianMataKuliah>>($"{baseAddress}CPMK.json");

            if (allCpl is null || allCpmk is null || katalogMataKuliah is null)
                return null;

            var capaianMapping = new Dictionary<MataKuliah, IEnumerable<bool>>();

            katalogMataKuliah.ToList().ForEach(x =>
            {
                var cpmk = allCpmk.Where(y => y.KodeMK == x.Kode);
                capaianMapping.Add(x, GetDukunganCapaian(cpmk is not null ? cpmk : [], allCpl));
            });

            return capaianMapping;

        }

        public async Task<Dictionary<string, int>?> GetCapaianMataKuliah(string programStudi)
        {
            var baseAddress = _addressService.GetBaseAddress(programStudi);

            var katalogMataKuliah = await _katalogService.GetKatalog(programStudi);
            var allCpl = await GetCPL(programStudi);
            var allCpmk = await _client.GetFromJsonAsync<IEnumerable<CapaianMataKuliah>>($"{baseAddress}CPMK.json");

            if (allCpl is null || allCpmk is null || katalogMataKuliah is null)
                return null;

            var capaian = new Dictionary<string, int>();

            var group = allCpmk.GroupBy(x => x.KodeCPL).OrderBy(x => x.Key).ToList();

            group.ForEach(x => capaian.Add(x.Key, x.GroupBy(y => y.KodeMK).Count()) );

            return capaian;
        }

        private IEnumerable<bool> GetDukunganCapaian(IEnumerable<CapaianMataKuliah> cpmk, IEnumerable<CapaianLulusan> cpl)
        {
            var capaianMap = cpmk.Select(x => x.KodeCPL).Distinct();

            return cpl.Select(x => capaianMap.Contains(x.KodeCPL));

        }



    }
}
