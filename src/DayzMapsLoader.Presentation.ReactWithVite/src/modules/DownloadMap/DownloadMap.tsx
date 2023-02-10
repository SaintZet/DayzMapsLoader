import SelectProvider from "../../components/DownloadMap/SelectProvider";
import React, {useContext} from "react";
import {ProvidedMap} from "../../helpers/types";
import {DownloadMapContext, DownloadMapProvider, useContextStore} from "./context/providedMapsContext";
import SelectMap from "../../components/DownloadMap/SelectMap";
import SelectMapType from "../../components/DownloadMap/SelectMapType";
import SelectZoom from "../../components/DownloadMap/SelectZoom";

export function DownloadMap() {
    const context = useContextStore();
    const providers = [...new Map(context.providedMaps.map(x => x.mapProvider).map(item => [item['name'], item])).values()];

    return (
        <div>
            <DownloadMapProvider>
                <SelectProvider providers={providers}></SelectProvider>
                <SelectMap maps={context.providedMaps.map(x => x.map)}></SelectMap>
                <SelectMapType types={context.providedMaps.map(x => x.mapType)}></SelectMapType>
            </DownloadMapProvider>
        </div>
    )
}

