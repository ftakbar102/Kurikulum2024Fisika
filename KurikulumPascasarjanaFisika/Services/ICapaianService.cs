using KurikulumPascasarjanaFisika.Domain;

namespace KurikulumPascasarjanaFisika.Services
{
    public interface ICapaianService
    {
        Task<Dictionary<MataKuliah, IEnumerable<bool>>?> GetCapaianMapping(string programStudi);
        Task<Dictionary<string, int>?> GetCapaianMataKuliah(string programStudi, KategoriKuliah kategori = KategoriKuliah.Semua);
        Task<IEnumerable<CapaianLulusan>?> GetCPL(string programStudi);
        Task<IEnumerable<CapaianMataKuliah>?> GetCPMK(string kodeMK, string programStudi);
    }
}