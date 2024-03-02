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

        void IsDeserializable_True_1();
        void IsDeserializable_True_2();
        void IsDeserializable_False_Null();
        void IsDeserializable_False_Text();

    }
}