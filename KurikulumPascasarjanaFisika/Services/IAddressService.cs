namespace KurikulumPascasarjanaFisika.Services
{
    public interface IAddressService
    {
        string GetBaseAddress(string programStudi);
        IEnumerable<string> GetAllProgramStudi();
    }
}