import SelectProvider from "../../components/DownloadMap/SelectProvider";
import React from "react";
import {DownloadMapProvider} from "./context/providedMapsContext";
import SelectMap from "../../components/DownloadMap/SelectMap";
import SelectMapType from "../../components/DownloadMap/SelectMapType";
import SelectZoom from "../../components/DownloadMap/SelectZoom";

const DownloadMapModule = () => {
    return (
        <DownloadMapProvider>
            <SelectProvider/>
            <SelectMap/>
            <SelectMapType/>
            <SelectZoom/>
        </DownloadMapProvider>
    );
};

export default DownloadMapModule;

