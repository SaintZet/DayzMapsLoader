export interface MapProvider {
    id: number;
    name: string;
    link: string;
}
export interface Maps{
    id: number;
    name: string;
    description: string;
    lastVersion: string;
    author: string;
    link: string;
}
// export interface Map {
//     isFirstQuadrant: Boolean;
//     mapExtension: number;
//     name: number;
//     nameForProvider: string;
//     version: string;
//     typesMap: number[];
//     zoomLevelRationSize: ZoomLevelRatioSize[];
// }

export interface ZoomLevelRatioSize {
    height: number;
    width: number;
}

interface MapTypes {
    id: number;
    name: string;
}

export interface ProvidedMaps{
    id: number;
    nameForProvider: string;
    mapProvidedId: number;
    mapId: number;
    mapTypeId: number;
    maxTypeForProvider: string;
    maxMapLevel: number;
    isFirstQuadrant: boolean;
    version: string;
    imageExtension: string;
}


