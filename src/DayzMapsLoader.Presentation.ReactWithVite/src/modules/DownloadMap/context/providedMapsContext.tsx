import {MapProvider, ProvidedMap} from "../../../helpers/types";
import {MapsProviderService, ProvidedMapsService} from "../api/providedMaps.services";
import React, {FC, PropsWithChildren, useContext, useMemo, useState} from "react";

export const getProvidedMaps = await ProvidedMapsService.getAll().then(data => data.data);
export const useContextStore = () => useContext(DownloadMapContext);

export const DownloadMapContext = React.createContext<ContextMembers>({
    providedMaps: getProvidedMaps,
    selectedProvider: {},
} as ContextMembers);

export const DownloadMapProvider: FC<PropsWithChildren> = ({children}) => {

    const providedMaps = getProvidedMaps;
    const [selectedProvider, setSelectedProvider] = useState<MapProvider>();

    const value: ContextMembers = useMemo(
        () => ({
            providedMaps,
            selectedProvider,
            setSelectedProvider
        }),
        [
            providedMaps,
            selectedProvider,
            setSelectedProvider
        ]
    );

    return <DownloadMapContext.Provider value={value}>{children}</DownloadMapContext.Provider>
};

interface ContextMembers {
    providedMaps: ProvidedMap[],
    selectedProvider: MapProvider | undefined,
}
