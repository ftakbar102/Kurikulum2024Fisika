using KurikulumPascasarjanaFisika.Domain;
using System.Net.Http.Json;

namespace KurikulumPascasarjanaFisika.Services
{
    public class RpmkGenerator(HttpClient client, IKatalogService katalogService, IAddressService addressService) : IRpmkGenerator
    {

        private readonly HttpClient _client = client;
        private readonly IKatalogService _katalogService = katalogService;
        private readonly IAddressService _addressService = addressService;

        public async Task<RPMK?> GetRpmk(string kodeMK, string programStudi)
        {
            var baseAddress = _addressService.GetBaseAddress(programStudi);

            var allCpl = await _client.GetFromJsonAsync<IEnumerable<CapaianLulusan>>($"{baseAddress}CPL.json");
            var allCpmk = await _client.GetFromJsonAsync<IEnumerable<CapaianMataKuliah>>($"{baseAddress}CPMK.json");
            var allBahanKajian = await _client.GetFromJsonAsync<IEnumerable<Kajian>>($"{baseAddress}BahanKajian.json");
            var allRelation = await _client.GetFromJsonAsync<IEnumerable<RelasiMataKuliah>>($"{baseAddress}Relation.json");

            if (allCpl is null || allCpmk is null || allBahanKajian is null || allRelation is null)
                return null;


            var mk = await _katalogService.GetMataKuliah(kodeMK, programStudi);

            if (mk is null)
                return null;

            var cpmk = allCpmk.Where(x => x.KodeMK == kodeMK);
            foreach(var cp in cpmk)
            {
                cp.CPL = allCpl.First(x => x.KodeCPL == cp.KodeCPL);
            }

            var relasi = allRelation.Where(x => x.KodeMK == kodeMK);
            foreach(var cp in relasi)
            {
                var mkReq = await _katalogService.GetMataKuliah(cp.KodeMKReq, programStudi);
                cp.NamaMKReq = mkReq is null ? string.Empty : mkReq.NamaId;
            }

            RPMK rpmk = new()
            {
                MataKuliah = mk,
                BahanKajian = allBahanKajian.Where(x => x.KodeMK == kodeMK),
                CPMK = cpmk,
                Relasi = relasi
            };

            return rpmk;

        }

    }
}
