export interface MapProvider {
    id: number;
    name: string;
    link: string;
}
export interface Map {
    id: number;
    name: string;
    description: string;
    lastVersion: string;
    author: string;
    link: string;
}

export interface ZoomLevelRatioSize {
    height: number;
    width: number;
}

export interface MapType {
    id: number;
    name: string;
}

export interface ProvidedMap {
    id: number;
    nameForProvider: string;
    mapProvider: MapProvider;
    map: Map;
    mapType: MapType;
    maxTypeForProvider: string;
    maxMapLevel: number;
    isFirstQuadrant: boolean;
    version: string;
    imageExtension: string;
}


