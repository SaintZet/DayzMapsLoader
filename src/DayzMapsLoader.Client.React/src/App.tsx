import React, {useState} from 'react';
import './App.css';
import {FormControl} from "@mui/material";
import {useQuery} from "react-query";
import {MapProvider, Map} from "./shared/types";
import SelectMapProvider from "./components/SelectMapProviderComponent";
import {ProvidedMapsService} from "./services/providedMaps.service";
import {FindDistinctProviders, FindMapsOfProvider} from "./utils";
import SelectMap from "./components/SelectMapComponent";

export default function App() {

    const {isLoading, error, data: response} = useQuery('provided maps data', () => ProvidedMapsService.getAll());
    const [selectedProvider, setSelectedProvider] = useState<MapProvider>({
        id: 0,
        name: '',
        link: ''
    });
    const providers: undefined | MapProvider[] = response && FindDistinctProviders(response.data);
    const updateSelectedProvider = (provider: MapProvider) => setSelectedProvider(provider);

    const maps: undefined | Map[] = response && FindMapsOfProvider(response.data, selectedProvider);

    if (isLoading) return (<p>Загрузка...</p>)

    if (error) return (<p>error</p>)

    return (
        <div className="select-provider-section">
            <FormControl sx={{width: 500}}>
                <SelectMapProvider providers={providers ?? []}
                                   customHandleChange={updateSelectedProvider}></SelectMapProvider>
                <SelectMap maps={maps ?? []}></SelectMap>
            </FormControl>
        </div>
    );
}

