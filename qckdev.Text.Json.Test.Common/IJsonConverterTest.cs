namespace qckdev.Text.Json.Test.Common
{
    public interface IJsonConverterTest
    {
        
        void Deserialize_Pokemon();
        void SerializeAndDeserialize_Temperature();

#if NO_DYNAMIC
#else
        void Deserialize_Dynamic();
        void Deserialize_Dynamic_Array();
        void Deserialize_Dynamic_Object();
        void Deserialize_Dynamic_ObjectAndArray();
#endif

    }
}