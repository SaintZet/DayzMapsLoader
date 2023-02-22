import {MapProvider, ProvidedMap} from "../../../helpers/types";
import { ProvidedMapsService} from "../api/providedMaps.services";
import React, {FC, PropsWithChildren, useContext, useMemo, useState} from "react";

export const getProvidedMaps = await ProvidedMapsService.getAll().then(data => data.data);
export const useContextStore = () => useContext(DownloadMapContext);

export const DownloadMapContext = React.createContext<ContextMembers>({
    providedMaps: getProvidedMaps,
    selectedProvider: {},
} as ContextMembers);

export const DownloadMapProvider: FC<PropsWithChildren> = ({children}) => {

    const providedMaps = getProvidedMaps;
    const [selectedProvider, setSelectedProvider] = useState<number>();
    const [selectedMap, setSelectedMap] = useState<number>();
    const [selectedType, setSelectedType] = useState<number>();
    const [selectedZoom, setSelectedZoom] = useState<number>();

    const value: ContextMembers = useMemo(
        () => ({
            providedMaps,
            selectedProvider,
            selectedMap,
            selectedType,
            selectedZoom,
            setSelectedProvider,
            setSelectedMap,
            setSelectedType,
            setSelectedZoom,
        }),
        [
            providedMaps,
            selectedProvider,
            selectedMap,
            selectedType,
            selectedZoom,
            setSelectedProvider,
            setSelectedMap,
            setSelectedType,
            setSelectedZoom,
        ]
    );

    return <DownloadMapContext.Provider value={value}>{children}</DownloadMapContext.Provider>
};

interface ContextMembers {
    providedMaps: ProvidedMap[],
    selectedProvider: number|undefined,
    selectedMap: number | undefined,
    selectedType: number | undefined,
    selectedZoom: number | undefined,
    setSelectedProvider: (provider: number | undefined) => void,
    setSelectedMap: (map: number|undefined) => void,
    setSelectedType: (map: number|undefined) => void,
    setSelectedZoom: (map: number|undefined) => void,


}
