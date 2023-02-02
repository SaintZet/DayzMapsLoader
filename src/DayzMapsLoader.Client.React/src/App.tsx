import React, {useEffect, useState} from 'react';
import './App.css';
import {FormControl} from "@mui/material";
import {useQuery} from "react-query";
import {MapProvider, Map, MapType, ProvidedMap} from "./shared/types";
import SelectMapProvider from "./components/SelectMapProviderComponent";
import {ProvidedMapsService} from "./services/providedMaps.service";
import {FindDistinctProviders, FindMapsOfProvider, FindMapTypesOfMap, FindMaxZoom} from "./utils";
import SelectMap from "./components/SelectMapComponent";
import SelectMapType from "./components/SelectMapTypeComponent";
import SelectZoom from "./components/SelectZoomComponent";
import SelectButton from "./components/SelectButtonComponent";

export default function App() {

    const {
        isLoading,
        error,
        data: response,
        isSuccess
    } = useQuery('provided maps data', () => ProvidedMapsService.getAll());
    const [selectedProvider, setSelectedProvider] = useState<MapProvider>({
        id: 0, name: '', link: ''
    });
    const [selectedMap, setSelectedMap] = useState<Map>({
        id: 0, name: '', author: "", description: "", lastVersion: "", link: ""
    });
    const [selectedMapType, setSelectedMapType] = useState<MapType>({
        id: 0, name: ""
    });
    const [selectedZoom, setSelectedZoom] = useState<number>(0);

    let providers: MapProvider[] = [];
    let maps: Map[] = [];
    let mapTypes: MapType[] = [];
    let maxZoom: number = 0;

    if (isSuccess) {
        providers = FindDistinctProviders(response.data);
        maps = FindMapsOfProvider(response.data, selectedProvider);
        mapTypes = FindMapTypesOfMap(response.data, selectedMap, selectedProvider);
        maxZoom = FindMaxZoom(response.data, selectedMap, selectedProvider)[0] ?? 1;
    }
    const updateSelectedProvider = (provider: MapProvider) => setSelectedProvider(provider);
    const updateSelectedMap = (map: Map) => setSelectedMap(map);
    const updateSelectedMapType = (type: MapType) => setSelectedMapType(type);
    const updateSelectedZoom = (zoom: number) => setSelectedZoom(zoom);

    if (isLoading) return (<p>Загрузка...</p>)

    if (error) return (<p>error</p>)

    return (
        <div className="select-provider-section">
            <FormControl sx={{width: 500}}>
                {providers && <SelectMapProvider providers={providers}
                                                 customHandleChange={updateSelectedProvider}></SelectMapProvider>}
                {maps && <SelectMap maps={maps} customHandleChange={updateSelectedMap}></SelectMap>}
                {mapTypes &&
                    <SelectMapType types={mapTypes} customHandleChange={updateSelectedMapType}></SelectMapType>}
                {maxZoom && <SelectZoom maxZoom={maxZoom} customHandleChange={updateSelectedZoom}></SelectZoom>}
                <SelectButton providerId={selectedProvider.id} mapId={selectedMap.id} typeId={selectedMapType.id} zoom={selectedZoom}></SelectButton>
            </FormControl>
        </div>
    );
}

