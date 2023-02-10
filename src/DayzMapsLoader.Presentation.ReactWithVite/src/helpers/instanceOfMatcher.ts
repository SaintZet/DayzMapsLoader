import {
    LoaderSelectorTypes,
    MappedMapType,
    MappedProvider,
    MapProvider,
    MapType,
    ZoomLevelRatioSize,
    Map,
    InterfaceMatcher
} from "./types";


export function interfaceMatcher(data: LoaderSelectorTypes): number {

    if (isProviderArray(data)) return InterfaceMatcher.ProviderArray

    if (isMapArray(data)) return InterfaceMatcher.MapArray

    if (isMapTypeArray(data)) return InterfaceMatcher.MapTypeArray

    if (isZoomArray(data)) return InterfaceMatcher.ZoomArray

    return InterfaceMatcher.Default;
}

function isProviderArray(item: LoaderSelectorTypes): item is MapProvider[] {
    if (!item) throw "Empty set in interface matcher";
    return "idProvider" in providerDecorator(item as MapProvider[])[0];
}

function isMapTypeArray(item: LoaderSelectorTypes): item is MapType[] {
    if (!item) throw "Empty set in interface matcher";
    return "idType" in typeDecorator(item as MapType[])[0];
}

function isZoomArray(item: LoaderSelectorTypes): item is ZoomLevelRatioSize[] {
    if (!item) throw "Empty set in interface matcher";
    return "width" in item[0];
}

function isMapArray(item: LoaderSelectorTypes): item is Map[] {
    if (!item) throw "Empty set in interface matcher";
    return "lastVersion" in item[0];
}

function providerDecorator(data: MapProvider[]): MappedProvider[] {
    let mappedProviders: MappedProvider[] = [];
    for (let i = 0; i < data.length; i++) {
        mappedProviders.push({
            idProvider: data[i].id,
            linkProvider: data[i].link,
            nameProvider: data[i].name
        })
    }
    return mappedProviders;
}

function typeDecorator(data: MapType[]): MappedMapType[] {
    let mappedType: MappedMapType[] = [];
    for (let i = 0; i < data.length; i++) {
        mappedType.push({
            idType: data[i].id,
            nameType: data[i].name
        })
    }
    return mappedType;
}