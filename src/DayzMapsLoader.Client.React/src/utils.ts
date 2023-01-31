import {MapProvider, ProvidedMap} from "./shared/types";

export const FindDistinctProviders = (data: ProvidedMap[]) => data ? data.map((x) => x.mapProvider).filter((provider, index , self) => self.indexOf(provider) === index) : [];

export const FindMapsOfProvider = (data: ProvidedMap[], selectedProvider: MapProvider) => data && data.filter(x => x.mapProvider === selectedProvider).map(x => x.map);