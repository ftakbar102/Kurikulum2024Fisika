using System.Text.Json.Serialization;

namespace KurikulumPascasarjanaFisika.Domain
{
    [JsonConverter(typeof(JsonStringEnumConverter<JenisMataKuliah>))]
    public enum JenisMataKuliah
    {
        MKWI,
        MKWP,
        MKPB,
        MKOP
    }
}
