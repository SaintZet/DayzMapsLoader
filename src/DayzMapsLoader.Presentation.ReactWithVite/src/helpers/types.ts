import {Provider} from "react";

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

export type LoaderTypes = Array<MapProvider> | Array<MapType> | Array<Map> | Array<number>;

type ProviderWithChanges<T extends MapProvider> = {
    [K in keyof T & string as `${K}Provider`]: T[K]
};

export type MappedProvider = ProviderWithChanges<MapProvider>;

type MapTypeWithChanges<T extends MapType> = {
    [K in keyof T & string as `${K}Type`]: T[K]
};

export type MappedMapType = MapTypeWithChanges<MapType>;

export enum InterfaceMatcher {
    "MapArray",
    "MapTypeArray",
    "ZoomArray",
    "ProviderArray",
    "Default" ,
}