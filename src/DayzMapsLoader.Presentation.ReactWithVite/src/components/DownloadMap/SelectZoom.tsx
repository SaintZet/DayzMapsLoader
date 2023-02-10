import {Map, MapType} from "../../helpers/types";
import LoaderSelector from "../../ui/DownloadMap/LoaderSelector";
import {switchType} from "../../modules/DownloadMap/helpers/switchLoaderDataType";
import React, {ReactNode} from "react";

interface ZoomProps {
    maxZoom: number,
}

export default function SelectZoom(props: ZoomProps) {
    const minZoom = 0;
    const maxZoom = props.maxZoom;
    const zoomRange = new Array(maxZoom - minZoom + 1).fill(undefined).map((_,i) => i + minZoom);
    const loaderSettings = {m: 3, width: 300};
    const menuItems: ReactNode = switchType(zoomRange);

    return (
        <LoaderSelector menuItems={menuItems} className="Select map type" settings={loaderSettings}/>
    );

}