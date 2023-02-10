import {Map, MapType} from "../../helpers/types";
import LoaderSelector from "../../ui/DownloadMap/LoaderSelector";
import {switchType} from "../../modules/DownloadMap/helpers/switchLoaderDataType";
import React, {ReactNode} from "react";

interface MapTypeProps {
    types: MapType[],
}

export default function SelectMapType(props: MapTypeProps) {
    const types = props.types;
    const loaderSettings = {m: 3, width: 300};
    const menuItems: ReactNode = switchType(types);

    return (
        <LoaderSelector menuItems={menuItems} className="Select map type" settings={loaderSettings}/>
    );

}