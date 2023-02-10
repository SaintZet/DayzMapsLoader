import {Map} from "../../helpers/types";
import LoaderSelector from "../../ui/DownloadMap/LoaderSelector";
import {switchType} from "../../modules/DownloadMap/helpers/switchLoaderDataType";
import React, {ReactNode} from "react";

interface MapProps {
    maps: Map[],
}

export default function SelectMap(props: MapProps) {
    const maps = props.maps;
    const loaderSettings = {m: 3, width: 300};
    const menuItems: ReactNode = switchType(maps);

    return (
        <LoaderSelector menuItems={menuItems} className="Select map" settings={loaderSettings}/>
    );

}