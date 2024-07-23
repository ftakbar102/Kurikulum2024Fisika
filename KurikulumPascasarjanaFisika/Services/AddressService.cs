namespace KurikulumPascasarjanaFisika.Services
{
    public class AddressService : IAddressService
    {
        public string GetBaseAddress(string programStudi)
        {
            var baseAddress = programStudi switch
            {
                "Sarjana Fisika" => "datasource/SarjanaFisika/SarjanaFisika",
                "Magister Fisika" => "datasource/MagisterFisika/MagisterFisika",
                "Magister Pengajaran Fisika" => "datasource/MagisterPengajaranFisika/MagisterPengajaranFisika",
                "Doktor Fisika" => "datasource/DoktorFisika/DoktorFisika",
                "Magister Ilmu dan Rekayasa Nuklir" => "datasource/MagisterNuklir/MagisterNuklir",
                "Magister Sains Komputasi" => "datasource/MagisterSainsKomputasi/MagisterSainsKomputasi",
                "Doktor Rekayasa Nuklir" => "datasource/DoktorNuklir/DoktorNuklir",
                _ => throw new Exception("Invalid Program Studi")
            };

            return baseAddress;
        }

        public IEnumerable<string> GetAllProgramStudi()
        {
            return ["Sarjana Fisika", "Magister Fisika", "Magister Pengajaran Fisika", "Magister Ilmu dan Rekayasa Nuklir", "Magister Sains Komputasi", 
                "Doktor Fisika", "Doktor Rekayasa Nuklir"];
        }
    }
}
