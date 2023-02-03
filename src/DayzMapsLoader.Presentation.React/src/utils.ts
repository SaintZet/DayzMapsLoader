import {MapProvider, ProvidedMap, Map} from "./shared/types";

export const FindDistinctProviders = (data: ProvidedMap[]) => data.map((x) => x.mapProvider).filter((provider, index, self) => self.indexOf(provider) === index);
export const FindMapsOfProvider = (data: ProvidedMap[], selectedProvider: MapProvider) => data.filter(x => x.mapProvider === selectedProvider).map(x => x.map);
export const FindMapTypesOfMap = (data: ProvidedMap[], selectedMap: Map, selectedProvider: MapProvider) => data.filter(x => x.mapProvider === selectedProvider && x.map === selectedMap).map(x => x.mapType);

export function FindMaxZoom(data: ProvidedMap[], selectedMap: Map, selectedProvider: MapProvider) {
    const t = data.filter(x => x.mapProvider === selectedProvider && x.map === selectedMap).map(x => x.maxMapLevel);
    return t;
};