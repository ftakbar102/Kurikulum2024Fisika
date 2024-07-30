using KurikulumPascasarjanaFisika.Domain;
using System.Net.Http.Json;

namespace KurikulumPascasarjanaFisika.Services
{
    public class RpmkGenerator(HttpClient client, IKatalogService katalogService, ICapaianService capaianService, IAddressService addressService) : IRpmkGenerator
    {

        private readonly HttpClient _client = client;
        private readonly IKatalogService _katalogService = katalogService;
        private readonly IAddressService _addressService = addressService;
        private readonly ICapaianService _capaianService = capaianService;

        public async Task<RPMK?> GetRpmk(string kodeMK, string programStudi)
        {
            var baseAddress = _addressService.GetBaseAddress(programStudi);
            var allBahanKajian = await _client.GetFromJsonAsync<IEnumerable<Kajian>>($"{baseAddress}BahanKajian.json");
            var allRelation = await _client.GetFromJsonAsync<IEnumerable<RelasiMataKuliah>>($"{baseAddress}Relation.json");

            if ( allBahanKajian is null || allRelation is null)
                return null;

            var mk = await _katalogService.GetMataKuliah(kodeMK, programStudi);

            if (mk is null)
                return null;

            var cpmk = await _capaianService.GetCPMK(kodeMK, programStudi);

            var relasi = await Task.WhenAll(
                allRelation.Where(x => x.KodeMK == kodeMK)
                    .Select(async x =>
                    {
                        var mkReq = await _katalogService.GetMataKuliah(x.KodeMKReq, programStudi);
                        return x with { NamaMKReq = mkReq is null ? string.Empty : mkReq.NamaId };
                    })
                );
                
            return new RPMK()
            {
                MataKuliah = mk,
                BahanKajian = allBahanKajian.Where(x => x.KodeMK == kodeMK),
                CPMK = cpmk is not null ? cpmk : [],
                Relasi = relasi
            };
        }

    }
}
