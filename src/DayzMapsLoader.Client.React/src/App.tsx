import React, {useState} from 'react';
import './App.css';
import {getProvidedMaps, getProviders} from "./shared/consts";
import {MapProvider, ProvidedMaps} from "./shared/types";
import useAxios from "./shared/hooks";
import {FormControl} from "@mui/material";
import SelectMap from "./components/SelectMapComponent";
import SelectMapProvider from "./components/SelectMapProviderComponent";

export default function App() {
    const {providers, loading, error} = useAxios<MapProvider>(getProviders);
    // const {providedMaps, loading, error} = useAxios<ProvidedMaps>(getProvidedMaps);
    const [selectedProvider, setSelectedProvider] = useState<MapProvider>();

    const updateSelectedProvider = (provider: MapProvider) => {
        setSelectedProvider(provider);
    }

    if (loading) {
        return (<div className="loading-section">
            <p>Loading ...</p>
        </div>)
    }

    if (error) {
        return (<div>
            <p>error</p>
        </div>)
    }

    return (
        <div className="select-provider-section">
            <FormControl sx={{width: 500}}>
                <SelectMapProvider providers={providers} customHandleChange={updateSelectedProvider}></SelectMapProvider>
                <SelectMap provider={selectedProvider}></SelectMap>
            </FormControl>
        </div>

    );
}

