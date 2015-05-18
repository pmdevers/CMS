using System;

namespace Panther.CMS.Interfaces
{
    public interface IPantherFileSystem
    {
        string Extension { get; }

        bool FileExists(string filename);

        void CreateFile(string filename);

        T ReadFile<T>(string filename);

        string ReadFile(string filename);

        void WriteToFile<T>(string filename, T content);

        void WriteToFile(string filename, string content);

        string CreateFilename(Type type);
    }
}