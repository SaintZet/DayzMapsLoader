import SelectProvider from "../components/DownloadMap/SelectProvider";
import {useContext} from "react";
import {ProvidedMap} from "../helpers/types";
import {ProvidedMapsContext} from "../modules/DownloadMap/context/providedMapsContext";
import React from "react";

export const Download = () => {
    const context = useContext<ProvidedMap[]>(ProvidedMapsContext);
    const providers = context.filter(x => x.mapProvider).map(x => x.mapProvider);
    return <SelectProvider providers={providers}></SelectProvider>;
};