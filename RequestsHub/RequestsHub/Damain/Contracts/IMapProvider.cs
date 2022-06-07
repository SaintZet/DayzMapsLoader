using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestsHub.Domain.Contracts
{
    internal interface IMapProvider
    {
        abstract List<IMap> Maps { get; }

        string QueryBuilder(NameMap nameMap, TypeMap typeMap, int zoom);

        public void SaveImage(NameMap nameMap, TypeMap typeMap, int zoom, string pathToSaveImage);

        public void SaveImages(TypeMap typeMap, NameMap nameMap, int zoom, string pathToSaveImages);
    }
}