import {
    LoaderTypes,
    MapProvider,
    MapType,
    Map,
    InterfaceMatcher,
    Zoom
} from "./types";


export function interfaceMatcher(data: LoaderTypes): number {
    if(!data || !data.length){
        return InterfaceMatcher.Default;
    }

    if (isZoomArray(data)) return InterfaceMatcher.ZoomArray

    if (isProviderArray(data)) return InterfaceMatcher.ProviderArray

    if (isMapArray(data)) return InterfaceMatcher.MapArray

    if (isMapTypeArray(data)) return InterfaceMatcher.MapTypeArray

    return InterfaceMatcher.Default;
}

function isProviderArray(item: LoaderTypes): item is MapProvider[] {
    if (!item) throw "Empty set in interface matcher";
    return "id" in (item as MapProvider[])[0] && "link" in (item as MapProvider[])[0] && "name" in (item as MapProvider[])[0];
}

function isMapTypeArray(item: LoaderTypes): item is MapType[] {
    if (!item) throw "Empty set in interface matcher";
    return "id" in (item as MapType[])[0] && "name" in (item as MapType[])[0];
}

function isZoomArray(item: LoaderTypes): item is Zoom[] {
    if (!item) throw "Empty set in interface matcher";
    return "zoom" in (item[0] as Zoom);
}

function isMapArray(item: LoaderTypes): item is Map[] {
    if (!item) throw "Empty set in interface matcher";
    return "lastVersion" in (item[0] as Map);
}